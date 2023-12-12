using LMS.AUTH.Data;
using LMS.AUTH.DTOs;
using LMS.AUTH.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;

namespace LMS.AUTH.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ITokenService _tokenService;

        public AccountService(IAppDbContext appDbContext, ITokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public async ValueTask<TokenDTO> Login(UserLoginDTO userLoginDTO)
        {
            //validate
            var user = await _appDbContext.Users.Include(x => x.Roles).ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Email == userLoginDTO.Email && x.Password == userLoginDTO.Password);
            //validate

            return await _tokenService.GetTokenAsync(user);
        }

        public async ValueTask<TokenDTO> RefreshToken(TokenDTO refreshTokenDTO)
        {
            var claims = await GetClaimsFromExpiredTokenAsync(refreshTokenDTO.AccessToken);

            var id = Convert.ToInt32(claims.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = await _appDbContext.Users.Include(x => x.Roles).ThenInclude(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);

            if (user.RefreshToken != refreshTokenDTO.RefreshToken)
                throw new Exception("Refresh token is not valid");

            var newAccessToken = await _tokenService.GetTokenAsync(user);

            return newAccessToken;
        }

        public async ValueTask<ClaimsPrincipal> GetClaimsFromExpiredTokenAsync(string token)
        {
            var validationParametrs = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = "Issuer",
                ValidateAudience = true,
                ValidAudience = "Audience",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mening-JWt-Keyim-She-Edi")),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParametrs, out SecurityToken securityToken);

            var jwtsecurityToken = securityToken as JwtSecurityToken;

            if (jwtsecurityToken == null)
                throw new Exception("Invalid token");

            return claimsPrincipal;
        }

        public async ValueTask<TokenDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            var roles = await _appDbContext.Roles.Include(x => x.Permissions).Where(x => userRegisterDTO.Roles.Contains(x.Id)).ToListAsync();

            //validate email
            var user = new User()
            {
                Name = userRegisterDTO.Name,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                RefreshToken = Guid.NewGuid().ToString(),
                Roles = roles
            };

            var entry = _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            var token = await _tokenService.GetTokenAsync(entry.Entity);

            return token;
        }
    }
}
