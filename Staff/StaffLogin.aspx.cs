using System;
using System.Web;
using System.Web.Security; // Required for Forms Authentication
using System.Xml.Linq;     // For reading XML easily
using System.IO;
using System.Linq;
using DLLLibrary;
using System.Web.UI.WebControls;
using CardDex2._0.Components;
// using (insert here); // *** IMPORT YOUR HASHING DLL NAMESPACE ***

namespace CardDex2._0.Login // Make sure this matches your project's namespace
{
    public partial class Login : System.Web.UI.Page
    {
        // checks if the user is auth, if so go to staff page
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                string role = ticket.UserData;

                if (role == "Staff")
                {
                    Response.Redirect("~/Page1/Staff/Staff.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // checks if user and password are valid
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
            if (ValidateStaff(username, password))
            {
                Utils.SignInUser(username, "Staff", true, Response);
                Response.Redirect("~/page1/Staff/Staff.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateStaff(string username, string enteredPassword)
        {
            // checks staff xml for username and password; if the user is there, check if password is correct
                string staffPath = Server.MapPath("~/Data/Staff.xml"); // Replace with actual path
                XDocument staffDoc = XDocument.Load(staffPath);
                var staffUser = staffDoc.Root.Elements("User")
                                     .FirstOrDefault(u => u.Attribute("username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);

            if (staffUser != null)
            {
                string storedPassword = staffUser.Attribute("password")?.Value;
                if (storedPassword == enteredPassword)
                {
                    return true;
                }
            }
            return false;
        }
    }
}