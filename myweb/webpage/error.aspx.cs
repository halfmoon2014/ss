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
            error.InnerHtml = errMsg;
            String myScript = "<script type=\"text/javascript\">"+
                "window.onload=function(){"+
                "  try {"+
                "      console.log(document.getElementById('error').value);"+
                "  } catch (e) { "+
                "     alert(e.message);"+
                "  }"+
                "};"+
                "</script>";
            Response.Write(myScript);            
            //error.InnerHtml =errMsg ;
        }
        else
        {
            error.InnerHtml = "登陆异常";
        }
    }
}