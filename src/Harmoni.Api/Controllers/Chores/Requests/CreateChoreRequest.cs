using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoni.Api.Controllers.Chores.Requests
{
    public record CreateChoreRequest(Guid OwnerId, string Title, string Description, DateTime DueDate);
}