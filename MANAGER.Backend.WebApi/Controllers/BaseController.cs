using FluentResults;
using MANAGER.Backend.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MANAGER.Backend.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult FluentResult(ResultBase result)
        {
            var errors = result.Errors
                .Select(e => new
                {
                    message = e.Message,
                    details = e.Reasons?.Select(r => r.Message),
                    reason = e.Message,
                });

            return new ObjectResult(new { errors } )
            {
                StatusCode = (int)GetStatusCode(result)
            };
        }

        private static HttpStatusCode GetStatusCode(ResultBase result) =>
            result.Errors.FirstOrDefault() switch
            {
                BadRequestError _ => HttpStatusCode.BadRequest,
                ConflictError _ => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError,
            };
        
    }
}
