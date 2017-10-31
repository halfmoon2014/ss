using System;
using System.Collections.Generic;
using Service.Util;

public partial class web_xtsz_web_xtsz_main_edit_sjy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {         
       
        wid.Value = Request.QueryString["wid"].ToString().Trim();
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        Dictionary<string, string> dic = ei.GettContEdit(int.Parse(wid.Value));

        name.Value = dic["name"];
        tbsql.Value = dic["sql"];
        fwsql.Value = dic["fwsql"];
        mxgl.Value = dic["mxgl"];
        mxsql.Value = dic["mxsql"];
        mxhgl.Value = dic["mxhgl"];
        mxhord.Value = dic["mxhord"];
        mxhsql.Value = dic["mxhsql"];
        mxly.Value = dic["mxly"];
        tbsql2.Value = dic["sql_2"];
        if (dic["mrcx"] == "1")
        {
            mrcx.Checked = true;
        }
        pagesize.Value = dic["pagesize"];
        orderby.Value = dic["orderby"];
        if (dic["myadd"] == "1")
        {
            myadd.Checked = true;
        }
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