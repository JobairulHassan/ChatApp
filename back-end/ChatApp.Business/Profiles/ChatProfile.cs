using AutoMapper;
using ChatApp.Business.DataTransferObject.ChatDTO;
using ChatApp.Persistence.Retreve;

namespace ChatApp.Business.Profiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatWithLastMessage, ChatWithLastMessageResponseDTO>();
        }
    }
}
