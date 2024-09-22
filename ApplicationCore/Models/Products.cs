using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Price { get; set; }
        public string Category { get; set; } = null!;

        public Products() { }
        public Products(float price, string category)
        {
            Price = price;
            Category = category;
        }
    }
}
