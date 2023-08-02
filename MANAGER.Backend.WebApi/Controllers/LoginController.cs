using MANAGER.Backend.Application.Users.Login;
using MANAGER.Backend.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MANAGER.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LoginController : BaseController
    {
        private readonly IMediator _mediator;

        //https://www.youtube.com/watch?v=vAUXU0YIWlU
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("authentication")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(LoginInput loginInput)
        {
            var command = new LoginCommand(loginInput.Email, loginInput.Password);

            var result = await _mediator.Send(command);

            return Ok(result.Value);
        }
    }
}
