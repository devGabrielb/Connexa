using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IMemberGroupRepository
    {
        Task AddMemberAsync(MemberGroup memberGroup);
        Task RemoveMemberAsync(MemberGroup memberGroup);
        Task<MemberGroup?> GetMemberByIdAsync(Guid memberId);
        Task<MemberGroup?> GetMemberByUserIdAsync(Guid userId);
        Task<IEnumerable<Guid>> GetGroupIdsByUserIdAsync(Guid userId);
        Task<List<MemberGroup>> GetMembersByGroupIdAsync(Guid groupId);
        Task<MemberGroup?> GetMembersByUserIdAndGroupIdAsync(Guid userId, Guid groupId);
        Task<List<Guid>> GetMembersIdByUserIdAsync(Guid userId);
    }
}