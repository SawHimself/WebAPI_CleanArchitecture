using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    public class OrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public OrderDetails() { }
        public OrderDetails(int productId, int quantity, float price) 
        { 
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}
