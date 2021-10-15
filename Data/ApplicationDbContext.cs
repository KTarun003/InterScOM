using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Query> Queries { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Supplies> Supplies { get; set; }

        public DbSet<Fee> Fee { get; set; }

        public DbSet<Application> Application { get; set; }

        public DbSet<VendorOrders> VendorOrders { get; set; }

        public DbSet<Vendor> Vendor { get; set; }
    }
}
