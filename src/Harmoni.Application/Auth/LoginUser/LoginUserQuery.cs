using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Auth.LoginUser
{
    public record LoginUserQuery(string Email, string Password) : IRequest<ErrorOr<AccessTokenResponse>>;

}