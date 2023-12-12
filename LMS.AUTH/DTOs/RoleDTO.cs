namespace LMS.AUTH.DTOs
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public IEnumerable<int> Permissions {  get; set; }
    }
}
