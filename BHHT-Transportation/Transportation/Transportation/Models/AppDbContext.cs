
using Microsoft.EntityFrameworkCore;

namespace Transportation.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<TransporterRecord> TransporterRecord { get; set; }
        public DbSet<BillToRecord> BillToRecord { get; set; }
        public DbSet<ReceiviedOrderRecord> ReceiviedOrderRecord { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
           (
                new User
                {
                    Id = 1,
                    FirstName = "Super",
                    LastName = "Admin",
                    Contact = "00000000000",
                    Email = "superadmin@yopmail.com",
                    Password = "123",
                    Role = 0,
                    IsActive = 1,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Contact = "00000000000",
                    Email = "admin@yopmail.com",
                    Password = "123",
                    Role = 1,
                    IsActive = 1,
                    CreatedAt = DateTime.Now
                }              

            );
        }
    }
}
