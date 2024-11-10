using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Connexa.Infra.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ConnexaContext _context;

        public RefreshTokenRepository(ConnexaContext context)
        {
            _context = context;
        }
        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.AddAsync(refreshToken);
        }

        public async Task AddOrUpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var existingRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == refreshToken.UserId);
            if (existingRefreshToken is not null)
            {
                _context.Entry(refreshToken).State = EntityState.Modified;
            }
            await _context.RefreshTokens.AddAsync(refreshToken);
        }
    }
}