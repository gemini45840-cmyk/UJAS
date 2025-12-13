using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(string fileName, byte[] fileData, string contentType);
        Task<byte[]> GetFileAsync(string filePath);
        Task DeleteFileAsync(string filePath);
        string GetFileUrl(string filePath);
        Task<string> SaveResumeAsync(byte[] fileData, string fileName, string contentType);
        Task<string> SaveDocumentAsync(byte[] fileData, string fileName, string contentType, DocumentType documentType);
    }
}