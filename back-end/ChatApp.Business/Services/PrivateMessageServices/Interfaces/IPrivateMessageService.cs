using ChatApp.Business.DataTransferObject.ChatDTO;
using ChatApp.Business.DataTransferObject.MessageDTO;

namespace ChatApp.Business.Services.PrivateMessageServices.Interfaces
{
    public interface IPrivateMessageService
    {
        Task<PrivateMessageResponseDTO> StorePrivateMessage(int destinationUserId, string textMessage);
        Task<PrivateMessagesWithPaginationResponseDTO> GetPrivateMessages(
            DateTime? pageDate,
            int pageSize,
            int firstUserId,
            int secoundUserId);
        Task<IEnumerable<ChatWithLastMessageResponseDTO>> GetRecentChatsForUser(int userId);
        Task<int> DeletePrivateMessage(int id);
    }
}