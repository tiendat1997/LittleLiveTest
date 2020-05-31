using LittleLive.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleLive.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.UserName)
                .IsRequired();

            builder
                .Property(m => m.Password)
                .IsRequired();

            builder
                .Property(m => m.Name)
                .IsRequired();

            builder
                .Property(u => u.Role)
                .IsRequired();

            builder
                .Property(u => u.CountryId)
                .IsRequired();

            builder
                .HasOne(u => u.Country);

            builder.ToTable("Users");
        }
    }
}
