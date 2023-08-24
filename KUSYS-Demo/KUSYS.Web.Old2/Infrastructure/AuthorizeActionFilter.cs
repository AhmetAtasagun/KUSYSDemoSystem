using KUSYS.DataAccess.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace KUSYS.Web.Infrastructure
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly IUserRepository _userRepository;

        public AuthorizeActionFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            string? authorizeToken = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
            //var userId = context.HttpContext.User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value?.ToInt();
            var user = context.HttpContext.GetAuthUser();
            //var user = _userRepository.Include(i => i.Tokens).FirstOrDefault(w => w.Id == userId);
            
            if (string.IsNullOrEmpty(authorizeToken) || user == null || !user.Token.Equals(authorizeToken))
            {
                //context.HttpContext.Response.StatusCode = 401;
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
