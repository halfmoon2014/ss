using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webpage_menu_4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EI.Web.WebMenu em = new EI.Web.WebMenu();
        menubody.InnerHtml = em.GetCont(MySession.SessionHandle.Get("userid"), MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("menupage"));
        
    }
}