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

namespace Harmoni.Application.GroupMembers.InviteMember
{
    public class InviteMemberCommandHandler : IRequestHandler<InviteMemberCommand, ErrorOr<Success>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InviteMemberCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Success>> Handle(InviteMemberCommand request, CancellationToken cancellationToken)
        {

            var group = await _groupRepository.GetByIdAsync(request.GroupId);

            if (group is null)
            {
                return Error.NotFound("Group not found");
            }

            var member = MemberGroup.Create(request.UserId, group.Id, group.Name, false);

            var addMemberResult = group.AddMember(member);

            if (addMemberResult.IsError)
            {
                return addMemberResult.Errors;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}