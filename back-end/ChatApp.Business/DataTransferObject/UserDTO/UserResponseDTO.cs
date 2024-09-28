
namespace ChatApp.Business.DataTransferObject.UserDTO
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public bool IsDarkTheme { get; set; }
    }
}
