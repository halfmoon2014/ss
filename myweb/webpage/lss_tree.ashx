<%@ WebHandler Language="C#" Class="lss_tree" %>

using System;
using System.Web;
using System.Web.SessionState;
using Service.Util;
/// <summary>
/// 构造树
/// </summary>
public class lss_tree : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string s = "";
        string bz = "";
        if (context.Request.QueryString["bz"] != null)
        {
            bz = context.Request.QueryString["bz"].ToString();
        }
        else if (context.Request.Form["bz"] != null)
        {
            bz = context.Request.Form["bz"].ToString();
        }

        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));

        s = ei.GetMyEuiTree(bz);
        context.Response.Write(s);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}