using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    public class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public Customers Customer { get; set; } = null!;
        public OrderDetails Details { get; set; } = null!;

        public Orders() { }

        public Orders(string status, Customers customer, OrderDetails details)
        {
            Status = status;
            Customer = customer;
            Details = details;
            OrderDate = DateTime.Now;
        }
    }
}
