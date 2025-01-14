using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Chores.CreateForGroup
{
    public record CreateForGroupCommand(Guid AssignedBy, string Title, string Description, DateTime DueDate, Guid GroupId) : IRequest<ErrorOr<Chore>>;

}