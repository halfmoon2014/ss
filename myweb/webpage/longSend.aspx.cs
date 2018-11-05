using Comet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webpage_longSend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        string to = Request.QueryString["t"].ToString();
        string data = Request.QueryString["d"].ToString();
       Response.Write( LongSataMrg.Send(to,data));

    }
}