using ChatApp.Business.DataTransferObject.UserDTO;

namespace ChatApp.Business.Services.UserService.Interfaces
{
    public interface IUserAccountService
    {
        Task<string> LoginUserAsync(LoginDTO userRequestDTO);
        Task<UserResponseDTO> RegisterUserAsync(UserRequestDTO userRequestDTO);
        Task<UserResponseDTO> GetUserByJwtTokenAsync();
        //Task ChangePasswordAsync(int userId, ChangePasswordRequestDTO changePasswordDto);
        Task ChangeUserAboutAsync(int userId, string newAbout);
        Task ChangeThemeModeAsync(int userId, bool isDarkMode);
    }
}