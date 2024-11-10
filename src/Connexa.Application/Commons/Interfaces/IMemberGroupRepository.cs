using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

namespace Connexa.Application.Commons.Interfaces
{
    public interface IMemberGroupRepository
    {
        Task AddMemberAsync(MemberGroup memberGroup);
        Task RemoveMemberAsync(MemberGroup memberGroup);
    }
}