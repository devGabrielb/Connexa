using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoni.Api.Controllers.Auth
{
    public record LoginRequest(string Email, string Password);
}