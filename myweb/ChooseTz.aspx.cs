using System;
using MySession;
using System.Data;
using DTO;
using MyTy;

public partial class ChooseTz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        //默认主页为menu_,数据库>自定义
        
        FM.Business.ChooseTz tz = new FM.Business.ChooseTz();
        HtmlMenu htmlMenu = tz.GetTzMenu(Request.PhysicalApplicationPath,"menu_");
        container.InnerHtml = htmlMenu.Htmlmark;        
        //if (mystring[0] == "Response")
        //{
        //    Response.Redirect(mystring[1]);
        //} else 
        //{
        //    container.InnerHtml = mystring[1];
        //}
    }
}