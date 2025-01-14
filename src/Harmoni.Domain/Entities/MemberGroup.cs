using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Commons;
using Harmoni.Domain.Enums;

using ErrorOr;

namespace Harmoni.Domain.Entities
{
    public class MemberGroup : Entity
    {
        public Guid UserId { get; private set; }
        public Guid GroupId { get; private set; }
        public string GroupName { get; private set; }
        public MemberStatus Status { get; private set; }
        public bool IsAdmin { get; private set; }

        private MemberGroup() { }
        private MemberGroup(Guid userId, Guid groupId, string groupName, MemberStatus status, bool isAdmin)
        {
            UserId = userId;
            GroupId = groupId;
            Status = status;
            IsAdmin = isAdmin;
            GroupName = groupName;
        }

        public static MemberGroup Create(Guid userId, Guid groupId, string groupName, bool isAdmin, MemberStatus status = MemberStatus.Pending)
        {

            var memberGroup = new MemberGroup(userId, groupId, groupName, status, isAdmin);
            return memberGroup;
        }
    }
}