using System;
using System.Web;
/// <summary>
///MyHttpModule 的摘要说明
/// </summary>
public class MyHttpModule : IHttpModule
{
    public MyHttpModule()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region IHttpModule 成员

    public void Dispose()
    {

    }

    public void Init(HttpApplication application)
    {
        application.BeginRequest += new EventHandler(Application_BeginRequest);
        application.EndRequest += new EventHandler(Application_EndRequest);
        application.AcquireRequestState += (new EventHandler(this.Application_AcquireRequestState));
    }

    private void Application_BeginRequest(object sender, EventArgs e)
    {
    
    }

    private void Application_EndRequest(object sender, EventArgs e)
    {

    }
    private void Application_AcquireRequestState(Object source, EventArgs e)
    {

    }


    #endregion
}