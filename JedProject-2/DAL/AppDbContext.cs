using JedProject_2.Models;
using Microsoft.EntityFrameworkCore;

namespace JedProject_2.DAL;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configure.CONNECTION_STRING);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Purveyor> Purveyors { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PurveyorStorage> PurveyorStorages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
