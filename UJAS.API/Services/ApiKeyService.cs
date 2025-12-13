using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UJAS.Infrastructure.Data;

namespace UJAS.API.Services
{
    public interface IApiKeyService
    {
        Task<string> GenerateApiKeyAsync(string name, int? companyId = null, DateTime? expiresAt = null);
        Task<bool> ValidateApiKeyAsync(string apiKey);
        Task<ApiKeyInfo> GetApiKeyInfoAsync(string apiKey);
        Task<bool> RevokeApiKeyAsync(string apiKey);
    }

    public class ApiKeyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    public class ApiKeyService : IApiKeyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyService> _logger;

        public ApiKeyService(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<ApiKeyService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateApiKeyAsync(string name, int? companyId = null, DateTime? expiresAt = null)
        {
            try
            {
                // Generate a secure API key
                var apiKey = GenerateSecureApiKey();
                var hashedKey = HashApiKey(apiKey);

                // Save to database
                var apiKeyEntity = new Core.Entities.System.ApiKey
                {
                    Name = name,
                    HashedKey = hashedKey,
                    CompanyId = companyId,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = expiresAt,
                    IsActive = true,
                    LastUsed = null
                };

                await _context.ApiKeys.AddAsync(apiKeyEntity);
                await _context.SaveChangesAsync();

                // Return the plain text key (only shown once)
                return apiKey;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating API key");
                throw;
            }
        }

        public async Task<bool> ValidateApiKeyAsync(string apiKey)
        {
            try
            {
                var hashedKey = HashApiKey(apiKey);
                var apiKeyEntity = await _context.ApiKeys
                    .FirstOrDefaultAsync(k => k.HashedKey == hashedKey && k.IsActive);

                if (apiKeyEntity == null)
                    return false;

                // Check if expired
                if (apiKeyEntity.ExpiresAt.HasValue && apiKeyEntity.ExpiresAt.Value < DateTime.UtcNow)
                {
                    apiKeyEntity.IsActive = false;
                    await _context.SaveChangesAsync();
                    return false;
                }

                // Update last used
                apiKeyEntity.LastUsed = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating API key");
                return false;
            }
        }

        public async Task<ApiKeyInfo> GetApiKeyInfoAsync(string apiKey)
        {
            try
            {
                var hashedKey = HashApiKey(apiKey);
                var apiKeyEntity = await _context.ApiKeys
                    .FirstOrDefaultAsync(k => k.HashedKey == hashedKey);

                if (apiKeyEntity == null)
                    return null;

                return new ApiKeyInfo
                {
                    Id = apiKeyEntity.Id,
                    Name = apiKeyEntity.Name,
                    CompanyId = apiKeyEntity.CompanyId,
                    CreatedAt = apiKeyEntity.CreatedAt,
                    ExpiresAt = apiKeyEntity.ExpiresAt,
                    IsActive = apiKeyEntity.IsActive
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting API key info");
                return null;
            }
        }

        public async Task<bool> RevokeApiKeyAsync(string apiKey)
        {
            try
            {
                var hashedKey = HashApiKey(apiKey);
                var apiKeyEntity = await _context.ApiKeys
                    .FirstOrDefaultAsync(k => k.HashedKey == hashedKey);

                if (apiKeyEntity == null)
                    return false;

                apiKeyEntity.IsActive = false;
                apiKeyEntity.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking API key");
                return false;
            }
        }

        private string GenerateSecureApiKey()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            // Convert to base64 and remove special characters
            var apiKey = Convert.ToBase64String(randomBytes)
                .Replace("+", "")
                .Replace("/", "")
                .Replace("=", "")
                .Replace(" ", "");

            // Add prefix for identification
            return $"ujas_{apiKey.Substring(0, Math.Min(apiKey.Length, 48))}";
        }

        private string HashApiKey(string apiKey)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}