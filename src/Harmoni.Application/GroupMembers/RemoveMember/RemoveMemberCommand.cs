using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.GroupMembers.RemoveMember
{
    public record RemoveMemberCommand(Guid GroupId, Guid MemberId) : IRequest<ErrorOr<Success>>;
}