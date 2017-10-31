using System;
using Service.Util;

public partial class web_xtsz_web_xtsz_main_edit_help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {               
        string[] rstring = new string[2];
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        rstring = ei.GettContEditHelp(int.Parse(wid.Value));
        tbhelp.Value = rstring[0];        
    }
}