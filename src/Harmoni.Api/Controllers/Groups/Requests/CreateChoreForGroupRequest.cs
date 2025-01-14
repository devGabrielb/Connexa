using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoni.Api.Controllers.Groups.Requests
{
    public record CreateChoreForGroupRequest(Guid OwnerId, string Title, string Description, DateTime DueDate);
}