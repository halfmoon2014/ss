using System;
public partial class webpage_content_menu3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EI.Web.WebMenu mu = new EI.Web.WebMenu();
        ///*20130607改为AJAX调用20130609停用,因为SESSION超时不好处理*/
        //Response.Write(mu.GetContentMenu());
        //Response.End();
        
        content_menu3_mydiv.InnerHtml = mu.GetContentMenu();
    }
}