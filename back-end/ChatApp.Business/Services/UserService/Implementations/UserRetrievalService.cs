using AutoMapper;
using ChatApp.Business.DataTransferObject.UserDTO;
using ChatApp.Business.Exceptions;
using ChatApp.Business.Services.UserService.Interfaces;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Business.Services.UserService.Implementations
{
    public class UserRetrievalService : IUserRetrievalService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserRetrievalService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UsersWithPaginationResponseDTO> GetUsers(
            int pageNumber,
            int pageSize,
            string searchText = null)
        {
            if (searchText != null)
            {
                searchText = searchText.Trim().ToLower();
            }
            var result = await userRepository.GetUsers(pageNumber, pageSize, searchText);
            if (result == null)
            {
                throw new BadRequestException(PaginationExceptionMessages.EnteredPageNumberExceedPagesCount);
            }
            var response = new UsersWithPaginationResponseDTO
            {
                users = mapper.Map<IEnumerable<UserResponseDTO>>(result.Item1),
                numOfPages = result.Item2,
                currentPage = pageNumber
            };
            return response;
        }

        public async Task<UserResponseDTO> GetUserById(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            return mapper.Map<UserResponseDTO>(user);
        }
    }
}
