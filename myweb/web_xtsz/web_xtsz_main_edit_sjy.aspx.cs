using Service.Util;
using System;
using DTO;
public partial class web_xtsz_main_edit_sjy : FM.Controls.Page
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        wid.Value = Request.QueryString["wid"].ToString().Trim();
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        PageDataSource pageDataSource  = ei.GettContEdit(int.Parse(wid.Value));

        name.Value = pageDataSource.Name;
        tbsql.Value = pageDataSource.Sql;
        fwsql.Value = pageDataSource.Fwsql;
        mxgl.Value = pageDataSource.Mxgl;
        mxsql.Value = pageDataSource.Mxsql;
        mxhgl.Value = pageDataSource.Mxhgl;
        mxhord.Value = pageDataSource.Mxhord;
        mxhsql.Value = pageDataSource.Mxhsql;
        mxly.Value = pageDataSource.Mxly;
        tbsql2.Value = pageDataSource.Sql_2;
        if (pageDataSource.Mrcx == 1)  mrcx.Checked = true;

        pagesize.Value = pageDataSource.Pagesize.ToString();
        orderby.Value = pageDataSource.Orderby;
        if (pageDataSource.Myadd == 1)  myadd.Checked = true;

        if (string.Compare(ei.GetDataTag(), "true") != 0)
            btnGroup.Controls.Remove(fb);
        
    }
}