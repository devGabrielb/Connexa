using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly HarmoniContext _context;

        public GroupRepository(HarmoniContext context)
        {
            _context = context;
        }

        public async Task<int> GetCountByUserIdAsync(Guid userId)
        {
            return await _context.MemberGroups.Where(m => m.UserId == userId).Select(m => m.GroupId).Distinct().CountAsync();
        }

        public async Task<List<Chore>> ListChoresByUserIdAsync(Guid userId)
        {
            var groupChores = await _context.Groups
                .Where(group => group.Members.Any(member => member.UserId == userId))
                .SelectMany(group => group.Chores.Where(chore =>
                    chore.AssignedTo.HasValue &&
                    group.Members.Where(member => member.UserId == userId)
                                 .Select(member => member.Id)
                                 .Contains(chore.AssignedTo.Value)))
                .ToListAsync();

            return groupChores;
        }

        public async Task<List<Group>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _context.Groups.Where(g => g.SubscriptionId == subscriptionId).ToListAsync();
        }

        public async Task AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task<bool> ExistsAsync(Guid Id)
        {
            return await _context.Groups.AsNoTracking().AnyAsync(g => g.Id == Id);
        }

        public async Task<Group?> GetByIdAsync(Guid groupId)
        {
            return await _context.Groups.Include(g => g.Chores).Include(g => g.Members).FirstOrDefaultAsync(c => c.Id == groupId);
        }

        public Task UpdateAsync(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            _context.Groups.Update(group);

            return Task.CompletedTask;
        }
    }
}