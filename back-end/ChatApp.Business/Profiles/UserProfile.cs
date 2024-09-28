using AutoMapper;
using ChatApp.Business.DataTransferObject.UserDTO;
using ChatApp.Persistence.Entities;

namespace BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>();
        }
    }
}
