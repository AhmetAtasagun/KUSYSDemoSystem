using KUSYS.Core.Abstracts;

namespace KUSYS.Core.Models.Authorization
{
    public class UserAuth : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
