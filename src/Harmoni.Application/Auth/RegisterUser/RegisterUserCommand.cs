using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Auth.RegisterUser
{
    public record RegisterUserCommand(string Name, string Email, string Password) : IRequest<ErrorOr<Guid>>;
}