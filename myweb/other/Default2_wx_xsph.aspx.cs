using System;
using System.Web;
using System.Web.Security;
using MyTy;

public partial class Default2_wx_xsph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OAappID = "wx0819d1b345729875";
        string OAappSecret = "ac13cbd085ae51b2970c9a75c9f5c61a";        
        string[] config = GetWXQYJsApiConfig(OAappID, OAappSecret);
        appIdVal.Value = config[0];
        timestampVal.Value = config[1];
        nonceStrVal.Value = config[2];
        signatureVal.Value = config[3];
    }
    /// <summary>
    /// 获取企业微信wx.config方法中所需的参数信息
    /// </summary>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public string[] GetWXQYJsApiConfig(string appid, string secret)
    {
        string QY = "";
        GetJsapi_ticket("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", QY, appid, secret);
        //截止到此步骤，已保证拥有可用的ticket
        return calJsApiConfig(appid, Convert.ToString(HttpContext.Current.Application[QY+"TK_Value" + appid]));
    }
    /// <summary>
    /// 根据参数计算Jsapi_ticket，该函数在微信公众号和微信企业号中都适用
    /// </summary>
    /// <param name="posturl">传入对应的调用POSTURL</param>
    /// <param name="QY">如果是企业号，则传入“QY”；否则传入空字符串</param>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    private void GetJsapi_ticket(string posturl, string QY, string appid, string secret)
    {
        string content = "";
        clsJsonHelper json;
        if (HttpContext.Current.Application[QY + "TK_Value" + appid] == null
        || Convert.ToDateTime(HttpContext.Current.Application[QY + "TK_Time" + appid]).Subtract(DateTime.Now).TotalSeconds < 1)      //没有获取Access_Token或再过一分钟就超时，则重新获取它
        {
            if (QY == "")
            {
                GetWXAccessToken(appid, secret);
                posturl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";     //原先非企业号的posturl没有定义，已于20151202补上。By:xlm
            }
            else
            {
                GetQYWXAccessToken(appid, secret);
                posturl = "https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}";
            }
            //截止到此步骤，已保证拥有可用的Access_Token

            posturl = String.Format(posturl, HttpContext.Current.Application[QY + "AT_Value" + appid].ToString());
            content = clsNetExecute.HttpRequest(posturl);
            json = clsJsonHelper.CreateJsonHelper(content);

            if (json.GetJsonValue("ticket") != "")
            {
                HttpContext.Current.Application[QY + "TK_Value" + appid] = json.GetJsonValue("ticket");
                HttpContext.Current.Application[QY + "TK_Time" + appid] = DateTime.Now.AddSeconds(Convert.ToInt32(json.GetJsonValue("expires_in")));
            }
            else //获取不到，则返回空！ 
            {
                HttpContext.Current.Application[QY + "TK_Value" + appid] = "";
                HttpContext.Current.Application[QY + "TK_Time" + appid] = DateTime.Now;
            }
        }
    }


    /// <summary>
    /// 获取微信公众号的Accesstoken
    /// </summary>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public string GetWXAccessToken(string appid, string secret)
    {
        return GetAccessToken("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", "", appid, secret);
    }

    /// <summary>
    /// 根据参数计算AccessToken，该函数在微信公众号和微信企业号中都适用
    /// </summary>
    /// <param name="posturl">传入对应的调用POSTURL</param>
    /// <param name="QY">如果是企业号，则传入“QY”；否则传入空字符串</param>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    private string GetAccessToken(string posturl, string QY, string appid, string secret)
    {
        string content = "";
        clsJsonHelper json;

        if (HttpContext.Current.Application[QY + "AT_Value" + appid] == null
        || Convert.ToDateTime(HttpContext.Current.Application[QY + "AT_Time" + appid]).Subtract(DateTime.Now).TotalSeconds < 1)      //没有获取Access_Token或再过一分钟就超时，则重新获取它
        {
            posturl = String.Format(posturl, appid, secret);
            content = clsNetExecute.HttpRequest(posturl);
            json = clsJsonHelper.CreateJsonHelper(content);

            if (json.GetJsonValue("access_token") != "")
            {
                HttpContext.Current.Application[QY + "AT_Value" + appid] = json.GetJsonValue("access_token");
                HttpContext.Current.Application[QY + "AT_Time" + appid] = DateTime.Now.AddSeconds(Convert.ToInt32(json.GetJsonValue("expires_in")) - 100);       //增加约2个小时的有效时间，以便接下来重新获取
            }
            else  //获取不到，则返回空！                
            {
                HttpContext.Current.Application[QY + "AT_Value" + appid] = "";
                HttpContext.Current.Application[QY + "AT_Time" + appid] = DateTime.Now;
            }
        }
        return HttpContext.Current.Application[QY + "AT_Value" + appid].ToString();
    }
    /// <summary>
    /// 获取企业微信的Accesstoken
    /// </summary>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public string GetQYWXAccessToken(string appid, string secret)
    {
        return GetAccessToken("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", "QY", appid, secret);
    }

    /// <summary>
    /// 根据现有资料计算JSAPI 的Config，该函数在微信公众号和微信企业号中都适用
    /// </summary>
    /// <param name="appid"></param>
    /// <param name="jsapi_ticket"></param>
    /// <returns></returns>
    private string[] calJsApiConfig(string appid, string jsapi_ticket)
    {
        string[] rtString = new string[4];

        //先拼接成string1
        string string1 = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
        string noncestr = Guid.NewGuid().ToString().Replace("-", "");
        noncestr = noncestr.Substring(noncestr.Length - 16);
        string timestamp = ConvertDateTimeInt(DateTime.Now).ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        if (url.Contains("#")) url = url.Substring(0, url.IndexOf('#'));

        string1 = string.Format(string1, jsapi_ticket, noncestr, timestamp, url);
        //使用SHA1方法，换算成 signature
        string signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1");
        signature = signature.ToLower();

        rtString[0] = appid;
        rtString[1] = timestamp;
        rtString[2] = noncestr;
        rtString[3] = signature;

        return rtString;
    }

    ///
    /// datetime转换为unixtime
    ///
    ///
    ///
    private int ConvertDateTimeInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }
}