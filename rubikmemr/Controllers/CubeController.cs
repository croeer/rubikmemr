using Microsoft.AspNetCore.Mvc;

namespace rubikmemr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CubeController : ControllerBase
    {
    
        private readonly ILogger<CubeController> _logger;

        public CubeController(ILogger<CubeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string s)
        {
            var cube = new Cube();
            cube.TurnByString(s);
            var image = cube.ToByteArray();
            return File(image, "image/png");
        }


    }
}