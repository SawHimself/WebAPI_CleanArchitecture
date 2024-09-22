using ApplicationCore.DTO;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services.OrderServices
{
    public class DisplayOrdersService
    {
        private readonly AppDbContext _context;

        public DisplayOrdersService(AppDbContext context)
        {
            _context = context;
        }
        public OrderListDto GetOrderDetail(int orderId)
        {
            var order = SelectQuery(_context.orders).SingleOrDefault(x => x.Id == orderId);
            if (order == null) 
            {
                throw new NullReferenceException($"Could not find the order with id of {orderId}.");
            }
            return order;
        }

        private IQueryable<OrderListDto> SelectQuery(IQueryable<Orders> orders)
        {
            return orders.Select(x => new OrderListDto
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                Details = x.Details
                    .OrderBy(q => q.OrderId) // or Price ?
                    .Select(details => new OrderDetailsDto
                    {
                        ProductId = details.ProductId,
                        Quantity = details.Quantity,
                        Price = details.Price,
                    })
            }
            );
        }
    }
}
