using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Threading;

public partial class SEND : System.Web.UI.Page
{
    HttpWebRequest req;
    SmtpClient serverobj;
    MailMessage msgobj;
    private CookieContainer cookieCntr;

    private string strNewValue;
    string allmobile = string.Empty;

    public static string responseee;

    private HttpWebResponse response;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["con"].ToString());
            SqlDataAdapter da = new SqlDataAdapter("select s.Hall_Ticket_Number,s.Mobile_number,s.Email_id,s.Name,sm.Subject_Names,sm.Internals,sm.Externals,sm.Credits,sm.Total from student_info s inner join student_marks sm on s.Hall_Ticket_number=sm.Hall_Ticket_Number", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "studentinfo");
            ViewState["studentinfo"] = ds;
            ListBox1.DataSource = ds;
            ListBox1.DataTextField = "Hall_Ticket_Number";
            ListBox1.DataBind();
            ListBox2.DataSource = ds;
            ListBox2.DataTextField = "Mobile_number";
            ListBox2.DataBind();
            ListBox3.DataSource = ds;
            ListBox3.DataTextField = "Email_id";
            ListBox3.DataBind();
        }
    }
    public void SendMail(string to, string marksmail)
    {



       SmtpClient serverobj = new SmtpClient();
        serverobj.Host = "smtp.gmail.com";
        serverobj.Credentials = new NetworkCredential("sampath.bairi@coign.net", "");
        MailMessage msgobj = new MailMessage();
        serverobj.EnableSsl = true;
        serverobj.Port = 587;
        msgobj.From = new MailAddress("kumaraswamy60@gmail.com", "Marks Info");
        msgobj.To.Add(to);
        msgobj.Subject = "Marks Info";
        msgobj.Body = marksmail;
        serverobj.Send(msgobj);
    }

   
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)ViewState["studentinfo"];
        foreach (DataRow dr1 in ds.Tables["studentinfo"].Rows)
        {
            if (dr1[4].ToString() != string.Empty && dr1[5].ToString() != string.Empty)
            {
                string allmobile = dr1[1].ToString();
                string allmails = dr1[2].ToString();
                string marks = "Sub:" + dr1[4].ToString() + ",IN:" + dr1[5].ToString() + ",Ex:" + dr1[6].ToString() + ",T:" + dr1[8].ToString() + ",C:" + dr1[7].ToString();
                //sendmsg(marks, allmobile);
               // SendMail(allmails, marks);
                sendmessage(allmobile, marks);
                Thread.Sleep(5000);
            }

            Label1.Text = "Message Send Successfully";

        }
       

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
      DataSet  ds = new DataSet();
        ds = (DataSet)ViewState["studentinfo"];
        foreach (DataRow dr1 in ds.Tables["studentinfo"].Rows)
        {
            if (dr1[4].ToString() != string.Empty && dr1[5].ToString() != string.Empty)
            {
               string allmobile = dr1[1].ToString();
               string allmails = dr1[2].ToString();
               string marks = "Sub:" + dr1[4].ToString() + ",IN:" + dr1[5].ToString() + ",Ex:" + dr1[6].ToString() + ",T:" + dr1[8].ToString() + ",C:" + dr1[7].ToString();
                //sendmsg(marks, allmobile);
                SendMail(allmails, marks);
                //sendmessage(allmobile,marks);
                Thread.Sleep(5000);

            }

            Label2.Text = "Mail Send Successfully";

        }
       
    }
    public void sendmessage( string to,string message)
    {

        //this.req = (HttpWebRequest)WebRequest.Create("http://www.9nodes.com/API/sendsms.php?username=coign.consultant@gmail.com&password=Knowdedge&from=9885848369&to=" + to + "&msg="+message+"&type=1");
        this.req = (HttpWebRequest)WebRequest.Create("http://bulk.rocktosms.com/api/web2sms.php?workingkey=16785790v644p85j5o1w&sender=BULKSMS&to=" + to + "&message=" + message + "");
        this.req.AllowAutoRedirect = false;

        this.req.CookieContainer = this.cookieCntr;

        this.req.Method = "POST";

        this.req.ContentType = "application/x-www-form-urlencoded";

        // this.strNewValue = "http://www.9nodes.com/API/sendsms.php?username=coign.consultant@gmail.com&password=Knowdedge&from=9885848369&to=" + to + "&msg="+message+"&type=1 ";
        this.strNewValue = "http://bulk.rocktosms.com/api/web2sms.php?workingkey=16785790v644p85j5o1w&sender=BULKSMS&to=" + to + "&message=" + message + "";
        this.req.ContentLength = this.strNewValue.Length;

        StreamWriter writer = new StreamWriter(this.req.GetRequestStream(), System.Text.Encoding.ASCII);

        writer.Write(this.strNewValue);

        writer.Close();

        this.response = (HttpWebResponse)this.req.GetResponse();

        this.response.Close();

        //Succes





    }
}
