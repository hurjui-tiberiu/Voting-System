using Microsoft.AspNetCore.Http;
using Voting_System.Application.Interfaces;

namespace Voting_System.Application.JWTUtil
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userValidationDto = jwtUtils.ValidateToken(token!);
            if (userValidationDto != null)
            {
                context.Items["User"] = await userService.GetUserByIdAsync(userValidationDto.Id);
            }

            await _next(context);
        }
    }
}
