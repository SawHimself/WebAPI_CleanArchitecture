using ApplicationCore.DTO;
using ApplicationCore.Models;

namespace Infrastructure.Interfaces
{
    public interface IChangePricePizzaService
    {
        ChangePricePizzaDto GetOriginal(int id);
        Pizza UpdatePizza(ChangePricePizzaDto dto);
    }
}
