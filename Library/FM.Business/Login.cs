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
        public bool CreateDbLink()
        {     

            //查找入口服务器是否配置了模版和主服务器
            string strSql = "select a.* from v_conn a where a.mbtag=1 or a.systag=1 ;";
            DataSet configForMbAndSys = execObj.SubmitTextDataSet(strSql); ;
            if (configForMbAndSys.Tables[0].Rows.Count == 2)
            {
                bool changLink = false;//标识是否需要变更链接

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
                        changLink = true;
                        strSql = "exec  sp_addlinkedserver '" + linkName + "',' ','SQLOLEDB','" + dr["ds"].ToString().Trim() + "" + (dr["m_port"].ToString().Trim() == "0" ? "" : "," + dr["m_port"].ToString().Trim()) + "' ";
                        strSql += " exec sp_addlinkedsrvlogin '" + linkName + "','false',null,'" + dr["m_ui"].ToString().Trim() + "','" + dr["m_pw"].ToString().Trim() + "'";
                        this.execObj.SubmitTextObject(strSql, createServerLinkConnentString);                        
                    }
                }
                
                #region 从母板中读取要更新的数据库对象,按名称和修改时间匹配,发现不存在名称或时间不一致的就更新掉
                strSql = " select a.ctime,a.pname,REPLACE(a.definition,' mb.dbo.',' " + this.connstr.GetMbLinkname() + "') definition ,a.type from " + this.connstr.GetMbLinkname() + "v_v_ptoclient a   ";
                strSql += " left join _V_ptoclient c on c.pname=a.pname where c.ctime is null or datediff(S,a.ctime,c.ctime)<>0 ;";
                strSql += " select a.name as pname  from sys.all_objects a inner join sys.sql_modules b on a.object_id = b.object_id where a.is_ms_shipped=0  ; ";

                //从主服务器上更新数据
                strSql += " select a.ctime,a.pname,REPLACE(a.definition,' master.dbo.',' " + this.connstr.GetMasterLinkname() + "') definition ,a.type from " + this.connstr.GetMasterLinkname() + "v_v_ptoclient a   ";
                strSql += " left join _V_ptoclient c on c.pname=a.pname where c.ctime is null or datediff(S,a.ctime,c.ctime)<>0 ;";                

                DataSet dsp = this.execObj.SubmitTextDataSet(strSql, createServerLinkConnentString); 
                
                foreach (DataRow dr in dsp.Tables[0].Rows)
                {
                    if (dr["type"].ToString().Trim().ToLower() == "p")
                    {//处理的存储过程
                        if (dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                        {//如果目标存在,就删除                            
                            execObj.SubmitTextInt("Drop procedure " + dr["pname"].ToString().Trim(), createServerLinkConnentString);
                        }
                        execObj.SubmitTextInt(dr["definition"].ToString().Trim(), createServerLinkConnentString);
                        execObj.SubmitTextInt(" ; delete from _V_ptoclient where pname='" + dr["pname"].ToString().Trim() + "'; "
                            +"insert _V_ptoclient(ctime,pname) values('" + dr["ctime"].ToString().Trim() + "','" + dr["pname"].ToString().Trim() + "');", createServerLinkConnentString);                        
                    }
                    else
                    {//视图
                        if (changLink || dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length <= 0)
                        {
                            if (dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除                                
                                execObj.SubmitTextInt("Drop view " + dr["pname"].ToString().Trim(), createServerLinkConnentString);
                            }
                            execObj.SubmitTextInt("create view " + dr["pname"].ToString().Trim() + " as select * from  " + this.connstr.GetMbLinkname() + dr["pname"].ToString().Trim() + "; ", createServerLinkConnentString);                            
                        }
                    }
                }
                #endregion   
                             
                #region 从主服务器上更新数据
                foreach (DataRow dr in dsp.Tables[2].Rows)
                {
                    if (dr["type"].ToString().Trim().ToLower() == "p")
                    {//处理的存储过程
                        if (dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                        {//如果目标存在,就删除
                            execObj.SubmitTextInt("Drop procedure " + dr["pname"].ToString().Trim(), createServerLinkConnentString); ;                            
                        }
                        this.execObj.SubmitTextInt(dr["definition"].ToString().Trim(), createServerLinkConnentString); ;
                        this.execObj.SubmitTextInt(" ; delete from _V_ptoclient where pname='" + dr["pname"].ToString().Trim() + "'; insert _V_ptoclient(ctime,pname) values('" + dr["ctime"].ToString().Trim() + "','" + dr["pname"].ToString().Trim() + "');", createServerLinkConnentString); ;                        
                    }
                    else
                    {//视图
                        if (changLink || dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length <= 0)
                        {
                            if (dsp.Tables[1].Select("pname='" + dr["pname"].ToString().Trim() + "'").Length > 0)
                            {//如果目标存在,就删除
                                this.execObj.SubmitTextInt("Drop view " + dr["pname"].ToString().Trim(), createServerLinkConnentString); ;                                                        
                            }
                            this.execObj.SubmitTextInt("create view " + dr["pname"].ToString().Trim() + " as select * from  " + this.connstr.GetMasterLinkname() + dr["pname"].ToString().Trim() + "; ", createServerLinkConnentString); ;                                                    
                        }
                    }
                }
                #endregion
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
