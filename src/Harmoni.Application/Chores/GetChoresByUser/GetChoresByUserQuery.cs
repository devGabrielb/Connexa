using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons;
using Harmoni.Domain.Entities;
using Harmoni.Domain.Enums;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Chores.GetChoresByUser
{
    public record GetChoresByUserQuery(Guid userId, int page, int pageSize, ChoreStatus? status) : IRequest<ErrorOr<PagedResult<ChoresResponse>>>;


}