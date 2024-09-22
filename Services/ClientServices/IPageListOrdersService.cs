using ApplicationCore.DTO;

namespace Services.ClientServices
{
    public interface IPageListOrdersService
    {
        public IQueryable<OrderDto> SortFilterPage(int ClientId, int position, int pageSize);
    }
}
