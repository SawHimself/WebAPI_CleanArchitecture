using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    public class Pizza
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Products Product { get; set; } = null!;

        public Pizza() { }
        public Pizza(string name, string size, string description, Products? product)
        {
            Name = name;
            Size = size;
            Description = description;
            if (product != null) 
            { 
                Product = product; 
            }
        }
    }
}
