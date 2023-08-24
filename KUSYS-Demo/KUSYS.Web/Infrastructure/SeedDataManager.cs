using KUSYS.Business.Helpers;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;

namespace KUSYS.Web.Infrastructure
{
    public class SeedDataManager
    {
        public static void SeedAll(WebApplication app)
        {
            SeedCourses(app);
            SeedRoles(app);
            SeedAdminUser(app);
        }

        public static void SeedCourses(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var courseRepository = scope.ServiceProvider.GetRequiredService<ICourseRepository>();
            if (courseRepository.Any(x => true))
                return;
            courseRepository.AddRange(
                new List<Course>
                {
                    new Course { CreateDate = new DateTime(), Name = "İngilizce" },
                    new Course { CreateDate = new DateTime(), Name = "Web Tasarım" },
                    new Course { CreateDate = new DateTime(), Name = "Yazılım Geliştiriciği" },
                    new Course { CreateDate = new DateTime(), Name = "Rölyef" },
                    new Course { CreateDate = new DateTime(), Name = "Telkari" }
                });
            courseRepository.SaveChanges();
        }

        public static void SeedRoles(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
            if (roleRepository.Any(x => true))
                return;
            roleRepository.Add(new Role { Name = KeyConsts.Admin, CreateDate = DateTime.UtcNow });
            roleRepository.Add(new Role { Name = KeyConsts.User, CreateDate = DateTime.UtcNow });
            roleRepository.SaveChanges();
        }

        public static void SeedAdminUser(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            if (userRepository.Any(x => true))
                return;
            var salt = HashingHelper.GenerateSecurityCode();
            var password = HashingHelper.HashUse("admin", salt);
            var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
            userRepository.Add(new User
            {
                Username = KeyConsts.Admin,
                Password = password,
                PasswordSalt = salt,
                Role = roleRepository.FirstOrDefaultNoTracking(w => w.Name == KeyConsts.Admin),
                CreateDate = DateTime.UtcNow,
            });
            userRepository.SaveChanges();
        }
    }
}
