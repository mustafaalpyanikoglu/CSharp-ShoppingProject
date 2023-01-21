using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            #region Order Detail Model Creation
            builder.ToTable("OrderDetails").HasKey(k => k.Id);
            builder.Property(u => u.Id).HasColumnName("Id").UseIdentityColumn(1, 1);
            builder.Property(u => u.OrderId).HasColumnName("OrderId").IsRequired();
            builder.Property(u => u.ProductId).HasColumnName("ProductId").IsRequired();
            builder.Property(u => u.Quantity).HasColumnName("Quantity").IsRequired();
            builder.Property(u => u.TotalPrice).HasColumnName("TotalPrice").IsRequired();

            builder.HasOne(u => u.Order).WithMany().HasForeignKey(u => u.OrderId);
            builder.HasOne(u => u.Product).WithMany().HasForeignKey(u => u.ProductId);
            #endregion
        }
    }
}
