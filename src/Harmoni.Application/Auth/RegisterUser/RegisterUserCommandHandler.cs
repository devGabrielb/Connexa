using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Commons;
using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Auth.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<Guid>>
    {
        private readonly IUserRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository usersRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.Name, request.Email, _passwordHasher.HashPassword(request.Password));

            await _usersRepository.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}