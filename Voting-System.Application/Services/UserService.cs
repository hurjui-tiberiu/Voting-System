using AutoMapper;
using System.Security.Cryptography;
using System.Text;
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

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtUtils jwtUtils)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }

        public async Task CreateUserAsync(UserRequestDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            user.Role = Role.User;
            user.Password=Encryptor.EncryptPlainTextToCipherText(userDto.Password!);


            await userRepository.CreateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
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
            var user = await userRepository.GetUserByIdAsync(userId);
            return mapper.Map<UserRequestDto>(user);
        }

        public async Task UpdateUserAsync(Guid userId, UserPatchDto userPatch)
        {
            var user = await userRepository.GetUserByIdAsync(userId);

            if (user is not null)
            {
                var mappedUser = mapper.Map<UserPatchDto, User>(userPatch, user);

                await userRepository.UpdateUserAsync(mappedUser);
            }
        }

        public async Task<string?> AuthenticateUser(UserLoginDto userLoginDto)
        {
            var user = await userRepository.GetUserByEmailAsync(userLoginDto.Email!);

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

        public async Task Deauthenticate(Guid userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);

            if (user is not null)
            {
                user.Token = null;
                await userRepository.UpdateUserAsync(user);
            }
        }

    }
}
