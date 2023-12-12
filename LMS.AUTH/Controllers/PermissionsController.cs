using LMS.AUTH.Data;
using Microsoft.AspNetCore.Mvc;

namespace LMS.AUTH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionsController:ControllerBase
    {
        private readonly IAppDbContext _appDbContext;

        public PermissionsController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async ValueTask<IActionResult> AddPermissionAsync(string name)
        {
            var entry = _appDbContext.Permissions.Add(new Entites.Auth.Permission
            {
                Name = name
            });

            await _appDbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }
        
        [HttpGet]
        public async ValueTask<IActionResult> GetPermissionsAsync()
            => Ok(_appDbContext.Permissions.ToList());
    }
}
