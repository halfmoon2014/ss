using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_ls_cpdaxx_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        myid.Value = Request.QueryString["id"].ToString().Trim();        
        lx.Value = Request.QueryString["lx"].ToString().Trim();
        zt.Value = Request.QueryString["zt"].ToString().Trim();
        if (Request.QueryString["mc"] != null)
        {
            mc.Value = (Request.QueryString["mc"].ToString().Trim());
        }

    }
}