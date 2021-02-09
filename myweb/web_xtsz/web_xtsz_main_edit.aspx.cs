using System;
using System.Web;
using FM.Business;
using EI.Web;
public partial class web_xtsz_web_xtsz_main_edit : FM.Controls.Page
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
        int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
        string title = "";

        Login lg = new Login();
        string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();
        WebEdit em = new WebEdit(tzid.ToString(), userid.ToString(), username, Request.Browser.Browser);
        if (string.Compare("default", Request.QueryString["title"].ToString().Trim(), true) == 0)
            title = em.GettWidTitle(int.Parse(Request.QueryString["wid"].ToString().Trim()));
        else
            title = Request.QueryString["title"].ToString().Trim();
        editbody.InnerHtml = em.GetContEdit(title, HttpContext.Current.Server.MapPath("."));
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        sysHead.Title = title +   "【" + wid.Value + "】";
    }
}