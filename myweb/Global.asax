<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码

    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码
        Exception exception = Server.GetLastError();
        string errorMsg = "";
        MyTy.Log.WriteLog("error", (exception.InnerException == null ? "" : exception.InnerException.Message) + exception.Message + "\r\n" + exception.StackTrace);

        if (MySession.SessionHandle.Get("userid") == null)
        {
            errorMsg = "登陆超时，请联系管理员!";
        }
        else
        {
            FM.Business.Login lg = new FM.Business.Login();
            System.Data.DataSet ds = lg.GetUser(MySession.SessionHandle.Get("userid").ToString().Trim());
            if (ds.Tables[0].Rows[0]["platform_edit_permission"].ToString().Trim() == "1")
            {
                errorMsg = (exception.InnerException == null ? "" : exception.InnerException.Message + "\r\n") + exception.Message + "\r\n" + exception.StackTrace;
            }
            else
            {
                errorMsg = "登陆异常，请联系管理员!";
            }
        }
        //string errorFile=@"../webpage/error.aspx";
        Application.Add("errMsg", errorMsg);

        if (System.IO.File.Exists(Server.MapPath("~/webpage/error.aspx")))
        {
            Server.Transfer("~/webpage/error.aspx");
        }
        Server.ClearError();
    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }

</script>
