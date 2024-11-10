using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

namespace Connexa.Application.Commons.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task AddOrUpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}