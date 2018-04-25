using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.EntityConfigs
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Town)
                .WithMany(t => t.Customers)
                .HasForeignKey(e => e.TownId);
            
        }
    }
}
