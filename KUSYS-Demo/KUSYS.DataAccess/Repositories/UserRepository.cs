using KUSYS.DataAccess.Contexts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories
{
    public class UserRepository : Repository<User, KUSYSDbContext>, IUserRepository
    {
        public UserRepository(KUSYSDbContext context) : base(context)
        {
        }
    }
}
