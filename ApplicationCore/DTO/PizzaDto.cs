namespace ApplicationCore.DTO
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
    }
}
