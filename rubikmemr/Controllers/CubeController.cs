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
        public IActionResult GetPng(string s)
        {
            var cube = new Cube();
            cube.TurnByString(s);
            var image = cube.ToByteArray();
            return File(image, "image/png");
        }

        [HttpGet("solve")]
        public IActionResult Solve(string s)
        {
            var cube = new Cube();
            cube.TurnByString(s);

            Solver slv = new(cube);
            return Ok(slv.Solve());

        }

    }
}