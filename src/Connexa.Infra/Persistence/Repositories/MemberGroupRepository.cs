using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Entities;

namespace Connexa.Infra.Persistence.Repositories
{
    public class MemberGroupRepository : IMemberGroupRepository
    {
        private readonly ConnexaContext _context;

        public MemberGroupRepository(ConnexaContext context)
        {
            _context = context;
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