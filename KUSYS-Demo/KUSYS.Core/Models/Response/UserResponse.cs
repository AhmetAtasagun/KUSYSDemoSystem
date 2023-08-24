namespace KUSYS.Core.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        //public string PasswordSalt { get; set; }

        public int RoleId { get; set; }
        public RoleResponse Role { get; set; }
    }
}
