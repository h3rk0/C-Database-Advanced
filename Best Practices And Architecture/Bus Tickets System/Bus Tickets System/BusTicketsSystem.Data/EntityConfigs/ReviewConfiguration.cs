using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.EntityConfigs
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(e => e.CustomerId);

            builder.HasOne(e => e.BusCompany)
                .WithMany(bc => bc.Reviews)
                .HasForeignKey(e => e.BusCompanyId);
        }
    }
}
