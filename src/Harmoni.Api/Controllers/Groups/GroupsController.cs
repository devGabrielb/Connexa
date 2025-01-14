using Harmoni.Api.Controllers.Groups.Requests;
using Harmoni.Application.Groups.CreateGroup;


using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Harmoni.Api.Controllers.Groups
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
            var command = new CreateGroupCommand(request.OwnerId, request.Name, request.Description, request.GroupPicture);
            var result = await _sender.Send(command);

            return Ok(result);
        }
    }
}