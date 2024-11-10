using System;
using System.Collections.Generic;
using System.Linq;

using Connexa.Domain.Commons;
using Connexa.Domain.Enums;
using Connexa.Domain.Events;

using ErrorOr;

namespace Connexa.Domain.Entities
{
    public class Group : Entity
    {
        private readonly List<MemberGroup> _members = [];
        private readonly List<Chore> _chores = [];

        public Guid OwnerId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyCollection<MemberGroup> Members => [.. _members];
        public IReadOnlyCollection<Chore> Chores => [.. _chores];

        private Group(Guid ownerId, string name, string description)
        {
            OwnerId = ownerId;
            Name = name;
            Description = description;
        }

        public static Group Create(Guid ownerId, string name, string description)
        {
            var group = new Group(ownerId, name, description);
            return group;
        }

        public ErrorOr<MemberGroup> InviteMember(Guid userId, MemberStatus status = MemberStatus.Pending)
        {
            if (_members.Exists(m => m.Id == userId))
            {
                return Error.Validation("Member already exists");
            }

            var member = MemberGroup.Create(userId, Id, status);
            _members.Add(member);

            RaiseDomainEvent(new MemberInvitedEvent(Id, member.GroupId));
            return member;
        }

        public ErrorOr<Success> RemoveMember(Guid userId)
        {
            var member = _members.Find(x => x.UserId == userId);
            if (member == null)
            {
                return Error.NotFound("Member not found");
            }
            _members.Remove(member);

            return Result.Success;
        }

        public void AddChore(Chore chore)
        {
            ArgumentNullException.ThrowIfNull(chore);
            if (!_members.Exists(x => x.UserId == chore.OwnerId))
            {
                throw new InvalidOperationException("User is not a member of this group");
            }
            var choreResponse = Chore.Create(chore.OwnerId, chore.Title, chore.Description, chore.DueDate);
            _chores.Add(choreResponse);
        }

    }
}