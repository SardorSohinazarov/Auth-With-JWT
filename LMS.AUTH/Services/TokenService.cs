using LMS.AUTH.DTOs;
using LMS.AUTH.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.AUTH.Services
{
    public class TokenService : ITokenService
    {
        public async ValueTask<TokenDTO> GetTokenAsync(User user)
        {
            var roles = user.Roles;
            var persmissions = roles.SelectMany(x => x.Permissions);
            var stringPermission = persmissions.Select(x => x.Name);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            foreach (var p in stringPermission)
            {
                claims.Add(new Claim(ClaimTypes.Role, p));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    key:Encoding.UTF8.GetBytes("Mening-JWt-Keyim-She-Edi")),
                    algorithm:SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials
                );

            var accesToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDTO
            {
                RefreshToken = user.RefreshToken,
                AccessToken = accesToken
            };
        }
    }
}
