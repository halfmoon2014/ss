using System;
using weixin.util;
using System.Xml;
using Newtonsoft.Json;
using MyTy;
using weixin.pojson;
using System.Web.Security;
public partial class Default2_wx : System.Web.UI.Page
{
    const string Token = "eesjtoken"; //你的token
    string appid = "";
    string appsecret = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
            string r = "";
            System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream);
            XmlDocument px = new XmlDocument();
            px.Load(Request.InputStream);

            WeixinUtil wu = new WeixinUtil();
            //r = wu.Valid(Request.QueryString, Token);           
            //r=  wu.CreateMenu(appid,appsecret);        
            r = wu.ResponseMsg(px);
           
            Response.Write(r);
            Response.End();
 
    }
    
}