using System;
using Service.Util;
public partial class web_xtsz_web_xtsz_main_edit_zdwh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
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
    public string HtmlCha(string str)
    {
        return MyTy.Utils.HtmlCha(str);
    }
}