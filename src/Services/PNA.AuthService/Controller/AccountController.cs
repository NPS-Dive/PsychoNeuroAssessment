using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PNA.Core.Commands;
using PNA.Core.Entities;
using PNA.Core.Queries;

namespace PNA.AuthService.Controller
    {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
        {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController ( IMediator mediator, UserManager<User> userManager, SignInManager<User> signInManager )
            {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register ( [FromBody] RegisterCommand command )
            {
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
            }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login ( [FromBody] LoginCommand command )
            {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
            }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout ()
            {
            await _signInManager.SignOutAsync();
            return Ok();
            }

        [HttpGet("profile")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<IActionResult> GetProfile ()
            {
            var user = await _userManager.GetUserAsync(User);
            return Ok(BuildUserResponse(user));
            }

        [HttpPut("profile")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<IActionResult> UpdateProfile ( string firstName, string lastName )
            {
            var user = await _userManager.GetUserAsync(User);
            user.Update(firstName, lastName);
            await _userManager.UpdateAsync(user);
            await _mediator.Send(new GetUserQuery(user.Id)); // Sync to MongoDB
            return Ok();
            }

        [HttpGet("users")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ListUsers ()
            {
            var users = await _mediator.Send(new ListUsersQuery());
            return Ok(users.Select(BuildUserResponse));
            }

        private static object BuildUserResponse ( User user ) =>
            new { user.UserName, user.Email, user.FirstName, user.LastName, user.Roles };
        }
    }
