using ApplicationCore.DTO;
using ApplicationCore.Models;

namespace Infrastructure.Interfaces
{
    public interface IChangeNamePizzaService
    {
        ChangeNamePizzaDto GetOriginal(int id);
        Pizza UpdatePizza(ChangeNamePizzaDto dto);
    }
}
