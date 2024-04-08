using Microsoft.EntityFrameworkCore;
using SJHAFitness.Models;
using SJHAFitness;
using System.Threading.Tasks;

public class AppDbContext : DbContext
{
    // Add a constructor that takes DbContextOptions of type AppDbContext
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Account { get; set; }
    public DbSet<Sessions> Sessions { get; set; }

}