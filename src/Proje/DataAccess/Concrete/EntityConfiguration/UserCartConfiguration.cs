using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class UserCartConfiguration : IEntityTypeConfiguration<UserCart>
    {
        public void Configure(EntityTypeBuilder<UserCart> builder)
        {
            #region UserCart Model Creation
            builder.ToTable("UserCarts").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.UserId).HasColumnName("UserId").IsRequired();

            builder.HasOne(u => u.User).WithMany().HasForeignKey(u => u.UserId);
            builder.HasMany(u => u.Orders).WithOne(u => u.UserCart);
            #endregion
        }
    }
}
