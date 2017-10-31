using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
public partial class main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Application["errMsg"]!= null)
        {
            string errMsg = Application["errMsg"].ToString();
            Application.Remove("errMsg");
            error.InnerHtml = "错误信息:</br>&nbsp;&nbsp;&nbsp;&nbsp;<textarea id='textarea1'readonly style='width:100%;height:600px;border-width: 0px;' >" + errMsg + "</textarea>";
        }
        else
        {
            error.InnerHtml = "登陆异常";
        }
    }
}