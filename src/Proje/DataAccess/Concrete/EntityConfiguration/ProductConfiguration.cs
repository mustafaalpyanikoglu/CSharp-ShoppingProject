using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product Model Creation
            builder.ToTable("Products").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.CategoryId).HasColumnName("CategoryId").IsRequired();
            builder.Property(u => u.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(u => u.Quantity).HasColumnName("Quantity").IsRequired();
            builder.Property(u => u.Price).HasColumnName("Price").IsRequired();

            builder.HasOne(u => u.Category).WithMany().HasForeignKey(u => u.CategoryId);
            builder.HasMany(u => u.OrderDetails).WithOne(u => u.Product);
            #endregion
        }
    }
}
