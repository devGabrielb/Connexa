using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

namespace Connexa.Application.Commons.Interfaces
{
    public interface IGroupRepository
    {
        Task AddGroupAsync(Group group);

        Task<Group?> GetByIdAsync(Guid groupId);
        Task<List<Group>> GetGroupsByUserIdAsync(Guid userId);

        Task UpdateAsync(Group group);
    }
}