using ApplicationCore.DTO;
using ApplicationCore.Models;
using BizLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ClientServices;
using Services.LoggerService;
using System.Security.Claims;

namespace PizzaWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IPageListOrdersService _clientService;
        private readonly IClientUseCase _clientUseCase;
        private readonly IEditCustomerService _editCustomerService;
        private readonly ILoggerManager _logger;
        public ClientController(IPageListOrdersService clientService, IClientUseCase clientUseCase, IEditCustomerService editCustomerService, ILoggerManager logger)
        {
            _clientService = clientService;
            _clientUseCase = clientUseCase;
            _editCustomerService = editCustomerService;
            _logger = logger;
        }

        [HttpGet("GetOriginalClient")]
        public ActionResult<CustomerDto> ViewOriginal()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized("User is not authorized");
            }
            return _editCustomerService.GetOriginal(Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid)));
        }

        //verified
        [HttpGet("GetOrdersPage")]
        public ActionResult<List<OrderDto>> ViewOrders(int Position, int PageSize)
        {
            if (Position < 0 || PageSize <= 0)
            {
                return BadRequest("Invalid pagination parameters.");
            }
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);
            if (userIdClaim == null)
            {
                return Unauthorized("User is not authorized.");
            }
            int id = Convert.ToInt32(userIdClaim);
            var orderlist = _clientService.SortFilterPage(id, Position, PageSize).ToList();
            return orderlist;
        }

        //verified
        [HttpPut("ChangeCustomerName")]
        public ActionResult<CustomerDto> ChangeCustomerName(CustomerDto dto) 
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized("User is not authorized");
            }
            Customers? client = _editCustomerService.UpdateCustomerName(dto);
            if (client == null) 
            {
                _logger.LogWarn($"Failed to change account name: Customer with id {dto.Id} not found for user {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)}");
                return BadRequest("Account not found");
            }
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} changed name");
            return new CustomerDto()
            {
                Name = client.Name,
                Email = client.Email,
                AccountBalance = client.AccountBalance,
            };
        }

        //verified
        [HttpPut("ChangeCustomerPassword")]
        public ActionResult<CustomerDto> ChangePassword(ChangePasswordDto dto)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if(email == null)
            {
                return Unauthorized("User is not authorized");
            }
            var client = _editCustomerService.UpdateCustomerPassword(dto);
            if(client == null || email != dto.Email) 
            {
                return BadRequest("Incorrect password or login");
            }
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} changed password");
            return new CustomerDto()
            {
                Name = client.Name,
                Email = client.Email,
                AccountBalance = client.AccountBalance
            };
        }

        //verified
        [HttpPut("BuyYummyPizza{id:int}")]
        public ActionResult<OrderDto>? BuyPizza(int ProductId)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized("User is not authorized");
            }

            OrderDto? orderRequest = _clientUseCase.TryToBuy(email, ProductId);
            if(orderRequest != null) 
            {
                _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} placed an order");
                return orderRequest;
            }
            else
            {
                _logger.LogWarn($"Order failed: Pizza with ProductId {ProductId} not found for user {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)}.");
                return NotFound("Pizza not found");
            }
        }

        //verified
        [HttpDelete("DeleteAccount")]
        public ActionResult<CustomerDto>? DeleteAccount(LoginDto dto)
        {
            Customers? client = _clientUseCase.DeleteCustomerAction(dto);
            if(client == null) 
            {
                return BadRequest("Incorrect password or login");
            }
            _logger.LogWarn($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} client deleted account! ({client.Id}:{client.Name})");
            return new CustomerDto
            {
                Id = client.Id,
                Name = client.Name,
                AccountBalance = client.AccountBalance,
                Email = client.Email
            };
        }
    }
}
