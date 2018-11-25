<%@ WebHandler Language="C#" Class="longPollingDel" %>

using System;
using System.Web;
using Comet;
using System.Collections.Generic;
public class longPollingDel : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        List<Complex> clienConnetList = LongSataMrg.clienConnetList;
        int code = 2002;//没有连接
        for (int i = clienConnetList.Count - 1; i >= 0; i--)
        {
            Complex complex = clienConnetList[i];
            if (!complex.CometResult.Context.Response.IsClientConnected)
            {
                clienConnetList.Remove(complex);
                code = Math.Min(code, 2001);//有可能存在多个相同用户名的连接
            }
            else
            {
                complex.CometResult.ExtraData = "被手动清理";
                code = complex.CometResult.Call();
                clienConnetList.Remove(complex);
            }
        }
        if (code == 2002)
        {
            context.Response.Write("没有连接");
        }
        else if (code == 2001)
        {
            context.Response.Write("失效连接,清理成功");
        }
        else if (code == 0)
        {
            context.Response.Write("有效连接,清理成功");
        }
        else
        {
            context.Response.Write("清理成功,但调用call方法可能失败了");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}