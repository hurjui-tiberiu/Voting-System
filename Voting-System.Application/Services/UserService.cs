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

        public async Task<IActionResult> CreateUserAsync(UserRequestDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            return await userRepository.CreateUserAsync(user);
        }

        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
           return await userRepository.DeleteUserAsync(userId);
        }

        public async Task<ActionResult<List<UserRequestDto>>> GetAllUsersAsync()
        {
           var users =  await userRepository.GetAllUsersAsync();
           return mapper.Map<List<UserRequestDto>>(users);
        }

        public async Task<ActionResult> GetUserByIdAsync(Guid userId)
        {
            return await GetUserByIdAsync(userId);
        }

        public async Task<IActionResult> UpdateUserAsync(Guid userId, dynamic property)
        {
            var userPatch = JsonConvert.DeserializeObject<User>(property.ToString());
            return await userRepository.UpdateUserAsync(userPatch);
        }
    }
}
