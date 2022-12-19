using AutoMapper;
using FluentValidation;
using Voting_System.Application.Encryption;
using Voting_System.Application.Interfaces;
using Voting_System.Application.JWTUtil;
using Voting_System.Application.Models.UserDto;
using Voting_System.Domain.Entities;
using Voting_System.Domain.Enums;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IJwtUtils jwtUtils;
        private readonly IValidator<User> userValidator;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtUtils jwtUtils, IValidator<User> userValidator)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
            this.userValidator = userValidator;
        }

        public async Task<bool> CreateUserAsync(UserRequestDto userDto)
        {
            var userByEmail = await userRepository.GetUserByPropertyAsync(entity => entity.Mail!.Equals(userDto.Mail));

            if (userByEmail is not null)
                return false;

            var userByPersonalId = await userRepository.GetUserByPropertyAsync(entity => entity.IdentityCardId!.Equals(userDto.IdentityCardId!));
            if (userByPersonalId is not null)
                return false;

            var user = mapper.Map<User>(userDto);
            user.Role = Role.Admin;
            user.Password = Encryptor.EncryptPlainTextToCipherText(userDto.Password!);


            var validationResult = userValidator.Validate(user);

            if (!validationResult.IsValid)
                return false;

            await userRepository.CreateUserAsync(user);

            return true;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await userRepository.GetUserByPropertyAsync(entity => entity.Id.Equals(userId));
            if (user is not null)
                await userRepository.DeleteUserAsync(user);
        }

        public async Task<List<UserRequestDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllUsersAsync();
            return mapper.Map<List<UserRequestDto>>(users);
        }

        public async Task<UserRequestDto> GetUserByIdAsync(Guid userId)
        {
            var user = await userRepository.GetUserByPropertyAsync(entity => entity.Id == userId);
            return mapper.Map<UserRequestDto>(user);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserPatchDto userPatch)
        {
            var user = await userRepository.GetUserByPropertyAsync(entity => entity.Id == userId);

            if (user is null)
                return false;

            var mappedUser = mapper.Map<UserPatchDto, User>(userPatch, user);

            var validationResult = await userValidator.ValidateAsync(mappedUser);

            if (validationResult.IsValid is false)
                return false;

            await userRepository.UpdateUserAsync(mappedUser);

            return true;
        }

        

        public async Task Deauthenticate(Guid userId)
        {
            var user = await userRepository.GetUserByPropertyAsync(entity => entity.Id == userId);

            if (user is not null)
            {
                user.Token = null;
                await userRepository.UpdateUserAsync(user);
            }
        }

        public async Task<string?> AuthenticateUser(UserLoginDto userLoginDto)
        {
            var user = await userRepository.GetUserByPropertyAsync(entity => entity.Mail!.Equals(userLoginDto.Email!));

            if (user is null)
                return null;

            if (userLoginDto.Password!.Equals(Encryptor.DecryptCipherTextToPlainText(user.Password!)))
            {
                user.Token = jwtUtils.GenerateToken(user);
                await userRepository.UpdateUserAsync(user);

                return user.Token;
            }

            return null;
        }
    }
}
