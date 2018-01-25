using System;
using System.Data;

public partial class webpage_m_myhelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EI.Web.WebMenu mu = new EI.Web.WebMenu();         
        DataTable dt = mu.GetHelp(Request.QueryString["id"].ToString().Trim()).Tables[0];
        myid.Value = Request.QueryString["id"].ToString().Trim();
        helpText.InnerText = dt.Rows[0]["help"].ToString();
    }
}