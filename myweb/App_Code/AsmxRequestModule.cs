using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// AsmxRequestModule 的摘要说明
/// </summary>
public class AsmxRequestModule : IHttpModule
{
    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(Application_BeginRequest);
    }

    public void Dispose()
    {
    }

    public void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpApplication application = sender as HttpApplication;
        string extension = Path.GetExtension(application.Request.Path);
        if (application.Request.Path.IndexOf(".asmx/") > -1 && application.Request.Path.EndsWith("/json"))
        {
            application.Response.Filter = new CatchTextStream(application.Response.Filter);
        }
    }
}