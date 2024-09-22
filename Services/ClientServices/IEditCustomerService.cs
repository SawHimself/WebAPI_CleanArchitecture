using ApplicationCore.DTO;
using ApplicationCore.Models;

namespace Services.ClientServices
{
    public interface IEditCustomerService
    {
        public CustomerDto GetOriginal(int id);
        public Customers? UpdateCustomerName(CustomerDto dto);
        public Customers? UpdateCustomerPassword(ChangePasswordDto dto);
    }
}
