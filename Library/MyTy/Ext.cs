using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace MyTy
{
    /// <summary>
    /// 业务模板加载
    /// </summary>
    public class ExtUtil
    {
        /// <summary>
        /// 获取业务模板HTML
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHtml(string path, string key)
        {
            StreamReader sR = File.OpenText(path +  key + ".ext");
            string nextLine;
            StringBuilder stringBuilder = new StringBuilder();
            while ((nextLine = sR.ReadLine()) != null)
            {
                stringBuilder.Append(nextLine);
            }
            sR.Close();
            return stringBuilder.ToString();
        }
    }
}
