using LMS.AUTH.Entites;
using LMS.AUTH.Entites.Auth;
using Microsoft.EntityFrameworkCore;

namespace LMS.AUTH.Data
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
