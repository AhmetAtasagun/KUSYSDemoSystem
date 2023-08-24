using KUSYS.Domain.Entities.Base;

namespace KUSYS.Domain.Entities
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
