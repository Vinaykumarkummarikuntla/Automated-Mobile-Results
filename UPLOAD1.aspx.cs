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

public partial class UPLOAD1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SINGLE UPLOAD.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con=new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into student_marks values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "')",con);
        cmd.ExecuteNonQuery();
        Label1.Text = "Student Marks Uploaded Successfully";
            
    }
}
