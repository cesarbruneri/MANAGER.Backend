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
        private readonly ILogger<LoginController> _logger;

        public LoginController(IMediator mediator, ILogger<LoginController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("authentication")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(LoginInput loginInput)
        {
            var command = new LoginCommand(loginInput.Email, loginInput.Password);
            _logger.LogInformation("api/authentication", loginInput);

            var result = await _mediator.Send(command);

            return Ok(result.Value);
        }
    }
}
