﻿using System;
using System.Web;
using MySession;
using MyTy;
using Log4NetApply;
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
    public void Dispose(){}

    public void Init(HttpApplication application)
    {
        log4net.Config.XmlConfigurator.Configure();
        application.BeginRequest += new EventHandler(Application_BeginRequest);
        application.EndRequest += new EventHandler(Application_EndRequest);
        application.AcquireRequestState += (new EventHandler(this.Application_AcquireRequestState));
    }

    private void Application_BeginRequest(object sender, EventArgs e){}

    private void Application_EndRequest(object sender, EventArgs e){}

    /// <summary>
    /// 管控session
    /// </summary>
    /// <param name="httpApplication"></param>
    /// <param name="e"></param>
    private void Application_AcquireRequestState(Object httpApplication, EventArgs e)
    {
        DateTime dt1 = DateTime.Now;
        HttpApplication application = (HttpApplication)httpApplication;
        MyCode myCode = new MyCode();
        //获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径
        string applicationPath = application.Context.Request.ApplicationPath.ToString().Trim();
        string absolutePath = application.Context.Request.Url.AbsolutePath.Remove(0, applicationPath.Length);
        string xml = application.Server.MapPath("~/config.xml");
        string loginFileName = ConfigReader.Read(xml, "/Root/WebFile/Login/FileName", "");
        string chooseTzFileName = ConfigReader.Read(xml, "/Root/WebFile/ChooseTz/FileName", "");
        string ip;
        if (application.Context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
            ip = application.Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
        else// not using proxy or can't get the Client IP
            ip = application.Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.

        LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent(ip, SessionHandle.Get("userid"), "Application_AcquireRequestState", absolutePath));
        
        //System.Diagnostics.Debug.Write("MyHttpModule TotalSeconds:"+ absolutePath);
        //if(absolutePath!= "webpage/lss.aspx")  application.Response.End();
        if (myCode.CheckPageType(absolutePath, "NoLimitUrl"))
        {
            #region 不受session控制的页面
            LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent(ip, SessionHandle.Get("userid"), "Application_AcquireRequestState", "NoLimitUrl "+absolutePath));
            #endregion
        }
        else if (myCode.CheckPageType(absolutePath, "Login"))
        {
            #region 登陆页面
            LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent(ip, SessionHandle.Get("userid"), "Application_AcquireRequestState", "login " + absolutePath));
            if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") != null)
            {                
                application.Response.Redirect("~/" + SessionHandle.Get("menupage"));
            }
            else if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") == null)
            {                
                LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent(ip, SessionHandle.Get("userid"), "Application_AcquireRequestState", chooseTzFileName));
                application.Response.Redirect("~/" + chooseTzFileName);
            }
            #endregion
        }
        else if (myCode.CheckPageType(absolutePath, "ChooseTz"))
        {
            #region 套账页面
            if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") != null)
            {
                string redirectURL ;
                if (SessionHandle.Get("urlreferrer") != null)
                {//超时重登陆
                    redirectURL = SessionHandle.Get("urlreferrer").ToString();
                    SessionHandle.Del("urlreferrer");
                    application.Response.Redirect(redirectURL);
                }
                else
                {
                    //直接输入CHOOSETZ.ASPX地址
                    if (application.Request.UrlReferrer == null)
                    {
                        redirectURL = SessionHandle.Get("menupage").ToString();
                        application.Response.Redirect(redirectURL);
                    }
                    //更改套账
                    else
                    {
                        SessionHandle.Del("tzid");
                        //redirectURL = "~/choosetz.aspx";
                    }
                }
                
            }
            else if (SessionHandle.Get("userid") == null)
            {
                application.Response.Redirect("~/" + loginFileName);
            }
            #endregion
        }
        else if (myCode.CheckPageType(absolutePath, "MenuPage"))
        {
            #region 如果是主页
            //保存获取当前主页!
            SessionHandle.Add("menupage", "/" + absolutePath);

            if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") != null)
            {

                if (SessionHandle.Get("urlreferrer") != null)
                {
                    string gourl = SessionHandle.Get("urlreferrer").ToString();
                    SessionHandle.Del("urlreferrer");
                    application.Response.Redirect(gourl);
                }                
            }
            else if (SessionHandle.Get("userid") == null)
            {
                application.Response.Redirect("~/" + loginFileName);
            }
            #endregion
        }
        else if ((absolutePath.ToUpper().Contains(".ASPX")))
        {
            #region 普通页,需受到session的限制
            if (SessionHandle.Get("userid") == null || SessionHandle.Get("tzid") == null)
            {
                //tzid session丢失或user session丢失             
                //对于普通面页,不包含用户登陆与套账选择页面!             
                //带参数地址,用于重新登陆后再次打开                
                SessionHandle.Add("UrlReferrer", application.Context.Request.RawUrl.ToString().Trim());
                application.Response.Redirect("~/" + loginFileName);
            }
            else
            {
                //获取客户访问的页面
                string module = "";//根据url得到所在的模块       
                if (!RightChecker.HasRight(SessionHandle.Get("userid").ToString(), module))
                {
                    application.Context.Server.Transfer("ErrorPage.aspx");
                }

            }
            #endregion
        }

        DateTime dt2 = DateTime.Now;
        System.Diagnostics.Debug.WriteLine("MyHttpModule TotalSeconds:" + absolutePath+ dt2.Subtract(dt1).TotalSeconds);        
    }
    #endregion
}