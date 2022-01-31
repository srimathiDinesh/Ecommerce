using System;

namespace Ecommerce.Domain.Common
{
    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity() => Id = Guid.NewGuid();
    }

    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; protected set; }
    }
}
