using System;
using System.Collections.Generic;
using System.Linq;

using Connexa.Domain.Commons;
using Connexa.Domain.Events;

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

        public void InviteMember(Guid userId)
        {
            var member = MemberGroup.Create(userId, Id);
            _members.Add(member);

            RaiseDomainEvent(new MemberInvitedEvent(Id, member.GroupId));

        }

        public void RemoveMember(Guid userId)
        {
            var member = _members.Find(x => x.UserId == userId);
            if (member == null)
            {
                throw new InvalidOperationException("User is not a member of this group");
            }
            _members.Remove(member);
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