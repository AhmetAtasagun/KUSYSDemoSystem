﻿using KUSYS.Core.Models.Response;
using System.Text.Json;

namespace KUSYS.Web.Infrastructure
{
    public static class SessionExtensions
    {
        private static string TokenKey = "Authorization";
        public static void SetAuthUser(this HttpContext httpContext, TokenResponse data)
        {
            var dataString = JsonSerializer.Serialize(data);
            httpContext.Session.SetString(TokenKey, dataString);
        }

        public static TokenResponse GetAuthUser(this HttpContext httpContext)
        {
            var dataString = httpContext.Session.GetString(TokenKey);
            if (string.IsNullOrEmpty(dataString))
                return default;
            return JsonSerializer.Deserialize<TokenResponse>(dataString);
        }

        public static void DeleteAuthUser(this HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }

        public static bool IsAuthorize(this TokenResponse tokenResponse, string roleKey)
        {
            if (tokenResponse?.User?.Role?.Name == null)
                return false;
            return tokenResponse.User.Role.Name == roleKey;
        }
    }
}
