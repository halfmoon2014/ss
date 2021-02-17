<%@ WebService Language="C#" Class="Ws" %>

using System.Web.Services;
using System.Data;
using Service.Util;
using System.Collections.Generic;
using MyTy;
using DTO;
using Newtonsoft.Json;
using MySession;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class Ws : System.Web.Services.WebService
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

        FM.Business.Login lg = new FM.Business.Login();
        int userid = lg.UserLogin(MyCode.MySysDate(ur), MyCode.MySysDate(ps));
        if (userid == 0)
            return "{\"r\":\"" + "false" + "\"}";
        else
        {
            SessionHandle.Add("userid", userid.ToString());
            TokenItem item = TokenHandle.AddUserid(userid);
            return "{\"r\":\"" + "true" + "\",\"token\":\"" + item.Token + "\"}";
        }

    }

    [WebMethod(EnableSession = true, MessageName = "Login/json")]
    public string LoginJson(string ur, string ps)
    {
        FM.Business.Login lg = new FM.Business.Login();
        int userid = lg.UserLogin(MyCode.MySysDate(ur), MyCode.MySysDate(ps));
        if (userid != 0)
        {
            TokenItem item = TokenHandle.AddUserid(userid);
            return JsonConvert.SerializeObject(ResultUtil<TokenItem>.Success(item));
        }
        else
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "登陆失败,请检查用户名与密码是否正确!"));
    }

    [WebMethod(EnableSession = true, MessageName = "TzList/json")]
    public string TzList(string token)
    {
        FM.Business.ChooseTz tz = new FM.Business.ChooseTz();
        TokenItem item = TokenHandle.GetToken(token);
        if (item == null)
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "请先登陆"));
        else
            return JsonConvert.SerializeObject(ResultUtil<DataTable>.Success(tz.GetTzMenuJson(item.Userid)));
    }

    /// <summary>
    /// 套账选择的时候使用
    /// </summary>
    /// <param name="tzid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string ChooseTz(string tzid, string updata)
    {
        FM.Business.ChooseTz ch = new FM.Business.ChooseTz();
        ch.AddTzid(tzid);
        TokenItem item = TokenHandle.GetTokenByUserid(int.Parse(SessionHandle.Get("userid")));
        item.Tzid = int.Parse(tzid);
        if (string.Compare(updata, "true") == 0)
        {
            FM.Business.Login lg = new FM.Business.Login();
            lg.CreateDbLink(int.Parse(tzid));//设置业务服务器上的 连接 主服务与母板的LINK
        }
        return "{\"r\":\"true\"}";
    }
    [WebMethod(EnableSession = true, MessageName = "ChooseTz/json")]
    public string ChooseTzJson(string token, int tzid, bool updata)
    {
        TokenItem item = TokenHandle.GetToken(token);
        item.Tzid = tzid;
        if (updata)
        {
            FM.Business.Login lg = new FM.Business.Login();
            lg.CreateDbLink(tzid);//设置业务服务器上的 连接 主服务与母板的LINK
        }
        return JsonConvert.SerializeObject(ResultUtil.Success());
    }

    [WebMethod(EnableSession = true, MessageName = "Menu/json")]
    public string MenuList(string token)
    {
        TokenItem item = TokenHandle.GetToken(token);
        if (item == null)
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "请先登陆"));
        else if (item.Tzid == 0)
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "请先选择套账"));
        else
        {
            FM.Business.Menu menu = new FM.Business.Menu(item.Tzid.ToString(), item.Userid.ToString());
            return JsonConvert.SerializeObject(ResultUtil<DataSet>.Success(menu.GetUserMenu(item.Userid.ToString())));
        }
    }
    [WebMethod(EnableSession = true, MessageName = "MenuItem/json")]
    public string MenuItemList(string token, int ssid)
    {
        TokenItem item = TokenHandle.GetToken(token);
        if (item == null)
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "请先登陆"));
        else if (item.Tzid == 0)
            return JsonConvert.SerializeObject(ResultUtil.Error(100, "请先选择套账"));
        else
        {
            FM.Business.Menu menu = new FM.Business.Menu(item.Tzid.ToString(), item.Userid.ToString());
            return JsonConvert.SerializeObject(ResultUtil<DataTable>.Success(menu.GetContentMenu(item.Userid.ToString(), ssid.ToString())));
        }

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

    public Business GetBusiness()
    {
        return new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
    }
    /// <summary>
    /// 保存菜单的帮助文档
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string HelpUp(string value1, string value2)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.UpHelp(value1.Trim(), value2) + "\"}";
    }
    /// <summary>
    /// 执行表创建视图过程
    /// </summary>
    /// <param name="value1"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string AutoView(string value1)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.AutoView(value1) + "\"}";
    }
    /// <summary>
    /// WEB设计数据源保存
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="value3"></param>
    /// <param name="value4"></param>
    /// <param name="mrcx"></param>
    /// <param name="myadd"></param>
    /// <param name="orderby"></param>
    /// <param name="pagesize"></param>
    /// <param name="mxgl"></param>
    /// <param name="mxsql"></param>
    /// <param name="mxhgl"></param>
    /// <param name="mxhord"></param>
    /// <param name="mxhsql"></param>
    /// <param name="mxly"></param>
    /// <param name="sql_2"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string SjyUp(int wid, string value1, string value3, string value4, int mrcx, int myadd, string orderby, int pagesize, string mxgl, string mxsql, string mxhgl, string mxhord, string mxhsql, string mxly, string sql_2)
    {
        Business ei = GetBusiness();
        CacheTools.WidUpdateDep(wid.ToString());
        PageDataSource pageDataSource = new PageDataSource { 
            Id=wid,
            Name=value1,
            Sql=value3,
            Fwsql=value4,
            Mrcx=mrcx,
            Myadd=myadd,
            Orderby=orderby,
            Pagesize=pagesize,
            Mxgl=mxgl,
            Mxsql=mxsql,
            Mxhgl=mxhgl,
            Mxhord=mxhord,
            Mxhsql=mxhsql,
            Mxly=mxly,
            Sql_2=sql_2
        };       
        return "{\"r\":\"" + ei.UpSJY(pageDataSource) + "\"}";
    }
    /// <summary>
    /// 页面布局
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string UpSYJLayout(string wid, string data)
    {
        Business ei = GetBusiness();
        CacheTools.WidUpdateDep(wid);
        return "{\"r\":\"" + ei.UpSYJLayout(data) + "\"}";
    }
    /// <summary>
    /// 面面字段
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string UpSYJZdwh(string wid, string data)
    {
        Business ei = GetBusiness();
        CacheTools.WidUpdateDep(wid);
        Result<string> result = ei.UpSYJZdwh(data);
        if (result.Errcode == 0)
            return "{\"r\":\"true\"}";
        else
            return "{\"r\":\"" + result.Errmsg.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";

    }

    /// <summary>
    ///  WEB设计js保存
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="js"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string SjyUpJS(string wid, string js)
    {
        Business ei = GetBusiness();
        CacheTools.WidUpdateDep(wid);
        return "{\"r\":\"" + ei.UpSJYJs(wid, js) + "\"}";
    }

    /// <summary>
    /// WEB设计help保存
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="help"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string SjyUpHelp(string wid, string help)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.UpSJYHelp(wid, help) + "\"}";
    }

    /// <summary>
    /// WEB设计 新增一个webid或者修改一个
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="mc"></param>
    /// <param name="lx"></param>
    /// <param name="wid"></param>
    /// <param name="zt"></param>
    /// <returns></returns>

    [WebMethod(EnableSession = true)]
    public string WebSjCl(string userid, string mc, string lx, string wid, string zt)
    {
        Business ei = GetBusiness();
        int r;
        if (zt == "add")
        {
            r = ei.AddSJYSJ(userid, mc, lx);
            if (r > 0)
                return "{\"r\":\"true\"}";
            else
                return "{\"r\":\"false\"}";
        }
        else
        {
            //edit                
            r = ei.EditSJYSJ(wid, mc, lx);
            if (r > 0)
                return "{\"r\":\"true\"}";
            else
                return "{\"r\":\"false\"}";
        }

    }
    /// <summary>
    /// WEB设计 删除webid
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string WebSjDel(string wid)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.DelSJYSJ(wid) + "\"}";
    }
    /// <summary>
    /// WEB设计 复制webid
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string WebSjFz(string wid)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.CopySJYSJ(wid) + "\"}";
    }

    /// <summary>
    /// 发布webid 或菜单
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string WebFb(string wid)
    {
        Business ei = GetBusiness();
        if (wid == "menu")
            return "{\"r\":\"" + ei.Web_fb_menu().Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
        else
            return "{\"r\":\"" + ei.Web_fb(wid).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
    }
    /// <summary>
    /// WEB设计 复制webid中的每项
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="newwid"></param>
    /// <param name="bs"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string WebsjFzZd(string wid, string newwid, string bs)
    {
        Business ei = GetBusiness();
        return "{\"r\":\"" + ei.CopyWebSJZD(wid, newwid, bs).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
    }

    [WebMethod(EnableSession = true)]
    //页面设计 字段处理 处理SQL语句
    //处理大类SQL
    public string ExecSqlCommand(string sqlCommand, string xact_abort, string wid)
    {
        Dictionary<string, string> arg = new Dictionary<string, string> {
            { "wid",wid},
            { "callFucntion","zdwh_up"}
        };

        Business ei = GetBusiness();
        Result<string> result;
        result = ei.ExecSqlCommand(sqlCommand, xact_abort, arg);
        return "{\"r\":\"" + (result.Errcode == 0 ? "true" : "false") + "\",\"msg\":\"" + result.Data.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
    }

    [WebMethod(EnableSession = true)]
    //重新登陆
    public string Reload(string value1, string value2, string a, string b)
    {
        value1 = MyCode.MySysDate(value1);
        value2 = MyCode.MySysDate(value2);
        a = MyCode.MySysDate(a);
        b = MyCode.MySysDate(b);
        FM.Business.Login lg = new FM.Business.Login();
        if (lg.Reload(value1, value2, a, b))
            return "{\"r\":\"ture\"}";
        else
            return "{\"r\":\"false\"}";
    }

    [WebMethod(EnableSession = true)]
    //重新登陆
    public void ReloadJson(string value1, string value2, string a, string b)
    {
        Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Context.Response.Charset = "utf-8";
        try
        {
            value1 = MyCode.MySysDate(value1);
            value2 = MyCode.MySysDate(value2);
            a = MyCode.MySysDate(a);
            b = MyCode.MySysDate(b);
            FM.Business.Login lg = new FM.Business.Login();
            if (lg.Reload(value1, value2, a, b))
                Context.Response.Write("{\"r\":\"true\"}");
            else
                Context.Response.Write("{\"r\":\"false\"}");
        }
        catch (System.Exception e)
        {
            Context.Response.Write("{\"r\":false,\"errmsg\":\"" + e.Message.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}");
        }
        Context.Response.End();
    }

    [WebMethod(EnableSession = true)]
    //根据mykey 返回 页面要加载的数据
    public string PosMyLoad(string mykey)
    {
        FM.Business.Pos sp = new FM.Business.Pos();
        DataSet ds = new DataSet();
        return sp.PosMyLoad(mykey, ref ds)[0];
    }

    [WebMethod(EnableSession = true)]
    public string SessionEnd()
    {
        string r = "";
        if (SessionHandle.Get("menupage") != null)
            r = SessionHandle.Get("menupage").ToString();
        if (null != SessionHandle.Get("userid")) TokenHandle.DelUserid(int.Parse(SessionHandle.Get("userid")));
        SessionHandle.Abandon();
        return "{\"r\":\"" + r.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
    }
    /// <summary>
    /// 检查SESSION 并返回哪些SESSION丢失
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string CheckSession()
    {
        string r = "";
        if (SessionHandle.Get("menupage") == null)
            r = "menupage,";
        if (SessionHandle.Get("tzid") == null)
            r += "tzid,";
        if (SessionHandle.Get("userid") == null)
            r += "userid,";

        if (!string.IsNullOrEmpty(r))
            r = r.Substring(0, r.Length - 1);

        return "{\"r\":\"" + r + "\"}";
    }
    /// <summary>
    /// 处理图片,生成略缩图
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string PZoom(string ppath, string pname, string newpath, int targetWidth, int targetHeight, string watermarkText, string watermarkImage)
    {
        Draw dr = new Draw();
        string rStr ;
        try
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(Server.MapPath("../" + ppath + "/" + pname), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            System.IO.Stream stream = fileStream as System.IO.Stream;
            dr.ZoomAuto(stream, Server.MapPath("../" + newpath + "/" + pname), targetWidth, targetHeight, watermarkText, watermarkImage);
            rStr = "{\"r\":\"true\"}";
        }
        catch (System.Exception e)
        {
            rStr = "{\"r\":\"" + e.Message.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
        }
        return rStr;
    }

    [WebMethod(EnableSession = true)]
    public string DelPic(int id)
    {
        Draw dr = new Draw();
        string rStr;
        try
        {
            Business ei = GetBusiness();
            ei.DelPic(id);
            rStr = "{\"r\":\"true\"}";
        }
        catch (System.Exception e)
        {
            rStr = "{\"r\":\"" + e.Message.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
        }
        return rStr;
    }

}