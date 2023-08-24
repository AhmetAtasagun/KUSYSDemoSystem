using KUSYS.DataAccess.Contexts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories
{
    public class CourseRepository : Repository<Course, KUSYSDbContext>, ICourseRepository
    {
        public CourseRepository(KUSYSDbContext context) : base(context)
        {
        }
    }
}
