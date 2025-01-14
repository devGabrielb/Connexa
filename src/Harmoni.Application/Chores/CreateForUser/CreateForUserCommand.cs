using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Chores.CreateForUser
{
    public record CreateForUserCommand(Guid userId, string Title, string Description, DateTime DueDate) : IRequest<ErrorOr<Chore>>;
}