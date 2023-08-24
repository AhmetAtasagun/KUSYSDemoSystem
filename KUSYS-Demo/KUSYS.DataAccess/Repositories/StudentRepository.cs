using KUSYS.DataAccess.Contexts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student, KUSYSDbContext>, IStudentRepository
    {
        public StudentRepository(KUSYSDbContext context) : base(context)
        {
        }
    }
}
