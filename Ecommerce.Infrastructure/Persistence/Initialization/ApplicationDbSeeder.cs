using Ecommerce.Domain.Identity;
using Ecommerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence.Initialization
{
    internal class ApplicationDbSeeder
    {
        private readonly RoleManager<Role> _roleManager;

        public ApplicationDbSeeder(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedDatabaseAsync()
        {
            await SeedRolesAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (await _roleManager.FindByNameAsync(EcommerceRoles.Admin)
                    is not Role)
            {
                Role role = new() { Name = EcommerceRoles.Admin };
                await _roleManager.CreateAsync(role);
            }

            if (await _roleManager.FindByNameAsync(EcommerceRoles.User)
                    is not Role)
            {
                Role role = new() { Name = EcommerceRoles.User };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
