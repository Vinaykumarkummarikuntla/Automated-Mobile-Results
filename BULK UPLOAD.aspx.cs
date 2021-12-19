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
using System.Data.OleDb;
using System.Data.SqlClient;


public partial class BULK_UPLOAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SERVICES.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());

        string hno = string.Empty;
        string name = string.Empty;
        string gender = string.Empty;
        string branch = string.Empty;
        string mno = string.Empty;
        string email = string.Empty;
        FileUpload1.SaveAs(Server.MapPath("~/files/" + FileUpload1.FileName));
        string path = Server.MapPath("~/files/" + FileUpload1.FileName);
      OleDbConnection OleDbcon = new OleDbConnection("Provider= Microsoft.jet.OLEDB.4.0;Data Source=" +path+ ";Extended Properties = Excel 8.0;");
       DataSet ds = new DataSet();
       OleDbDataAdapter oda = new OleDbDataAdapter("select * from [StudentInfo$]", OleDbcon);
        oda.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            hno = dr[0].ToString();
            name = dr[1].ToString();
            gender = dr[2].ToString();
            branch = dr[3].ToString();
            mno = dr[4].ToString();
            email = dr[5].ToString();
            SqlCommand cmd = new SqlCommand("insert into student_info values('" + hno + "','" + name + "','" + gender + "','" + branch + "','" + mno + "','" + email + "')", con);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            int res = cmd.ExecuteNonQuery();
            if(res>0)
            {
                Label1.Text = "Student Personal Data Uploaded Successfully";
                Panel1.Visible = true;
                Button1.Enabled = false;
            }
            else
                Label1.Text = "Student Personal Data not Uploaded";
            con.Close();

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());
        string hno = string.Empty;
        string subcode = string.Empty;
        string subjectname = string.Empty;
        string internal1 = string.Empty;
        string external = string.Empty;
        string total = string.Empty;
        string credits = string.Empty;
        FileUpload3.SaveAs(Server.MapPath("~/files/" + FileUpload3.FileName));
        string path = Server.MapPath("~/files/" + FileUpload3.FileName);
        OleDbConnection OleDbcon = new OleDbConnection("Provider= Microsoft.jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties = Excel 8.0;");
        DataSet ds = new DataSet();
        OleDbDataAdapter oda = new OleDbDataAdapter("select * from [MarksInfo$]", OleDbcon);
        oda.Fill(ds);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            hno = dr[0].ToString();
            subcode = dr[1].ToString();
            subjectname = dr[2].ToString();
            internal1 = dr[3].ToString();
            external = dr[4].ToString();
            total= dr[5].ToString();
            credits = dr[6].ToString();
            SqlCommand cmd = new SqlCommand("insert into student_marks values('" + hno + "','" + subcode + "','" +subjectname + "','" + internal1+ "','" + external + "','" + total + "','"+credits+"')", con);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                Label2.Text = "Student Marks Uploaded Successfully";
               
                Button1.Enabled = true;
            }
            else
                Label2.Text = "Student Marks not Uploaded";
            con.Close();

        }
    }
}
