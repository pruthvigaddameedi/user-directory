using Microsoft.EntityFrameworkCore;
using UserDirectory.Api.Models;

namespace UserDirectory.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Optional seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Test User 1", Age = 29, City = "Hyderabad", State = "Telangana", Pincode = "500001" },
                new User { Id = 2, Name = "Test User 2", Age = 35, City = "Bengaluru", State = "Karnataka", Pincode = "560001" }
            );
        }
    }
}
