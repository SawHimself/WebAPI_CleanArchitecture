using ApplicationCore.DTO;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AdminServices
{
    public class ChangePricePizzaService
    {
        private readonly AppDbContext _context;

        ChangePricePizzaService(AppDbContext context)
        {
            _context = context;
        }

        public ChangePricePizzaDto GetOriginal(int id)
        {
            return _context.pizza
                .Join(_context.products,
                pizza => pizza.ProductId,
                product => product.Id,
                (pizza, product) => new { pizza, product })
                .Select(p => new ChangePricePizzaDto
                {
                    ProductId = p.product.Id,
                    Name = p.pizza.Name,
                    Price = p.product.Price
                })
            .Single(k => k.ProductId == id);
        }

        public Pizza UpdatePizza(ChangePricePizzaDto dto)
        {
            // Single немного быстрее Find
            var product = _context.products.Single(
                x => x.Id == dto.ProductId);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            product.Price = dto.Price;
            var pizza = _context.pizza.Single(
                x => x.ProductId == dto.ProductId && x.Name == dto.Name);
            _context.SaveChanges();
            if (pizza != null)
            {
                throw new ArgumentException("Pizza not found");
            }
            return pizza;
        }
    }
}
