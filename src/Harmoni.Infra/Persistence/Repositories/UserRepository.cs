using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HarmoniContext _context;

        public UserRepository(HarmoniContext dbContext)
        {
            _context = dbContext;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            return await _context.Users
                        .Where(user => userIds.Contains(user.Id))
                        .ToListAsync();
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            return Task.CompletedTask;
        }
    }

}