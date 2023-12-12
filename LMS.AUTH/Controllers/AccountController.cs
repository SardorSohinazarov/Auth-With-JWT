using LMS.AUTH.DTOs;
using LMS.AUTH.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.AUTH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
            => _accountService = accountService;

        [HttpPost]
        public async ValueTask<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = await _accountService.Register(userRegisterDTO);

            return Ok(user);
        }
        
        [HttpPost]
        public async ValueTask<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _accountService.Login(userLoginDTO);

            return Ok(user);
        }

        [HttpPost]
        public async ValueTask<IActionResult> RefreshToken(TokenDTO tokenDTO)
        {
            var token = await _accountService.RefreshToken(tokenDTO);

            return Ok(token);
        }
    }
}
