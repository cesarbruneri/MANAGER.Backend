﻿using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Application.Users.Query;
using MANAGER.Backend.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task<IActionResult> CreateUserAsync(UserInput userInput)
        {
            var command = new CreateUserCommand(userInput.Name, userInput.LastName, userInput.Email, userInput.Age);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return FluentResult(result);
            }

            return Ok();
        }

        [HttpGet("all-users")]
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
