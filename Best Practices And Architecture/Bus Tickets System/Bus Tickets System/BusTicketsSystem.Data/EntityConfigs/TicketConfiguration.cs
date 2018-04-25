using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.EntityConfigs
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new {e.Id, e.CustomerId, e.TripId}).IsUnique();

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(e => e.CustomerId);

            builder.HasOne(e => e.Trip)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(e => e.TripId);


        }
    }
}
