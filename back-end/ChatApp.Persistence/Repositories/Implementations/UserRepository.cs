using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ChatApp.Persistence.DbContexts;
using ChatApp.Persistence.Entities;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Persistence.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext context;
        public UserRepository(ChatContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<Tuple<List<User>, int>> GetUsers(
            int pageNumber,
            int pageSize,
            string searchText = null)
        {
            var users = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                // Search by first name, last name, or email
                users = users.Where(u =>
                    u.FirstName.Contains(searchText) ||
                    u.LastName.Contains(searchText) ||
                    u.Email.Contains(searchText));
            }

            var usersCount = await users.CountAsync();
            var numOfPages = Math.Ceiling(usersCount / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                return null;
            }

            var usersList = await users.OrderBy(c => c.FirstName)
                           .ThenBy(c => c.LastName)
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();

            var result = Tuple.Create(usersList, (int)numOfPages);
            return result;
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync();
        }

        public bool CheckIfEmailExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

    }
}
