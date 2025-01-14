using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Commons;

namespace Harmoni.Domain.Events
{
    public record MemberInvitedEvent(Guid UserId, Guid GroupId) : IDomainEvent;
}