using System;
using MySession;
using System.Data;
using DTO;
using MyTy;

public partial class ChooseTz : FM.Controls.Page
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        //默认主页为menu_,数据库>自定义
        FM.Business.ChooseTz tz = new FM.Business.ChooseTz();
        HtmlMenu htmlMenu = tz.GetTzMenu(Request.PhysicalApplicationPath,"menu_");
        container.InnerHtml = htmlMenu.Htmlmark;
    }
}