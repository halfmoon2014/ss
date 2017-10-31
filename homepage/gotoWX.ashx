<%@ WebHandler Language="C#" Class="gotoWX" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class gotoWX : IHttpHandler
{    
    public void ProcessRequest(HttpContext context){
        WXJumpOuterSite jump = new WXJumpOuterSite(@"http://www.157.hk:4529/other/Default2_wxcs.aspx?");
        context.Response.Write(jump.Post(context.Request));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
   
    
}