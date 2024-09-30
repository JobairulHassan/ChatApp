
namespace ChatApp.Business.DataTransferObject.UserDTO
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public bool IsDarkTheme { get; set; }
    }
}
