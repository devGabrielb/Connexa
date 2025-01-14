using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IJwtService
    {
        public string GetAccessToken(User user);
        public RefreshToken GetRefreshToken(Guid userId);

    }
}