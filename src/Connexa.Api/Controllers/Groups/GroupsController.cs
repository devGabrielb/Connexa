using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Api.Controllers.Groups.Requests;
using Connexa.Application.Groups.CreateGroup;
using Connexa.Application.Groups.InviteMember;
using Connexa.Application.Groups.RemoveMember;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Connexa.Api.Controllers.Groups
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ISender _sender;

        public GroupsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var command = new CreateGroupCommand(request.OwnerId, request.Name, request.Description);
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpPost("/{groupId}/members/invite")]
        public async Task<IActionResult> InviteMember([FromRoute] Guid groupId, [FromBody] InviteMemberRequest request)
        {
            var command = new InviteMemberCommand(groupId, request.MemberId);
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpDelete("/{groupId}/members")]
        public async Task<IActionResult> RemoveMember([FromRoute] Guid groupId, [FromBody] RemoveMemberRequest request)
        {
            var command = new RemoveMemberCommand(groupId, request.MemberId);
            var result = await _sender.Send(command);

            return Ok(result);
        }
    }
}