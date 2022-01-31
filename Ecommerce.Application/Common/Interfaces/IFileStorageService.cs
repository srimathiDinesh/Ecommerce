using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        public Task<string> UploadAsync(IFormFile file, string folder, CancellationToken cancellationToken = default);
        public void Remove(string fileName, string folder);
    }
}
