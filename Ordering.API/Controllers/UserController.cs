using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.User.Create;
using Ordering.Application.Commands.User.Delete;
using Ordering.Application.Commands.User.Update;
using Ordering.Application.DTOs;
using Ordering.Application.Queries.Users;
using System.Data;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin, Management")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command, CancellationToken ct = default)
        {
            _logger.LogInformation("Create User");
            return Ok(await _mediator.Send(command, ct));
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<UserResponseDTO>))]
        public async Task<IActionResult> GetAllUserAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Get all users");
            return Ok(await _mediator.Send(new GetUserQuery(), ct));
        }

        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<IActionResult> DeleteUser(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation($"Delete user {userId}");
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId }, ct);
            return Ok(result);
        }

        [HttpGet("GetUserDetails/{userId}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetails(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation($"Get user {userId}");
            var result = await _mediator.Send(new GetUserDetailsQuery() { UserId = userId }, ct);
            return Ok(result);
        }

        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName, CancellationToken ct = default)
        {
            _logger.LogInformation($"Get user {userName}");
            var result = await _mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName }, ct);
            return Ok(result);
        }

        [HttpPost("AssignRoles")]
        [ProducesDefaultResponseType(typeof(bool))]

        public async Task<ActionResult> AssignRoles(AssignUsersRoleCommand command, CancellationToken ct = default)
        {
            _logger.LogInformation("Assign role");
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPut("EditUserRoles")]
        [ProducesDefaultResponseType(typeof(bool))]

        public async Task<ActionResult> EditUserRoles(UpdateUserRolesCommand command, CancellationToken ct = default)
        {
            _logger.LogInformation($"Edit user role");
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpGet("GetAllUserDetails")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetAllUserDetails(CancellationToken ct = default)
        {
            _logger.LogInformation("Get all users");
            var result = await _mediator.Send(new GetAllUsersDetailsQuery(), ct);
            return Ok(result);
        }


        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(bool))]
        public async Task<ActionResult> EditUserProfile(string id, [FromBody] EditUserProfileCommand command, CancellationToken ct = default)
        {
            if (id == command.Id)
            {
                _logger.LogInformation($"Get user profilr {id}");
                var result = await _mediator.Send(command, ct);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
