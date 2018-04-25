using System;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;

namespace ProductsShop.Data
{
    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
        {
            
        }

        public ProductsShopContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(15);

                entity.HasMany(e => e.CategoryProducts)
                    .WithOne(cp => cp.Category)
                    .HasForeignKey(e => e.CategoryId);

            });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.CategoryId,
                    e.ProductId
                });

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BuyerId)
                    .IsRequired(false);

                entity.HasMany(e => e.CategoryProducts)
                    .WithOne(cp => cp.Product)
                    .HasForeignKey(e => e.ProductId);

                entity.HasOne(e => e.Seller)
                    .WithMany(p => p.ProductsSold)
                    .HasForeignKey(e => e.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Buyer)
                    .WithMany(u => u.ProductsBought)
                    .HasForeignKey(e => e.BuyerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired(false);

                entity.Property(e => e.Age)
                    .IsRequired(false);
            });
        }
    }
}
