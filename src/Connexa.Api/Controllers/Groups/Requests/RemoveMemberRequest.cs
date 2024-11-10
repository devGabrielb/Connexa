using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connexa.Api.Controllers.Groups.Requests
{
    public record RemoveMemberRequest(Guid MemberId);
}