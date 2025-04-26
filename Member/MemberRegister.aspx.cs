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
    public partial class MemberRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
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

            bool isAuthenticated = false;

            if (ValidateStaff(username, password))
            {
                isAuthenticated = true;
            }
            else if (ValidateMember(username, password))
            {
                isAuthenticated = true;
            }
            if (isAuthenticated)
            {
                FormsAuthentication.RedirectFromLoginPage(username, false);

            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateStaff(string username, string enteredPassword)
        {
            string staffPath = Server.MapPath("~/Data/Staff.xml"); // Replace with actual path
            XDocument staffDoc = XDocument.Load(staffPath);
            var staffUser = staffDoc.Root.Elements("User")
                                 .FirstOrDefault(u => u.Element("Username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);

            if (staffUser != null)
            {
                string storedPassword = staffUser.Element("Password")?.Value;
                if (storedPassword == enteredPassword)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ValidateMember(string username, string enteredPassword)
        {
            string memberPath = Server.MapPath("~/Data/Members.xml"); // Replace with actual path

            XDocument memberDoc = XDocument.Load(memberPath);
            var memberUser = memberDoc.Root.Elements("User")
                                 .FirstOrDefault(u => u.Element("Username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);
            if (memberUser != null)
            {
                string storedHashedPassword = memberUser.Element("Password")?.Value;

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