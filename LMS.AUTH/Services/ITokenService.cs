using LMS.AUTH.DTOs;
using LMS.AUTH.Entites;

namespace LMS.AUTH.Services
{
    public interface ITokenService
    {
        public ValueTask<TokenDTO> GetTokenAsync(User user);
    }
}
