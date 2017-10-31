<%@ WebHandler Language="C#" Class="MyHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using MyTy;
public class MyHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        int id = 0; string type = "";
        if (context.Request.QueryString["id"] != null)
        {
            id = int.Parse(context.Request.QueryString["id"].ToString());
        }
        else if (context.Request.Form["id"] != null)
        {
            id = int.Parse(context.Request.Form["id"].ToString());
        }
        if (context.Request.QueryString["type"] != null)
        {
            type = context.Request.QueryString["type"].ToString();
        }
        else if (context.Request.Form["type"] != null)
        {
            type = context.Request.Form["type"].ToString();
        }
        int page = 1; int rows = 10;
        if (context.Request.Form["page"] != null)
        {
            page = int.Parse(context.Request.Form["page"].ToString().Trim());
        }
        if (context.Request.Form["rows"] != null)
        {
            rows = int.Parse(context.Request.Form["rows"].ToString().Trim());
        }
        string lx = "";
        if (context.Request.QueryString["lx"] != null)
        {
            lx = context.Request.QueryString["lx"].ToString();
        }
        else if (context.Request.Form["lx"] != null)
        {
            lx = context.Request.Form["lx"].ToString();
        }
        string js = "";
        if (context.Request.QueryString["js"] != null)
        {
            js = context.Request.QueryString["js"].ToString();
        }
        else if (context.Request.Form["js"] != null)
        {
            js = context.Request.Form["js"].ToString();
        }
        string sql = "";
        if (context.Request.QueryString["sql"] != null)
        {
            sql = context.Request.QueryString["sql"].ToString();
        }
        else if (context.Request.Form["sql"] != null)
        {
            sql = context.Request.Form["sql"].ToString();
        }
        string wid = "";
        if (context.Request.QueryString["wid"] != null)
        {
            wid = context.Request.QueryString["wid"].ToString();
        }
        else if (context.Request.Form["wid"] != null)
        {
            wid = context.Request.Form["wid"].ToString();
        }
        string myname = "";
        if (context.Request.QueryString["myname"] != null)
        {
            myname = context.Request.QueryString["myname"].ToString();
        }
        else if (context.Request.Form["myname"] != null)
        {
            myname = context.Request.Form["myname"].ToString();
        }

        int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
        int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
        FM.Business.Login lg = new FM.Business.Login();
        string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();

        EI.Web.WebEdit em = new EI.Web.WebEdit(tzid.ToString(), userid.ToString(), username);
        string s = "";
        switch (type)
        {
            case "GetTree":
                s = em.GetTree();
                break;
            case "GetCTable":
                s = em.GetCTable(id, MyCode.MySysDate(lx), MyCode.MySysDate(js), MyCode.MySysDate(sql), MyCode.MySysDate(wid), MyCode.MySysDate(myname), page, rows);
                break;
        }
        context.Response.Write(s);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}