using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Models.UserDto;

namespace Voting_System.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet, Route("/{userId}")]
        public async Task<ActionResult<UserRequestDto>> GetUserByIdAsync(Guid userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            logger.LogInformation("User retrived");

            return Ok(user);
        }

        [HttpGet, Route("/users")]
        public async Task<ActionResult<List<UserRequestDto>>> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            logger.LogInformation("Users retrived: {count}", users.Count);

            return Ok(users);
        }

        [HttpPut, Route("/create")]
        public async Task CreateUserAsync(UserRequestDto userDto)
        {
            await userService.CreateUserAsync(userDto);

            logger.LogInformation("User created succesfully.");
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await userService.DeleteUserAsync(id);
            logger.LogInformation("User deleted succesfully.");

            return Ok();
        }

        [HttpPatch, Route("/post")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] dynamic property)
        {

            await userService.UpdateUserAsync(userId, property);
            logger.LogInformation("User updated succesfully.");

            return Ok();
        }
    }
}
