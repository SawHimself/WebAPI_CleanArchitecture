using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IPizzaRepository
    {
        Pizza AddPizza(Pizza pizza);
        Pizza DeletePizza(int id);
        Pizza GetPizzaById(int id);
        IQueryable<Pizza> SearchPizza(string Name);
        Products? GetDetailsById(int pizzaId);
    }
}
