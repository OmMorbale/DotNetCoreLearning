using LearningDotNetCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotNetCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreeterController : ControllerBase
    {
        private readonly IGreeter _greeter;

        // 👇 THIS is the injection — DI sees the constructor needs an IGreeter,
        // looks it up in the container (where you registered it), and passes it in automatically
        public GreeterController(IGreeter greeter)
        {
            _greeter = greeter;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_greeter.Greet());
        }
    }
}
