using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Application.Models.UserDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Application.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> CreateUserAsync(UserRequestDto user);
        Task<IActionResult> DeleteUserAsync(Guid userId);
        Task<ActionResult> GetUserByIdAsync(Guid userId);
        Task<ActionResult<List<UserRequestDto>>> GetAllUsersAsync();
        Task<IActionResult> UpdateUserAsync(Guid userId, dynamic property);
    }
}
