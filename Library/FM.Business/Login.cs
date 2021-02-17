using System.Data;
using Service.DAL;
using MySession;
using Service.Util;
namespace FM.Business
{
    public class Login
    {
        DALInterface execObj;
        SqlCommandString sqlstring;
        ConnetString connstr;
        public Login()
        {
            sqlstring = new SqlCommandString();
            connstr = new ConnetString();
            execObj = new DALInterface(null, connstr.GetConnString());
        }

        /// <summary>
        /// 用户表,根据用户名密码取
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public int UserLogin(string usr, string psw)
        {
            Help hp = new Help();
            string EnPswdStr = hp.GetMM(psw);
            DataSet ds = execObj.SubmitTextDataSet(sqlstring.User(usr, EnPswdStr));
            if (ds.Tables[0].Rows.Count <= 0)
                //没有找到单据
                return 0;
            else
            {               
                return int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            }
        }
        /// <summary>
        /// 根据用户ID来取用户表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetUser(string userid)
        {
            return execObj.SubmitTextDataSet(sqlstring.User(userid));
        }

        /// <summary>
        /// 重新登陆,注意value1是用户名称,不是用户名,因为session失效
        /// </summary>
        /// <param name="value1">用户名称,不是用户名</param>
        /// <param name="value2"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Reload(string value1, string value2, string a, string b)
        {
            Help hp = new Help();
            string EnPswdStr = hp.GetMM(value2);
            DataSet ds = execObj.SubmitTextDataSet(sqlstring.ReloadUser(value1, EnPswdStr));

            if (ds.Tables[0].Rows.Count == 1)
            {          
                SessionHandle.Add("userid", ds.Tables[0].Rows[0]["id"].ToString());
                SessionHandle.Add("menupage", a);
                SessionHandle.Add("tzid", b);
                return true;
            }
            else         
                return false;

        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassWord"></param>
        /// <param name="newPassWord"></param>
        /// <param name="adminTag"></param>
        /// <returns></returns>
        public string SetUserPsw(string username, string oldPassWord, string newPassWord, string adminTag)
        {
            Help hp = new Help();
            Login lg = new Login();

            string r = "false";
            //1判断用户名
            if (username != lg.GetUser(SessionHandle.Get("userid")).Tables[0].Rows[0]["usr"].ToString() && adminTag == "0")
                r = "username";
            else
            {
                //2 判断旧密码
                DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.User(username, hp.GetMM(oldPassWord)));
                if (ds.Tables[0].Rows.Count == 0 && adminTag == "0")
                    r = "oldPassWord";
                else
                {
                    if (this.execObj.SubmitTextInt(this.sqlstring.UpdataUserPsw(username, hp.GetMM(newPassWord)), connstr.GetDb(SessionHandle.Get("tzid"), SessionHandle.Get("userid"))) >= 0)
                        r = "true";
                }
            }
            return r;
        }
        /// <summary>
        /// 业务服务器创造与主服务器,模版服务器的连接
        /// </summary>
        /// <returns></returns>
        public void CreateDbLink(int tzid)
        {
            UpdateServerLink(tzid);
            UpdateBLLCite(tzid);

        }

        /// <summary>
        /// 更新业务服务器上的引用
        /// </summary>
        /// <returns></returns>
        public bool UpdateServerLink(int tzid)
        {
            //查找入口服务器是否配置了模版和主服务器
            
            string strSql = sqlstring.GetCtlServers();
            DataSet configForMbAndSys = execObj.SubmitTextDataSet(strSql); ;
            if (configForMbAndSys.Tables[0].Rows.Count == 2)
            {
                //在业务服务器上,查找否已经存在模版连接服务器和主服务器链接
                //如果没有就创建
                strSql = sqlstring.GetSearchLink();

                //在业务服务器上,能创建链接服务器权限的用户
                string createServerLinkConnentString = connstr.GetCreateLinkServerConnetStringInBLL(tzid.ToString());
                DataSet linkedServers = execObj.SubmitTextDataSet(strSql, createServerLinkConnentString);
                foreach (DataRow dr in configForMbAndSys.Tables[0].Rows)
                {
                    string linkName = dr["linkname"].ToString().Trim();
                    if (linkedServers.Tables[0].Select("SRV_NAME='" + linkName + "'").Length <= 0)
                    {//如果不存在连接  
                        strSql = sqlstring.GetCreateLink(linkName, dr["ds"].ToString().Trim(), dr["m_port"].ToString().Trim(), dr["m_ui"].ToString().Trim(), dr["m_pw"].ToString().Trim());                        
                        this.execObj.SubmitTextObject(strSql, createServerLinkConnentString);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新业务服务器上的引用
        /// </summary>   
        public bool UpdateBLLCite(int tzid)
        {
            try
            {
                string strSql;
                strSql = sqlstring.Getptoclient(connstr.GetMbLinkname());
                DataSet rootSetMb = execObj.SubmitTextDataSet(strSql, connstr.GetMbConn()); ;

                strSql = sqlstring.Getptoclient(connstr.GetMasterLinkname());
                DataSet rootSetMaster = execObj.SubmitTextDataSet(strSql, connstr.GetMasterConn()); ;

                string createServerLinkConnentString = connstr.GetCreateLinkServerConnetStringInBLL(tzid.ToString());
                strSql = sqlstring.GetBLLptoclient();
                DataSet targetSet = execObj.SubmitTextDataSet(strSql, createServerLinkConnentString);

                UpdateBLLSQL(connstr.GetMbLinkname(), rootSetMb, createServerLinkConnentString, targetSet);
                UpdateBLLSQL(connstr.GetMasterLinkname(), rootSetMaster, createServerLinkConnentString, targetSet);
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.Write(e.Message);
                return false;
            }

        }

        private bool UpdateBLLSQL(string linkName, DataSet rootSet, string createServerLinkConnentString, DataSet targetSet)
        {
            try
            {
                foreach (DataRow dr in rootSet.Tables[0].Rows)
                {
                    DataRow[] name = targetSet.Tables[0].Select("pname='" + dr["pname"] + "'");
                    System.DateTime ctime = (System.DateTime)dr["CTIME"];
                    if (name.Length == 0 || ctime.CompareTo((System.DateTime)name[0]["ctime"])!=0)
                    {
                        if (dr["type"].ToString().Trim().ToLower() == "p" || dr["type"].ToString().Trim().ToLower() == "tf")
                        {//处理的存储过程
                            if (targetSet.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除
                                execObj.SubmitTextInt(sqlstring.GetDropProcedure( dr["pname"].ToString().Trim()), createServerLinkConnentString); ;
                            }
                            execObj.SubmitTextInt(sqlstring.GetCreateProcedure(dr["definition"].ToString().Trim(),linkName), createServerLinkConnentString); ;
                        }
                        else
                        {//视图

                            if (targetSet.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除
                                this.execObj.SubmitTextInt(sqlstring.GetDropView (dr["pname"].ToString().Trim()), createServerLinkConnentString); ;
                            }
                            execObj.SubmitTextInt(sqlstring.GetCreateView(dr["pname"].ToString().Trim(),linkName), createServerLinkConnentString); ;                            
                        }
                        execObj.SubmitTextInt(sqlstring.GetUpBLLPtoclient(dr["pname"].ToString().Trim(), ctime.ToString("yyyy/MM/dd HH:mm:ss:fff")), createServerLinkConnentString); 
                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.Write(e.Message);
                return false;
            }
        }
    }
}
