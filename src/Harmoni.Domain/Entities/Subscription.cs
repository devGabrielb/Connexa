using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Commons;
using Harmoni.Domain.Enums;

using ErrorOr;

namespace Harmoni.Domain.Entities
{
    public class Subscription : Entity
    {
        private readonly List<Group> _groups = [];

        public Guid UserId { get; private set; }
        public GroupPlan GroupPlan { get; private set; }

        public IReadOnlyList<Group> Groups => _groups.AsReadOnly();

        private Subscription() { }

        private Subscription(Guid userId, GroupPlan groupPlan)
        {
            UserId = userId;
            GroupPlan = groupPlan;
        }

        public static Subscription Create(Guid userId, GroupPlan groupPlan)
        {
            var subscription = new Subscription(userId, groupPlan);
            return subscription;
        }
        public ErrorOr<Success> AddGroup(Group group)
        {
            if (_groups.Count > GetMaxGroups)
            {
                return Error.Validation(
                    code: "Subscription.MaxGroupsReached",
                    description: "Max groups reached"
                );
            }

            if (_groups.Exists(g => g.Id == group.Id))
            {
                return Error.Validation(
                    code: "Subscription.GroupAlreadyExists",
                    description: "Group already exists in the subscription"
                );
            }

            _groups.Add(group);

            return Result.Success;
        }

        public int GetMaxGroups => GroupPlan switch
        {
            GroupPlan.Free => 2,
            GroupPlan.Starter => 10,
            GroupPlan.Premium => 50,
            _ => throw new InvalidOperationException("Invalid GroupPlan")
        };

        public int getMaxMembers => GroupPlan switch
        {
            GroupPlan.Free => 10,
            GroupPlan.Starter => 20,
            GroupPlan.Premium => 40,
            _ => throw new InvalidOperationException("Invalid GroupPlan")
        };

        public int GetMaxChores => GroupPlan switch
        {
            GroupPlan.Free => 10,
            GroupPlan.Starter => 20,
            GroupPlan.Premium => 40,
            _ => throw new InvalidOperationException("Invalid GroupPlan")
        };

    }
}