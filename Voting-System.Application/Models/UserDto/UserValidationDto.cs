using Voting_System.Domain.Enums;

namespace Voting_System.Application.Models.UserDto
{
    public class UserValidationDto
    {
        public string? Email { get; set; }
        public Guid Id { get; set; }
        public Role? Role { get; set; }
    }
}
