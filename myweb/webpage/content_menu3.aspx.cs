using System;
public partial class content_menu3 : FM.Controls.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        EI.Web.WebMenu mu = new EI.Web.WebMenu();
        ///*20130607改为AJAX调用20130609停用,因为SESSION超时不好处理*/
        //Response.Write(mu.GetContentMenu());
        //Response.End();
        if (Request.QueryString["m"] != null && Request.QueryString["m"].ToString() == "data")
        {
            Response.Write(mu.GetContentMenu());
            Response.End();
        }
        else
            content_menu3_mydiv.InnerHtml = mu.GetContentMenu();
    }
}