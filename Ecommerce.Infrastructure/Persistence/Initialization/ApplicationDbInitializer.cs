using Ecommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence.Initialization
{
    internal class ApplicationDbInitializer
    {
        private readonly EcommerceDbContext _dbContext;
        private readonly ApplicationDbSeeder _dbSeeder;

        public ApplicationDbInitializer(EcommerceDbContext dbContext, ApplicationDbSeeder dbSeeder)
        {
            _dbContext = dbContext;
            _dbSeeder = dbSeeder;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                if (_dbContext.Database.CanConnect())
                {
                    await _dbSeeder.SeedDatabaseAsync();
                }
            }
        }
    }
}
