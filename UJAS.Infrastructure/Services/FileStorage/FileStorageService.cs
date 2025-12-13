using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UJAS.Infrastructure.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName = null);
        Task<Stream> DownloadFileAsync(string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<string> GetFileUrlAsync(string containerName, string fileName);
        Task<bool> FileExistsAsync(string containerName, string fileName);
    }

    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AzureBlobStorageService> _logger;

        public AzureBlobStorageService(
            IConfiguration configuration,
            ILogger<AzureBlobStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var connectionString = _configuration["AzureStorage:ConnectionString"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty");

                fileName ??= GenerateFileName(file.FileName);
                var containerClient = GetContainerClient(containerName);

                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                var blobClient = containerClient.GetBlobClient(fileName);

                // Set content type
                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                };

                using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, blobHttpHeaders);

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to blob storage");
                throw;
            }
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                    throw new FileNotFoundException($"File {fileName} not found in container {containerName}");

                var downloadInfo = await blobClient.DownloadAsync();
                return downloadInfo.Value.Content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file from blob storage");
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                    return false;

                await blobClient.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file from blob storage");
                throw;
            }
        }

        public async Task<string> GetFileUrlAsync(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                    throw new FileNotFoundException($"File {fileName} not found in container {containerName}");

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting file URL from blob storage");
                throw;
            }
        }

        public async Task<bool> FileExistsAsync(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);
                return await blobClient.ExistsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking file existence in blob storage");
                throw;
            }
        }

        private BlobContainerClient GetContainerClient(string containerName)
        {
            return _blobServiceClient.GetBlobContainerClient(containerName);
        }

        private string GenerateFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            return fileName.ToLowerInvariant();
        }

        public async Task<string> GenerateSasTokenAsync(string containerName, string fileName, TimeSpan expiryTime)
        {
            try
            {
                var containerClient = GetContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                    throw new FileNotFoundException($"File {fileName} not found in container {containerName}");

                // Create SAS token that expires in specified time
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = fileName,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.Add(expiryTime)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                var sasToken = blobClient.GenerateSasUri(sasBuilder);
                return sasToken.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating SAS token");
                throw;
            }
        }
    }

    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LocalFileStorageService> _logger;
        private readonly string _basePath;

        public LocalFileStorageService(
            IConfiguration configuration,
            ILogger<LocalFileStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _basePath = _configuration["FileStorage:LocalPath"] ?? "wwwroot/uploads";

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty");

                fileName ??= GenerateFileName(file.FileName);
                var containerPath = Path.Combine(_basePath, containerName);

                if (!Directory.Exists(containerPath))
                    Directory.CreateDirectory(containerPath);

                var filePath = Path.Combine(containerPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                var relativePath = Path.Combine("uploads", containerName, fileName);
                return $"/{relativePath.Replace("\\", "/")}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file locally");
                throw;
            }
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            try
            {
                var filePath = GetFilePath(containerName, fileName);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"File {fileName} not found in container {containerName}");

                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file locally");
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                var filePath = GetFilePath(containerName, fileName);

                if (!File.Exists(filePath))
                    return false;

                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file locally");
                throw;
            }
        }

        public async Task<string> GetFileUrlAsync(string containerName, string fileName)
        {
            try
            {
                var filePath = GetFilePath(containerName, fileName);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"File {fileName} not found in container {containerName}");

                var relativePath = Path.Combine("uploads", containerName, fileName);
                return $"/{relativePath.Replace("\\", "/")}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting file URL locally");
                throw;
            }
        }

        public async Task<bool> FileExistsAsync(string containerName, string fileName)
        {
            try
            {
                var filePath = GetFilePath(containerName, fileName);
                return File.Exists(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking file existence locally");
                throw;
            }
        }

        private string GetFilePath(string containerName, string fileName)
        {
            return Path.Combine(_basePath, containerName, fileName);
        }

        private string GenerateFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            return fileName.ToLowerInvariant();
        }
    }
}