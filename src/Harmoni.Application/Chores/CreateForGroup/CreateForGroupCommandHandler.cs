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

namespace Harmoni.Application.Chores.CreateForGroup
{
    public class CreateForGroupCommandHandler : IRequestHandler<CreateForGroupCommand, ErrorOr<Chore>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateForGroupCommandHandler(IGroupRepository groupsRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Chore>> Handle(CreateForGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);


            if (group is null)
            {
                return Error.NotFound("Group not found");
            }

            var chore = Chore.Create(request.Title, request.Description, request.DueDate);
            chore.AddToGroup(group.Id, request.AssignedBy);

            var addChoreResult = group.AddChore(chore);
            if (addChoreResult.IsError)
            {
                return addChoreResult.Errors;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return chore;

        }
    }
}