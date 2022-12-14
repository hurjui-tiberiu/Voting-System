using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Voting_System.Application.Interfaces;
using Voting_System.Application.JWTUtil;
using Voting_System.Application.Models.MailDto;
using Voting_System.Application.Models.UserDto;

namespace Voting_System.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMailService mailService;
        private readonly ILogger logger;
        private readonly IValidator<UserRequestDto> userRequestDtoValidator;
        private readonly IValidator<UserPatchDto> userPatchValidator;

        public UserController(IUserService userService, ILogger<UserController> logger,
         IMailService mailService, IValidator<UserRequestDto> validator, IValidator<UserPatchDto> userPatchValidator)
        {
            this.userService = userService;
            this.logger = logger;
            this.mailService = mailService;
            this.userRequestDtoValidator = validator;
            this.userPatchValidator = userPatchValidator;
        }

        [SwaggerOperation(Summary = "Authenticate | Auth:Anonymous")]
        [HttpPost, Route("authenticate"), AllowAnonymous]
        public async Task<ActionResult<string>> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var token = await userService.AuthenticateUser(userLoginDto);
                if (token is null)
                    return Unauthorized();

                logger.LogInformation("User authenticated succesfully");

                return Ok(token);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Log out | Auth:User")]
        [HttpPost, Route("deauthenticate"), AuthorizeMultiplePolicy(Policies.Admin + ";" + Policies.User, false)]
        public async Task<IActionResult> Deauthenticate(Guid userId)
        {
            try
            {
                await userService.Deauthenticate(userId);
                logger.LogInformation("User deauthenticated succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get user by ID | Auth:Admin")]
        [HttpGet, Route("{userId}"), AuthorizeMultiplePolicy(Policies.Admin + ";" + Policies.User, false)]
        public async Task<ActionResult<UserRequestDto>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(userId);
                logger.LogInformation("User retrived");

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get all users | Auth:Admin")]
        [HttpGet, Route("users"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<ActionResult<List<UserRequestDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                logger.LogInformation("Users retrived: {count}", users.Count);

                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Create account | Auth:Anonymous")]
        [HttpPut, Route("create"), AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync(UserRequestDto userDto)
        {
            try
            {
                var result = await userRequestDtoValidator.ValidateAsync(userDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.ToString());
                }

                if (await userService.CreateUserAsync(userDto) is false)
                {
                    logger.LogInformation("User already exists.");
                    return BadRequest();
                }

                await userService.CreateUserAsync(userDto);
                var mailRequest = new MailRequest
                {
                    ToEmail = userDto.Mail,
                    Subject = "Your accout has been created!",
                    Body = $"Your accout has been created! <br/> Password: {userDto.Password}"
                };

                await mailService.SendEmailAsync(mailRequest);

                logger.LogInformation("User created succesfully.");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Delete user | Auth:Admin")]
        [HttpDelete, Route("{id}"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                await userService.DeleteUserAsync(id);
                logger.LogInformation("User deleted succesfully.");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Edit user | Auth:User")]
        [HttpPatch, Route("post"), AuthorizeMultiplePolicy(Policies.User + ";" + Policies.Admin, false)]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] dynamic property)
        {
            try
            {
                var userPatch = JsonConvert.DeserializeObject<UserPatchDto>(property.ToString());

                var validationResult = await userPatchValidator.ValidateAsync(userPatch);

                if (validationResult.IsValid is false)
                {
                    return BadRequest(validationResult.ToString());
                }

                var result = await userService.UpdateUserAsync(userId, userPatch);

                if (result is false)
                {
                    logger.LogInformation("User not updated.");
                    return BadRequest();
                }

                logger.LogInformation("User updated succesfully.");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}