using Voting_System.Application.Models.UserDto;

namespace Voting_System.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserRequestDto user);
        Task DeleteUserAsync(Guid userId);
        Task<UserRequestDto> GetUserByIdAsync(Guid userId);
        Task<List<UserRequestDto>> GetAllUsersAsync();
        Task<bool>UpdateUserAsync(Guid userId, UserPatchDto userPatch);
        Task<string?> AuthenticateUser(UserLoginDto userLoginDto);
        Task Deauthenticate(Guid userId);
    }
}
