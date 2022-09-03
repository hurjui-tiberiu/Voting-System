using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<IActionResult> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<User>> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
