using System;

namespace MyTy
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 记录日志
        /// </summary>        
        /// <param name="tag">标记</param>
        /// <param name="strMemo">日志内容</param>
        public void WriteLog(string tag, string strMemo)
        {
            Utils utils = new Utils();
            string PhysicalApplicationPath = utils.getWebPath() + "//logs";

            if (PhysicalApplicationPath != string.Empty)
            {
                System.IO.StreamWriter sr = null;
                try
                {
                    string filename = PhysicalApplicationPath + "//log.txt";
                    if (!System.IO.Directory.Exists(PhysicalApplicationPath))
                    {
                        System.IO.Directory.CreateDirectory(PhysicalApplicationPath);
                    }                    
                    

                    if (!System.IO.File.Exists(filename))
                    {
                        sr = System.IO.File.CreateText(filename);
                    }
                    else
                    {
                        sr = System.IO.File.AppendText(filename);
                    }
                    sr.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH-mm-ss] "));
                    sr.WriteLine(tag + ":" + strMemo);
                }
                catch
                {
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }

        }
    }
}
