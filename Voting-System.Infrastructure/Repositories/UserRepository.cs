using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await context.Users.FirstOrDefaultAsync(entity => entity.Id.Equals(userId));
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string mail)
        {
            var user = await context.Users.FirstOrDefaultAsync(entity => entity.Mail!.Equals(mail));

            return user;
        }
    }
}
