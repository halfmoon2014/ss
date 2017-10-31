<%@ WebService Language="C#" Class="longPolling" %>
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.DataBase;
using System.Web.Script.Services;
using System.Collections.Generic;
using MyTy;
using Service.Util;
using System.Threading;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class longPolling : System.Web.Services.WebService
{


    [WebMethod(EnableSession = true)]
    public string g(string timed)
    {
        //Thread t = new Thread(test);
        //t.Start();
        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("wid", "-1");
        arg.Add("callFucntion", "test");
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        Dictionary<string, string> dic = new Dictionary<string, string>();
        DateTime start = DateTime.Now;
        string msg = "false";
        while (true)
        {
            //TimeSpan ts = DateTime.Now - start;
            //if (ts.TotalMilliseconds > 5000)
            //{
            //    break;
            //}
            dic = ei.execSqlCommand("select 1 from _V_sp_cpjhb where bz='hello'", "off", arg);
            if (dic["resultState"] == "true" && dic["resultText"] == "1")
            {

                ei.execSqlCommand("update  _V_sp_cpjhb set bz='hellogx' where bz='hello'", "off", arg);
                msg = "true";
                break;

            }
            else
            {
                Thread.Sleep(1000);
            }
        }
        return msg;
    }



}