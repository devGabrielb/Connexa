using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Commons;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.GroupMembers.RemoveMember
{
    public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, ErrorOr<Success>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMemberCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);

            if (group is null)
            {
                return Error.NotFound("Group not found");
            }

            var member = group.Members.FirstOrDefault(m => m.Id == request.MemberId);

            if (member is null)
            {
                return Error.NotFound("Member not found in the group");
            }

            var removeMemberResult = group.RemoveMember(member);

            if (removeMemberResult.IsError)
            {
                return removeMemberResult.Errors;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}