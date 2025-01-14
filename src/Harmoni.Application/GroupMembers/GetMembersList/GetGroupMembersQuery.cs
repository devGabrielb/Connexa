using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.GroupMembers.GetMembersList
{
    public record GetGroupMembersQuery(Guid GroupId) : IRequest<ErrorOr<List<GetGroupMembersQueryResponse>>>;
}