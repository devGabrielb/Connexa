using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Commons;
using Connexa.Domain.Enums;

namespace Connexa.Domain.Entities
{
    public class MemberGroup : Entity
    {
        public Guid UserId { get; private set; }
        public Guid GroupId { get; private set; }
        public MemberStatus Status { get; private set; }

        private MemberGroup(Guid userId, Guid groupId, MemberStatus status)
        {
            UserId = userId;
            GroupId = groupId;
            Status = status;
        }

        public static MemberGroup Create(Guid userId, Guid groupId, MemberStatus status)
        {

            var memberGroup = new MemberGroup(userId, groupId, status);
            return memberGroup;
        }
    }
}