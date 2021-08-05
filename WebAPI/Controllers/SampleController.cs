using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SampleController : Controller
    {
        /// <summary>
        /// Tests if the api is online and returns the environment variable ASPNETCORE_ENVIRONMENT value
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"I'm on baby! - Current Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
        }
    }
}
