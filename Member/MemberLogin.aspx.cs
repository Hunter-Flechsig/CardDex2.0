using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLLibrary;
using System.Xml.Linq;

namespace CardDex2._0.Member
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Example Member: Username: AshKetchum Password: Pikachu123
            //HttpCookie myCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            HttpCookie myCookies = Request.Cookies["MemberCookie"];
            if ((myCookies == null) || (!ValidateMember(myCookies["Username"], myCookies["Password"])))
            {
                lblMessage.Text = "Welcome to Member Login";
            }
            else
            {
                //Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.Redirect("Member.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Username and password are required.";
                return;
            }
            // Username and password for staff are stored as plaintext to make it easier for graders to tell the right credentials were used for staff
            // Does not say that cookies need to be encrypted, but if I needed to encrypt the cookies, I would follow this website
            // https://learn.microsoft.com/en-us/dotnet/api/system.web.security.formsauthentication.encrypt?view=netframework-4.8.1 - Tyler
            if (ValidateMember(username, password))
            {
                //HttpCookie myCookies = new HttpCookie(FormsAuthentication.FormsCookieName);
                HttpCookie myCookies = new HttpCookie("MemberCookie");
                myCookies["Username"] = username;
                // Says the password in the xml file needs to be encrypted but not necessarily the cookies
                myCookies["Password"] = password;
                myCookies.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(myCookies);
                //FormsAuthentication.RedirectFromLoginPage(username, true);
                Response.Redirect("Member.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateMember(string username, string enteredPassword)
        {
            string memberPath = Server.MapPath("~/Data/Members.xml"); // Replace with actual path

            XDocument memberDoc = XDocument.Load(memberPath);
            var memberUser = memberDoc.Root.Elements("User")
                                 .FirstOrDefault(u => u.Attribute("username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);
            if (memberUser != null)
            {
                string storedHashedPassword = memberUser.Attribute("password")?.Value;

                // *** THIS IS WHERE YOU CALL YOUR DLL ***
                string enteredPasswordHash = EncryptionDecryption.Encrypt(enteredPassword);

                if (!string.IsNullOrEmpty(storedHashedPassword) && storedHashedPassword.Equals(enteredPasswordHash))
                {
                    return true;
                }
            }
            return false;
        }
    }
}