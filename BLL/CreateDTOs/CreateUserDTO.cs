namespace BLL.CreateDTOs
{
    public class CreateUserDTO
    {
        public required string Username { get; set; } = string.Empty;
        public string? Role { get; set; } = "USER";
        public string Password { get; set; } = string.Empty;
    }
}
