using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Arduinoapi.Controllers
{
    [ApiController]
    [Route("api")]
    public class CurrentStateController : ControllerBase
    {
        public async Task<bool> GetState()
        {
            
            string text = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\state.txt");
            if (text == "on")
            {
                return true;
            } else
            {
                return false;
            }
        }
        public async Task ChangeState()
        {
            if (await GetState())
            {
                await System.IO.File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "\\state.txt", "off");
            } else
            {
                await System.IO.File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "\\state.txt", "on");
            }
        }
        private readonly ILogger<CurrentStateController> _logger;
        public CurrentStateController(ILogger<CurrentStateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getState")]
        public async Task<IActionResult> Get()
        {
            
            return Ok(await GetState() ? "On" : "Off");
        }
        [HttpGet]
        [Route("changeState")]
        public async Task<IActionResult> Change()
        {
            ChangeState();
            return Ok("State Changed");
            

        }
    }
}
