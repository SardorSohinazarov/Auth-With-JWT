namespace LMS.AUTH.Entites.Auth
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
