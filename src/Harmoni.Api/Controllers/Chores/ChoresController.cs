using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Api.Controllers.Chores.Requests;
using Harmoni.Application.Chores.CreateForGroup;
using Harmoni.Application.Chores.CreateForUser;
using Harmoni.Application.Chores.GetChoresByUser;
using Harmoni.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Harmoni.Api.Controllers.Chores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoresController : ControllerBase
    {
        private readonly ISender _sender;

        public ChoresController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("/{userId}/tasks")]
        public async Task<IActionResult> GetChoresByUser(
            [FromRoute] Guid userId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] ChoreStatus? status = null)
        {
            var query = new GetChoresByUserQuery(userId, page, pageSize, status);
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("/tasks")]
        public async Task<IActionResult> CreateChoreForUser([FromBody] CreateChoreRequest request)
        {
            var command = new CreateForUserCommand(request.OwnerId, request.Title, request.Description, request.DueDate);
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpPost("/{groupId}/tasks")]
        public async Task<IActionResult> CreateChoreForGroup([FromRoute] Guid groupId, [FromBody] CreateChoreRequest request)
        {
            var command = new CreateForGroupCommand(request.OwnerId, request.Title, request.Description, request.DueDate, groupId);
            var result = await _sender.Send(command);

            return Ok(result);
        }

    }
}