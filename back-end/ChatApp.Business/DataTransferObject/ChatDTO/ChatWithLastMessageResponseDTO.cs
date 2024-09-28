using ChatApp.Business.DataTransferObject.MessageDTO;
using ChatApp.Business.DataTransferObject.UserDTO;

namespace ChatApp.Business.DataTransferObject.ChatDTO
{
    public class ChatWithLastMessageResponseDTO
    {
        public UserResponseDTO User { get; set; }
        public PrivateMessageResponseDTO LastMessage { get; set; }
    }
}
