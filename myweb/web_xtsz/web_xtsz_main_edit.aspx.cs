using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using EI.Web;

public partial class web_xtsz_web_xtsz_main_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
        int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
        FM.Business.Login lg = new FM.Business.Login();
        string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();
        EI.Web.WebEdit em = new EI.Web.WebEdit(tzid.ToString(),userid.ToString(),username);
        editbody.InnerHtml = em.GetContEdit(Request.QueryString["title"].ToString().Trim(), HttpContext.Current.Server.MapPath("."));
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        sysHead.TITLE = "页面设计-" + Request.QueryString["title"].ToString().Trim();

    }
}