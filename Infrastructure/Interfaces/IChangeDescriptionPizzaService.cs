using ApplicationCore.DTO;
using ApplicationCore.Models;

namespace Infrastructure.Interfaces
{
    public interface IChangeDescriptionPizzaService
    {
        ChangeDescriptionPizzaDto GetOriginal(int id);
        Pizza UpdatePizza(ChangeDescriptionPizzaDto dto);
    }
}
