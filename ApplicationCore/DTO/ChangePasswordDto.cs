namespace ApplicationCore.DTO
{
    public class ChangePasswordDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
