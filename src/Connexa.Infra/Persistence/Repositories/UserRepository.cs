using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Connexa.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnexaContext _context;

        public UserRepository(ConnexaContext dbContext)
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

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            return Task.CompletedTask;
        }
    }

}