using AutoMapper;
using ChatApp.Business.DataTransferObject.MessageDTO;
using ChatApp.Persistence.Entities;

namespace BusinessLayer.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile() { CreateMap<PrivateMessage, PrivateMessageResponseDTO>(); }
    }
}
