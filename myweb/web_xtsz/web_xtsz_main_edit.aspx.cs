using System;
using System.Web;
using FM.Business;
using EI.Web;
public partial class web_xtsz_web_xtsz_main_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
        int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
        Login lg = new Login();
        string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();
        WebEdit em = new WebEdit(tzid.ToString(),userid.ToString(),username);
        editbody.InnerHtml = em.GetContEdit(Request.QueryString["title"].ToString().Trim(), HttpContext.Current.Server.MapPath("."));
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        if (string.Compare("default", Request.QueryString["title"].ToString().Trim(), true)==0)
        {
            sysHead.Title = "E" + wid.Value;
        }
        else
        {
            sysHead.Title = "E"+ wid.Value+ "【" + Request.QueryString["title"].ToString().Trim()+ "】";
        }
    }
}