using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Commons;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Groups.RemoveMember
{
    public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, ErrorOr<Success>>
    {
        private readonly IGroupRepository _groupsRepository;
        private readonly IMemberGroupRepository _membersGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMemberCommandHandler(IGroupRepository groupsRepository, IUnitOfWork unitOfWork, IMemberGroupRepository membersRepository)
        {
            _groupsRepository = groupsRepository;
            _unitOfWork = unitOfWork;
            _membersGroupRepository = membersRepository;
        }

        public async Task<ErrorOr<Success>> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupsRepository.GetByIdAsync(request.GroupId);
            if (group is null)
            {
                return Error.NotFound("Group not found");
            }

            var member = group.Members.FirstOrDefault(m => m.Id == request.MemberId);

            if (member is null)
            {
                return Error.NotFound("Member not found in the group");
            }

            var RemoveMemberResult = group.RemoveMember(request.MemberId);

            if (RemoveMemberResult.IsError)
            {
                return RemoveMemberResult.Errors;
            }
            await _membersGroupRepository.RemoveMemberAsync(member);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}