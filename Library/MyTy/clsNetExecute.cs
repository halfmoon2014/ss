using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace MyTy
{
    /// <summary>
    /// 网络操作类
    /// </summary>
    public class clsNetExecute
    {
        public const string Successed = "Successed";
        public const string Error = "Error:";
        public static System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 执行web服务，并返回执行结果:以下采用POST方式发送参数
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ExecWebServiceUsePOST(string ExecUrl, ref string[] VarNames, ref string[] VarValues)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                ExecUrl = System.Uri.EscapeUriString(ExecUrl);
                string execInfo = "";
                try
                {
                    //以下采用POST方式发送参数
                    System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

                    for (int i = 0; i <= VarNames.GetUpperBound(0); i++)
                    {
                        PostVars.Add(VarNames[i], System.Web.HttpUtility.UrlEncode(VarValues[i], myEncoding));
                    }

                    byte[] byRemoteInfo = wc.UploadValues(ExecUrl, "POST", PostVars);
                    execInfo = myEncoding.GetString(byRemoteInfo);

                    return execInfo;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// 执行web服务，并返回执行结果:以下采用POST方式发送参数
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ExecWebServiceUsePOST(string ExecUrl, NameValueCollection PostVars)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                ExecUrl = System.Uri.EscapeUriString(ExecUrl);
                string execInfo = "";
                try
                {
                    byte[] byRemoteInfo = wc.UploadValues(ExecUrl, "POST", PostVars);
                    execInfo = myEncoding.GetString(byRemoteInfo);

                    return execInfo;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// 使用HttpRequest访问特定URL；上传基础信息流（此方法仅用于微信上传Body）
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns> 
        public static string HttpRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();//得到结果
        }


        /// </summary>  
        /// HttpRequest: 使用HttpWebRequest方式访问URL并返回执行结果。  
        /// 适合方式:POST/GET  
        /// </summary>  
        /// <param name="strUrl">目标URL</param>  
        /// <param name="strPostDatas">POST的数据。格式： Var1=Val1&Var2=Val2&Var3=Val3</param>  
        /// <param name="method">发送方式："POST"或"GET"</param>  
        /// <param name="objencoding">编码方式("utf-8")</param>  
        /// <param name="Timeout">超时时间，单位：毫秒。默认为100,000毫秒</param>
        /// <returns></returns>
        public static string HttpRequest(string strUrl, string strPostDatas, string method, string objencoding, int Timeout)
        {
            CookieContainer objCookieContainer = null;
            string rescookie = "";
            return HttpRequest(strUrl, strPostDatas, method, objencoding, Timeout, ref objCookieContainer, ref rescookie, false);
        }

        /// </summary>  
        /// HttpRequest: 使用HttpWebRequest方式访问URL并返回执行结果（包括CookieContainer 和 rescookie）。  
        /// 适合方式:POST/GET  
        /// </summary>  
        /// <param name="strUrl">目标URL</param>  
        /// <param name="strPostDatas">POST的数据。格式： Var1=Val1&Var2=Val2&Var3=Val3</param>  
        /// <param name="method">发送方式："POST"或"GET"</param>  
        /// <param name="objencoding">编码方式("utf-8")</param>  
        /// <param name="Timeout">超时时间，单位：毫秒。默认为100,000毫秒</param>
        /// <param name="objCookieContainer">cookie's(session's)容器 cookie's(session's) container</param>  
        /// <param name="rescookie">返回string类型的cookie</param>  
        /// <param name="flag">参数是否分割。flag = false 直接发送 不分割 （默认）</param>  
        /// <returns>返回HTML页面</returns>  
        public static string HttpRequest(string strUrl, string strPostDatas, string method, string objencoding, int Timeout
                , ref CookieContainer objCookieContainer, ref string rescookie, bool flag)
        {
            HttpWebResponse res = null;
            string strResponse = "";
            //string strcookie = "";  
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);
                req.Method = method;
                req.KeepAlive = true;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Timeout = Timeout;

                if (objCookieContainer == null)
                    objCookieContainer = new CookieContainer();
                req.CookieContainer = objCookieContainer;

                StringBuilder objEncodedPostDatas = new StringBuilder();
                byte[] postDatas = null;

                req.ContentLength = 0;
                #region SendData
                if (strPostDatas != null && strPostDatas.Length > 0)
                {
                    if (flag == true)
                    {
                        string[] datas = strPostDatas.TrimStart('?').Split(new char[] { '&' });
                        for (int i = 0; i < datas.Length; i++)
                        {
                            string[] keyValue = datas[i].Split(new char[] { '=' });
                            if (keyValue.Length >= 2)
                            {
                                objEncodedPostDatas.Append(HttpUtility.UrlEncode(keyValue[0]));
                                objEncodedPostDatas.Append("=");
                                objEncodedPostDatas.Append(HttpUtility.UrlEncode(keyValue[1]));
                                if (i < datas.Length - 1)
                                {
                                    objEncodedPostDatas.Append("&");
                                }
                            }
                        }
                        //postDatas = Encoding.UTF8.GetBytes(objEncodedPostDatas.ToString());  
                        postDatas = Encoding.GetEncoding(objencoding.Trim()).GetBytes(objEncodedPostDatas.ToString());
                    }
                    else
                    {
                        //postDatas = Encoding.UTF8.GetBytes(strPostDatas);  
                        postDatas = Encoding.GetEncoding(objencoding.Trim()).GetBytes(strPostDatas);
                    }

                    req.ContentLength = postDatas.Length;
                    using (Stream reqStream = req.GetRequestStream())
                    {
                        reqStream.Write(postDatas, 0, postDatas.Length);
                    }
                }
                #endregion
                res = (HttpWebResponse)req.GetResponse();
                objCookieContainer = req.CookieContainer;
                CookieCollection mycookie = objCookieContainer.GetCookies(req.RequestUri);
                foreach (Cookie cook in mycookie)
                {
                    rescookie = cook.Name + "=" + cook.Value;
                }
                using (Stream resStream = res.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding(objencoding.Trim())))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                strResponse = Error + ex.Message;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
            return strResponse;
        }


        /// <summary>
        /// 编码.使用 myEncoding 编码
        /// </summary>
        /// <param name="SourceValue">源数据</param>
        /// <returns></returns>
        public static string GetEncodeValue(string SourceValue)
        {
            return System.Web.HttpUtility.UrlEncode(SourceValue, myEncoding);
        }
        /// <summary>
        /// 编码.使用 myEncoding 编码
        /// </summary>
        /// <param name="SourceValue">源数据</param>
        /// <param name="SelectEncoding">指定的编码。省略该参数表示使用myEncoding（默认为：GB2312）</param>
        /// <returns></returns>
        public static string GetEncodeValue(string SourceValue, Encoding SelectEncoding)
        {
            return System.Web.HttpUtility.UrlEncode(SourceValue, SelectEncoding);
        }
        /// <summary>
        /// 解码.使用 myEncoding 解码
        /// </summary>
        /// <param name="SourceValue">源数据</param>
        /// <param name="SelectEncoding">指定的编码。省略该参数表示使用myEncoding（默认为：GB2312）</param>
        /// <returns></returns>
        public static string GetDecodeValue(string SourceValue, Encoding SelectEncoding)
        {
            return System.Web.HttpUtility.UrlDecode(SourceValue, SelectEncoding);
        }
        /// <summary>
        /// 解码.使用 myEncoding 解码
        /// </summary>
        /// <param name="SourceValue">源数据</param>
        /// <returns></returns>
        public static string GetDecodeValue(string SourceValue)
        {
            return System.Web.HttpUtility.UrlDecode(SourceValue, myEncoding);
        }


        /// <summary>
        /// 获取XML后解析出第一个节点，出错则返回null
        /// </summary>
        /// <param name="xmlInfo">xml信息</param>
        /// <returns></returns>
        public static XmlNode GetXmlNode(string xmlInfo)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlInfo);

                return xd.FirstChild.NextSibling;
            }
            catch
            {
                return null;
            }
        }
    }
}
