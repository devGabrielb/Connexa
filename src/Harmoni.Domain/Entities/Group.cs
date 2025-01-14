using System;
using System.Collections.Generic;
using System.Linq;

using Harmoni.Domain.Commons;
using Harmoni.Domain.Entities.VOs;
using Harmoni.Domain.Enums;
using Harmoni.Domain.Events;

using ErrorOr;

namespace Harmoni.Domain.Entities
{
    public class Group : Entity
    {
        private readonly List<MemberGroup> _members = [];
        private readonly List<Chore> _chores = [];
        private readonly int _maxMembers;
        private readonly int _maxChores;

        public Guid SubscriptionId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string GroupPicture { get; private set; }
        public GroupPlan GroupPlan { get; private set; }
        public IReadOnlyList<MemberGroup> Members => _members.AsReadOnly();
        public IReadOnlyList<Chore> Chores => _chores.AsReadOnly();

        private Group() { }
        private Group(Guid subscriptionId, string name, string description, string groupPicture, int maxMembers, int maxChores)
        {
            SubscriptionId = subscriptionId;
            Name = name;
            Description = description;
            GroupPicture = groupPicture;
            _maxMembers = maxMembers;
            _maxChores = maxChores;
        }

        public static Group Create(Guid subscriptionId, string name, string description, string groupPicture, int maxMembers, int maxChores)
        {
            var group = new Group(subscriptionId, name, description, groupPicture, maxMembers, maxChores);
            return group;
        }

        public ErrorOr<Success> AddChore(Chore chore)
        {
            if (Chores.Count >= _maxChores)
            {
                return Error.Validation(
                    code: "Group.MaxChoresReached",
                    description: "Max chores reached"
                );
            }

            if (_chores.Exists(c => c.Id == chore.Id))
            {
                return Error.Validation(
                    code: "Group.ChoreAlreadyExists",
                    description: "Chore already exists in the group"
                );
            }

            _chores.Add(chore);

            return Result.Success;
        }

        public ErrorOr<Success> AddMember(MemberGroup memberGroup)
        {

            if (_members.Count > _maxMembers)
            {
                return Error.Validation(
                    code: "Group.MaxMembersReached",
                    description: "Max members reached"
                );
            }

            if (_members.Exists(m => m.UserId == memberGroup.UserId))
            {
                return Error.Validation(
                    code: "Group.MemberAlreadyExists",
                    description: "Member already exists in the group"
                );
            }
            _members.Add(memberGroup);

            return Result.Success;
        }

        public ErrorOr<Success> RemoveMember(MemberGroup memberGroup)
        {

            if (!_members.Exists(m => m.UserId == memberGroup.UserId))
            {
                return Error.Validation(
                                    code: "Group.MemberDoesNotExist",
                                    description: "Member does not exist in the group"
                                );
            }

            _members.Remove(memberGroup);

            return Result.Success;
        }
    }
}