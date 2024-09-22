using ApplicationCore.DTO;

namespace Services.AdminServices
{
    public interface IEditPizzaService
    {
        public PizzaDto GetOriginal(int id);
        public PizzaDto UpdatePizzaDescription(PizzaDto dto);
        public PizzaDto UpdatePizzaName(PizzaDto dto);
        public PizzaDto UpdatePizzaPrice(PizzaDto dto);
    }
}
