using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Models.UserDto;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task CreateUserAsync(UserRequestDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            await userRepository.CreateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
           var user = await userRepository.GetUserByIdAsync(userId);
            if(user is not null)
                await userRepository.DeleteUserAsync(user);
        }

        public async Task<List<UserRequestDto>> GetAllUsersAsync()
        {
           var users =  await userRepository.GetAllUsersAsync();
           return mapper.Map<List<UserRequestDto>>(users);
        }

        public async Task<UserRequestDto> GetUserByIdAsync(Guid userId)
        {
           var user = await userRepository.GetUserByIdAsync(userId);
            return mapper.Map<UserRequestDto>(user);
        }

        public async Task UpdateUserAsync(Guid userId, dynamic property)
        {
            var userPatch = JsonConvert.DeserializeObject<UserPatchDto>(property.ToString());
            var user = await userRepository.GetUserByIdAsync(userId);

            var mappedUser = mapper.Map<UserPatchDto, User>(userPatch, user);

            await userRepository.UpdateUserAsync(mappedUser);
        }
    }
}
