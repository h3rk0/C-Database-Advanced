using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.EntityConfigs
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.OriginBusStation)
                .WithMany(bs => bs.departTrips)
                .HasForeignKey(e => e.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationBusStation)
                .WithMany(bs => bs.ArriveTrips)
                .HasForeignKey(e => e.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.BusCompany)
                .WithMany(bc => bc.Trips)
                .HasForeignKey(e => e.BusCompanyId);
        }
    }
}
