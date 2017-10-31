using System;

public partial class webpage_menu_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        string ip = "";
        if (Context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
        {
            ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
        }
        else// not using proxy or can't get the Client IP
        {
            ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
        }

        EI.Web.WebMenu em = new EI.Web.WebMenu();
        em.Log(ip, "menu");            
        menubody.InnerHtml = em.GetCont(MySession.SessionHandle.Get("userid"), MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("menupage"));        
    }
}