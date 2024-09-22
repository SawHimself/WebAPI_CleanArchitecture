using ApplicationCore.Models;

namespace BizLogic
{
    public interface IPizzaUseCase
    {
        public Pizza AddPizza(string Name, string Size, string Desciption, string Category, float Price);
        public Pizza DeletePizza(int id);
    }
}
