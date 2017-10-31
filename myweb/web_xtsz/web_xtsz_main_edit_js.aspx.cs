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
        string db = ei.GetDataTag();
        if (db == "true")
        {
            fb.InnerHtml = "<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" id=\"fb\">发布</a>";
        }
        else
        {
            fb.InnerHtml = "&nbsp;";
        }
    }
}