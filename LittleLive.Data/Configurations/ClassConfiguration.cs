using LittleLive.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Data.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(c => c.Id);
            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .IsRequired();

            builder
                .HasOne(c => c.Teacher)
                .WithMany(u => u.Classes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.School);

            builder.ToTable("Classes");
        }
    }
}
