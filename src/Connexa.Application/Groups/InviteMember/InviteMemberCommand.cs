using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Groups.InviteMember
{
    public record InviteMemberCommand(Guid GroupId, Guid MemberId) : IRequest<ErrorOr<Success>>;
}