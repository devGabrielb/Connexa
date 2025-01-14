using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);

        Task<bool> ExistsByEmailAsync(string email);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(Guid userId);
        Task<List<User>> GetUsersByIdsAsync(List<Guid> userIds);

        Task UpdateAsync(User user);

    }
}