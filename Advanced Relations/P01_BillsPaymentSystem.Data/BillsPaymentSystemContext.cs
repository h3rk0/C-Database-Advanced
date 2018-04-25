using P01_BillsPaymentSystem.Data.EntityConfigurations;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class BillsPaymentSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BankAccountConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new CreditCardConfiguration());

            builder.ApplyConfiguration(new PaymentMethodConfiguration());
        }
    }
}
