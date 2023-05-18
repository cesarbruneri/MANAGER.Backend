using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MANAGER.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/create-user")]
        public async Task<IActionResult> CreateUserAsync(UserInput userInput)
        {
            var command = new CreateUserCommand(userInput.Name, userInput.LastName, userInput.Email, userInput.Age);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
