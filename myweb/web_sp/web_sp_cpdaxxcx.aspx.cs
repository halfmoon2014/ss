using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_ls_web_ls_cpdaxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["khlbid"]!=null){
            khlbid.Value = Request.QueryString["khlbid"].ToString().Trim();
        }        
    }
}