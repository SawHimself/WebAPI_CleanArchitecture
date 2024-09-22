using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BizLogic.concrete
{
    public class ClientUseCase : IClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPizzaRepository _pizzaRepository;

        public ClientUseCase(IClientRepository clientRepository, IPizzaRepository pizzaRepository)
        {
            _clientRepository = clientRepository;
            _pizzaRepository = pizzaRepository;
        }

        public Customers? PlaceCustomerAction(UserDto newUser)
        {
            if(String.IsNullOrWhiteSpace(newUser.Name))
            {
                return null;
            }
            if(String.IsNullOrWhiteSpace(newUser.Email))
            {
                return null;
            }

            if(_clientRepository.SearchClientByEmail(newUser.Email) != null) 
            {
                return null;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            Customers newCustomer = new Customers(newUser.Name, 0, newUser.Email, passwordHash);

            _clientRepository.AddClient(newCustomer);

            return newCustomer;
        }

        public Customers? DeleteCustomerAction(LoginDto client)
        {
            Customers? search = _clientRepository.SearchClientByEmail(client.Email);
            if(search != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(client.Password, search.PasswordHash))
                {
                    return null;
                }
                if (search != null)
                {
                    _clientRepository.DeleteClient(search.Id);
                }
            }
            return search;
        }

        public string? GetCustomerAction(LoginDto client, string secretKey) 
        { 
            Customers? search = _clientRepository.SearchClientByEmail(client.Email);
            if(search == null) 
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(client.Password, search.PasswordHash))
            {
                return null;
            }
            else
            {
                string token = CreateToken(search, secretKey);
                return token;
            }
        }

        public OrderDto? TryToBuy(string clientEmail, int pizzaId)
        {
            Customers? search = _clientRepository.SearchClientByEmail(clientEmail);

            Products? details = _pizzaRepository.GetDetailsById(pizzaId);
            if(details != null && search != null)
            {
                Orders newOrder = new Orders("Confirmed", search, new OrderDetails(pizzaId, 1, details.Price));

                if (search.AccountBalance >= details.Price)
                {
                    newOrder = _clientRepository.AddOrders(search.Id, newOrder);
                    if (search != null)
                    {
                        var pizza = _pizzaRepository.GetPizzaById(pizzaId);
                        return new OrderDto
                        {
                            Id = newOrder.Id,
                            ProducName = pizza.Name,
                            OrderDate = newOrder.OrderDate,
                            TotalAmount = newOrder.Details.Price * newOrder.Details.Quantity,
                            Status = newOrder.Status
                        };
                    }
                } 
            }
            return null;
        }

        private string CreateToken(Customers customers, string secretKey)
        {
            List<Claim> claims = new List<Claim>();
            if (customers.Role != null)
            {
                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, customers.Name),
                    new Claim(ClaimTypes.Sid, customers.Id.ToString()),
                    new Claim(ClaimTypes.Role, customers.Role)
                };
            }
            else
            {
                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, customers.Name),
                    new Claim(ClaimTypes.Email, customers.Email),
                    new Claim(ClaimTypes.Sid, customers.Id.ToString()),
                };
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
