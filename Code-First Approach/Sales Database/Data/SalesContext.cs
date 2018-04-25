namespace P03_SalesDatabase
{
    using System;
    using P03_SalesDatabase.Data.Models;
    using P03_SalesDatabase.Data;
    using Microsoft.EntityFrameworkCore;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {
            
        }

        public SalesContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                    .IsUnicode()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasMaxLength(80);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                    .IsUnicode()
                    .HasMaxLength(50);
            });

            builder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                    .IsUnicode()
                    .HasMaxLength(80);
            });

            builder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(e => e.ProductId);

                entity.HasOne(e => e.Store)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(e => e.StoreId);


            });
        }

    }
}
