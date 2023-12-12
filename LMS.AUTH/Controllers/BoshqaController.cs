using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.AUTH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoshqaController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "GetAllHomeworks")]
        public async ValueTask<IActionResult> GetHomeWorks()
        {
            return Ok("Homeworks");
        }
        
        [HttpGet]
        [Authorize(Roles = "GetAllUsers")]
        public async ValueTask<IActionResult> GetStudents()
        {
            return Ok("Students");
        }

        [HttpGet]
        [Authorize(Roles = "GetAllVideos")]
        public async ValueTask<IActionResult> GetVideos()
        {
            return Ok("Videos");
        }
    }
}
