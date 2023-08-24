using KUSYS.DataAccess.Contexts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories
{
    public class UserTokenRepository : Repository<UserToken, KUSYSDbContext>, IUserTokenRepository
    {
        public UserTokenRepository(KUSYSDbContext context) : base(context)
        {
        }
    }
}
