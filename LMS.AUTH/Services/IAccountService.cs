using LMS.AUTH.DTOs;
using LMS.AUTH.Entites;

namespace LMS.AUTH.Services
{
    public interface IAccountService
    {
        public ValueTask<TokenDTO> Register(UserRegisterDTO userRegisterDTO);
        public ValueTask<TokenDTO> Login(UserLoginDTO userLoginDTO);
        public ValueTask<TokenDTO> RefreshToken(TokenDTO tokenDTO);
    }
}
