using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace BizLogic.concrete
{
    public class PizzaUseCase : IPizzaUseCase
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaUseCase(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public Pizza AddPizza(string Name, string Size, string Desciption, string Category, float Price)
        {

            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Invalid value for field Name");
            }
            if (Size != "Large" && Size != "Medium" && Size != "Small")
            {
                throw new ArgumentException("use only values ​​from the list for the Price field: Small, Medium, Large");
            }
            if (String.IsNullOrWhiteSpace(Desciption))
            {
                throw new ArgumentException("Invalid value for field Description");
            }
            if (String.IsNullOrWhiteSpace(Category))
            {
                throw new ArgumentException("Invalid value for field Category");
            }
            if (Price <= 0)
            {
                throw new ArgumentException("Invalid value for field Price");
            }

            //имя-размер не должно повторяться
            List<Pizza> similarPizzaList = _pizzaRepository.SearchPizza(Name).ToList();
            if (similarPizzaList != null)
            {
                foreach (Pizza similarPizza in similarPizzaList)
                {
                    if (similarPizza.Size == Size)
                    {
                        throw new ArgumentException("Name should not be repeated for pizzas of the same size");
                    }
                }
            }

            Products newproduct = new Products(Price, Category);
            Pizza pizza = new Pizza(Name, Size, Desciption, newproduct);

            return _pizzaRepository.AddPizza(pizza);
        }
        public Pizza DeletePizza(int id)
        {
            return _pizzaRepository.DeletePizza(id);
        }
    }
}
