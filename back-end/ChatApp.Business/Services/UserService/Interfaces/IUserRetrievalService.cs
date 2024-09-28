using ChatApp.Business.DataTransferObject.UserDTO;

namespace ChatApp.Business.Services.UserService.Interfaces
{
    public interface IUserRetrievalService
    {
        Task<UsersWithPaginationResponseDTO> GetUsers(int pageNumber, int pageSize, string searchText = null);
        Task<UserResponseDTO> GetUserById(int userId);
    }
}