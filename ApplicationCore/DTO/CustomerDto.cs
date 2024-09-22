namespace ApplicationCore.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float AccountBalance { get; set; }
        public string Email { get; set; } = null!;
    }
}
