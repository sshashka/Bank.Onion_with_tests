using System;
using Bank.Data.Banks;
using Bank.Data.Clients;
using Client = Bank.Data.Clients.Client;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class BankContext : DbContext
    {
        public DbSet<Banks.Bank> Banks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
