using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilities
{
    public static class LoginManager
    {
        public static void LoginToSite(this HttpContext httpContext, long UserId, string Name)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),
                    new Claim(ClaimTypes.Name, Name),
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };
            httpContext.SignInAsync(principal, properties);
        }

        public static async Task LoginToSiteAsync(this HttpContext httpContext, long UserId, string Name)
        {
            await Task.Run(() =>
            {
                var claims = new List<Claim>()
                {
                      new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),
                      new Claim(ClaimTypes.Name, Name),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                };
                httpContext.SignInAsync(principal, properties);
            });
        }

        public static void SignoutSite(this HttpContext httpContext)
        {
            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static async Task SignoutSiteAsync(this HttpContext httpContext)
        {
            await Task.Run(() =>
            {
                httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            });
        }
    }
}
