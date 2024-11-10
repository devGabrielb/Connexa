using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connexa.Application.Auth.LoginUser
{
    public record AccessTokenResponse(string Token, string RefreshToken);
}