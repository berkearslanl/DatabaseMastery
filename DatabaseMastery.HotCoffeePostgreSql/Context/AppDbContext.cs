using DatabaseMastery.HotCoffeePostgreSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Review> Reviews  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                AdminUserId = 1,
                Username = "berkearslan",
                Password = "5896"
            });
        }
    }
}
