using Microsoft.EntityFrameworkCore;
using UserDirectory.Domain.Entities;

namespace UserDirectory.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Pincode).IsRequired().HasMaxLength(10);

        // Optional seed
        modelBuilder.Entity<User>().HasData(
            new User("Alice Johnson", 29, "Hyderabad", "Telangana", "500001") { Id = 1 },
            new User("Bob Kumar", 35, "Bengaluru", "Karnataka", "560001") { Id = 2 }
        );
    }
}
