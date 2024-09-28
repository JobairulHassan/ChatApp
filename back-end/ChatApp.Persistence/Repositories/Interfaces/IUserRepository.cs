using ChatApp.Persistence.Entities;

namespace ChatApp.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        bool CheckIfUsernameExists(string username);
        void Delete(User user);
        Task<User?> GetUserById(int userId);
        Task<Tuple<List<User>, int>> GetUsers(
            int pageNumber,
            int pageSize,
            string searchText = null);
        Task<User?> GetUserByUsername(string username);
    }
}