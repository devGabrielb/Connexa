using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.Commons.Interfaces;
using Harmoni.Domain.Commons;
using Harmoni.Domain.Entities;

using ErrorOr;

using MediatR;

namespace Harmoni.Application.GroupMembers.GetMembersList
{
    public class GetGroupMembersQueryHandler : IRequestHandler<GetGroupMembersQuery, ErrorOr<List<GetGroupMembersQueryResponse>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemberGroupRepository _memberGroupRepository;

        public GetGroupMembersQueryHandler(IUserRepository userRepository, IMemberGroupRepository memberGroupRepository)
        {
            _userRepository = userRepository;
            _memberGroupRepository = memberGroupRepository;
        }
        public async Task<ErrorOr<List<GetGroupMembersQueryResponse>>> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
        {
            //TODO: refactor this
            var groupMembers = await _memberGroupRepository.GetMembersByGroupIdAsync(request.GroupId);

            if (groupMembers.Count <= 0)
            {

                return Error.NotFound("Group members not found");
            }

            var memberIds = groupMembers.Select(m => m.UserId).ToList();

            if (memberIds.Count <= 0)
            {
                return new List<GetGroupMembersQueryResponse>();
            }

            var users = await _userRepository.GetUsersByIdsAsync(memberIds);

            var memberResponses = users.Select(user => new GetGroupMembersQueryResponse(user.Name, user.Email)).ToList();

            return memberResponses;


        }
    }
}