using System;
using System.IO;
using System.Net;
using System.Text;

namespace FM.Components
{
    /// <summary>
    ///  获取HTML内容
    /// </summary>
    public class Spider
    {
        /// <summary>
        /// 创建访问URL的Request
        /// </summary>
        private HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.AllowWriteStreamBuffering = true;
            request.Timeout = 20 * 1000; 

            return request;
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        public string GetHtml(string url)
        {
            return GetHtml(url, "default");
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        public string GetHtml(string url, string encodeName)
        {
            StringBuilder sb = new StringBuilder();
            HttpWebRequest request = GetRequest(url);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return "";
            }

            //网页编码
            Encoding encode = Encoding.Default;
            if (encodeName.ToLower() != "default")
            {
                encode = Encoding.GetEncoding(encodeName);
            }

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream, encode);
            char[] content = new char[255];
            int len = sr.Read(content, 0, 255);
            while (len > 0)
            {
                sb.Append(new String(content, 0, len));
                len = sr.Read(content, 0, 255);
            }

            sr.Close();
            stream.Close();

            return sb.ToString();
        }
    }
}
