using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly HarmoniContext _context;

        public RefreshTokenRepository(HarmoniContext context)
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