using System;
using Service.Util;
public partial class web_xtsz_main_edit_zdwh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        if (string.Compare(ei.GetDataTag(), "true") != 0)
        {
            btnGroup.Controls.Remove(fb);
        }
    }
    public string HtmlCha(string str)
    {
        return MyTy.Utils.HtmlCha(str);
    }
}