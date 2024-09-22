using ApplicationCore.Models;

namespace Infrastructure.Services.AdminServices
{
    public class AddPizzaService
    {
        private readonly AppDbContext _context;

        public AddPizzaService(AppDbContext context)
        {
            _context = context;
        }

        //pizza уже содержит класс product и поле product является обязательным
        public Pizza AddPizza(Pizza pizza)
        {
            _context.pizza.Add(pizza);
            //Здесь должно быть создание ProductId
            _context.SaveChanges();
            return pizza;
        }
    }
}
