using KUSYS.DataAccess.Contexts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role, KUSYSDbContext>, IRoleRepository
    {
        public RoleRepository(KUSYSDbContext context) : base(context)
        {
        }
    }
}
