using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardDex2._0
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMemberSignUp_Click(object sender, EventArgs e)
        {

        }

        protected void btnMemberLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Member/Member.aspx");
        }

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {
            // change to login aspx eventually
            Response.Redirect("/Staff/Staff.aspx");
        }
    }
}