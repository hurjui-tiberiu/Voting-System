using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Voting_System.Domain.Entities;

namespace Voting_System.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task<User?> GetUserByPropertyAsync(Expression<Func<User, bool>>func);
    }
}
