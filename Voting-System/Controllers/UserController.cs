using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Models.UserDto;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UserController(IUserService userService, ILogger logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

         [HttpGet, Route("/{id}")]
         public async Task<ActionResult<UserRequestDto>> GetUserByIdAsync(Guid userId)
         {
             return await userService.GetUserByIdAsync(userId);
         }

        [HttpGet]
        public async Task<ActionResult<List<UserRequestDto>>> GetAllUsersAsync()
        {
            return await userService.GetAllUsersAsync();
        }

        [HttpPut, Route("/create")]
        public async Task<IActionResult> CreateUserAsync(UserRequestDto userDto)
        {
            return await userService.CreateUserAsync(userDto);
        }

        [HttpDelete, Route("/delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            return await userService.DeleteUserAsync(id);
        }
        
        [HttpPatch, Route("/post/{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody]dynamic property)
        {
            return await userService.UpdateUserAsync(userId, property);
        }
    }
}
