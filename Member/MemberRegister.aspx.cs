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
//using CardDex2._0.WordFilter;

namespace CardDex2._0.Member
{
    public partial class MemberRegister : System.Web.UI.Page
    {
        // Initializes the page (currently unused client initialization is commented out)
        protected void Page_init(object sender, EventArgs e)
        {
            //client = new Service1Client();
        }

        // Handles page load events and redirects authenticated users
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)User.Identity; // Get the user's identity
                FormsAuthenticationTicket ticket = id.Ticket; // Get the authentication ticket
                string role = ticket.UserData; // Extract the user's role

                // Redirect to the Member page if the user is a member
                if (role == "Member")
                {
                    Response.Redirect("~/Page1/Member/Member.aspx");
                }
            }
        }

        // Handles the registration button click event
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Get the entered username
            string password = txtPassword.Text; // Get the entered password
            bool captchaResult = (bool)Session["CaptchaResult"]; // Check the captcha result

            // Validate that both username and password are provided
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Username and password are required."; // Display an error message
                return;
            }

            // Validate the captcha result
            if (!captchaResult)
            {
                lblMessage.Text = "Correct Captcha Required."; // Display an error message
                return;
            }

            // Uncommented code for word filtering (currently disabled)
            //string filteredUsername = client.WordFilter(username);
            //if (!username.Equals(filteredUsername)) {
            //    lblMessage.Text = "Inappropriate Username.";
            //    return;
            //}

            // Check if the username is valid and not already registered
            if (ValidateMember(username))
            {
                // Encrypt the password before storing it
                string enteredPasswordHash = EncryptionDecryption.Encrypt(password);

                // Load the XML file containing member data
                string memberPath = Server.MapPath("~/Data/Members.xml"); // Path to the XML file
                XDocument memberDoc = XDocument.Load(memberPath);

                // Create a new user element and add it to the XML
                XElement user = new XElement("User");
                user.Add(new XAttribute("username", username));
                user.Add(new XAttribute("password", enteredPasswordHash));
                user.Add(new XElement("PokemonCards")); // Add an empty PokemonCards element
                memberDoc.Element("Users").Add(user);
                memberDoc.Save(memberPath); // Save the updated XML file

                lblMessage.Text = "Registration Successful! Navigate to Member Login"; // Display success message
            }
            else
            {
                lblMessage.Text = "Invalid username or password."; // Display an error message
            }
        }

        // Validates if the username is unique and not already registered
        private bool ValidateMember(string username)
        {
            string memberPath = Server.MapPath("~/Data/Members.xml"); // Path to the XML file

            // Load the XML file and check if the username already exists
            XDocument memberDoc = XDocument.Load(memberPath);
            var memberUser = memberDoc.Root.Elements("User")
                                 .FirstOrDefault(u => u.Element("Username")?.Value.Equals(username, StringComparison.OrdinalIgnoreCase) ?? false);

            // Return true if the username is not found, otherwise false
            return memberUser == null;
        }
    }
}