using ApplicationCore.DTO;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ClientServices
{
    public class PlaceOrderAction
    {
        private readonly AppDbContext _context;

        public PlaceOrderAction(AppDbContext context)
        {
            _context = context;
        }

        public Orders Action(PlaceOrderInDto dto)
        {
            return new Orders();
        }
    }
}
