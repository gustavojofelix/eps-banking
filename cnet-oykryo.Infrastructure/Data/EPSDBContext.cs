using cnet_oykryo.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.Infrastructure.Data
{
    public class EPSDBContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        public EPSDBContext(DbContextOptions<EPSDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedData(modelBuilder);

            // Configure the relationship between BankAccount and Transfer
            modelBuilder.Entity<BankAccount>()
                    .HasMany(b => b.Transfers)
                    .WithOne(t => t.SourceAccount)
                    .HasForeignKey(t => t.SourceAccountId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.SourceAccount)
                .WithMany(b => b.Transfers)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.DestinationAccount)
                .WithMany()
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Arisha Barron" },
                new Customer { Id = 2, Name = "Branden Gibson" },
                new Customer { Id = 3, Name = "Rhonda Church" },
                new Customer { Id = 4, Name = "Georgina Hazel" }
            );

            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { Id = 1, AccountNumber= "123456", Balance=2000, CustomerId =1 },
                new BankAccount { Id = 2, AccountNumber = "65231", Balance = 2500, CustomerId = 2 },
                new BankAccount { Id = 3, AccountNumber = "8523", Balance = 2900, CustomerId = 3 },
                new BankAccount { Id = 4, AccountNumber = "69874", Balance = 2500, CustomerId = 4 }
            );

            modelBuilder.Entity<Transfer>().HasData(
                new Transfer { Id = 1, Amount=10, DestinationAccountId = 1, SourceAccountId= 4, TransferDate = DateTime.Now},
                new Transfer { Id = 2, Amount = 50, DestinationAccountId = 2, SourceAccountId = 3, TransferDate = DateTime.Now },
                new Transfer { Id = 3, Amount=100, DestinationAccountId = 3, SourceAccountId= 2, TransferDate = DateTime.Now },
                new Transfer { Id = 4, Amount=75, DestinationAccountId = 4, SourceAccountId= 1, TransferDate = DateTime.Now }
            );
        }
    }
}
