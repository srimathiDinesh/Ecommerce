using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(b => b.Name)
                    .HasMaxLength(1024);

            builder
                .Property(b => b.Price)
                    .HasPrecision(18, 2);

            builder
                .Property(p => p.ImagePath)
                    .HasMaxLength(2048);
        }
    }
}
