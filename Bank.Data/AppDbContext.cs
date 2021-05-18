using Bank.Core.Clients;
using Microsoft.EntityFrameworkCore;
using Clients = Bank.Data.Client.Clients;

namespace Bank.Data
{
    public class AppDbContext : DbContext

    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Bank.Bank> Banks { get; set; }
    public DbSet<Clients> Clients { get; set; }
    }
}