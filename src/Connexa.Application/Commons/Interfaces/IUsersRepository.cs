using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

namespace Connexa.Application.Commons.Interfaces
{
    public interface IUsersRepository
    {
        Task AddUserAsync(User user);

        Task<bool> ExistsByEmailAsync(string email);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(Guid userId);

        Task UpdateAsync(User user);

    }
}