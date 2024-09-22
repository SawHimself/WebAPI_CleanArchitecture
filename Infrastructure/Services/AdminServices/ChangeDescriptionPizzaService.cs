using ApplicationCore.DTO;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AdminServices
{
    public class ChangeDescriptionPizzaService
    {
        private readonly AppDbContext _context;

        public ChangeDescriptionPizzaService(AppDbContext context)
        {
            _context = context;
        }

        /*Метод отвечает за первый этап обновления. 
         *Он получает данные из выбранной пиццы для отображения пользователю*/
        public ChangeDescriptionPizzaDto GetOriginal(int id)
        {
            return _context.pizza.Select(
                p => new ChangeDescriptionPizzaDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                })
            .Single(k => k.Id == id);
        }

        public Pizza UpdatePizza(ChangeDescriptionPizzaDto dto)
        {
            Pizza pizza = _context.pizza
                .Single(p => p.Id == dto.Id);
            if (pizza == null)
            {
                throw new ArgumentException("pizza not found");
            }
            pizza.Description = dto.Description;
            return pizza;
        }
    }
}
