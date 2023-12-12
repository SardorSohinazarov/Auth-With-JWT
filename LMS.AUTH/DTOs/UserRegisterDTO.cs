namespace LMS.AUTH.DTOs
{
    public class UserRegisterDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<int> Roles { get; set; }
    }
}
