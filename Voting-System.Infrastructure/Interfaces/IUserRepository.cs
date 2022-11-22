using Voting_System.Domain.Entities;

namespace Voting_System.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string mail);
        Task<User?> GetUserByPersonalIdAsync(string personalId);
    }
}
