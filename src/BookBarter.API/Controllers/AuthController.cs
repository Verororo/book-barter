using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookBarter.Application.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using BookBarter.API.Common.Models;

namespace BookBarter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ErrorDetails{ Messages = result.Messages.ToArray() });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                return Ok(result.AccessToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}