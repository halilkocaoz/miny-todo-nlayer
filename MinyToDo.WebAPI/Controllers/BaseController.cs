using System;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Models;
using MinyToDo.Models.Enums;

namespace MinyToDo.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ApiReturn(ApiResponse response) => StatusCode((int)response.StatusCode, response);
    }
}