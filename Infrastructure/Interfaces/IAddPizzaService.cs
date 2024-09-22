using ApplicationCore.Models;

namespace Infrastructure.Interfaces
{
    public interface IAddPizzaService
    {
        Pizza AddPizza(Pizza pizza);
    }
}
