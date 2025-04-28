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
        private UserManager manager; // Manages user-related operations

        // Initializes the page and sets up the UserManager
        protected void Page_init(object sender, EventArgs e)
        {
            manager = new UserManager(); // Initialize the UserManager
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
                    Response.Redirect("~/Member/Member.aspx");
                }
            }
        }

        // Handles the login button click event
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Get the entered username
            string password = txtPassword.Text; // Get the entered password

            // Validate that both username and password are provided
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Username and password are required."; // Display an error message
                return;
            }

            // Validate the user's credentials
            if (manager.ValidateMember(username, password))
            {
                // Sign in the user and redirect to the Member page
                Utils.SignInUser(username, "Member", true, Response);
                Response.Redirect("~/Member/Member.aspx");
            }
            else
            {
                // Display an error message for invalid credentials
                lblMessage.Text = "Invalid username or password.";
            }
        }
    }
}