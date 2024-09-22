using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Products> products { get; set; }
        public DbSet<Pizza> pizza { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<Customers> customers { get; set; }
    }
}
