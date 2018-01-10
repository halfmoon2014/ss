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
            this.sqlstring = new SqlCommandString();
            this.connstr = new ConnetString();
            this.execObj = new Service.DAL.DALInterface(null, connstr.GetConnString());
        }

        /// <summary>
        /// 用户表,根据用户名密码取
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public string UserLogin(string usr, string psw)
        {
            Help hp = new Help();
            string EnPswdStr = hp.GetMM(psw);
            DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.v_user(usr, EnPswdStr));
            string r = "";
            if (ds.Tables[0].Rows.Count <= 0)
            {//没有找到单据
                r = "false";
            }
            else
            {
                SessionHandle.Add("userid", ds.Tables[0].Rows[0]["id"].ToString().Trim());
                r = "true";
            }
            return r;

        }
        /// <summary>
        /// 根据用户ID来取用户表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetUser(string userid)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.v_user(userid));
        }

        //重新登陆,注意value1是用户名称,不是用户名,因为session失效
        public bool Reload(string value1, string value2, string a, string b)
        {
            Help hp = new Help();
            string EnPswdStr = hp.GetMM(value2);
            DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.reload_user(value1, EnPswdStr));

            if (ds.Tables[0].Rows.Count == 1)
            {
                //CSession.Add("username", value1);
                //CSession.Add("user", ds.Tables[0].Rows[0]["usr"].ToString());
                SessionHandle.Add("userid", ds.Tables[0].Rows[0]["id"].ToString());
                SessionHandle.Add("menupage", a);
                SessionHandle.Add("tzid", b);
                return true;
            }
            else
            {
                return false;
            }
            //            Session.Keys[0]
            //"username"
            //Session.Keys[1]
            //"user"
            //Session.Keys[2]
            //"userid"
            //Session.Keys[3]
            //"menupage"
            //Session.Keys[4]
            //"tzid"
            //Session[0]
            //"张茂洪"
            //Session[1]
            //"xz"
            //Session[2]
            //"1"
            //Session[3]
            //"/myweb/webpage/menu_3.aspx"
            //Session[4]
            //"1"
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
            {
                r = "username";
            }
            else
            {//2 判断旧密码


                DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.v_user(username, hp.GetMM(oldPassWord)));
                if (ds.Tables[0].Rows.Count == 0 && adminTag == "0")
                {
                    r = "oldPassWord";
                }
                else
                {
                    if (this.execObj.SubmitTextInt(this.sqlstring.UpdataUserPsw(username, hp.GetMM(newPassWord)), connstr.GetDb(SessionHandle.Get("tzid"), SessionHandle.Get("userid"))) >= 0)
                    {
                        r = "true";
                    }
                }
            }
            return r;
        }
        /// <summary>
        /// 业务服务器创造与主服务器,模版服务器的连接
        /// </summary>
        /// <returns></returns>
        public void CreateDbLink()
        {
            UpdateServerLink();
            UpdateBLLCite();

        }

        /// <summary>
        /// 更新业务服务器上的引用
        /// </summary>
        /// <returns></returns>
        public bool UpdateServerLink()
        {
            //查找入口服务器是否配置了模版和主服务器
            string strSql = "select a.* from v_conn a where a.mbtag=1 or a.systag=1 ;";
            DataSet configForMbAndSys = execObj.SubmitTextDataSet(strSql); ;
            if (configForMbAndSys.Tables[0].Rows.Count == 2)
            {
                //在业务服务器上,查找否已经存在模版连接服务器和主服务器链接
                //如果没有就创建
                strSql = "exec  sp_linkedservers";

                //在业务服务器上,能创建链接服务器权限的用户
                string createServerLinkConnentString = this.connstr.GetCreateLinkServerConnetStringInBLL(SessionHandle.Get("tzid"));
                DataSet linkedServers = this.execObj.SubmitTextDataSet(strSql, createServerLinkConnentString);
                foreach (DataRow dr in configForMbAndSys.Tables[0].Rows)
                {
                    string linkName = dr["linkname"].ToString().Trim();
                    if (linkedServers.Tables[0].Select("SRV_NAME='" + linkName + "'").Length <= 0)
                    {//如果不存在连接  
                        strSql = "exec  sp_addlinkedserver '" + linkName + "',' ','SQLOLEDB','" + dr["ds"].ToString().Trim() + "" + (dr["m_port"].ToString().Trim() == "0" ? "" : "," + dr["m_port"].ToString().Trim()) + "' ";
                        strSql += " exec sp_addlinkedsrvlogin '" + linkName + "','false',null,'" + dr["m_ui"].ToString().Trim() + "','" + dr["m_pw"].ToString().Trim() + "'";
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
        public bool UpdateBLLCite()
        {
            try
            {
                string strSql;
                strSql = " select a.ctime,a.pname,REPLACE(a.definition,' mb.dbo.',' " + this.connstr.GetMbLinkname() + "') definition ,a.type from v_v_ptoclient a   ";
                DataSet rootSetMb = execObj.SubmitTextDataSet(strSql, this.connstr.GetMbConn()); ;

                strSql = " select a.ctime,a.pname,REPLACE(a.definition,' mb.dbo.',' " + this.connstr.GetMasterLinkname() + "') definition ,a.type from v_v_ptoclient a   ";
                DataSet rootSetMaster = execObj.SubmitTextDataSet(strSql, this.connstr.GetMasterConn()); ;

                string createServerLinkConnentString = this.connstr.GetCreateLinkServerConnetStringInBLL(SessionHandle.Get("tzid"));
                strSql = " SELECT C.PNAME,C.CTIME FROM _V_ptoclient  C;";
                strSql += " select a.name as pname  from sys.all_objects a inner join sys.sql_modules b on a.object_id = b.object_id where a.is_ms_shipped=0  ; ";
                DataSet targetSet = this.execObj.SubmitTextDataSet(strSql, createServerLinkConnentString);

                UpdateBLLSQL(this.connstr.GetMbLinkname(), rootSetMb, createServerLinkConnentString, targetSet);
                UpdateBLLSQL(this.connstr.GetMasterLinkname(), rootSetMaster, createServerLinkConnentString, targetSet);
                return true;
            }
            catch (System.Exception e)
            {
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
                        if (dr["type"].ToString().Trim().ToLower() == "p")
                        {//处理的存储过程
                            if (targetSet.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除
                                execObj.SubmitTextInt("Drop procedure " + dr["pname"].ToString().Trim(), createServerLinkConnentString); ;
                            }
                            execObj.SubmitTextInt("/*此存储过程由外部服务器("+ linkName + ")创建,不能直接修改*/"
                                +dr["definition"].ToString().Trim(), createServerLinkConnentString); ;
                        }
                        else
                        {//视图

                            if (targetSet.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除
                                this.execObj.SubmitTextInt("Drop view " + dr["pname"].ToString().Trim(), createServerLinkConnentString); ;
                            }
                            execObj.SubmitTextInt("create view  " + dr["pname"].ToString().Trim() 
                                + " as /*此存储过程由外部服务器(" + linkName + ")创建,不能直接修改*/ "
                                +" select * from  " + linkName + dr["pname"].ToString().Trim(), createServerLinkConnentString); ;                            
                        }
                        execObj.SubmitTextInt(" ; delete from _V_ptoclient where pname='" + dr["pname"].ToString().Trim()
                                + "'; insert _V_ptoclient(ctime,pname) values(CONVERT(DATETIME, '" + ctime.ToString("yyyy/MM/dd HH:mm:ss:fff") + "'),'" + dr["pname"].ToString().Trim() + "');", createServerLinkConnentString); ;

                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
    }
}
