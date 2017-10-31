<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.DataBase;
using System.Data.SqlClient;

using MyTy;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    
    /// <summary>
    /// 用户登陆
    /// </summary>
    /// <param name="ur"></param>
    /// <param name="ps"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string Login(string ur, string ps)
    {
        ur = MyTy.MyCode.MySysDate(ur);
        ps = MyTy.MyCode.MySysDate(ps);
        FM.Business.Login lg = new FM.Business.Login();
        return "{r:'" + lg.UserLogin(ur, ps) + "'}";
    }
    
    /// <summary>
    /// 套账选择的时候使用
    /// </summary>
    /// <param name="tzid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string ChooseTz(string tzid)
    {        
        FM.Business.ChooseTz ch = new FM.Business.ChooseTz();
        ch.AddTzid(tzid);        
        return "{r:'true'}";
    }    
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="username"></param>
    /// <param name="oldPassWord"></param>
    /// <param name="newPassWord"></param>
    /// <param name="adminTag"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string SetUserPsw(string username, string oldPassWord, string newPassWord, string adminTag)
    {
        username = MyTy.MyCode.MySysDate(username);//用户名
        oldPassWord = MyTy.MyCode.MySysDate(oldPassWord);//原密码
        newPassWord = MyTy.MyCode.MySysDate(newPassWord);//现密码
        adminTag = MyTy.MyCode.MySysDate(adminTag);//管理员标识
        FM.Business.Login lg = new FM.Business.Login();
        return lg.SetUserPsw(username, oldPassWord, newPassWord, adminTag);
    }

}