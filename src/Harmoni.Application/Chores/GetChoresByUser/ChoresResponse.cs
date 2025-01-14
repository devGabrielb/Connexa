using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Enums;

namespace Harmoni.Application.Chores.GetChoresByUser
{
    public record ChoresResponse(Guid Id, string Title, string Description, ChoreStatus Status, DateTime DueDate, string GroupName = "");

}

