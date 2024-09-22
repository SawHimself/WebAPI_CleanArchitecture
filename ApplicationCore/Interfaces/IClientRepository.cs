using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IClientRepository
    {
        public Customers AddClient(Customers customer);
        public Customers DeleteClient(int id);
        public Customers ChangePassword(int id, string passwordHash);
        public Orders AddOrders(int ClientId, Orders orders);
        public Customers? SearchClientByEmail(string email);
    }
}
