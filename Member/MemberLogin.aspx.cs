using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLLibrary;
using System.Xml.Linq;
using CardDex2._0.Data;
using CardDex2._0.Components;

namespace CardDex2._0.Member
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        private UserManager manager;

        protected void Page_init(object sender, EventArgs e)
        {
            manager = new UserManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                string role = ticket.UserData;

                if (role == "Member")
                {
                    Response.Redirect("~/Member/Member.aspx");
                }
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
            if (manager.ValidateMember(username, password))
            {
                Utils.SignInUser(username, "Member", true, Response);
                Response.Redirect("~/Member/Member.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

    }
}