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
        Task CreateUserAsync(UserRequestDto user);
        Task DeleteUserAsync(Guid userId);
        Task<UserRequestDto> GetUserByIdAsync(Guid userId);
        Task<List<UserRequestDto>> GetAllUsersAsync();
        Task UpdateUserAsync(Guid userId, dynamic property);
    }
}
