using ApplicationCore.DTO;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Services.ClientServices.Concrete
{
    public class PageListOrdersService : IPageListOrdersService
    {
        private readonly AppDbContext _context;
        public PageListOrdersService(AppDbContext context) { _context = context; }

        public IQueryable<OrderDto> SortFilterPage(int ClientId, int position, int pageSize)
        {
            var orderQuery = (from o in _context.orders
                              join od in _context.orderDetails on o.Id equals od.OrdersId
                              join pizza in _context.pizza on od.ProductId equals pizza.ProductId
                              where o.CustomerId == ClientId
                              orderby o.OrderDate
                              select new OrderDto
                              {
                                  ProducName = pizza.Name,
                                  OrderDate = o.OrderDate,
                                  TotalAmount = od.Price * od.Quantity,
                                  Status = o.Status
                              })
                              .AsNoTracking()
                              .Skip(position)
                              .Take(pageSize);
            return orderQuery;
        }
    }
}
