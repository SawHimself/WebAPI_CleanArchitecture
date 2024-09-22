using ApplicationCore.DTO;
using ApplicationCore.Models;
using BizLogic;
using Microsoft.AspNetCore.Mvc;
using Services.LoggerService;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClientUseCase _clientUseCase;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public AuthController(IClientUseCase clientUseCase, IConfiguration configuration, ILoggerManager logger)
        {
            _clientUseCase = clientUseCase;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public ActionResult<CustomerDto>? Register(UserDto request)
        {
            Customers? client = _clientUseCase.PlaceCustomerAction(request);
            if(client != null) 
            {
                CustomerDto customer = new CustomerDto()
                { 
                    Id = client.Id,
                    AccountBalance = client.AccountBalance,
                    Email = client.Email,
                    Name = client.Name
                };
                _logger.LogInfo($"Database changes. New user registration: {client.Id}:{client.Name}");
                return customer;
            }
            return null;
        }
        [HttpPost("login")]
        public ActionResult<string?> Login(LoginDto client) 
        {
            if (string.IsNullOrWhiteSpace(client.Email) || string.IsNullOrWhiteSpace(client.Password))
            {
                return BadRequest("Username and password must not be empty");
            }

            string? token = _clientUseCase.GetCustomerAction(client, _configuration.GetSection("AppSettings:Token").Value!);
            if (token == null)
            {
                return Unauthorized("Incorrect password or login");
            }
            return token;
        }
    }
}
