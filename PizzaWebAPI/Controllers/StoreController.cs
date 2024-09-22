using ApplicationCore.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.ClientServices;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        public StoreController(IPageListPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        private readonly IPageListPizzaService _pizzaService;

        [HttpGet("GetPizzaPage")]
        public ActionResult<List<PizzaDto>> ViewPizzaCatalog(int Position, int PageSize, string? PizzaName)
        {
            return _pizzaService.SortFilterPage(Position, PageSize, PizzaName).ToList();
        }
    }
}
