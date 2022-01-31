using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalFileStorageService(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken = default)
        {
            if (file != null)
            {
                Stream streamData = file.OpenReadStream();
                if (streamData.Length > 0)
                {
                    string folderName = Path.Combine("Files", "Images", folder);
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    using FileStream stream = new(filePath, FileMode.Create);
                    await streamData.CopyToAsync(stream, cancellationToken);
                    return uniqueFileName;
                }
            }
            return string.Empty;
        }

        public void Remove(string fileName, string folder)
        {
            string folderName = Path.Combine("Files", "Images", folder);
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
