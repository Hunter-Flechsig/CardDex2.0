using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLLibrary;
using System.Xml.Linq;
using Assignment_5;

namespace CardDex2._0.Member
{
    public partial class MemberRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            bool captchaResult = (bool)Session["CaptchaResult"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Username and password are required.";
                return;
            }
            if (!captchaResult) {
                lblMessage.Text = "Correct Captcha Required.";
                return;
            }

            if (ValidateMember(username))
            {
                // add word filter to verify if the username is suitable to store on server
                // Add logic to add username and password to xml file. 
                string enteredPasswordHash = EncryptionDecryption.Encrypt(password);
                string memberPath = Server.MapPath("~/Data/Members.xml"); // Replace with actual path
                XDocument memberDoc = XDocument.Load(memberPath);
                XElement user = new XElement("User");
                user.Add(new XAttribute("username", username));
                user.Add(new XAttribute("password", enteredPasswordHash));
                user.Add(new XElement("PokemonCards"));
                memberDoc.Element("Users").Add(user);
                memberDoc.Save(memberPath);
                lblMessage.Text = "Registration Successful! Navigate to Member Login";
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateMember(string username)
        {
            string memberPath = Server.MapPath("~/Data/Members.xml"); // Replace with actual path

            XDocument memberDoc = XDocument.Load(memberPath);
            var memberUser = memberDoc.Root.Elements("User")
                                 .FirstOrDefault(u => u.Element("Username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);
            if (memberUser == null)
            {
                return true;
            }
            return false;
        }
    }
}