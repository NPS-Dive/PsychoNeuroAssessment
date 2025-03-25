using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PNA.Core.Commands;
using PNA.Core.Entities;

namespace PNA.AuthService.Controller
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;

            public AccountController ( IMediator mediator, UserManager<User> userManager, SignInManager<User> signInManager )
            {
                _mediator = mediator;
                _userManager = userManager;
                _signInManager = signInManager;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register ( [FromBody] RegisterCommand command )
            {
                var userId = await _mediator.Send(command);
                return Ok(new { UserId = userId });
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login ( [FromBody] LoginCommand command )
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token });
            }

            [HttpPost("logout")]
            [Authorize]
            public async Task<IActionResult> Logout ()
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }

            [HttpGet("profile")]
            [Authorize]
            public async Task<IActionResult> GetProfile ()
            {
                var user = await _userManager.GetUserAsync(User);
                return Ok(new { user.UserName, user.Email, user.FirstName, user.LastName });
            }
        }
    }
