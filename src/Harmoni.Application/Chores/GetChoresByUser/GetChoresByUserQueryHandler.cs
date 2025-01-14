using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons;
using Harmoni.Application.Commons.Interfaces;
using Harmoni.Application.GroupMembers.GetMembersList;
using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.Chores.GetChoresByUser
{
    public class GetChoresByUserQueryHandler : IRequestHandler<GetChoresByUserQuery, ErrorOr<PagedResult<ChoresResponse>>>
    {
        private readonly IChoreRepository _choreRepository;

        public GetChoresByUserQueryHandler(IChoreRepository choreRepository)
        {
            _choreRepository = choreRepository;
        }
        public async Task<ErrorOr<PagedResult<ChoresResponse>>> Handle(GetChoresByUserQuery request, CancellationToken cancellationToken)
        {

            var chores = await _choreRepository.ListByUserIdAsync(request.userId, request.page, request.pageSize, request.status);
            var choresResponse = chores.Items;

            var result = new PagedResult<ChoresResponse>
            {
                Items = choresResponse,
                Page = chores.Page,
                PageSize = chores.PageSize,
                TotalCount = chores.TotalCount
            };

            return result;
        }

    }
}