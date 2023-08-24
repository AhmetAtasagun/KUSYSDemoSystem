using KUSYS.Core.Abstracts;
using KUSYS.DataAccess.Contexts;
using KUSYS.Domain.Entities;

namespace KUSYS.DataAccess.Repositories.Abstracts
{
    public interface IUserRepository : IRepository<User, KUSYSDbContext>
    {
    }
}
