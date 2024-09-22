using ApplicationCore.DTO;
using BizLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.AdminServices;
using Services.LoggerService;
using System.Security.Claims;

namespace PizzaWebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPizzaUseCase _pizzaUseCase;
        private readonly IEditPizzaService _pizzaService;
        ILoggerManager _logger;
        public AdminController(IPizzaUseCase pizzaUseCase, IEditPizzaService pizzaService, ILoggerManager logger)
        {
            _pizzaUseCase = pizzaUseCase;
            _pizzaService = pizzaService;
            _logger = logger;
        }

        [HttpPost("pizza/create")]
        public ActionResult<PizzaDto> AddPizza(string Name, string Size, string Description, string Category, float Price)
        {
            var newPizza = _pizzaUseCase.AddPizza(Name, Size, Description, Category, Price);
            PizzaDto pizzaDto = new PizzaDto()
            {
                Id = newPizza.Id,
                Name = newPizza.Name,
                Size = newPizza.Size,
                Description = newPizza.Description
            };
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} added a new product");
            return pizzaDto;
        }

        [HttpGet("pizza/{id:int}/get_original")]
        public ActionResult<PizzaDto> GetOriginalPizza(int id)
        {
            return _pizzaService.GetOriginal(id);
        }

        [HttpPut("pizza/change/name")]
        public ActionResult<PizzaDto> ChangePizzaName(PizzaDto dto)
        {
            var pizza = _pizzaService.UpdatePizzaName(dto);
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} update {pizza.Id}");
            return pizza;
        }
        [HttpPut("pizza/change/description")]
        public ActionResult<PizzaDto> ChangePizzaDesciption(PizzaDto dto) 
        {
            var pizza = _pizzaService.UpdatePizzaDescription(dto);
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} update {pizza.Id}");
            return pizza;
        }
        [HttpPut("pizza/change/price")]
        public ActionResult<PizzaDto> ChangePizzaPrice(PizzaDto dto)
        {
            var pizza = _pizzaService.UpdatePizzaPrice(dto);
            _logger.LogInfo($"Database changes. {User.FindFirstValue(ClaimTypes.Sid)}:{User.FindFirstValue(ClaimTypes.Name)} update {pizza.Id}");
            return pizza;
        }
    }
}
