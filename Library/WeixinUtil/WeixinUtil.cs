using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MyTy;
using weixin.pojson;
using System.Xml;
using System.Web.Security; 
using System.Data;
using System.DataBase;
using System.Web;
using Service.Util;
namespace weixin.util
{

    public class WeixinUtil
    {
        public static string access_token_url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
        public AccessToken GetAccessToken(String appid, String appsecret)
        {
            if (HttpContext.Current.Application[appid + appsecret] == null
           || Convert.ToDateTime(HttpContext.Current.Application[appid + appsecret + "time"]).Subtract(DateTime.Now).TotalSeconds < 1)
            {

                string tagUrl = access_token_url.Replace("APPID", appid).Replace("APPSECRET", appsecret);
                //CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略
                System.Net.HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                System.IO.Stream instream = response.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(instream, System.Text.Encoding.UTF8);
                //返回结果 
                string content = sr.ReadToEnd();
                AccessToken at = JsonConvert.DeserializeObject<AccessToken>(content);
                HttpContext.Current.Application[appid + appsecret] = at;
                HttpContext.Current.Application[appid + appsecret + "time"] = DateTime.Now;
            }
            return (AccessToken)HttpContext.Current.Application[appid + appsecret];

        }
        // 菜单创建（POST） 限100（次/天）
        public static String menu_create_url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN";
        /**
         * 创建菜单
         * 
         * @param menu 菜单实例
         * @param accessToken 有效的access_token
         * @return 0表示成功，其他值表示失败
         */
        public string CreateMenu(string menu, String accessToken)
        {

            // 拼装创建菜单的url
            String url = menu_create_url.Replace("ACCESS_TOKEN", accessToken);
            Encoding encoding = Encoding.GetEncoding("UTF-8");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("tpl", "fa");  

            string outputStr = menu;
            System.Net.HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, outputStr, null, null, encoding, null);

            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();//得到结果       


        }
        ///
        /// 验证微信签名
        ///
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        ///
        private bool CheckSignature(System.Collections.Specialized.NameValueCollection nv, string Token)
        {
            string signature = nv["signature"].ToString();
            string timestamp = nv["timestamp"].ToString();
            string nonce = nv["nonce"].ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 公众号验证
        /// </summary>
        /// <param name="nv"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public string Valid(System.Collections.Specialized.NameValueCollection nv, string Token)
        {
            string echoStr = nv["echoStr"].ToString();
            if (CheckSignature(nv, Token))
            {
                return echoStr;
            }
            else
            {
                return "";
            }
        }
      
        ///
        /// 返回信息结果(微信信息返回)
        ///    
        public string ResponseMsg(XmlDocument px)
        {

            //发送方帐号
            string fromUsername = px.GetElementsByTagName("FromUserName")[0].InnerText;
            //开发者微信号 
            string toUsername = px.GetElementsByTagName("ToUserName")[0].InnerText;
            //回复的消息内容        
            string msgtype = px.GetElementsByTagName("MsgType")[0].InnerText;

           ConnetString db = new ConnetString();

            string conn = db.GetMasterConn();

            DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, "select top 1 * from v_wx_sm");
            string r = ds.Tables[0].Rows[0]["wxsm"].ToString();

            String backWord = "无效指令," + r + "/:,@f";
            try
            {
                if (msgtype == "text")
                {
                    backWord = ResponseMsg_text(px, backWord);
                }
                else if (msgtype == "location")
                {
                    backWord = "你的位置:" + px.GetElementsByTagName("Location_X")[0].InnerText + "," + px.GetElementsByTagName("Location_Y")[0].InnerText; ;
                }
                else if (msgtype == "image")
                {
                    backWord = "你上传的图片很好看";
                }
                else if (msgtype == "voice")
                {
                    backWord = "你的声音很好听";
                }
                else if (msgtype == "video")
                {
                    backWord = "你上传的视频很精彩";
                }
                else if (msgtype == "link")
                {
                    backWord = "你分享的信息很好";
                }
                else if (msgtype == "event")
                {
                    backWord = ResponseMsg_event(px);
                }
            }
            catch (System.Exception e)
            {
                backWord = e.Message;
                Log.WriteLog("ResponseMsg", backWord);
            }            
            string textTpl = "<xml> <ToUserName><![CDATA[" + fromUsername + @"]]></ToUserName>\r\n";
            textTpl += "<FromUserName><![CDATA[" + toUsername + @"]]></FromUserName>\r\n";
            textTpl += "<CreateTime>" + DateTime.Now + @"</CreateTime>\r\n";
            textTpl += "<MsgType><![CDATA[text]]></MsgType>\r\n";
            textTpl += "<Content><![CDATA[" + backWord + @"]]></Content>\r\n";
            textTpl += "<FuncFlag>0</FuncFlag>\r\n";
            textTpl += "</xml>";
            return textTpl;

        }
        /// <summary>
        /// 处理关注和取消关注
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        public string ResponseMsg_event(XmlDocument px)
        {
            string r = "";
            ConnetString db = new ConnetString();

            string conn = db.GetMasterConn();
            if (px.GetElementsByTagName("Event")[0].InnerText == "subscribe")
            {//关注
                DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, "select top 1 * from v_wx_sm");
                r = ds.Tables[0].Rows[0]["wxsm"].ToString();
            }
            else if (px.GetElementsByTagName("Event")[0].InnerText == "unsubscribe")
            {
                r = "再见./:bye";
            }
            return r;
        }
        /// <summary>
        /// 响应文本消息
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        public string ResponseMsg_text(XmlDocument px, string r)
        {
            string fromUsername = px.GetElementsByTagName("FromUserName")[0].InnerText;
            ConnetString db = new ConnetString();

            string conn = db.GetMasterConn();

            //操作#字操作#数据*数据2*
            string content = px.GetElementsByTagName("Content")[0].InnerText;
            if (content.Split('#').Length > 1)
            {

                if (content.Split('#')[0].ToString().ToUpper() == "H")
                {//查看帮助               
                    r = GetHelp();
                }
                else
                {
                    //确定type
                    string type = GetType(content);
                    //确认参数

                    string[] xx = content.Split('#')[1].Split('*');
                    if (type == "bdsfz")
                    {//绑定身份证
                        string sql = "select * from v_wx_user where pid='" + fromUsername + "'";
                        DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            r = "您已经绑定过了./::)";
                        }
                        else
                        {
                            sql = "insert v_wx_user(pid,pp,pname,sj) values('" + fromUsername + "','" + xx[0] + "','" + xx[1] + "','" + xx[2] + "');";
                            sql += " SELECT CAST(scope_identity() AS int)";
                            if ((Int32)SqlHelper.ExecuteScalar(conn, CommandType.Text, sql) > 0)
                            {
                                r = "绑定成功./::)";
                            }
                            else
                            {
                                r = "绑定失败.请联系客服./::Q";
                            }
                        }

                    }
                    else if (type == "zxdp")
                    {
                        r = "<a href='http://halfmoon2008.gicp.net/Default2_wx_zxdp.aspx'>最新单品</a>/:,@f";
                         
                    }
                    else if (type == "pp" || type == "pname")
                    {//查看用户身份证/查看用户姓名
                        string sql = "select pp,pname from  v_wx_user where pid='" + fromUsername + "'";
                        DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            r = (type == "pp" ? ds.Tables[0].Rows[0]["pp"].ToString() : ds.Tables[0].Rows[0]["pname"].ToString()) + "/::)";
                        }
                        else
                        {
                            r = "您还没绑定身份证./::Q";
                        }

                    }
                    else if (type == "pjf")
                    {//查看积分

                    }
                    else if (type == "cxcpda")
                    {//查看产品资料 ruirui
                        r = "";
                        string sql = "select   '品名:'+pm+'单价:'+cast(lsj as varchar(max)) as ti from  v_sp_cpda where kh='" + xx[0] + "'";
                        
                        DataSet ds = SqlHelper.ExecuteDataset(db.GetCreateLinkServerConnetStringInBLL("8"), CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                r += dr["ti"].ToString() + "\n";
                            }
                        }
                        else
                        {
                            r = "条码错误";
                        }
                    }
                    else
                    {
                        r = type;
                    }

                }
            }
            return r;
        }
        //返回帮助
        public string GetHelp()
        {
            string r = "";
            ConnetString db = new ConnetString();

            string conn = db.GetMasterConn();
            string sql = "select * from v_wx_cz where ty=0;";
            sql += "select a.id as czid,b.ord, c.ms,c.type,c.bz  from v_wx_cz  a inner join v_wx_cz_par b on a.id=b.ssid inner join v_wx_cz_parattr c on b.attrid=c.id where a.ty=0     ";

            DataSet ds0 = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
            foreach (DataRow dr in ds0.Tables[0].Select("ssid=0"))//1
            {
                if (dr["type"].ToString() != "")
                {
                    r += dr["dm"].ToString() + "#" + GetPar(dr["id"].ToString(), ds0.Tables[1]) + "/:share" + dr["ms"].ToString() + "\n";

                }


                foreach (DataRow dr2 in ds0.Tables[0].Select("ssid=" + dr["id"].ToString()))//2
                {
                    if (dr2["type"].ToString() != "")
                    {
                        r += dr["dm"].ToString() + "#" + dr2["dm"].ToString() + "#" + GetPar(dr2["id"].ToString(), ds0.Tables[1]) + "/:share" + dr2["ms"].ToString() + "\n";
                    }
                    foreach (DataRow dr3 in ds0.Tables[0].Select("ssid=" + dr2["id"].ToString()))//3
                    {
                        if (dr3["type"].ToString() != "")
                        {
                            r += dr["dm"].ToString() + "#" + dr2["dm"].ToString() + "#" + dr3["dm"].ToString() + "#" + GetPar(dr3["id"].ToString(), ds0.Tables[1]) + "/:share" + dr3["ms"].ToString() + "\n";
                        }
                    }
                }



            }
            return r;
        }
        /// <summary>
        /// 根据操作id返回对应的参数
        /// 返回如 身份证*姓名*手机号
        /// </summary>
        /// <param name="id">操作id</param>
        /// <param name="dt">参数</param>
        /// <returns></returns>
        public string GetPar(string id, DataTable dt)
        {
            string r = "";            
            DataRow[] drs = dt.Select("czid=" + id, "ord");
            int i = 0;
            foreach (DataRow dr in drs)
            {
                r += dr["ms"].ToString();
                i++;
                if (i != drs.Length)
                {//最后一个不加*
                    r += "*";
                }
            }

            return r;
        }
        /// <summary>
        /// 根据字段得到type
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetType(string content)
        {
            string type = "";
            ConnetString db = new ConnetString();

            string conn = db.GetMasterConn();
            string sql = "select * from v_wx_cz;";
            //sql += "select a.id as czid,b.ord, c.ms,c.type,c.bz  from v_wx_cz  a inner join v_wx_cz_par b on a.id=b.ssid inner join v_wx_cz_parattr c on b.attrid=c.id     ";
            DataSet ds0 = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
            string lsid = "";
            string[] spl = content.Split('#');

            for (int i = 0; i < spl.Length - 1; i++)
            {
                if (i == 0)
                {//第一个
                    if (ds0.Tables[0].Select("ssid=0 and dm='" + spl[i].ToString() + "'").Length == 0)
                    {
                        type = "无效操作";
                        break;
                    }
                    else
                    {
                        lsid = ds0.Tables[0].Select("ssid=0 and dm='" + spl[i].ToString() + "'")[0]["id"].ToString();
                    }
                }
                else
                {
                    if (ds0.Tables[0].Select("ssid=" + lsid + " and dm='" + spl[i].ToString() + "'").Length == 0)
                    {
                        type = "无效操作";
                        break;
                    }
                    else
                    {
                        lsid = ds0.Tables[0].Select("ssid=" + lsid + " and dm='" + spl[i].ToString() + "'")[0]["id"].ToString();
                    }
                }
            }
            if (type == "")
            {
                type = ds0.Tables[0].Select("id=" + lsid)[0]["type"].ToString();
            }
            return type;
        }
        /// <summary>
        /// 菜单
        /// </summary>
        public string CreateMenuTest(string appid, string appsecret)
        {

            WeixinUtil wu = new WeixinUtil();
            weixin.pojson.AccessToken ac = wu.GetAccessToken(appid, appsecret);
            if (ac.Access_token != null)
            {
                List<WeiXinButton> btnList = new List<WeiXinButton>();

                WeiXinButton btn1 = new WeiXinButton
                {
                    Name = "我的衣衣",
                    SubButton = new List<WeiXinButton>()
                };
                WeiXinButton btn11 = new WeiXinButton
                {
                    Name = "最新单品",
                    Key = "11",
                    Type = "click"
                };
                btn1.SubButton.Add(btn11);
                WeiXinButton btn12 = new WeiXinButton
                {
                    Name = "销售排行",
                    Key = "12",
                    Type = "click"
                };
                btn1.SubButton.Add(btn12);

                WeiXinButton btn13 = new WeiXinButton
                {
                    Name = "我的朋友",
                    Key = "13",
                    Type = "click"
                };
                btn1.SubButton.Add(btn13);
                btnList.Add(btn1);

                WeiXinButton btn3 = new WeiXinButton
                {
                    Name = "我的微生活",
                    SubButton = new List<WeiXinButton>()
                };
                WeiXinButton btn31 = new WeiXinButton
                {
                    Name = "天气查询",
                    Type = "view",
                    Url = "http://m.46644.com/tool/?tpltype=weixin"
                };
                btn3.SubButton.Add(btn31);

                btnList.Add(btn3);

                JsonSerializerSettings jSetting = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string menuStr = "{\"button\":" + JsonConvert.SerializeObject(btnList, jSetting) + "}";

                return wu.CreateMenu(menuStr, ac.Access_token);

            }
            else
            {
                return "tokenerr";

            }
        }       

    }
}
