using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Commons;

namespace Connexa.Domain.Events
{
    public record MemberInvitedEvent(Guid UserId, Guid GroupId) : IDomainEvent;
}