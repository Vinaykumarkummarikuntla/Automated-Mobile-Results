using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class change_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("update login set password='" + TextBox2.Text + "' where password='"+TextBox1.Text+"'", con);

       int i=cmd.ExecuteNonQuery();
        if (i>0)
        {
            Response.Write("password change sucessfully");
        }
        else
        {
            Response.Write("invalid user");
        }
    }
}
