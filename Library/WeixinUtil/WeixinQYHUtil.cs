using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weixin.pojson;
using System.Web;
using MyTy;
using Newtonsoft.Json;
namespace weixinqyh.util
{
    public class WeixinQYHUtil
    {

        public static string access_token_url = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=CORPID&corpsecret=CORPSECRET";
        public AccessToken getAccessToken(String corpid, String corpsecret)
        {
            if (HttpContext.Current.Application[corpid + corpsecret] == null
           || Convert.ToDateTime(HttpContext.Current.Application[corpid + corpsecret + "time"]).Subtract(DateTime.Now).TotalSeconds < 1)
            {

                string tagUrl = access_token_url.Replace("CORPID", corpid).Replace("CORPSECRET", corpsecret);
                //CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略
                System.Net.HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                System.IO.Stream instream = response.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(instream, System.Text.Encoding.UTF8);
                //返回结果 
                string content = sr.ReadToEnd();
                AccessToken at = JsonConvert.DeserializeObject<AccessToken>(content);
                HttpContext.Current.Application[corpid + corpsecret] = at;
                HttpContext.Current.Application[corpid + corpsecret + "time"] = DateTime.Now;
            }
            return (AccessToken)HttpContext.Current.Application[corpid + corpsecret];

        }
        public string sendMessage(AccessToken at, string content)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=ACCESS_TOKEN";

            string tagUrl = url.Replace("ACCESS_TOKEN", at.Access_token);
            WeiXinQYHMessageText text = new WeiXinQYHMessageText();
            text.Touser = "@all";
            text.Toparty = "";
            text.Totag = "";
            text.Msgtype = "text";
            text.Agentid = 23;
            WeiXinQYHMessageTextContent textContent = new WeiXinQYHMessageTextContent();
            textContent.Content = content;
            text.Text = textContent;
            text.Safe = "0";

            JsonSerializerSettings jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            string menuStr =  JsonConvert.SerializeObject(text, jSetting) ;
            
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            System.Net.HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(tagUrl, null, menuStr, null, null, encoding, null);
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();//得到结果             
            

        }

        /// <summary>
        /// 企业号验证
        /// </summary>
        /// <param name="nv"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public string ValidQYH(System.Collections.Specialized.NameValueCollection nv, string sToken, string sEncodingAESKey, string sCorpID)
        {
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);

            string sVerifyMsgSig = System.Web.HttpUtility.UrlDecode(nv["msg_signature"]);
            //string sVerifyMsgSig = "5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3";
            string sVerifyTimeStamp = System.Web.HttpUtility.UrlDecode(nv["timestamp"]);
            //string sVerifyTimeStamp = "1409659589";
            string sVerifyNonce = System.Web.HttpUtility.UrlDecode(nv["nonce"]);
            //string sVerifyNonce = "263014780";
            string sVerifyEchoStr = System.Web.HttpUtility.UrlDecode(nv["echostr"]);
            //string sVerifyEchoStr = "P9nAzCzyDtyTWESHep1vC5X9xho/qYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp+4RPcs8TgAE7OaBO+FZXvnaqQ==";
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
                return "";
            }
            else
            {
                return sEchoStr;
            }
        }


    }
}
