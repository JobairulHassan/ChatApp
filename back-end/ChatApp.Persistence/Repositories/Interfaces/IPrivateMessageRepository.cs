using ChatApp.Persistence.Entities;
using ChatApp.Persistence.Retreve;

namespace ChatApp.Persistence.Repositories.Interfaces
{
    public interface IPrivateMessageRepository
    {
        Task AddAsync(PrivateMessage message);
        void Delete(PrivateMessage message);
        int DeletePrivateMessage(int id, int userId);
        Task<Tuple<List<PrivateMessage>, bool>> GetPrivateMessagesForPrivateChat(
            DateTime pageDate,
            int pageSize,
            int firstUserId,
            int secoundUserId);

        Task<IEnumerable<ChatWithLastMessage>> GetRecentChatsForUser(int userId);
    }
}