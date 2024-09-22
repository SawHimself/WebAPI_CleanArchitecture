using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public Customers AddClient(Customers customer)
        {
            _context.customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customers DeleteClient(int id) 
        { 
            Customers customer = _context.customers
                .Include(o => o.orders)
                .Single(p => p.Id == id);
            _context.Remove(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customers ChangePassword(int id, string passwordHash) 
        {
            var client = _context.customers
                .Single(c => c.Id == id);
            client.PasswordHash = passwordHash;
            _context.SaveChanges();
            return client;
        }

        public Orders AddOrders(int ClientId, Orders orders)
        {
            var customer = _context.customers.Single(
                         x => x.Id == ClientId);
            if(customer == null)
            {
                throw new ArgumentException("Client not found");
            }
            else
            {
                if (customer.orders == null)
                {
                    customer.orders = new List<Orders>();
                    _context.SaveChanges();
                }
                customer.orders.Add(orders);
                customer.AccountBalance = customer.AccountBalance - orders.Details.Price;
                _context.SaveChanges();
                return _context.orders.Single(o => o.Id == orders.Id);
            }
        }

        public Customers? SearchClientByEmail(string email)
        {
            Customers? customer = _context.customers.FirstOrDefault(x => x.Email == email);
            return customer;
        }
    }
}
