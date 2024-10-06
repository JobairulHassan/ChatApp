using AutoMapper;
using ChatApp.Business.DataTransferObject.UserDTO;
using ChatApp.Business.Exceptions;
using ChatApp.Business.Services.UserService.Interfaces;
using ChatApp.Business.Util;
using ChatApp.Persistence.Entities;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Business.Services.UserService.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAuthenticatedUserService authenticatedUserService;

        public UserAccountService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.authenticatedUserService = authenticatedUserService;
        }


        public async Task<UserResponseDTO> RegisterUserAsync(UserRequestDTO userRequestDTO)
        {
            if (userRepository.CheckIfEmailExists(userRequestDTO.Email))
            {
                throw new NotFoundException(UserExceptionMessages.EmailAlreadyExsist);
            }
            //PasswordHashing.HashPassword(userRequestDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                FirstName = userRequestDTO.FirstName,
                LastName = userRequestDTO.LastName,
                Email = userRequestDTO.Email,
            };
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserResponseDTO>(user);
        }

        public async Task<string> LoginUserAsync(LoginDTO userRequestDTO)
        {
            
            var user = await userRepository.GetUserByEmail(userRequestDTO.Email); //.ToLower()
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserByEmail);
            }
            // if (!PasswordHashing.VerifyPassword(userRequestDTO.Password, user.PasswordHash, user.PasswordSalt))
            // {
            //     throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
            // }
            var token = TokenGenerator.Generate(user);
            return token;
        }

        public async Task<UserResponseDTO> GetUserByJwtTokenAsync()
        {
            var userId = authenticatedUserService.GetAuthenticatedUserId();
            var user = await userRepository.GetUserById(userId);
            return mapper.Map<UserResponseDTO>(user);
        }

        // public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDTO changePasswordDTO)
        // {
        //     var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
        //     if (authenticatedUserId != userId)
        //     {
        //         throw new UnauthorizedException();
        //     }
        //     var user = await userRepository.GetUserById(userId);
        //     if (user == null)
        //     {
        //         throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
        //     }
        //     if (!PasswordHashing.VerifyPassword(changePasswordDTO.OldPassword, user.PasswordHash, user.PasswordSalt))
        //     {
        //         throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
        //     }
        //     PasswordHashing.HashPassword(changePasswordDTO.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        //     user.PasswordHash = passwordHash;
        //     user.PasswordSalt = passwordSalt;
        //     await unitOfWork.SaveChangesAsync();
        // }

        public async Task ChangeUserAboutAsync(int userId, string newAbout)
        {
            var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            user.About = newAbout;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ChangeThemeModeAsync(int userId, bool isDarkMode)
        {
            var authenticatedUserId = authenticatedUserService.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            user.IsDarkTheme = isDarkMode;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
