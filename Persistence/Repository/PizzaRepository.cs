using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly AppDbContext _context;

        public PizzaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Pizza AddPizza(Pizza pizza)
        {
            _context.pizza.Add(pizza);
            _context.SaveChanges();
            return pizza;
        }
        public Pizza DeletePizza(int id)
        {
            Pizza pizza = _context.pizza
                .Include(p => p.Product)
                .Single(p => p.Id == id);
            _context.Remove(pizza);
            _context.SaveChanges();

            return pizza;
        }

        public Pizza GetPizzaById(int id) 
        { 
            Pizza pizza = _context.pizza
                .Single(p => p.Id == id);
            return pizza;
        }

        public IQueryable<Pizza> SearchPizza(string Name)
        {
            var pizza = _context.pizza
                .Where(p => p.Name == Name);
            return (pizza);
        }

        public Products? GetDetailsById(int pizzaId)
        {
            var pizza = _context.pizza
                .FirstOrDefault(p => p.Id == pizzaId);
            if(pizza != null)
            {
                var product = _context.products
                    .FirstOrDefault(p => p.Id == pizza.ProductId);
                return product;
            }   
            return null;
        }
    }
}
