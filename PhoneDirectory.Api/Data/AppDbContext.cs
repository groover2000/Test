using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Api.Models;

namespace PhoneDirectory.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfiguration(new PersonConfiguration());
    }
}