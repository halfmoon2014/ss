<%@ WebService Language="C#" Class="ws" %>

using System.Web.Services;

using System.Data;

using Service.Util;

using System.Collections.Generic;
using MyTy;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ws : System.Web.Services.WebService
{
    public Business getBusiness()
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
        Business ei = getBusiness();
        return "{r:'" + ei.UpHelp(value1.Trim(), value2) + "'}";
    }
    /// <summary>
    /// 执行表创建视图过程
    /// </summary>
    /// <param name="value1"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string autoview(string value1)
    {
        Business ei = getBusiness();
        return "{r:'" + ei.AutoView(value1) + "'}";
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
    public string sjy_up(string wid, string value1, string value3, string value4, string mrcx, string myadd, string orderby, string pagesize, string mxgl, string mxsql, string mxhgl, string mxhord, string mxhsql, string mxly, string sql_2)
    {
        Business ei = getBusiness();
        CacheTools.UpdateDep(wid);
        return "{r:'" + ei.UpSJY(wid, value1, value3, value4, mrcx, myadd, orderby, pagesize, mxgl, mxsql, mxhgl, mxhord, mxhsql, mxly, sql_2) + "'}";
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
        Business ei =getBusiness();
        CacheTools.UpdateDep(wid);
        return "{r:'" + ei.UpSYJLayout(data) + "'}";
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
        Business ei = getBusiness();
        CacheTools.UpdateDep(wid);
        return "{r:'" + ei.UpSYJZdwh(data) + "'}";
    }



    /// <summary>
    ///  WEB设计js保存
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="js"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string sjy_upjs(string wid, string js)
    {
        Business ei = getBusiness();
        CacheTools.UpdateDep(wid);
        return "{r:'" + ei.UpSJYJs(wid, js) + "'}";
    }

    /// <summary>
    /// WEB设计help保存
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="help"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string sjy_uphelp(string wid, string help)
    {

        Business ei = getBusiness();
        return "{r:'" + ei.UpSJYHelp(wid, help) + "'}";
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
    public string websj_cl(string userid, string mc, string lx, string wid, string zt)
    {

        Business ei = getBusiness();
        string rstring = "";
        if (zt == "add")
        {
            int r = ei.AddSJYSJ(userid, mc, lx);
            if (r > 0)
            {
                rstring = "true";
            }
            else
            {
                rstring = "false";
            }
        }
        else
        {//edit                
            int r = ei.EditSJYSJ(wid, mc, lx);
            if (r > 0)
            {
                rstring = "true";
            }
            else
            {
                rstring = "false";
            }

        }

        return "{r:'" + rstring + "'}";
    }
    /// <summary>
    /// WEB设计 删除webid
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string websj_del(string wid)
    {

        Business ei = getBusiness();
        return "{r:'" + ei.DelSJYSJ(wid) + "'}";
    }
    /// <summary>
    /// WEB设计 复制webid
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string websj_fz(string wid)
    {
        Business ei = getBusiness();
        return "{r:'" + ei.CopySJYSJ(wid) + "'}";
    }

    /// <summary>
    /// 发布webid 或菜单
    /// </summary>
    /// <param name="wid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string web_fb(string wid)
    {
        Business ei = getBusiness();
        if (wid == "menu")
        {
            return "{r:'" + ei.web_fb_menu() + "'}";
        }
        else
        {
            return "{r:'" + ei.web_fb(wid) + "'}";
        }
    }
    /// <summary>
    /// WEB设计 复制webid中的每项
    /// </summary>
    /// <param name="wid"></param>
    /// <param name="newwid"></param>
    /// <param name="bs"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string websj_fz_zd(string wid, string newwid, string bs)
    {

        Business ei = getBusiness();
        return "{r:'" + ei.CopyWebSJZD(wid, newwid, bs) + "'}";
    }

    [WebMethod(EnableSession = true)]
    //页面设计 字段处理 处理SQL语句
    //处理大类SQL
    public string execSqlCommand(string sqlCommand, string xact_abort, string wid)
    {

        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("wid", wid);
        arg.Add("callFucntion", "zdwh_up");

        Business ei = getBusiness();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic = ei.execSqlCommand(sqlCommand, xact_abort, arg);
        return "{r:'" + dic["resultState"] + "',msg:'" + Microsoft.JScript.GlobalObject.encodeURIComponent(dic["resultText"]).Replace("'", "\\\'") + "'}";
    }

    [WebMethod(EnableSession = true)]
    //重新登陆
    public string reload(string value1, string value2, string a, string b)
    {
        //sjxg.Class1 sj = new sjxg.Class1();
        value1 = MyTy.MyCode.MySysDate(value1);
        value2 = MyTy.MyCode.MySysDate(value2);
        a = MyTy.MyCode.MySysDate(a);
        b = MyTy.MyCode.MySysDate(b);
        //bool t  = sj.Reload(value1,value2,a,b);
        FM.Business.Login lg = new FM.Business.Login();
        bool t = lg.Reload(value1, value2, a, b);
        if (t)
        {
            return "{r:'ture'}";
        }
        else
        {
            return "{r:'false'}";
        }
    }

    [WebMethod(EnableSession = true)]
    //重新登陆
    public void reloadJson(string value1, string value2, string a, string b)
    {
        //sjxg.Class1 sj = new sjxg.Class1();
        value1 = MyCode.MySysDate(value1);
        value2 = MyCode.MySysDate(value2);
        a = MyCode.MySysDate(a);
        b = MyCode.MySysDate(b);
        //bool t  = sj.Reload(value1,value2,a,b);
        FM.Business.Login lg = new FM.Business.Login();
        bool t = lg.Reload(value1, value2, a, b);
        Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Context.Response.Charset = "utf-8";
        if (t)
            Context.Response.Write("{\"r\":true}");
        else
            Context.Response.Write("{\"r\":false}");

        Context.Response.End();
    }

    [WebMethod(EnableSession = true)]
    //根据mykey 返回 页面要加载的数据
    public string Pos_MyLoad(string mykey)
    {

        FM.Business.Pos sp = new FM.Business.Pos();
        DataSet ds = new DataSet();
        return sp.Pos_MyLoad(mykey, ref ds)[0];
    }

    [WebMethod(EnableSession = true)]
    public string SessionEnd()
    {
        string r = "";
        if (Session["menupage"] != null)
        {
            r = Session["menupage"].ToString();
        }
        Session.Abandon();
        return "{r:'" + r + "'}";
    }
    /// <summary>
    /// 检查SESSION 并返回哪些SESSION丢失
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string CheckSession()
    {
        string r = "";
        if (Session["menupage"] == null)
        {
            r = "menupage,";
        }
        if (Session["tzid"] == null)
        {
            r += "tzid,";
        }
        if (Session["userid"] == null)
        {
            r += "userid,";
        }

        if (r != "")
        {
            r = r.Substring(0, r.Length - 1);
        }
        return "{r:'" + r + "'}";
    }
    /// <summary>
    /// 处理图片,生成略缩图
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string pzoom(string ppath, string pname, string newpath, int targetWidth, int targetHeight, string watermarkText, string watermarkImage)
    {
        MyTy.Draw dr = new Draw();

        try
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(Server.MapPath("../" + ppath + "/" + pname), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            System.IO.Stream stream = fileStream as System.IO.Stream;
            dr.ZoomAuto(stream, Server.MapPath("../" + newpath + "/" + pname), targetWidth, targetHeight, watermarkText, watermarkImage);
            return "{r:'true'}";
        }
        catch (System.Exception e)
        {
            return "{r:'" + e.Message + "'}";
        }
    }

}