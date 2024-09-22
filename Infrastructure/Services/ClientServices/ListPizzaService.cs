using ApplicationCore.DTO;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClientServices
{
    public class ListPizzaService
    {
        private readonly AppDbContext _context;
        public ListPizzaService(AppDbContext context) { _context = context; }

        public IQueryable<ListPizzaDto> SortFilterPage(int position, int pageSize)
        {
            //добавить проверки для position и pageSize
            var pizzaQuery = _context.pizza
                .AsNoTracking()
                .Join(_context.products,
                piz => piz.ProductId,
                prod => prod.Id,
                (piz, prod) => new ListPizzaDto
                {
                    Name = piz.Name,
                    Size = piz.Size,
                    Description = piz.Description,
                    Price = prod.Price
                })
                .OrderByDescending(p => p.Price)
                .Skip(position) // вместо Skiop рекомендуется использовать Where
                                // но для его использования необходимо выполнить сортировку по Id 
                .Take(pageSize);
            return pizzaQuery;
        }
    }
}
