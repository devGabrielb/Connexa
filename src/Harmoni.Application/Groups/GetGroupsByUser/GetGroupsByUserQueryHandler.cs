using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Commons;
using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Groups.GetGroupsByUser
{
    public class GetGroupsByUserQueryHandler : IRequestHandler<GetGroupsByUserQuery, ErrorOr<List<Group>>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetGroupsByUserQueryHandler(IGroupRepository groupRepository, ISubscriptionRepository subscriptionRepository)
        {
            _groupRepository = groupRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ErrorOr<List<Group>>> Handle(GetGroupsByUserQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByUserIdAsync(request.SubscriptionId);

            if (subscription is null)
            {
                return Error.NotFound("Subscription not found");
            }

            var groups = await _groupRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
            return groups;
        }
    }
}