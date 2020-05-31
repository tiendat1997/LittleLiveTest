using LittleLive.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleLive.Data.Configurations
{
    class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(m => m.Id);

            builder
               .Property(m => m.Code)
               .IsRequired();

            builder
                .Property(m => m.Name)
                .IsRequired();

            builder.ToTable("Countries");
        }
    }
}