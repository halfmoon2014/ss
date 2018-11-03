using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_xtsz_main_add : FM.Controls.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        userid.Value = Request.QueryString["userid"].ToString().Trim();
        zt.Value = Request.QueryString["zt"].ToString().Trim();
        if (Request.QueryString["wid"] != null)
        {
            wid.Value = Request.QueryString["wid"].ToString().Trim();
        }
        if (Request.QueryString["mc"] != null)
        {
            mc.Value = Request.QueryString["mc"].ToString().Trim();
        }
        if (Request.QueryString["lx"] != null)
        {
            lx.Value = Request.QueryString["lx"].ToString().Trim();
        }
    }
}