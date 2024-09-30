using System.ComponentModel.DataAnnotations;

namespace ChatApp.Business.DataTransferObject.UserDTO
{
    public class UserRequestDTO
    {
        [RegularExpression(@"^[A-Za-z][A-Za-z]*$", ErrorMessage = "Invalid name format.")]
        public string FirstName { get; set; } = string.Empty;

        [RegularExpression(@"^[A-Za-z][A-Za-z]*$", ErrorMessage = "Invalid name format.")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;


        [MinLength(8, ErrorMessage = "password length must be greater or equal 8 character")]
        public string Password { get; set; } = string.Empty;
    }
}
