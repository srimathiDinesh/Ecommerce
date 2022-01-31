using Ecommerce.Application.Common.Persistence;
using Ecommerce.Domain;
using Ecommerce.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence.Context
{
    public class EcommerceDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
               : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction = Database.BeginTransaction(isolationLevel);
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContextTransaction.CommitAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
