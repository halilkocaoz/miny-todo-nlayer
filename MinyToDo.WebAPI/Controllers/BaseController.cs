using System;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Models;
using MinyToDo.Models.Enums;

namespace MinyToDo.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ApiReturn(ApiResponse response) => response.Type switch
        {
            ApiResponseType.Ok              => Ok(new { response.Data }),
            ApiResponseType.Created         => StatusCode(201),
            ApiResponseType.NoContent       => NoContent(),

            ApiResponseType.BadRequest      => BadRequest(new { response.Message }),
            ApiResponseType.NotFound        => NotFound(new { response.Message }),
            ApiResponseType.Unauthorized    => Unauthorized(),
            ApiResponseType.Forbidden       => Forbid(),
            _                               => throw new ArgumentOutOfRangeException(nameof(response.Type)),
        };
    }
}