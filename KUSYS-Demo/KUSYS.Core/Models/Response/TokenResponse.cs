namespace KUSYS.Core.Models.Response
{
    public class TokenResponse
    {
        public UserResponse User { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
