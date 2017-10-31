<%@ WebHandler Language="C#" Class="MyHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.DataBase;
using MyTy;
public class MyHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string s = "";
        string lx = "";
        if (context.Request.QueryString["lx"] != null)
        {
            lx = context.Request.QueryString["lx"].ToString();
            //得到大类 JSON
            s = GetXx(lx);
        }
        if (s == string.Empty)
        {
            s="[{\"id\":-1,\"text\":\"没有大类\"}]";
        }
        context.Response.Write(s);

    }
    public string GetXx(string lx)
    {
       
        string str = "[";        
        FM.Business.WebSp sp = new FM.Business.WebSp();
        DataSet ds = sp.GetSPXx(lx); 
        DataTable dtjg = ds.Tables[0];//字级
        DataTable dtno1 = ds.Tables[1];//第一级菜单
        foreach (DataRow dr in dtno1.Rows)
        {
            str += GetNextTree(int.Parse(dr["id"].ToString()), dtjg,  dr);
        }
        if (str != "[")
        {
            str = str.Substring(0, str.Length - 1) + "]";
        }
        else { str = ""; }
        return str;
    }
    public string GetNextTree(int id, DataTable dtjg,  DataRow dr)
    {
        string myrs = "{\"id\":" + dr["id"].ToString() + ",\"text\":" + "\"" + MyTy.Utils.HtmlCha(dr["mc"].ToString()) + "\", \"attributes\":{\"xjbs\":\"" + dr["xjbs"].ToString().Trim() + "\"} , \"iconCls\":\"icon-sum\" ";
        DataRow[] mydr = dtjg.Select("ssid=" + id);
        string mychil = "";
        if (mydr.Length > 0)
        {
            foreach (DataRow dr1 in mydr)
            {
                mychil += GetNextTree(int.Parse(dr1["id"].ToString()), dtjg,  dr1);
            }
        }
        else
        {//如果一级菜单没有下级                 
        }
        string utmp =   mychil;
        return myrs + (utmp == string.Empty ? "" : ",\"children\":[ " + utmp.Substring(0, utmp.Length - 1) + "] ") + "},";           
    }
    
    
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}