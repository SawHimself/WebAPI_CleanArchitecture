using ApplicationCore.DTO;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AdminServices
{
    public class ChangeNamePizzaService
    {
        private readonly AppDbContext _context;

        ChangeNamePizzaService(AppDbContext context)
        {
            _context = context;
        }

        public ChangeNamePizzaDto GetOriginal(int id)
        {
            return _context.pizza
                .Select(p => new ChangeNamePizzaDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            .Single(k => k.Id == id);
        }

        public Pizza UpdatePizza(ChangeNamePizzaDto dto)
        {
            var pizza = _context.pizza.Single(
                x => x.Id == dto.Id);
            if (pizza == null)
            {
                throw new ArgumentException("Pizza not found");
            }
            pizza.Name = dto.Name;
            _context.SaveChanges();
            return pizza;
        }
    }
}
