using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.EntityConfigs
{
    public class BusStationConfiguration : IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Town)
                .WithMany(t => t.BusStations)
                .HasForeignKey(e => e.TownId);
        }
    }
}
