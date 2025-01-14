using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Commons;
using Harmoni.Domain.Entities;
using Harmoni.Domain.Enums;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Groups.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ErrorOr<Guid>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByUserIdAsync(request.OwnerId);

            if (subscription == null)
            {
                return Error.NotFound("Subscription not found");
            }

            var group = Group.Create(subscription.Id, request.Name, request.Description, request.GroupPicture, subscription.getMaxMembers, subscription.GetMaxChores);

            var memberGroup = MemberGroup.Create(request.OwnerId, group.Id, group.Name, true, MemberStatus.Active);

            var memberGroupResult = group.AddMember(memberGroup);

            if (memberGroupResult.IsError)
            {
                return memberGroupResult.Errors;
            }

            var groupResult = subscription.AddGroup(group);

            if (groupResult.IsError)
            {
                return groupResult.Errors;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return group.Id;
        }
    }
}