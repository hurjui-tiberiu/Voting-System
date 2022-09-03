using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Application.Interfaces;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> CreateUserAsync(User user)
        {
            return await userRepository.CreateUserAsync(user);
        }

        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
           return await userRepository.DeleteUserAsync(userId);
        }

        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsersAsync();
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
