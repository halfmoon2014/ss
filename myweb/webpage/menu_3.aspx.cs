using System;
using EI.Web;
public partial class webpage_menu_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebMenu em = new WebMenu();                 
        menubody.InnerHtml = em.GetCont();        
    }
}