using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IGroupRepository
    {
        Task AddGroupAsync(Group group);
        Task<int> GetCountByUserIdAsync(Guid userId);
        Task<List<Group>> ListBySubscriptionIdAsync(Guid subscriptionId);
        Task<bool> ExistsAsync(Guid Id);
        Task<Group?> GetByIdAsync(Guid groupId);
        Task UpdateAsync(Group group);
    }
}