using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Auth.LoginUser;
using Connexa.Application.Auth.RegisterUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Connexa.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ISender _sender;
        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var query = new LoginUserQuery(request.Email, request.Password);
            var response = await _sender.Send(query);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(request.Name, request.Email, request.Password);
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}