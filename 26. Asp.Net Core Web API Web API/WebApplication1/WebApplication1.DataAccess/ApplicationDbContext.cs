using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Entities;

namespace WebApplication1.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<OrderItem> OrderItems { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
