using ApplicationCore.DTO;
using ApplicationCore.Models;
using Persistence;

namespace Services.AdminServices.Concrete
{
    public class EditPizzaService : IEditPizzaService
    {
        private readonly AppDbContext _context;

        public EditPizzaService(AppDbContext context)
        {
            _context = context;
        }


        /*Метод отвечает за первый этап обновления. 
         *Он получает данные из выбранной пиццы для отображения пользователю*/
        public PizzaDto GetOriginal(int id)
        {
            return _context.pizza
                .Join(_context.products,
                pizza => pizza.ProductId,
                product => product.Id,
                (pizza, product) => new { pizza, product })
                .Select(p => new PizzaDto
                {
                    Id = p.pizza.Id,
                    Name = p.pizza.Name,
                    Size = p.pizza.Size,
                    Description = p.pizza.Description,
                    Price = p.product.Price
                })
            .Single(k => k.Id == id);
        }
        public PizzaDto UpdatePizzaDescription(PizzaDto dto)
        {
            Pizza pizza = _context.pizza
                .Single(p => p.Id == dto.Id);
            if (pizza == null)
            {
                throw new ArgumentException("pizza not found");
            }
            pizza.Description = dto.Description;
            return new PizzaDto()
            {
                Id = dto.Id,
                Name = pizza.Name,
                Description = dto.Description,
                Price = dto.Price,
            };
        }
        public PizzaDto UpdatePizzaName(PizzaDto dto)
        {
            var pizza = _context.pizza.Single(
                x => x.Id == dto.Id);
            if (pizza == null)
            {
                throw new ArgumentException("Pizza not found");
            }
            pizza.Name = dto.Name;
            _context.SaveChanges();
            return new PizzaDto()
            {
                Id = dto.Id,
                Name = pizza.Name,
                Description = dto.Description,
                Price = dto.Price,
            };
        }
        public PizzaDto UpdatePizzaPrice(PizzaDto dto)
        {
            // Single немного быстрее Find
            var search = _context.pizza.Single(
                x => x.Id == dto.Id);

            var product = _context.products.Single(
                x => x.Id == search.ProductId);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            product.Price = dto.Price;
            var pizza = _context.pizza.Single(
                x => x.ProductId == search.ProductId/* && x.Name == dto.Name*/);
            _context.SaveChanges();
            if (pizza == null)
            {
                throw new ArgumentException("Pizza not found");
            }
            return new PizzaDto() 
            { 
                Id = pizza.Id, 
                Name = pizza.Name, 
                Description = pizza.Description, 
                Price = pizza.Product.Price 
            };
        }
    }
}
