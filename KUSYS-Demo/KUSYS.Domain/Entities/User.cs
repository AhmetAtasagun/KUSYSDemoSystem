using KUSYS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.Domain.Entities
{
    public class User : BaseEntity
    {
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string PasswordSalt { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<UserToken> Tokens { get; set; }
    }
}
