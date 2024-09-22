using ApplicationCore.DTO;

namespace Services.ClientServices
{
    public interface IPageListPizzaService
    {
        public IQueryable<PizzaDto> SortFilterPage(int position, int pageSize, string? name);
    }
}
