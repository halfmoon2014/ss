<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using Comet;
using MyTy;
public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        byte[] byts = new byte[context.Request.InputStream.Length];
        context.Request.InputStream.Read(byts, 0, byts.Length);
        string req = System.Text.Encoding.Default.GetString(byts);
        req = context.Server.UrlDecode(req);
        Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(req);

        if (jobject["topic"].ToString() == "longPollingData")
        {
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(longPollingData(jobject["to"].ToString(), jobject["msg"])));
        }
        else
        {
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(ResultUtil<string>.error(2000,"no method")));
        }
    }

    public Result<string> longPollingData(string to, object data)
    {
        int code;
        try
        {
            code = LongSataMrg.Send(to, data);
        }
        catch (System.Exception e)
        {
            code = 2001;
        }
        if (code == 0)
            return ResultUtil<string>.success("");
        else
            return ResultUtil<string>.error(code, "longPollingData");

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}