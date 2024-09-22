using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ClientServices
{
    public class AddClientService
    {
        private readonly AppDbContext _context;

        public AddClientService (AppDbContext context)
        {
            _context = context;
        }

        public Customers AddClient (Customers client)
        {
            _context.customers.Add(client);
            _context.SaveChanges();
            return client;
        }
    }
}
