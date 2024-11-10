using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Connexa.Infra.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ConnexaContext _context;

        public GroupRepository(ConnexaContext context)
        {
            _context = context;
        }

        public async Task AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task<Group?> GetByIdAsync(Guid groupId)
        {
            return await _context.Groups.Include(m => m.Members).FirstOrDefaultAsync(c => c.Id == groupId);
        }

        public async Task<List<Group>> GetGroupsByUserIdAsync(Guid userId)
        {
            var groups = await _context.Groups.Where(g => g.OwnerId == userId).ToListAsync();
            return groups;
        }

        public Task UpdateAsync(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            _context.Groups.Update(group);

            return Task.CompletedTask;
        }
    }
}