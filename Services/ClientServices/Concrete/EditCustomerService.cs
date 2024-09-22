using ApplicationCore.DTO;
using ApplicationCore.Models;
using Persistence;

namespace Services.ClientServices.Concrete
{
    public class EditCustomerService : IEditCustomerService
    {
        private readonly AppDbContext _context;

        public EditCustomerService(AppDbContext context)
        {
            _context = context;
        }
        public CustomerDto GetOriginal(int id)
        {
            return _context.customers
                .Select(p => new CustomerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    AccountBalance = p.AccountBalance,
                })
                .Single(k => k.Id == id);
        }

        public Customers? UpdateCustomerName(CustomerDto dto)
        {
            var customer = _context.customers.Single(
                x => x.Id == dto.Id);
            if (customer == null)
            {
                return null;
            }
            customer.Name = dto.Name;
            _context.SaveChanges();
            return customer;
        }

        public Customers? UpdateCustomerPassword(ChangePasswordDto dto)
        {
            Customers? search = _context.customers.Single(x => x.Email == dto.Email);
            if(search == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, search.PasswordHash))
            {
                return null;
            }
            search.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.SaveChanges();
            return _context.customers.Single(x => x.Email == dto.Email);
        }
    }
}
