namespace KUSYS.Core.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthAttribute : Attribute
    {
        public AuthAttribute()
        {
            
        }

        public string Roles { get; set; }
    }
}
