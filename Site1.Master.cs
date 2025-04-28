using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardDex2._0
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)Page.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                string role = ticket.UserData; // Get "Staff" or "Member"


                if (role == "Member")
                {
                    btnMemberLogin.Text = "My Cards";
                    btnMemberSignUp.Visible = false;
                    btnStaffLogin.Visible = false;
                }
                else if (role == "Staff")
                {
                    btnStaffLogin.Text = "Staff Page";
                    btnMemberSignUp.Visible = false;
                    btnMemberLogin.Visible = false;
                }
            } else
            {
                btnLogOut.Visible = false;
            }
        }

        protected void btnMemberSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member/MemberRegister.aspx");
        }

        protected void btnMemberLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Member/MemberLogin.aspx");
        }

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Staff/StaffLogin.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}