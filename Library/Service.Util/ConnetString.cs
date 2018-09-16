using System.DataBase;
using MyTy;
using System;
using System.Data;
namespace Service.Util
{
    public class ConnetString
    {
        /// <summary>
        /// 主服务器
        /// </summary>
        private string masterSql = "select a.ds,a.m_port,a.ic,a.m_ui,a.m_pw,a.linkname from v_conn a   where systag=1";

        /// <summary>
        /// 模版服务器
        /// </summary>
        private string mbSql = "select a.ds,a.m_port,a.ic,a.m_ui,a.m_pw,a.linkname from v_conn a   where mbtag=1";

        /// <summary>
        /// 取web.config中的连接字串
        /// </summary>
        /// <returns></returns>
        public string GetConnString()
        {
            return ConfigReader.Read("DBCon");
        }

        /// <summary>
        /// 根据tzid和userid返回账套对应的连接,同个套账不同的用户使用独自的用户名密码
        /// 业务和管理员角色分开
        /// </summary>
        /// <param name="tzid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetDb(string tzid, string userid)
        {
            if ((DateTime.Now - Convert.ToDateTime("2026-1-1")).TotalDays > 0)
            {
                Log.WriteLog("DBstring", "许可过期！");
                return "";
            }
            else if (string.IsNullOrEmpty(tzid) || string.IsNullOrEmpty(userid))
            {
                Log.WriteLog("DBstring", "用户账套信息不足！");
                return "";
            }
            else
            {
                string conn = (string)CacheTools.DbConnGet(tzid, userid);
                if (string.IsNullOrEmpty(conn))
                {
                    string str_sql = "select a.ds,a.m_port,a.ic,b.ui,b.pw from v_conn a  inner join v_tz tz on tz.connid=a.id inner join v_usertz b on tz.id=b.tzid where b.userid ={1} and tz.id={0}";
                    DataSet ds = SqlHelper.ExecuteDataset(ConfigReader.Read("DBCon"), CommandType.Text, string.Format(str_sql, tzid, userid));
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        Log.WriteLog("DBstring", "没有找到对应用户账套的数据库连接字");
                        return "";
                    }
                    else
                    {
                        conn = "Data Source=" + ds.Tables[0].Rows[0]["ds"].ToString().Trim() + (ds.Tables[0].Rows[0]["m_port"].ToString() == "0" ? "" : "," + ds.Tables[0].Rows[0]["m_port"].ToString())
                            + ";Initial Catalog=" + ds.Tables[0].Rows[0]["ic"].ToString().Trim()
                            + ";User ID=" + ds.Tables[0].Rows[0]["ui"].ToString().Trim() + ";Password=" + ds.Tables[0].Rows[0]["pw"].ToString().Trim() + ";";
                        CacheTools.DbConnInsert(tzid, userid, conn);
                        return conn;
                    }
                }
                else
                    return conn;
            }
        }

        /// <summary>
        /// 根据tzid查询对应的业务服务器上有能力创建链接服务器的 数据库用户
        /// 在业务服务器上创建模版,或主系统的链接服务器,使用此连接字,
        /// </summary>
        /// <returns></returns>
        public string GetCreateLinkServerConnetStringInBLL(string tzid)
        {
            string conn = (string)CacheTools.BllConnGet(tzid);
            if (string.IsNullOrEmpty(conn))
            {
                string str_sql = "select a.ds,a.m_port,a.ic,a.y_ui,a.y_pw from v_conn a inner join v_tz b on a.id=b.connid  where b.id={0}";
                DataSet ds = SqlHelper.ExecuteDataset(ConfigReader.Read("DBCon"), CommandType.Text, string.Format(str_sql, tzid));
                if (ds.Tables[0].Rows.Count <= 0)
                {//没有找到
                    Log.WriteLog("DBstring", "没有找到创建连接服务器的用户");
                    return "";
                }
                else
                {
                    conn = "Data Source=" + ds.Tables[0].Rows[0]["ds"].ToString().Trim() + (ds.Tables[0].Rows[0]["m_port"].ToString() == "0" ? "" : "," + ds.Tables[0].Rows[0]["m_port"].ToString())
                        + ";Initial Catalog=" + ds.Tables[0].Rows[0]["ic"].ToString().Trim()
                        + ";User ID=" + ds.Tables[0].Rows[0]["y_ui"].ToString().Trim() + ";Password=" + ds.Tables[0].Rows[0]["y_pw"].ToString().Trim() + ";";
                    CacheTools.BllConnInsert(tzid, conn);
                    return conn;
                }
            }
            else
                return conn;
        }

        /// <summary>
        /// 获取 模版数据库链接服务器字串
        /// </summary>
        public string GetMbLinkname()
        {
            return GetServer("Mb", "linkname") + "." + GetServer("Mb", "ic") + ".dbo.";
        }

        /// <summary>
        /// 获取 主服务器数据库链接服务器字串
        /// </summary>
        public string GetMasterLinkname()
        {
            return GetServer("Master", "linkname") + "." + GetServer("Master", "ic") + ".dbo.";
        }

        public string GetServer(string ServerName, string Type)
        {
            DataTable dataTable=null;
            if (ServerName == "Master")
                dataTable = GetMasterConnService();
            else if (ServerName == "Mb")
                dataTable = GetMbConnService();

            //Type == "linkname" || Type == "ic"
            return dataTable.Rows[0][Type].ToString().Trim();

        }

        /// <summary>
        /// 获取主服务器
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public string GetMasterConn()
        {
            DataTable dataTable = GetMasterConnService();
            return "Data Source=" + dataTable.Rows[0]["ds"].ToString().Trim() + (dataTable.Rows[0]["m_port"].ToString() == "0" ? "" : "," + dataTable.Rows[0]["m_port"].ToString())
                       + ";Initial Catalog=" + dataTable.Rows[0]["ic"].ToString().Trim()
                       + ";User ID=" + dataTable.Rows[0]["m_ui"].ToString().Trim() + ";Password=" + dataTable.Rows[0]["m_pw"].ToString().Trim() + ";";
        }

        private DataTable GetMasterConnService()
        {
            DataTable dataTable = (DataTable)CacheTools.MasterConnGet();
            if (dataTable == null)
            {
                dataTable = SqlHelper.ExecuteDataset(ConfigReader.Read("DBCon"), CommandType.Text, this.masterSql).Tables[0];
                if (dataTable.Rows.Count <= 0)
                {
                    Log.WriteLog("DBstring", "无法获取主服务器地址！");
                    return null;
                }
                else
                    CacheTools.MasterConnInsert(dataTable);
            }
            return dataTable;
        }

        /// <summary>
        /// 获取模版服务器
        /// </summary>
        /// <returns></returns>
        public string GetMbConn()
        {
            DataTable dataTable = GetMbConnService();
            return "Data Source=" + dataTable.Rows[0]["ds"].ToString().Trim() + (dataTable.Rows[0]["m_port"].ToString() == "0" ? "" : "," + dataTable.Rows[0]["m_port"].ToString())
                        + ";Initial Catalog=" + dataTable.Rows[0]["ic"].ToString().Trim()
                        + ";User ID=" + dataTable.Rows[0]["m_ui"].ToString().Trim() + ";Password=" + dataTable.Rows[0]["m_pw"].ToString().Trim() + ";";
        }

        private DataTable GetMbConnService()
        {
            DataTable dataTable = (DataTable)CacheTools.MbConnGet();
            if (dataTable == null)
            {
                dataTable = SqlHelper.ExecuteDataset(MyTy.ConfigReader.Read("DBCon"), CommandType.Text, this.mbSql).Tables[0];
                if (dataTable.Rows.Count <= 0)
                {
                    Log.WriteLog("DBstring", "无法获取模版地址！");
                    return null;
                }
                else
                    CacheTools.MbConnInsert(dataTable);

            }
            return dataTable;
        }

    }
}
