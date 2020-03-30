using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Web;

namespace Radyalabs.Core.Helper
{
    public class AppCookieHelper
    {
        public static void Set(object obj, bool isRemember, string key)
        {
            string identifier = key + "|" + DateTime.Now.ToString("yyyyMMddHHmmssffff");

            string strJsonUser = JsonConvert.SerializeObject(obj);

            var ident = new ClaimsIdentity(
                new[] 
                { 
                    new Claim(ClaimTypes.NameIdentifier, identifier),
                    new Claim(ClaimTypes.UserData, strJsonUser),
                },
                DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.Current.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = isRemember, ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60 * 24) }
                   ,ident);

            HttpContext.Current.User = HttpContext.Current.GetOwinContext().Authentication.User;
        }

        public static T Get<T>() where T : class
        {
            T userAuth = null;

            if (!(HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity != null &&
                HttpContext.Current.User.Identity.IsAuthenticated))
            {
                return null;
            }

            var userData = ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirst(ClaimTypes.UserData);

            if (userData == null) return null;

            userAuth = JsonConvert.DeserializeObject<T>(userData.Value);

            return userAuth;
        }

        public static void Remove()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
