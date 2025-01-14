using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Chores.GetChoresByUser;
using Harmoni.Application.Commons;
using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Entities;
using Harmoni.Domain.Enums;

using ErrorOr;

using Microsoft.EntityFrameworkCore;

namespace Harmoni.Infra.Persistence.Repositories
{
    public class ChoreRepository : IChoreRepository
    {
        private readonly HarmoniContext _context;

        public ChoreRepository(HarmoniContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Chore group)
        {
            await _context.Chores.AddAsync(group);
        }

        public Task<Chore> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ChoresResponse>> GetGroupChoresAsync(Guid userId, int page, int pageSize, ChoreStatus? status)
        {

            var groupChores = await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Chores)
                .AsNoTracking()
                .Where(group => group.Members.Any(member => member.UserId == userId))
                .SelectMany(group => group.Chores
                    .Where(chore =>
                        chore.AssignedTo.HasValue &&
                        group.Members.Any(member => member.Id == chore.AssignedTo.Value))
                    .Select(chore => new ChoresResponse(
                        chore.Id,
                        chore.Title,
                        chore.Description,
                        chore.State,
                        chore.DueDate,
                        group.Name)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<ChoresResponse>
            {
                Items = groupChores,
                Page = page,
                PageSize = pageSize,
                TotalCount = groupChores.Count
            };
            return result;
        }

        public async Task<PagedResult<ChoresResponse>> GetUserChoresAsync(Guid userId, int page, int pageSize, ChoreStatus? status)
        {
            var userChore = _context.Chores.AsNoTracking().Where(c => c.UserId == userId).AsQueryable();

            if (status.HasValue)
            {
                userChore = userChore.Where(c => c.State == status);
            }
            else
            {
                userChore = userChore.Where(c => c.State == ChoreStatus.New || c.State == ChoreStatus.InProgress);
            }

            var totalCount = await userChore.CountAsync();

            var userChores = await userChore
                .Select(chore => new ChoresResponse(
                    chore.Id,
                    chore.Title,
                    chore.Description,
                    chore.State,
                    chore.DueDate,
                    string.Empty))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<ChoresResponse>
            {
                Items = userChores,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            return result;
        }
        public async Task<PagedResult<ChoresResponse>> ListByUserIdAsync(Guid userId, int page, int pageSize, ChoreStatus? status)
        {


            var userChores = await GetUserChoresAsync(userId, page, pageSize / 2, status);
            var groupChores = await GetGroupChoresAsync(userId, page, pageSize / 2, status);


            var combinedChores = userChores.Items.Concat(groupChores.Items).DistinctBy(c => c.Id)
                .OrderBy(c => c.DueDate)
                .ToList();



            return new PagedResult<ChoresResponse>
            {
                Items = combinedChores,
                Page = page,
                PageSize = pageSize,
                TotalCount = combinedChores.Count
            };
        }
    }
}