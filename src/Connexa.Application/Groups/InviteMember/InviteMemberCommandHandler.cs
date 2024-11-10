using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Commons;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Groups.InviteMember
{
    public class InviteMemberCommandHandler : IRequestHandler<InviteMemberCommand, ErrorOr<Success>>
    {
        private readonly IGroupRepository _groupsRepository;
        private readonly IMemberGroupRepository _membersGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InviteMemberCommandHandler(IGroupRepository groupsRepository, IUnitOfWork unitOfWork, IMemberGroupRepository membersRepository)
        {
            _groupsRepository = groupsRepository;
            _unitOfWork = unitOfWork;
            _membersGroupRepository = membersRepository;
        }
        public async Task<ErrorOr<Success>> Handle(InviteMemberCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupsRepository.GetByIdAsync(request.GroupId);
            if (group is null)
            {
                return Error.NotFound("Group not found");
            }
            var inviteMemberResult = group.InviteMember(request.MemberId);
            if (inviteMemberResult.IsError)
            {
                return inviteMemberResult.Errors;
            }
            await _membersGroupRepository.AddMemberAsync(inviteMemberResult.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}