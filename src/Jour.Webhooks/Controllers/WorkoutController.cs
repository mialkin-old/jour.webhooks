﻿using Microsoft.AspNetCore.Mvc;

namespace Jour.Webhooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {ok = true});
        }
    }
}