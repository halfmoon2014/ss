using Service.DAL;
using System.Data;
using Service.Util;
namespace FM.Business
{
    public class Menu
    {
        DALInterface execObj;
        SqlCommandString sqlstring;
        public Menu()
        {
            this.sqlstring = new SqlCommandString();
            ConnetString connstr = new ConnetString();
            this.execObj = new DALInterface(null, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));
        }

        ////得到顶级菜单
        public DataTable GetTopLM(string userid)
        {                      
            return this.execObj.SubmitTextDataSet(this.sqlstring.TopLM(userid)).Tables[0];

        }
        /// <summary>
        /// 用户菜单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetUserMenu(string userid)
        {
            return execObj.SubmitTextDataSet(sqlstring.UserMenu(userid)); 
        }
        /// <summary>
        /// 得到非顶级菜单,指定SSID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetMenu(string userid,string id)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.Menu(userid, id)).Tables[0];

        }
        /// <summary>
        /// 取当前TZ信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTzInfo()
        {
            SqlCommandString sqlstring = new SqlCommandString();
            ConnetString connstr = new ConnetString();            
            Service.DAL.DALInterface execObj = new Service.DAL.DALInterface(null, connstr.GetConnString());
            return execObj.SubmitTextDataSet(sqlstring.TzInfo(MySession.SessionHandle.Get("tzid").Trim())).Tables[0];

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">外网地址</param>
        /// <param name="action">发送对象</param>
        public void Log(string ip, string action)
        {
            if (action == "menu")
            {
                this.execObj.SubmitTextInt(this.sqlstring.GetLog(MySession.SessionHandle.Get("userid").Trim(), action, ip));
            }
        }

        /// <summary>
        /// 菜单中间内容项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetContentMenu(string ssid)
        {
            SqlCommandString sqlstring = new SqlCommandString();
            ConnetString connstr = new ConnetString();            
            Service.DAL.DALInterface execObj = new Service.DAL.DALInterface(null, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));
            return execObj.SubmitTextDataSet(sqlstring.GetSsidMenu(MySession.SessionHandle.Get("userid").Trim(), ssid)).Tables[0];  
        }

        public DataSet GetHelp(string id)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.GetHelp(id));
        }
    }
}
