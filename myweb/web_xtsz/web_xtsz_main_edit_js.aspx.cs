using System;
using Service.Util;

public partial class web_xtsz_web_xtsz_main_edit_js : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        string[] rstring = new string[2];
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        rstring = ei.GettContEditJs(int.Parse(wid.Value));
        tbjs.Value = rstring[0];
        if (string.Compare(ei.GetDataTag(), "true") != 0)
        {
            btnGroup.Controls.Remove(fb);
        }
    }
}