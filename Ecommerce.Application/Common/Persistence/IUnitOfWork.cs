﻿using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Persistence
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
        void CommitTransaction();
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    }
}
