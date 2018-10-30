using System;
using EI.Web;
using MyTy;

public partial class webpage_menu_3 : FM.Controls.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        WebMenu em = new WebMenu();

        //手机
        if (RequestExtensions.IsMobileBrowser(Request))
        {
            menubody.Style.Add("padding-top", "70px");
            menubody.Style.Add("padding-bottom", "50px");
        }            
        else
            menubody.Attributes.Add("class", "easyui-layou");

        menubody.InnerHtml = em.GetCont(Request.PhysicalApplicationPath);        
    }
}