using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class PurseConfiguration : IEntityTypeConfiguration<Purse>
    {
        public void Configure(EntityTypeBuilder<Purse> builder)
        {
            #region Purse Model Creation
            builder.ToTable("Purses").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(u => u.Money).HasColumnName("Money").IsRequired();

            builder.HasOne(u => u.User).WithMany().HasForeignKey(u => u.UserId);
            #endregion
        }
    }
}
