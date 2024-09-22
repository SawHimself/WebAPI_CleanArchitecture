

using ApplicationCore.Models;

namespace Infrastructure.Interfaces
{
    public interface IDeletePizzaService
    {
        Pizza DeletePizza(int id);
    }
}
