using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Application.Users.Query;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.WebApi.Infraestructure;
using MANAGER.Backend.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace MANAGER.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-user")]
        [CustomAuthorize(Roles.Admin, Roles.Manager)]
        public async Task<IActionResult> CreateUserAsync(UserInput userInput)
        {
            var command = new CreateUserCommand(userInput.Name, userInput.LastName, userInput.Email, userInput.Password);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return FluentResult(result);
            }

            return Ok();
        }

        [HttpGet("all-users")]
        [CustomAuthorize(Roles.Admin, Roles.Manager)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return FluentResult(result);
            }

            return Ok(result.ValueOrDefault);
        }
    }
}
