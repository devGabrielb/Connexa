using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoni.Api.Controllers.Groups.Requests
{
    public record CreateGroupRequest(Guid OwnerId, string Name, string Description, string GroupPicture);
}