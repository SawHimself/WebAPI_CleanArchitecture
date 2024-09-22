using ApplicationCore.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AdminServices
{
    public class DeletePizzaService : IDeletePizzaService
    {
        protected AppDbContext _context;

        DeletePizzaService(AppDbContext context)
        {
            _context = context;
        }

        //каскадное удаление зависимых сущностей (Product)
        public Pizza DeletePizza(int id)
        {
            Pizza pizza = _context.pizza
                /* Если бы вы не включили эти методы в свой код, EF Core не знал бы 
                   о зависимых сущностях и не смог бы удалить три зависимые сущности. 
                   В этом случае проблема сохранения ссылочной целостности будет лежать на сервере базы данных*/
                .Include(p => p.Product)
                .Single(p => p.Id == id);
            _context.Remove(pizza);
            _context.SaveChanges();

            return pizza;
        }
    }
}
