using Ecommerce.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.FileStorage
{
    internal static class Extensions
    {
        internal static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            return services.AddSingleton<IFileStorageService, LocalFileStorageService>();
        }
    }
}
