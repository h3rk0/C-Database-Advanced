using System;
using System.Security.Cryptography;
using BusTicketsSystem.Data.EntityConfigs;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BusTicketsSystem.Data
{
    public class BusTicketsSystemContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BusCompany> BusCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());

            modelBuilder.ApplyConfiguration(new BusCompanyConfiguration());

            modelBuilder.ApplyConfiguration(new BusStationConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            modelBuilder.ApplyConfiguration(new TicketConfiguration());

            modelBuilder.ApplyConfiguration(new TownConfiguration());

            modelBuilder.ApplyConfiguration(new TripConfiguration());
        }
    }
}
