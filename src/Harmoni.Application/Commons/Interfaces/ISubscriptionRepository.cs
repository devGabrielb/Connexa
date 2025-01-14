using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByUserIdAsync(Guid userId);
        Task<Subscription?> GetByGroupIdAsync(Guid groupId);
        Task<Subscription?> GetByIdAsync(Guid subscriptionId);
    }
}