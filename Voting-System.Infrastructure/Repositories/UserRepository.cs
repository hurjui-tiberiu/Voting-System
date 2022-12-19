using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Contexts;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EFContext context;

        public UserRepository(EFContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            context.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByPropertyAsync(Expression<Func<User, bool>> func)
        {
            var user = await context.Users.FirstOrDefaultAsync(func);

            return user;
        }
    }
}
