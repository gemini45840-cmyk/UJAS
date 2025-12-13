using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UJAS.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName = null);
        Task<Stream> DownloadFileAsync(string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<string> GetFileUrlAsync(string containerName, string fileName);
        Task<bool> FileExistsAsync(string containerName, string fileName);
        Task<string> GenerateSasTokenAsync(string containerName, string fileName, TimeSpan expiryTime);
        Task<FileInfo> ValidateFileAsync(IFormFile file, int maxSizeMB, string[] allowedExtensions);
    }

    public class FileInfo
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FileService : IFileService
    {
        private readonly Infrastructure.Services.FileStorage.IFileStorageService _storageService;
        private readonly ILogger<FileService> _logger;

        public FileService(
            Infrastructure.Services.FileStorage.IFileStorageService storageService,
            ILogger<FileService> logger)
        {
            _storageService = storageService;
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName = null)
        {
            try
            {
                return await _storageService.UploadFileAsync(file, containerName, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to {ContainerName}", containerName);
                throw;
            }
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            try
            {
                return await _storageService.DownloadFileAsync(containerName, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file {FileName} from {ContainerName}", fileName, containerName);
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                return await _storageService.DeleteFileAsync(containerName, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file {FileName} from {ContainerName}", fileName, containerName);
                throw;
            }
        }

        public async Task<string> GetFileUrlAsync(string containerName, string fileName)
        {
            try
            {
                return await _storageService.GetFileUrlAsync(containerName, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting URL for file {FileName} from {ContainerName}", fileName, containerName);
                throw;
            }
        }

        public async Task<bool> FileExistsAsync(string containerName, string fileName)
        {
            try
            {
                return await _storageService.FileExistsAsync(containerName, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence of file {FileName} in {ContainerName}", fileName, containerName);
                throw;
            }
        }

        public async Task<string> GenerateSasTokenAsync(string containerName, string fileName, TimeSpan expiryTime)
        {
            try
            {
                if (_storageService is Infrastructure.Services.FileStorage.AzureBlobStorageService azureService)
                {
                    return await azureService.GenerateSasTokenAsync(containerName, fileName, expiryTime);
                }
                throw new NotImplementedException("SAS token generation only available for Azure Blob Storage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating SAS token for file {FileName} in {ContainerName}", fileName, containerName);
                throw;
            }
        }

        public async Task<FileInfo> ValidateFileAsync(IFormFile file, int maxSizeMB, string[] allowedExtensions)
        {
            var fileInfo = new FileInfo
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                Extension = Path.GetExtension(file.FileName).ToLowerInvariant(),
                IsValid = true
            };

            // Check file size
            var maxSizeBytes = maxSizeMB * 1024 * 1024;
            if (file.Length > maxSizeBytes)
            {
                fileInfo.IsValid = false;
                fileInfo.ErrorMessage = $"File size exceeds maximum allowed size of {maxSizeMB}MB";
                return fileInfo;
            }

            // Check file extension
            if (allowedExtensions != null && allowedExtensions.Length > 0)
            {
                if (!allowedExtensions.Contains(fileInfo.Extension))
                {
                    fileInfo.IsValid = false;
                    fileInfo.ErrorMessage = $"File type not allowed. Allowed types: {string.Join(", ", allowedExtensions)}";
                    return fileInfo;
                }
            }

            // Additional validation can be added here (e.g., virus scanning, content validation)

            return fileInfo;
        }
    }
}