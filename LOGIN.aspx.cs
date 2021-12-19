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

public partial class LOGIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("change password.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con=new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from login where username='"+TextBox1.Text+"' and password='"+TextBox2.Text+"'",con);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Response.Redirect("SERVICES.aspx");
        }
        else
        {
            Label1.Text="invalid user";
        }

    }
}
