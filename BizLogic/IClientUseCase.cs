using ApplicationCore.DTO;
using ApplicationCore.Models;

namespace BizLogic
{
    public interface IClientUseCase
    {
        public Customers? PlaceCustomerAction(UserDto newUser);
        public string? GetCustomerAction(LoginDto client, string secretKey);
        public Customers? DeleteCustomerAction(LoginDto client);
        public OrderDto? TryToBuy(string clientEmail, int productId);
    }
}
