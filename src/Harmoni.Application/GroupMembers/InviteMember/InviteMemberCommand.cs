using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.GroupMembers.InviteMember
{
    public record InviteMemberCommand(Guid GroupId, Guid UserId) : IRequest<ErrorOr<Success>>;
}