<%@ WebHandler Language="C#" Class="longPollingAction" %>

using System;
using System.Web;
using Comet;
using System.Collections.Generic;
using MyTy;
public class longPollingAction : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Result<string> result = new Result<string>();
        List<Complex> clienConnetList = LongSataMrg.clienConnetList;
        string action = context.Request.QueryString["ac"].ToString();
        string command = context.Request.QueryString["command"].ToString();
        result.Errcode = 2002;//没有连接
        result.Errmsg = "没有连接";
        for (int i = clienConnetList.Count - 1; i >= 0; i--)
        {
            Complex complex = clienConnetList[i];
            if (!complex.CometResult.Context.Response.IsClientConnected)
            {
                clienConnetList.Remove(complex);
                result.Errcode = Math.Min(result.Errcode, 2001);//有可能存在多个相同用户名的连接
            }
            else
            {
                if (action == "del")
                    complex.CometResult.ExtraData = "{\"Errcode\":0,\"Errmsg\":\"\",\"Data\":\"被手动清理\"}";            
                else if (action == "command")
                    complex.CometResult.ExtraData = "{\"Errcode\":-1,\"Errmsg\":\"\",\"Data\":\"" + command + "\"}";
                result.Errcode = complex.CometResult.Call();
                clienConnetList.Remove(complex);
            }
        }
        if (result.Errcode == 2001)
        {
            result.Errmsg = "失效连接,清理成功";
        }
        else if (result.Errcode == 0)
        {
            if (action == "command")
            {
                result.Data = "有效连接,发送信息成功";
            }
            else
            {
                result.Data = "有效连接,清理成功";
            }
            result.Errmsg = "";
        }
        else
        {
            result.Data = "清理成功,但调用call方法可能失败了";
        }
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}