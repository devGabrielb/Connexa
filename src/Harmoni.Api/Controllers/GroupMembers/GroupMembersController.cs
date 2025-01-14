using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Application.GroupMembers.GetMembersList;
using Harmoni.Application.GroupMembers.InviteMember;
using Harmoni.Application.GroupMembers.RemoveMember;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Harmoni.Api.Controllers.GroupMembers
{
    [ApiController]
    [Route("api/group")]
    public class GroupMembersController : ControllerBase
    {
        private readonly ISender _sender;

        public GroupMembersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/{groupId}/members")]
        public async Task<IActionResult> GetGroupMembers([FromRoute] Guid groupId)
        {
            var query = new GetGroupMembersQuery(groupId);
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("/{groupId}/members/invite/{userId}")]
        public async Task<IActionResult> InviteMember([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            var command = new InviteMemberCommand(groupId, userId);
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpDelete("/{groupId}/members/{memberId}")]
        public async Task<IActionResult> RemoveMember([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            var command = new RemoveMemberCommand(groupId, memberId);
            var result = await _sender.Send(command);

            return Ok(result);
        }
    }
}