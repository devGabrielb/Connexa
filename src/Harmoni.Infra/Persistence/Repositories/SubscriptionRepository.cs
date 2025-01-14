using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly HarmoniContext _context;

        public SubscriptionRepository(HarmoniContext context)
        {
            _context = context;
        }

        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            return await _context.Subscriptions.Include(s => s.Groups).ThenInclude(s => s.Members).FirstOrDefaultAsync(s => s.Id == subscriptionId);
        }

        public async Task<Subscription?> GetByUserIdAsync(Guid userId)
        {
            var subscription = await _context.Subscriptions.Include(s => s.Groups).ThenInclude(s => s.Members).FirstOrDefaultAsync(s => s.UserId == userId);

            var entry = _context.Entry(subscription!);
            if (entry.State != EntityState.Unchanged)
            {
                Console.WriteLine("loguinho: " + entry.State);
            }

            return subscription;
        }

        public async Task<Subscription?> GetByGroupIdAsync(Guid groupId)
        {
            return await _context.Subscriptions.Include(s => s.Groups).ThenInclude(s => s.Members).FirstOrDefaultAsync(s => s.Groups.Any(g => g.Id == groupId));
        }
    }
}