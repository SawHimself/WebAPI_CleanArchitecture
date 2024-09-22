using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
namespace Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Products> products { get; set; }
        public DbSet<Pizza> pizza { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<Customers> customers { get; set; }
    }
}
