using LittleLive.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Data.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasIndex(m => m.Code)
                .IsUnique();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(s => s.PaymentType)
                .IsRequired();

            builder
                .HasOne(u => u.ParentSchool)
                .WithMany(s => s.ChildSchools)
                .HasForeignKey(u => u.ParentId);

            builder.HasOne(s => s.Owner);            
            
            builder.ToTable("Schools");
        }
    }
}
