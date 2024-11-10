using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Commons;
using Connexa.Domain.Entities;
using Connexa.Domain.Enums;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Groups.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ErrorOr<Guid>>
    {
        private readonly IGroupRepository _groupsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupCommandHandler(IGroupRepository groupsRepository, IUnitOfWork unitOfWork)
        {
            _groupsRepository = groupsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = Group.Create(request.OwnerId, request.Name, request.Description);

            var inviteAdminResult = group.InviteMember(request.OwnerId, MemberStatus.Active);

            if (inviteAdminResult.IsError)
            {
                return inviteAdminResult.Errors;
            }

            await _groupsRepository.AddGroupAsync(group);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return group.Id;
        }
    }
}