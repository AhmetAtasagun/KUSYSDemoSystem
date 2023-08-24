using KUSYS.Core.Abstracts;
using KUSYS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.DataAccess.Contexts
{
    public class KUSYSDbContext : DbContext, IDbContext
    {
        public KUSYSDbContext(DbContextOptions options) : base(options)
        {
            //if (!Database.CanConnect())
            //{
            //    Database.EnsureCreated();
            //}
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
