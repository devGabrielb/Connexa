using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Groups.CreateGroup
{
    public record CreateGroupCommand(Guid OwnerId, string Name, string Description) : IRequest<ErrorOr<Guid>>;
}