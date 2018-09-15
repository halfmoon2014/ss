using System;
using EI.Web;
using MyTy;

public partial class webpage_menu_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebMenu em = new WebMenu();
        if (!RequestExtensions.IsMobileBrowser(Request))
            menubody.Attributes.Add("class", "easyui-layou");
        else
        {
            menubody.Style.Add("padding-top", "70px");
            menubody.Style.Add("padding-bottom", "50px");
        }                   
        menubody.InnerHtml = em.GetCont(Request.PhysicalApplicationPath);        
    }
}