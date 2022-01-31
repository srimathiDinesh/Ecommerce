using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
