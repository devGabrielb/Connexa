using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Groups.CreateGroup
{
    public record CreateGroupCommand(Guid OwnerId, string Name, string Description, string GroupPicture) : IRequest<ErrorOr<Guid>>;
}