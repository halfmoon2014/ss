using System;
using System.Xml;

//using MyClass;
using weixin.util;

public partial class Default2_wxcs : System.Web.UI.Page
{
    const string Token = "nextstoken"; //你的token
    //string appid = "wxd6e90668ef7ec4db";//wx0819d1b345729875
    //string appsecret = "ac13cbd085ae51b2970c9a75c9f5c61a";
    //测试公众号
    //string appid = "wx0819d1b345729875";
    //string appsecret = "ac13cbd085ae51b2970c9a75c9f5c61a"; 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string r = "";

            WeixinUtil wu = new WeixinUtil();
            //r = wu.Valid(Request.QueryString, Token);
            //Response.Write(r);
  

            //r = wu.CreateMenu(appid, appsecret); 
            //Response.Write(r);


            XmlDocument px = new XmlDocument();
            px.Load(Request.InputStream);
            //Response.Write(px.GetElementsByTagName("Content")[0].InnerText);
            if (px.GetElementsByTagName("MsgType")[0].InnerText == "event")
            {
                r = ResponseMsg(px);
            }
            else
            {
                r = wu.ResponseMsg(px);
            }
            Response.Write(r);

        }
        catch (Exception ex)
        {
            Response.Write("default2_wxcs.aspx.cs</br>");            
            Response.Write("Exception.Source" + ex.Source + "</br>");            
            Response.Write(ex.Message);

        }
        finally
        {
            Response.End();
        }

    }


    private string ResponseMsg(XmlDocument px)
    {
        string r = "";
        WeixinUtil wu = new WeixinUtil();
        try
        {
            string msgtype = px.GetElementsByTagName("MsgType")[0].InnerText;
            string Event = px.GetElementsByTagName("Event")[0].InnerText;
            if (Event == "CLICK")
            {
                string eventKey = px.GetElementsByTagName("EventKey")[0].InnerText;

                if (eventKey == "11")
                {//积分查询

                    r = "<a href='http://www.157.hk:4529/other/Default2_wx_zxdp.aspx'>最新单品</a>/:,@f";
                }
                else if (eventKey == "12")
                {//消费查询
                    r = "<a href='http://www.157.hk:4529/other/Default2_wx_xsph.aspx'>销售排行</a>/:,@f";
                }
            }
            else if (Event == "subscribe")
            {
                r = "欢迎光临本小站";
            }
        }
        catch (System.Exception e)
        {
            r = e.Message;
        }

        string textTpl = "<xml> <ToUserName><![CDATA[" + px.GetElementsByTagName("FromUserName")[0].InnerText + @"]]></ToUserName>\r\n";

        textTpl += "<FromUserName><![CDATA[" + px.GetElementsByTagName("ToUserName")[0].InnerText + @"]]></FromUserName>\r\n";

        textTpl += "<CreateTime>" + DateTime.Now + @"</CreateTime>\r\n";
        textTpl += "<MsgType><![CDATA[text]]></MsgType>\r\n";

        textTpl += "<Content><![CDATA[" + r + "" + @"]]></Content>\r\n";
        textTpl += "<FuncFlag>0</FuncFlag>\r\n";
        textTpl += "</xml>";

        return textTpl;
    }


}