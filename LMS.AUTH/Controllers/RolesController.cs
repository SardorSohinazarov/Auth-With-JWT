using LMS.AUTH.Data;
using LMS.AUTH.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.AUTH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController:ControllerBase
    {
        private readonly IAppDbContext _appDbContext;

        public RolesController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async ValueTask<IActionResult> AddRolesAsync(RoleDTO roleDTO)
        {
            var permissions = await _appDbContext.Permissions.Where(x => roleDTO.Permissions.Contains(x.Id)).ToListAsync();
            var entry = _appDbContext.Roles.Add(new Entites.Auth.Role
            {
                Name = roleDTO.Name,
                Permissions = permissions
            });

            await _appDbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetRolesAsync()
            => Ok(_appDbContext.Roles.ToList());
    }
}
