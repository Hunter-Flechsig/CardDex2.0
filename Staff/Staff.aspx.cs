using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLLLibrary;
using System.Xml.Linq;
using System.Web.Security;

namespace CardDex2._0.Staff
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                string role = ticket.UserData;

                if (role != "Staff")
                {
                    Response.Redirect("~/Staff/StaffLogin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Staff/StaffLogin.aspx");
            }
        }

        protected void CredentialCreateButton_Click(object sender, EventArgs e)
        {
            string username = StaffUsernameTextBox.Text.Trim();
            string password = StaffPasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Result.Text = "Username and password are required.";
                return;
            }

            if (ValidateStaff(username))
            {
                // add word filter to verify if the username is suitable to store on server
                // Add logic to add username and password to xml file. 
                string staffPath = Server.MapPath("~/Data/Staff.xml"); // Replace with actual path
                XDocument staffDoc = XDocument.Load(staffPath);
                XElement user = new XElement("User");
                user.Add(new XAttribute("username", username));
                user.Add(new XAttribute("password", password));
                staffDoc.Element("Users").Add(user);
                staffDoc.Save(staffPath);
                Result.Text = "Credential Created! Check Staff.xml or wait for cookies to expire to test";
            }
            else
            {
                Result.Text = "Invalid username or password.";
            }
        }

        private bool ValidateStaff(string username)
        {
            string memberPath = Server.MapPath("~/Data/Staff.xml"); // Replace with actual path

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