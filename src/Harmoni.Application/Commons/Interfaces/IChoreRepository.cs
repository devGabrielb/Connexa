using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Chores.GetChoresByUser;
using Harmoni.Domain.Entities;
using Harmoni.Domain.Enums;

namespace Harmoni.Application.Commons.Interfaces
{
    public interface IChoreRepository
    {
        Task AddAsync(Chore group);
        Task<PagedResult<ChoresResponse>> ListByUserIdAsync(Guid userId, int page, int pageSize, ChoreStatus? status);
        Task<PagedResult<ChoresResponse>> GetGroupChoresAsync(Guid userId, int page, int pageSize, ChoreStatus? status);
        Task<PagedResult<ChoresResponse>> GetUserChoresAsync(Guid userId, int page, int pageSize, ChoreStatus? status);

    }
}