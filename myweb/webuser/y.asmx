<%@ WebService Language="C#" Class="y" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization;//！！！
using System.Runtime.Serialization.Json;//！！！

using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.IO;
using System.Text;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class y  : System.Web.Services.WebService {

    [WebMethod(EnableSession = true)]
    public string code()
    {
        /*sjxg.Class1 sj = new sjxg.Class1();
        string[] rstring = new string[2];

        string[] arguments = new string[2];
        arguments[1] = wid;
        arguments[0] = "normal";
        rstring = sj.zdwh_up(v1, xact_abort, arguments);
        return "{r:'" + rstring[0] + "',msg:'" + Microsoft.JScript.GlobalObject.encodeURIComponent(rstring[1]).Replace("'", "\\\'") + "'}";*/

        System.IO.Stream stream = HttpContext.Current.Request.InputStream;
        System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));
        string sJson = sr.ReadToEnd();

       // List<tb> pmObj = FromJsonTo<List<tb>>(sJson);
        List<tb> p = FromJsonTo<List<tb>>(sJson);
        
        sJson = "";
        /*foreach (pmGPS item in pmObj)
        {
            //sJson += string.Format(" ID:{0},Lat:{1},Long:{2}\r\n", item.ID, item.Lat, item.Long);

        }*/
                
        return "{r:'ture',msg:'a'}";
    }
    public static T FromJsonTo<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        T jsonObject = (T)ser.ReadObject(ms);
        return jsonObject;

    }
     
}

[DataContract]  //非常重要，没有这语句，则无法进行反序列化Json对象
public class pmGPS
{
    [DataMember]//非常重要
    public int id { get; set; }
    [DataMember]//非常重要
    public string s { get; set; }
    [DataMember]//非常重要
    public tb t1 { get; set; }

}

public class tb
{
    [DataMember]//非常重要
    public string  name { get; set; }
    [DataMember]//非常重要
    public List <tr> rows { get; set; }
    public tb()
    {
        rows = new List<tr>();
    }

    public static tb FromJson(string json)
    {
        try
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(tb));
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                return (tb)ser.ReadObject(stream);

        }
        catch
        {
            return null;
        }
    }
}

 

public class tr
{
    [DataMember]//非常重要
    public int c { get; set; }
    public  string n { get; set; } 
}
  