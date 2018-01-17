using System;
using weixinqyh.util;
using System.Xml;
public partial class other_Default2_qyhcs : System.Web.UI.Page
{
    
    //公众平台上开发者设置的token, corpID, EncodingAESKey
    //string sToken = "QncQq";
    string sCorpID = "wx01a540772ebf1115";
    //string sEncodingAESKey = "RQnLIz9NJqtg9gO3uHBjnjWB7ixwnGd8xoe5XJzkCIm";
    string corpsecret = "er8hIINt1JiI71TNp7vT7YXDAMn3LII3vwT5FOxW_6xb2BjZY2jgKFXDAgJuMbsK";//管理组Secret
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string r = "";
            WeixinQYHUtil wu = new WeixinQYHUtil();
            //r = wu.ValidQYH(Request.QueryString, sToken, sEncodingAESKey, sCorpID);
            //Response.Write(r);
            r = wu.sendMessage(wu.getAccessToken(sCorpID, corpsecret), Request.QueryString["text"].ToString());
            Response.Write(r);
            //r = wu.CreateMenu(appid, appsecret); 
            //Response.Write(r);


            //XmlDocument px = new XmlDocument();
            //px.Load(Request.InputStream);
            ////Response.Write(px.GetElementsByTagName("Content")[0].InnerText);
            //if (px.GetElementsByTagName("MsgType")[0].InnerText == "event")
            //{
            //    r = ResponseMsg(px);
            //}
            //else
            //{
            //    r = wu.ResponseMsg(px);
            //}
            //Response.Write(r);

        }
        catch (Exception ex)
        {
            Response.Write("default2_QYHcs.aspx.cs</br>");
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
        return "";
    }
}