using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default23 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //连接数据库返回数据
        Response.Write("[{ \"name\": \"Violet\", \"occupation\": \"character\" },{ \"name\": \"Violet\", \"occupation\": \"character\" }]"); 

    }
}