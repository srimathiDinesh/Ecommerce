using System;

namespace Ecommerce.Domain.Common
{
    public abstract class AuditableEntity : AuditableEntity<Guid>
    {
    }

    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; private set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
            LastModifiedOn = DateTime.UtcNow;
        }
    }
}
