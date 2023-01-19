using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Category Model Creation
            builder.ToTable("Categories").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            #endregion
        }
    }
}
