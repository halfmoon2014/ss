using System.Configuration;
namespace MyTy
{
    public class ConfigReader
    {
        /// <summary>
        /// 读取webconfig中的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Read(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }       
        /// <summary>
        /// 判定节点是否包含串联值,不区分大小写
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="text">串联值</param>
        /// <returns></returns>
        public static bool CheckInnerText(string path, string node, string text)
        {
            return CheckInnerText(path,node,text,true);
        }
        /// <summary>
        /// 判定节点是否包含串联值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="text">串联值</param>
        /// <param name="casesensitive">是否区分大小写</param>
        /// <returns></returns>
        public static bool CheckInnerText(string path, string node, string text, bool IgnoreCase)
        {
            return XmlHelp.CheckInnerText(path, node, text, IgnoreCase);
        }
                /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string Read(string path, string node, string attribute){
            return XmlHelp.Read(path, node, attribute);
        }
    }
}
