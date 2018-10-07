using System;
using log4net.Layout;
using log4net.Layout.Pattern;
using System.Reflection;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Log4NetApply
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(msg);
        }
        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, LogContent msg)

        public static void WriteLog(Type t, LogContent msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(msg);
        }
        #endregion

    }

    /// <summary>  
    /// 包含了所有的自定字段属性  
    /// </summary>  
    public class LogContent
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        /// <param name="userIP">ip</param>
        /// <param name="username">用户名</param>
        /// <param name="actionsclick"></param>
        /// <param name="description"></param>
        public LogContent(string userIP, string username, string actionsclick, string description)
        {
            UserIP = userIP;
            UserName = username;
            ActionsClick = actionsclick;
            Message = description;
        }

        /// <summary>  
        /// 访问IP  
        /// </summary>  
        public string UserIP { get; set; }

        /// <summary>  
        /// 系统登陆用户  
        /// </summary>  
        public string UserName { get; set; }

        /// <summary>  
        /// 动作事件  
        /// </summary>  
        public string ActionsClick { get; set; }

        /// <summary>  
        /// 日志描述信息  
        /// </summary>  
        public string Message { get; set; }


    }
    public class MyLayout : PatternLayout
    {
        public MyLayout()
        {
            this.AddConverter("property", typeof(LogInfoPatternConverter));
        }
    }
    public class LogInfoPatternConverter : PatternLayoutConverter
    {

        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key  
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs  
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }
        /// <summary>  
        /// 通过反射获取传入的日志对象的某个属性的值  
        /// </summary>  
        /// <param name="property"></param>  
        /// <returns></returns>  

        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            return propertyValue;
        }
    }
}