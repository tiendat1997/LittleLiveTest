using LittleLive.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Data.Configurations
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(t => t.Id);
            builder
                .Property(t => t.Id)
                .UseIdentityColumn();

            builder
                .Property(t => t.ChildName)
                .IsRequired();

            builder.HasOne(t => t.Class);

            builder.ToTable("Tracks");
        }
    }
}
