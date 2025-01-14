using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Groups.GetGroupsByUser
{
    public record GetGroupsByUserQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Group>>>;
}