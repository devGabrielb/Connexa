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

namespace Harmoni.Application.Chores.CreateForUser
{
    public class CreateForUserCommandHandler : IRequestHandler<CreateForUserCommand, ErrorOr<Chore>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChoreRepository _choreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateForUserCommandHandler(IUserRepository userRepository, IChoreRepository choreRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _choreRepository = choreRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Chore>> Handle(CreateForUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);

            if (user is null)
            {
                return Error.NotFound("user not found");
            }

            var chore = Chore.Create(request.Title, request.Description, request.DueDate);
            chore.AddToUser(user.Id);

            await _choreRepository.AddAsync(chore);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return chore;

        }
    }
}