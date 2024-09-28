using AutoMapper;
using ChatApp.Business.DataTransferObject.ChatDTO;
using ChatApp.Business.DataTransferObject.MessageDTO;
using ChatApp.Business.Exceptions;
using ChatApp.Business.Services.PrivateMessageServices.Interfaces;
using ChatApp.Business.Services.UserService.Interfaces;
using ChatApp.Persistence.Entities;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Business.Services.PrivateMessageServices.Implementations
{
    public class PrivateMessageService : IPrivateMessageService
    {
        private readonly IPrivateMessageRepository privateMessageRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthenticatedUserService authenticatedUserService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PrivateMessageService(
            IPrivateMessageRepository privateMessageRepository,
            IUnitOfWork unitOfWork,
            IAuthenticatedUserService authenticatedUserService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.privateMessageRepository = privateMessageRepository;
            this.unitOfWork = unitOfWork;
            this.authenticatedUserService = authenticatedUserService;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<PrivateMessageResponseDTO> StorePrivateMessage(int destinationUserId, string textMessage)
        {
            var sourceUserId = authenticatedUserService.GetAuthenticatedUserId();
            var destinationUser = await userRepository.GetUserById(destinationUserId);
            if (destinationUser == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            var message = new PrivateMessage()
            {
                ReceiverId = destinationUserId,
                SenderId = sourceUserId,
                CreationDate = DateTime.Now,
                TextBody = textMessage
            };
            await privateMessageRepository.AddAsync(message);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<PrivateMessageResponseDTO>(message);
        }

        public async Task<PrivateMessagesWithPaginationResponseDTO> GetPrivateMessages(
            DateTime? pageDate,
            int pageSize,
            int firstUserId,
            int secoundUserId)
        {
            var firstUser = await userRepository.GetUserById(firstUserId);
            var secoundUser = await userRepository.GetUserById(secoundUserId);
            if (firstUser == null || secoundUser == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
            if (authenticatedUserId != firstUserId)
            {
                throw new UnauthorizedException();
            }
            await privateMessageRepository.GetRecentChatsForUser(authenticatedUserId);
            if (pageDate == null)
            {
                pageDate = DateTime.Now;
            }
            var queryResult = await privateMessageRepository.GetPrivateMessagesForPrivateChat((DateTime)pageDate, pageSize, firstUserId, secoundUserId);
            var result = new PrivateMessagesWithPaginationResponseDTO
            {
                Messages = mapper.Map<IEnumerable<PrivateMessageResponseDTO>>(queryResult.Item1),
                IsThereMore = queryResult.Item2
            };
            return result;
        }

        public async Task<IEnumerable<ChatWithLastMessageResponseDTO>> GetRecentChatsForUser(int userId)
        {
            var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var queryResult = await privateMessageRepository.GetRecentChatsForUser(userId);

            return mapper.Map<IEnumerable<ChatWithLastMessageResponseDTO>>(queryResult);
        }
    }
}
