using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Interfaces;
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
         public async Task<ActionResult<User>> GetUserByIdAsync(Guid userId)
         {
             return await userService.GetUserByIdAsync(userId);
         }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            return await userService.GetAllUsersAsync();
        }

        [HttpPut, Route("/create")]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            return await userService.CreateUserAsync(user);
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
