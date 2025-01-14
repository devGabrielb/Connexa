using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class MemberGroupRepository : IMemberGroupRepository
    {
        private readonly HarmoniContext _context;

        public MemberGroupRepository(HarmoniContext context)
        {
            _context = context;
        }
        public async Task<MemberGroup?> GetMemberByIdAsync(Guid memberId)
        {
            return await _context.MemberGroups.FirstOrDefaultAsync(x => x.Id == memberId);
        }

        public async Task<MemberGroup?> GetMemberByUserIdAsync(Guid userId)
        {
            return await _context.MemberGroups.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Guid>> GetGroupIdsByUserIdAsync(Guid userId)
        {
            var query = _context.MemberGroups.Where(mg => mg.UserId == userId && !mg.IsAdmin);

            return await query.Select(mg => mg.GroupId).ToListAsync();
        }

        public async Task<List<MemberGroup>> GetMembersByGroupIdAsync(Guid groupId)
        {
            var members = await _context.MemberGroups.Where(m => m.GroupId == groupId).ToListAsync();

            return members;
        }


        public async Task<List<Guid>> GetMembersIdByUserIdAsync(Guid userId)
        {
            var members = await _context.MemberGroups.Where(m => m.UserId == userId).Select(m => m.Id).ToListAsync();

            return members;
        }

        public async Task<MemberGroup?> GetMembersByUserIdAndGroupIdAsync(Guid userId, Guid groupId)
        {

            var member = await _context.MemberGroups.FirstOrDefaultAsync(m => m.UserId == userId && m.GroupId == groupId);

            return member;
        }
        public async Task AddMemberAsync(MemberGroup memberGroup)
        {
            await _context.MemberGroups.AddAsync(memberGroup);
        }
        public Task RemoveMemberAsync(MemberGroup memberGroup)
        {
            _context.MemberGroups.Remove(memberGroup);
            return Task.CompletedTask;
        }

    }
}