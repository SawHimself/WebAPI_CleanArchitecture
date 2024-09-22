using ApplicationCore.DTO;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Services.ClientServices.Concrete
{
    public class PageListPizzaService : IPageListPizzaService
    {
        private readonly AppDbContext _context;
        public PageListPizzaService(AppDbContext context) { _context = context; }

        public IQueryable<PizzaDto> SortFilterPage(int position, int pageSize, string? name)
        {
            var pizzaQuery = _context.pizza
                .AsNoTracking()
                .Join(_context.products,
                    piz => piz.ProductId,
                    prod => prod.Id,
                    (piz, prod) => new PizzaDto
                    {
                        Id = piz.Id,
                        Name = piz.Name,
                        Size = piz.Size,
                        Description = piz.Description,
                        Price = prod.Price
                    });

            // Добавляем фильтр по имени, если оно не null и не пустое
            if (!string.IsNullOrWhiteSpace(name))
            {
                pizzaQuery = pizzaQuery.Where(p => p.Name.Contains(name));
            }

            pizzaQuery = pizzaQuery
                .OrderByDescending(p => p.Price)
                .Skip(position * pageSize)
                .Take(pageSize);

            return pizzaQuery;
        }
    }
}
