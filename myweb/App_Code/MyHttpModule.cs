using System;
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

    public void Dispose()
    {

    }

    public void Init(HttpApplication application)
    {
        log4net.Config.XmlConfigurator.Configure();
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
    /// <summary>
    /// 管控session
    /// </summary>
    /// <param name="httpApplication"></param>
    /// <param name="e"></param>
    private void Application_AcquireRequestState(Object httpApplication, EventArgs e)
    {
        HttpApplication application = (HttpApplication)httpApplication;
        Log log = new Log();
        //获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径
        string applicationPath = application.Context.Request.ApplicationPath.ToString().Trim();
        string absolutePath = application.Context.Request.Url.AbsolutePath.Remove(0, applicationPath.Length);
        string xml = application.Server.MapPath("~/config.xml");
        string loginFileName = ConfigReader.Read(xml, "/Root/WebFile/Login/FileName", "");
        string chooseTzFileName = ConfigReader.Read(xml, "/Root/WebFile/ChooseTz/FileName", "");
        LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent("", "", "Application_AcquireRequestState", absolutePath));
        if (ConfigReader.CheckInnerText(xml, "/Root/NoLimitUrl/Site", absolutePath))
        {
            #region 不受session控制的页面
            log.WriteLog("MyHttpModule:" + absolutePath, "NoLimitUrl");
            #endregion
        }
        else if (string.Compare(loginFileName, absolutePath) == 0)
        {
            #region 登陆页面
            log.WriteLog("MyHttpModule", "login");
            if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") != null)
            {
                application.Response.Redirect("~/" + SessionHandle.Get("menupage"));
            }
            else if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") == null)
            {
                application.Response.Redirect("~/" + chooseTzFileName);
            }
            #endregion
        }
        else if (string.Compare(chooseTzFileName, absolutePath) == 0)
        {
            #region 套账页面
            if (SessionHandle.Get("userid") != null && SessionHandle.Get("tzid") != null)
            {
                string redirectURL = "";
                if (SessionHandle.Get("urlreferrer") != null)
                {//超时重登陆
                    redirectURL = SessionHandle.Get("urlreferrer").ToString();
                    SessionHandle.Del("urlreferrer");
                }
                else
                {
                    if (application.Request.UrlReferrer == null)
                    {/*直接输入CHOOSETZ.ASPX地址*/
                        redirectURL = SessionHandle.Get("menupage").ToString();
                    }
                    else
                    {/*更改套账*/
                        SessionHandle.Del("tzid");
                        redirectURL = "~/choosetz.aspx";
                    }
                }
                application.Response.Redirect(redirectURL);
            }
            else if (SessionHandle.Get("userid") == null)
            {
                application.Response.Redirect("~/" + loginFileName);
            }
            #endregion

        }
        else if (ConfigReader.CheckInnerText(xml, "/Root/WebFile/MenuPage/FileName", absolutePath))
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
                else
                {
                    FM.Business.Login lg = new FM.Business.Login();
                    lg.CreateDbLink();//设置业务服务器上的 连接 主服务与母板的LINK
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
                if (!RightChecker.HasRight(application.Context.Session["userid"].ToString(), module))
                {
                    application.Context.Server.Transfer("ErrorPage.aspx");
                }

            }
            #endregion
        }


    }


    #endregion
}