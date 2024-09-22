namespace ApplicationCore.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ProducName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string Status { get; set; } = null!;
    }
}
