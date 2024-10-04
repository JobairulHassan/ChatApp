using System.ComponentModel.DataAnnotations;

namespace ChatApp.Business.DataTransferObject.UserDTO
{
    public class LoginDTO
    {
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

    }
}