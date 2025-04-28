using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CardDex2._0.Components
{
    public class Utils
    {
        public static void SignInUser(string username, string role, bool isPersistent, HttpResponse response)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                username,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                isPersistent,
                role, // Role inside UserData
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            response.Cookies.Add(authCookie);
        }
    }
}