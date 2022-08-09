using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BBBankContext : DbContext
    {
        public BBBankContext(DbContextOptions<BBBankContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(b =>
            {
                modelBuilder.Entity<User>(b =>
                {
                    b.HasData(new User
                    {
                        Id = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3",
                        FirstName = "Raas",
                        LastName = "Masood",
                        Email = "rassmasood@hotmail.com",
                        ProfilePicUrl = "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg"   // Profile Image URL

                    });
                });

                b.HasData(new Account
                {
                    // Here Id is a Primary key which acts as a forign key in Transaction class
                    Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                    AccountNumber = "0001-1001",                // Account Number
                    AccountTitle = "Raas Masood",                   // Account Title
                    CurrentBalance = 3500M,                     // Current Balance
                    AccountStatus = AccountStatus.Active,        // Account status
                    UserId = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3" // Forign Key of User
                });

            });
            modelBuilder.Entity<Transaction>().HasData(
                  new
                  {
                      Id = Guid.NewGuid().ToString(),                           // Auto generating Id    
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",       // Here AccountId is a Forign key from linked with Class Account and Id property
                      TransactionAmount = 3000M,                                // Transaction of 3000$
                      TransactionDate = DateTime.Now.AddDays(-1),               // Transaction happend one day ago
                      TransactionType = TransactionType.Deposit                 // Ammount was added    
                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),                           // Auto generating Id
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = -500M,                                // Transaction of 500$
                      TransactionDate = DateTime.Now.AddYears(-1),              // Transaction happend one year ago
                      TransactionType = TransactionType.Withdraw                // Amount was subtracted

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),                           // Auto generating Id    
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 1000M,                                // Transaction of 1000$
                      TransactionDate = DateTime.Now.AddYears(-2),              // Transaction happend two years ago
                      TransactionType = TransactionType.Deposit                 // Ammount was added

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 500M,
                      TransactionDate = DateTime.Now.AddMonths(-3),
                      TransactionType = TransactionType.Deposit

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = -200M,
                      TransactionDate = DateTime.Now.AddMonths(-4),
                      TransactionType = TransactionType.Withdraw

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 500M,
                      TransactionDate = DateTime.Now.AddMonths(-5),
                      TransactionType = TransactionType.Deposit

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 200M,
                      TransactionDate = DateTime.Now.AddMonths(-6),
                      TransactionType = TransactionType.Deposit

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = -300M,
                      TransactionDate = DateTime.Now.AddMonths(-7),
                      TransactionType = TransactionType.Withdraw

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = -100M,
                      TransactionDate = DateTime.Now.AddMonths(-8),
                      TransactionType = TransactionType.Withdraw

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 200M,
                      TransactionDate = DateTime.Now.AddMonths(-9),
                      TransactionType = TransactionType.Deposit

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = -500M,
                      TransactionDate = DateTime.Now.AddMonths(-10),
                      TransactionType = TransactionType.Withdraw

                  },
                  new
                  {
                      Id = Guid.NewGuid().ToString(),
                      AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                      TransactionAmount = 900M,
                      TransactionDate = DateTime.Now.AddMonths(-11),
                      TransactionType = TransactionType.Deposit

                  }
                );
        }
    }
}