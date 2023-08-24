using KUSYS.Core.Infrastructure;
using KUSYS.Core.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace KUSYS.Web.Infrastructure
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        //private TokenSettings _tokenSettings;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            var tokenResponse = context.HttpContext.GetAuthUser();
            //var tokenSettingsOption = context.HttpContext.RequestServices.GetRequiredService<IOptions<TokenSettings>>();
            //_tokenSettings = tokenSettingsOption.Value;
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.Token) || tokenResponse.ExpireDate < DateTime.UtcNow/*.AddMinutes(_tokenSettings.AccessTokenExpiration)*/)
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }

            var authorizeAttribute = context.ActionDescriptor.EndpointMetadata.OfType<AuthAttribute>().FirstOrDefault();
            if (authorizeAttribute == null || (tokenResponse.User?.Role != null && !authorizeAttribute.Roles.Contains(tokenResponse.User.Role.Name)))
                context.Result = new RedirectResult("/Home/AccessDenied");
        }
    }
}
