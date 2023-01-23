using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region Order Model Creation
            builder.ToTable("Orders").HasKey(k => k.Id);
            builder.Property(u => u.Id).HasColumnName("Id").UseIdentityColumn(1, 1);
            builder.Property(u => u.UserCartId).HasColumnName("UserCartId").IsRequired();
            builder.Property(u => u.OrderNumber).HasColumnName("OrderNumber").HasMaxLength(50).IsRequired();
            builder.Property(u => u.OrderDate).HasColumnName("OrderDate").IsRequired();
            builder.Property(u => u.ApprovalDate).HasColumnName("ApprovalDate").IsRequired();
            builder.Property(u => u.OrderAmount).HasColumnName("OrderAmount").IsRequired();
            builder.Property(u => u.Status).HasColumnName("Status").IsRequired();


            builder.HasOne(u => u.UserCart).WithMany().HasForeignKey(u => u.UserCartId);
            #endregion
        }
    }
}
