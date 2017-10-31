<%@ WebHandler Language="C#" Class="jsonp" %>

using System;
using System.Web;
using System.Text;
public class jsonp : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string key = context.Request["cb"];

        StringBuilder strJsonData = new StringBuilder();//拼接json所有格式  
        StringBuilder strJsonMsgData = new StringBuilder();//拼接json内容  

        strJsonData.AppendFormat("{0}([", key);//json begin  

        string name = context.Request["name"].ToString();
        string age = context.Request["age"].ToString();
        strJsonMsgData.Append("{\"name\"" + ":" + "\"" + name + "\"" + ",\"age\"" + ":" + "\"" + age
                        + "\"}" + ",");
        strJsonData.Append(strJsonMsgData.ToString().TrimEnd(','));
        strJsonData.Append("])");//json end 
        

        context.Response.Write(strJsonData);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}