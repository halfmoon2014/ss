using System.Collections.Generic;
using System.Text;
using Service.DAL;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

using Service.Util.Modal;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Linq;
using MyTy;

namespace Service.Util
{
    public class Business
    {
        DALInterface execObj;
        SqlCommandString sqlstring;
        ConnetString connstr;
        string tzid;
        string userid;
        public Business(string tzid,string userid)
        {
            this.sqlstring = new SqlCommandString();
            this.connstr = new ConnetString();
            this.tzid = tzid;
            this.userid = userid;
            this.execObj = new DALInterface(null, this.connstr.GetDb(tzid,userid));
        }

        /// <summary>
        /// 执行表创建视图过程
        /// </summary>
        /// <param name="value1"></param>
        /// <returns></returns>
        public string AutoView(string value1)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@tbname", System.Data.SqlDbType.VarChar, 4000);
            Parm[0].Value = value1;
            this.execObj.SubmitStoredProcedureDataSet("p_AUTOVIEW", Parm);
            return "true";
        }

        public DataSet GetLeftTree()
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.LeftTree());

        }

        public DataSet GetCTable(int id, string lx, string js, string sql, string wid, string myname, int page, int rows)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.CTable(id, lx, js, sql, wid, myname));
        }

        public DataSet GetCTableLx()
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.CTableLx());
        }

        public Dictionary<string, string> GettContEdit(int wid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] rstring = new string[15];
            rstring[2] = "";
            rstring[3] = "";
            rstring[1] = "";
            rstring[0] = "";

            rstring[4] = "";
            rstring[5] = "";
            rstring[6] = "";
            rstring[7] = "";

            rstring[8] = "";
            rstring[9] = "";
            rstring[10] = "";
            rstring[11] = "";
            rstring[12] = "";
            rstring[13] = "";
            rstring[14] = "";
            DataTable dt = this.execObj.SubmitTextDataSet(this.sqlstring.ContEditSql(wid)).Tables[0];
            //rstring[1] = dt.Rows[0]["tbywname"].ToString().Trim();
            //rstring[0] = dt.Rows[0]["sql"].ToString().Trim();
            //rstring[2] = dt.Rows[0]["tbzwname"].ToString().Trim();
            //rstring[3] = dt.Rows[0]["fwsql"].ToString().Trim();

            //rstring[4] = dt.Rows[0]["mrcx"].ToString().Trim();
            //rstring[5] = dt.Rows[0]["pagesize"].ToString().Trim();
            //rstring[6] = dt.Rows[0]["orderby"].ToString().Trim();
            //rstring[7] = dt.Rows[0]["myadd"].ToString().Trim();

            //rstring[8] = dt.Rows[0]["mxgl"].ToString().Trim();
            //rstring[9] = dt.Rows[0]["mxsql"].ToString().Trim();
            //rstring[10] = dt.Rows[0]["mxhgl"].ToString().Trim();
            //rstring[11] = dt.Rows[0]["mxhord"].ToString().Trim();
            //rstring[12] = dt.Rows[0]["mxhsql"].ToString().Trim();
            //rstring[13] = dt.Rows[0]["mxly"].ToString().Trim();
            //rstring[14] = dt.Rows[0]["sql_2"].ToString().Trim();
            dic.Add("name", dt.Rows[0]["name"].ToString().Trim());
            dic.Add("sql", dt.Rows[0]["sql"].ToString().Trim());
            dic.Add("fwsql", dt.Rows[0]["fwsql"].ToString().Trim());
            dic.Add("mrcx", dt.Rows[0]["mrcx"].ToString().Trim());
            dic.Add("pagesize", dt.Rows[0]["pagesize"].ToString().Trim());
            dic.Add("orderby", dt.Rows[0]["orderby"].ToString().Trim());
            dic.Add("myadd", dt.Rows[0]["myadd"].ToString().Trim());
            dic.Add("mxgl", dt.Rows[0]["mxgl"].ToString().Trim());
            dic.Add("mxsql", dt.Rows[0]["mxsql"].ToString().Trim());
            dic.Add("mxhgl", dt.Rows[0]["mxhgl"].ToString().Trim());
            dic.Add("mxhord", dt.Rows[0]["mxhord"].ToString().Trim());
            dic.Add("mxhsql", dt.Rows[0]["mxhsql"].ToString().Trim());
            dic.Add("mxly", dt.Rows[0]["mxly"].ToString().Trim());
            dic.Add("sql_2", dt.Rows[0]["sql_2"].ToString().Trim());

            return dic;
        }

        public string[] GettContEditJs(int wid)
        {
            string[] rstring = new string[1];
            rstring[0] = "";
            DataTable dt = this.execObj.SubmitTextDataSet(this.sqlstring.ContEdit(wid, "js")).Tables[0];
            rstring[0] = dt.Rows[0]["js"].ToString().Trim();
            return rstring;
        }

        public string[] GettContEditHelp(int wid)
        {
            string[] rstring = new string[1];
            rstring[0] = "";
            DataTable dt = this.execObj.SubmitTextDataSet(this.sqlstring.ContEdit(wid, "help")).Tables[0];
            rstring[0] = dt.Rows[0]["help"].ToString().Trim();
            return rstring;
        }

        public DataSet GetTbzd(int wid)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.Tbzd(wid));
        }
        public DataSet GetTbLayOut(int wid, string lx)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.TbLayOut(wid, lx));
        }

        /// <summary>
        /// WEB设计数据源保存
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="mrcx"></param>
        /// <param name="myadd"></param>
        /// <param name="orderby"></param>
        /// <param name="pagesize"></param>
        /// <param name="mxgl"></param>
        /// <param name="mxsql"></param>
        /// <param name="mxhgl"></param>
        /// <param name="mxhord"></param>
        /// <param name="mxhsql"></param>
        /// <param name="mxly"></param>
        /// <param name="sql_2"></param>
        /// <returns></returns>
        public string UpSJY(string wid, string value1, string value3, string value4, string mrcx, string myadd, string orderby, string pagesize, string mxgl, string mxsql, string mxhgl, string mxhord, string mxhsql, string mxly, string sql_2)
        {
            SqlParameter[] Parm = new SqlParameter[15];
            Parm[0] = new SqlParameter("@wid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 4000);
            Parm[1].Value = value1;
            Parm[2] = new SqlParameter("@sql", System.Data.SqlDbType.VarChar, 8000);
            Parm[2].Value = value3;
            Parm[3] = new SqlParameter("@fwsql", System.Data.SqlDbType.VarChar, 8000);
            Parm[3].Value = value4;
            Parm[4] = new SqlParameter("@mrcx", System.Data.SqlDbType.Int);
            Parm[4].Value = int.Parse(mrcx);
            Parm[5] = new SqlParameter("@myadd", System.Data.SqlDbType.Int);
            Parm[5].Value = int.Parse(myadd);
            Parm[6] = new SqlParameter("@orderby", System.Data.SqlDbType.VarChar, 4000);
            Parm[6].Value = orderby;
            Parm[7] = new SqlParameter("@pagesize", System.Data.SqlDbType.Int);
            Parm[7].Value = int.Parse(pagesize);
            Parm[8] = new SqlParameter("@mxgl", System.Data.SqlDbType.VarChar, 4000);
            Parm[8].Value = mxgl;
            Parm[9] = new SqlParameter("@mxsql", System.Data.SqlDbType.VarChar, 8000);
            Parm[9].Value = mxsql;
            Parm[10] = new SqlParameter("@mxhgl", System.Data.SqlDbType.VarChar, 4000);
            Parm[10].Value = mxhgl;
            Parm[11] = new SqlParameter("@mxhord", System.Data.SqlDbType.VarChar, 4000);
            Parm[11].Value = mxhord;
            Parm[12] = new SqlParameter("@mxhsql", System.Data.SqlDbType.VarChar, 8000);
            Parm[12].Value = mxhsql;
            Parm[13] = new SqlParameter("@mxly", System.Data.SqlDbType.VarChar, 100);
            Parm[13].Value = mxly;
            Parm[14] = new SqlParameter("@sql_2", System.Data.SqlDbType.VarChar, 8000);
            Parm[14].Value = sql_2;

            int r = this.execObj.SubmitStoredProcedureInt("p_UPSJY", Parm);
            if (r > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }


        }

        /// <summary>
        /// WEB设计js保存
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="js"></param>
        /// <returns></returns>
        public string UpSJYJs(string wid, string js)
        {
            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@wid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@js", System.Data.SqlDbType.NText);
            Parm[1].Value = MyTy.MyCode.DoUrlDecode(js);

            int r = this.execObj.SubmitStoredProcedureInt("p_UPWEBJS", Parm);
            if (r > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }

        }

        /// <summary>
        ///  WEB设计help保存
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="js"></param>
        /// <returns></returns>
        public string UpSJYHelp(string wid, string js)
        {
            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@wid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@help", System.Data.SqlDbType.VarChar, 4000);
            Parm[1].Value = MyTy.MyCode.DoUrlDecode(js);

            int r = this.execObj.SubmitStoredProcedureInt("p_UPWEBHELP", Parm);
            if (r > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }

        }

        /// <summary>
        /// WEB设计 新增一个webid
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="mc"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public int AddSJYSJ(string userid, string mc, string lx)
        {
            SqlParameter[] Parm = new SqlParameter[3];
            Parm[0] = new SqlParameter("@userid", System.Data.SqlDbType.Int);
            Parm[0].Value = userid;
            Parm[1] = new SqlParameter("@mc", System.Data.SqlDbType.VarChar, 4000);
            Parm[1].Value = mc;
            Parm[2] = new SqlParameter("@lx", System.Data.SqlDbType.VarChar, 4000);
            Parm[2].Value = lx;

            return this.execObj.SubmitStoredProcedureInt("p_WEBSJ_ADD", Parm);
        }

        /// <summary>
        /// WEB设计 修改一个webid
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="mc"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public int EditSJYSJ(string wid, string mc, string lx)
        {
            SqlParameter[] Parm = new SqlParameter[3];
            Parm[0] = new SqlParameter("@wid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@mc", System.Data.SqlDbType.VarChar, 4000);
            Parm[1].Value = mc;
            Parm[2] = new SqlParameter("@lx", System.Data.SqlDbType.VarChar, 4000);
            Parm[2].Value = lx;

            return this.execObj.SubmitStoredProcedureInt("p_WEBSJ_EDIT", Parm);
        }

        /// <summary>
        /// WEB设计 删除webid
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public string DelSJYSJ(string wid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@wid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;

            int r = this.execObj.SubmitStoredProcedureInt("p_WEBSJ_DEL", Parm);
            if (r > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// WEB设计 复制webid
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public string CopySJYSJ(string wid)
        {
            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@oldwid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@USERID", System.Data.SqlDbType.Int);
            Parm[1].Value = this.userid;
            int r = 0;
            try
            {
                 r = this.execObj.SubmitStoredProcedureInt("p_WEBSJ_FZ", Parm);
            }catch(Exception e)
            {
                Console.Write(e.Message);
                r = 0;
            }
            if (r > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// WEB设计 复制webid中的每项
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public string CopyWebSJZD(string wid, string newwid, string bs)
        {
            SqlParameter[] Parm = new SqlParameter[3];
            Parm[0] = new SqlParameter("@oldwid", System.Data.SqlDbType.Int);
            Parm[0].Value = wid;
            Parm[1] = new SqlParameter("@newwid", System.Data.SqlDbType.Int);
            Parm[1].Value = newwid;
            Parm[2] = new SqlParameter("@bs", System.Data.SqlDbType.VarChar, 400);
            Parm[2].Value = bs;

            int r = this.execObj.SubmitStoredProcedureInt("p_WEBSJ_FZ_ZD", Parm);
            if (r >= 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }

        }

        public DataSet GetLayout(int wid, string wz)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.Layout(wid, wz));
        }

        public DataSet GetWebFwSql(int wid)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.WebFwSql(wid));
        }

        /// <summary>
        /// 保存菜单的帮助文档
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public string UpHelp(string value1, string value2)
        {
            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@id", System.Data.SqlDbType.Int);
            Parm[0].Value = value2;
            Parm[1] = new SqlParameter("@help", System.Data.SqlDbType.VarChar, 4000);
            Parm[1].Value = value1;

            int r = this.execObj.SubmitStoredProcedureInt("p_UPMENUHELP", Parm);
            if (r >= 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }

        }

        /// <summary>
        /// 发布webid
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public string web_fb(string wid)
        {
            string str_sql = "select * from v_user_conn where mbtag=1 ;" +                
                "select zd.* from v_tbzd zd   where zd.webid=" + wid + " ;" +
                "select * from v_wid where id=" + wid + " ;" +
                "select * from v_wid_layout where webid=" + wid + ";";
            DAL.DALInterface execObj = new DAL.DALInterface(null, connstr.GetMbConn());
            DataSet ds = execObj.SubmitTextDataSet(str_sql);
            string error = "";
            string upstr = "";
            //查找模版服务器上有没有特定链接服务器
            str_sql = "exec  sp_linkedservers";
            execObj.SetConnectionString(connstr.GetCreateLinkServerConnetStringInBLL(this.tzid));
            DataSet dsexit = execObj.SubmitTextDataSet(str_sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    #region 发布页面SQL
                    /*清*/
                    upstr = " ";
                    upstr += " alter table tb_tbzd disable trigger trtb_tbzd; delete from v_tbzd where webid=" + wid + ";alter table tb_tbzd enable trigger trtb_tbzd;";
                    upstr += "alter table tb_wid disable trigger trtb_wid; delete from v_wid where id=" + wid + ";alter table tb_wid enable trigger trtb_wid;";
                    upstr += " alter table tb_wid_layout disable trigger trtb_wid_layout ; delete from v_wid_layout where webid=" + wid + ";alter table tb_wid_layout enable trigger trtb_wid_layout";

                    if (dsexit.Tables[0].Select("SRV_NAME='" + dr["tzmc"].ToString() + "无用" + "'").Length <= 0)
                    {//如果不存在连接  ,创建链接服务器   
                        //暂时不用
                        str_sql = "exec  sp_addlinkedserver '" + dr["tzmc"].ToString() + "无用" + "',' ','SQLOLEDB','" + dr["ds"].ToString().Trim() + "" + (dr["m_port"].ToString().Trim() == "0" ? ",1433" : "," + dr["m_port"].ToString().Trim()) + "' ";
                        str_sql += " exec sp_addlinkedsrvlogin '" + dr["tzmc"].ToString() + "无用" + "','false',null,'" + dr["y_ui"].ToString().Trim() + "','" + dr["y_pw"].ToString().Trim() + "'";
                        execObj.SetConnectionString(connstr.GetCreateLinkServerConnetStringInBLL(this.tzid));
                        execObj.SubmitTextInt(str_sql);
                        //SqlHelper.ExecuteScalar(db.GetTzDb(CSession.Get("tzid")), CommandType.Text, str_sql);
                    }

                    //upstr += "  SET IDENTITY_INSERT tb_tbinfo ON  ";
                    //upstr += "  INSERT v_tbinfo ([id],[tbywname],[tbzwname],[bz],[sql],[sql_2],[mxgl],[mxhgl],[mxhord],[mxhsql],[mxsql],[addrq],[fwsql],[mxly],sysdel,sysdeltime) ";
                    //upstr += "  values ";
                    //upstr += "  ('" + ds.Tables[1].Rows[0]["id"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["tbywname"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["tbzwname"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["bz"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["sql"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["sql_2"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["mxgl"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["mxhgl"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["mxhord"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["mxhsql"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["mxsql"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["addrq"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["fwsql"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["mxly"].ToString().Replace("'", "''") + "','"
                    //    + ds.Tables[1].Rows[0]["sysdel"].ToString().Replace("'", "''") + "','" + ds.Tables[1].Rows[0]["sysdeltime"].ToString().Replace("'", "''") + "')";
                    //upstr += " SET IDENTITY_INSERT tb_tbinfo OFF ";

                    upstr += "  SET IDENTITY_INSERT tb_tbzd ON  ";
                    foreach (DataRow dr2 in ds.Tables[1].Rows)
                    {
                        upstr += "  INSERT v_tbzd (id,sysdel,sysdeltime,[ywname],[zwname],[ord],[width],[webid],[visible],[readonly],[type],[sx],[bz],[showzero],[event],[btnvalue],[showmrrq],[hj],[hbltname],[px],[format],[prtname]) ";
                        upstr += "  values ";
                        upstr += "  ('" + dr2["id"].ToString().Replace("'", "''") + "','" + dr2["sysdel"].ToString().Replace("'", "''") + "','"
                            + dr2["sysdeltime"].ToString().Replace("'", "''") + "','" + dr2["ywname"].ToString().Replace("'", "''") + "','"
                            + dr2["zwname"].ToString().Replace("'", "''") + "','" + dr2["ord"].ToString().Replace("'", "''") + "','"
                            + dr2["width"].ToString().Replace("'", "''") + "','" + dr2["webid"].ToString().Replace("'", "''") + "','"
                            + dr2["visible"].ToString().Replace("'", "''") + "','" + dr2["readonly"].ToString().Replace("'", "''") + "','"
                            + dr2["type"].ToString().Replace("'", "''") + "','"
                            + dr2["sx"].ToString().Replace("'", "''") + "','" + dr2["bz"].ToString().Replace("'", "''") + "','"
                            + dr2["showzero"].ToString().Replace("'", "''") + "','" + dr2["event"].ToString().Replace("'", "''") + "','"
                            + dr2["btnvalue"].ToString().Replace("'", "''") + "','" + dr2["showmrrq"].ToString().Replace("'", "''") + "','"
                            + dr2["hj"].ToString().Replace("'", "''") + "','" + dr2["hbltname"].ToString().Replace("'", "''") + "','"
                            + dr2["px"].ToString().Replace("'", "''") + "','"+ dr2["format"].ToString().Replace("'", "''") + "','" + dr2["prtname"].ToString().Replace("'", "''") + "')";
                    }
                    upstr += " SET IDENTITY_INSERT tb_tbzd OFF ";

                    upstr += "  SET IDENTITY_INSERT tb_wid ON  ";
                    upstr += "  INSERT v_wid (id,sysdel,sysdeltime,[name],[sql],[sql_2],[mxgl],[mxhgl],[mxhord],[mxhsql],[mxsql],[fwsql],[mxly],[mrcx],[orderby],[PageSize],[JS],[myadd],[insertcmd],[updatecmd],[deletecmd],[wwidth],[wheight],[wbdll],[classNamespace],[className],[methodName],[DllPath],[onlycx],[Userid],[help],[guid],[lx]) ";
                    upstr += "  values ";
                    upstr += "  ('" + ds.Tables[2].Rows[0]["id"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["sysdel"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["sysdeltime"].ToString().Replace("'", "''") + "','"

                        + ds.Tables[2].Rows[0]["name"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["sql"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["sql_2"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["mxgl"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["mxhgl"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["mxhord"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["mxhsql"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["mxsql"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["fwsql"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["mxly"].ToString().Replace("'", "''") + "','"

                        + ds.Tables[2].Rows[0]["mrcx"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["orderby"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["PageSize"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["js"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["myadd"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["insertcmd"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["updatecmd"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["deletecmd"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["wwidth"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["wheight"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["wbdll"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["classNamespace"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["className"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["methodName"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["DllPath"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["onlycx"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["Userid"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["help"].ToString().Replace("'", "''") + "','"
                        + ds.Tables[2].Rows[0]["guid"].ToString().Replace("'", "''") + "','" + ds.Tables[2].Rows[0]["lx"].ToString().Replace("'", "''") + "')";

                    upstr += " SET IDENTITY_INSERT tb_wid OFF ";

                    upstr += "  SET IDENTITY_INSERT tb_wid_layout ON  ";
                    foreach (DataRow dr4 in ds.Tables[3].Rows)
                    {
                        upstr += "  INSERT v_wid_layout (id,sysdel,sysdeltime, [webid],[lx],[mc],[ord],[mobileord],[type],[qwidth],[width],[visible],[readonly],[event],[bz],[nwebid],[htmlid],[eastwidth],[westwidth],[northheight],[southheight],[dwidth],[dheight],[naspx],[yy],[session],[zb],css,css0,[mrz],[bds]) ";
                        upstr += "  values ";
                        upstr += "  ('" + dr4["id"].ToString().Replace("'", "''") + "','" + dr4["sysdel"].ToString().Replace("'", "''") + "','"
                            + dr4["sysdeltime"].ToString().Replace("'", "''") + "','" + dr4["webid"].ToString().Replace("'", "''") + "','"
                            + dr4["lx"].ToString().Replace("'", "''") + "','" + dr4["mc"].ToString().Replace("'", "''") + "','"
                            + dr4["ord"].ToString().Replace("'", "''") + "','"+ dr4["mobileord"].ToString().Replace("'", "''") + "','" + dr4["type"].ToString().Replace("'", "''") + "','"
                            + dr4["qwidth"].ToString().Replace("'", "''") + "','" + dr4["width"].ToString().Replace("'", "''") + "','"
                            + dr4["visible"].ToString().Replace("'", "''") + "','" + dr4["readonly"].ToString().Replace("'", "''") + "','"
                            + dr4["event"].ToString().Replace("'", "''") + "','" + dr4["bz"].ToString().Replace("'", "''") + "','"
                            + dr4["nwebid"].ToString().Replace("'", "''") + "','" + dr4["htmlid"].ToString().Replace("'", "''") + "','"
                            + dr4["eastwidth"].ToString().Replace("'", "''") + "','" + dr4["westwidth"].ToString().Replace("'", "''") + "','"
                            + dr4["northheight"].ToString().Replace("'", "''") + "','" + dr4["southheight"].ToString().Replace("'", "''") + "','"
                            + dr4["dwidth"].ToString().Replace("'", "''") + "','" + dr4["dheight"].ToString().Replace("'", "''") + "','"
                            + dr4["naspx"].ToString().Replace("'", "''") + "','" + dr4["yy"].ToString().Replace("'", "''") + "','"
                            + dr4["session"].ToString().Replace("'", "''") + "','" + dr4["zb"].ToString().Replace("'", "''") + "','"
                            + dr4["css"].ToString().Replace("'", "''") + "','" + dr4["css0"].ToString().Replace("'", "''") + "','"
                            + dr4["mrz"].ToString().Replace("'", "''") + "','" + dr4["bds"].ToString().Replace("'", "''") + "')";
                    }
                    upstr += " SET IDENTITY_INSERT tb_wid_layout OFF ";
                    #endregion
                    string conn = "";
                    conn = "Data Source=" + dr["ds"].ToString().Trim() + (dr["m_port"].ToString() == "0" ? "" : "," + dr["m_port"].ToString()) + ";Initial Catalog=" + dr["ic"].ToString().Trim() + ";User ID=" + dr["y_ui"].ToString().Trim() + ";Password=" + dr["y_pw"].ToString().Trim() + ";";
                    //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, upstr);
                    execObj.SetConnectionString(conn);
                    execObj.SubmitTextInt(upstr);

                    error += "套账:" + dr["tzmc"].ToString() + "发布成功</br>";
                }
                catch (System.Exception e)
                {
                    error += "套账:" + dr["tzmc"].ToString() + "发布失败,消息:" + e.Message + "</br>";
                }

            }

            return error;
        }

        /// <summary>
        /// 发布菜单
        /// </summary>
        /// <returns></returns>
        public string web_fb_menu()
        {
            string str_sql = "select * from v_user_conn where systag=1 ;" +
                "";
            SqlCommandString sqlstring = new SqlCommandString();
            DAL.DALInterface execObj = new DAL.DALInterface(null, connstr.GetMbConn());
            DataTable userConn = execObj.SubmitTextDataSet(str_sql).Tables[0];

            DataTable menu = this.execObj.SubmitTextDataSet("select a.* from v_menu a ").Tables[0];
             

            string error = "";
            string upstr = "";

            //查找模版服务器上有没有特定链接服务器
            str_sql = "exec  sp_linkedservers";
            execObj.SetConnectionString(connstr.GetCreateLinkServerConnetStringInBLL(this.tzid));
            DataSet dsexit = execObj.SubmitTextDataSet(str_sql);

            foreach (DataRow dr in userConn.Rows)
            {
                try
                {
                    #region 发布菜单SQL
                    /*清*/
                    upstr = "  select * into #sys_menubak from sys_menu ; ";

                    if (dsexit.Tables[0].Select("SRV_NAME='" + dr["tzmc"].ToString() + "无用" + "'").Length <= 0)
                    {//如果不存在连接  ,创建链接服务器   
                        //暂时不用
                        str_sql = "exec  sp_addlinkedserver '" + dr["tzmc"].ToString() + "无用" + "',' ','SQLOLEDB','" + dr["ds"].ToString().Trim() + "" + (dr["m_port"].ToString().Trim() == "0" ? ",1433" : "," + dr["m_port"].ToString().Trim()) + "' ";
                        str_sql += " exec sp_addlinkedsrvlogin '" + dr["tzmc"].ToString() + "无用" + "','false',null,'" + dr["y_ui"].ToString().Trim() + "','" + dr["y_pw"].ToString().Trim() + "'";
                        execObj.SetConnectionString(connstr.GetCreateLinkServerConnetStringInBLL(this.tzid));
                        execObj.SubmitTextInt(str_sql);
                        //SqlHelper.ExecuteScalar(db.GetTzDb(CSession.Get("tzid")), CommandType.Text, str_sql);
                    }                    
                    upstr += "SELECT * INTO #sys_menu FROM sys_menu  WHERE 1=2;";
                    upstr += "SET IDENTITY_INSERT #sys_menu ON";
                    foreach (DataRow dr1 in menu.Rows)
                    {
                        upstr += "  INSERT #sys_menu ([id],[xh],[text],[jb],[ssid],[cmd],[mj],[alone],[ty],[bz],[sysdel],[sysdeltime],sadel,sadeltime,[help],[webid]) ";
                        upstr += "  values ";
                        upstr += "  ('" + dr1["id"].ToString().Replace("'", "''") + "','" + dr1["xh"].ToString().Replace("'", "''") + "','"
                            + dr1["text"].ToString().Replace("'", "''") + "','" + dr1["jb"].ToString().Replace("'", "''") + "','"
                            + dr1["ssid"].ToString().Replace("'", "''") + "','" + dr1["cmd"].ToString().Replace("'", "''") + "','"
                            + dr1["mj"].ToString().Replace("'", "''") + "','" + dr1["alone"].ToString().Replace("'", "''") + "','" + dr1["ty"].ToString().Replace("'", "''") + "','"
                            + dr1["bz"].ToString().Replace("'", "''") + "','" + dr1["sysdel"].ToString().Replace("'", "''") + "','"
                            + dr1["sysdeltime"].ToString().Replace("'", "''") + "','0','1900/1/1 0:00:00','"
                            + dr1["help"].ToString().Replace("'", "''") + "','" + dr1["webid"].ToString().Replace("'", "''") + "')";
                    }
                    upstr += " update a SET a.xh=b.xh,a.text=b.text,a.jb=b.jb,a.ssid=b.ssid,a.cmd=b.cmd,a.mj=b.mj,a.bz=b.bz,a.sysdel=b.sysdel,a.sysdeltime=b.sysdeltime,a.sadel=b.sadel,a.sadeltime=b.sadeltime,a.help=b.help,a.webid=b.webid "
                        +"FROM sys_menu a  INNER join #sys_menu b ON a.id=b.id";

                    //upstr += " update a set a.ty=b.ty,a.alone=b.alone from sys_menu a inner join #sys_menubak b on a.id=b.id where a.id=b.id ";
                    upstr += " SET IDENTITY_INSERT #sys_menu OFF ";
                    upstr += " SET IDENTITY_INSERT sys_menu on ";
                    upstr += " INSERT sys_menu ([id],[xh],[text],[jb],[ssid],[cmd],[mj],[alone],[ty],[bz],[sysdel],[sysdeltime],sadel,sadeltime,[help],[webid])  SELECT a.[id],a.[xh],a.[text],a.[jb],a.[ssid],a.[cmd],a.[mj],a.[alone],a.[ty],a.[bz],a.[sysdel],a.[sysdeltime],a.sadel,a.sadeltime,a.[help],a.[webid] FROM #sys_menu  a   left JOIN sys_menu b ON a.id=b.id  WHERE b.id IS NULL  ";
                    upstr += " SET IDENTITY_INSERT sys_menu OFF ";

                    #endregion
                    string conn = "";
                    conn = "Data Source=" + dr["ds"].ToString().Trim() + (dr["m_port"].ToString() == "0" ? "" : "," + dr["m_port"].ToString()) + ";Initial Catalog=" + dr["ic"].ToString().Trim() + ";User ID=" + dr["y_ui"].ToString().Trim() + ";Password=" + dr["y_pw"].ToString().Trim() + ";";
                    execObj.SetConnectionString(conn);
                    execObj.SubmitTextInt(upstr);
                    //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, upstr);
                    error += "套账:" + dr["tzmc"].ToString() + "发布成功</br>";
                }
                catch (System.Exception e)
                {
                    error += "套账:" + dr["tzmc"].ToString() + "发布失败,消息:" + e.Message + "</br>";
                }

            }

            return error;
        }

        public DataSet GetJavaScript(string webId)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.GetJavaScript(webId));
        }

        /// <summary>
        /// 处理SQL语句
        /// </summary>
        /// <param name="sqlCommand">sql语句</param>
        /// <param name="xact_abort">事务回滚</param>
        /// <param name="arg">调用信息</param>
        /// <returns></returns>
        public Dictionary<string, string> execSqlCommand(string sqlCommand, string xact_abort, Dictionary<string, string> arg)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("resultState", "true");
            dic.Add("resultText", "");
            /*处理SQL语句中的 mb.dbo. db.GetMb()*/
            sqlCommand = sqlCommand.Replace(" mb.dbo.", " " + this.connstr.GetMbLinkname()).Replace(" master.dbo.", " " + this.connstr.GetMasterLinkname());
            /*加上事务机制*/
            /* 方法1   */
            if (xact_abort != string.Empty)
            {
                sqlCommand = "SET xact_abort  " + xact_abort + " ;" +
                     " begin tran; " + sqlCommand + " ; commit tran;";
            }
            /* 方法2  返回的值暂时不是程序想要的         
            str = " begin try "+
                  " begin tran;" +
                    str +
                  " commit tran; " +
                  " end try "+
                  " begin catch "+
                  " rollback tran "+
                  " select error_message() "+
                  " end catch";*/

            try
            {
                object r = null;
                r = this.execObj.SubmitTextObject(sqlCommand);
                if (r == null)
                {
                    r = "null";
                }
                dic["resultText"] = r.ToString().Trim();
            }
            catch (System.Exception ex)
            {
                dic["resultText"] = ex.Message;
                dic["resultState"] = "false";
            }

            try
            {//记录sql语句及处理状态
                this.execObj.SubmitTextObject(this.sqlstring.GetLog(this.userid, arg["wid"], arg["callFucntion"], sqlCommand.Replace("'", "''"), dic["resultState"]));
            }
            catch { }
            return dic;

        }

        /// <summary>
        /// 得到数据库发布标识
        /// </summary>
        /// <returns></returns>
        public string GetDataTag()
        {
            if (connstr.GetDb(this.tzid, this.userid).IndexOf("etest") >= 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 构造EasyUi树
        /// 说明:第一级树,就是最顶级数据,树按id ssid布局关系,text为要显示的文本,带attr_ 的字段例:attr_name 会被加到附加属性中,如name:'zs'
        /// </summary>
        /// <param name="dt1">第一级树</param>
        /// <param name="dt">树数据</param>
        /// <returns></returns>
        public string GetMyEuiTree(string bz)
        {             
            DataSet ds = this.execObj.SubmitTextDataSet(bz);
            DataTable dt1 = ds.Tables[0];
            DataTable dt = ds.Tables[1];
            //begin得到附加属性数组
            string[] attr = new string[dt.Columns.Count];
            foreach (DataColumn mycolumn in dt.Columns)
            {
                attr[mycolumn.Ordinal] = mycolumn.ColumnName;
            }
            //end得到附加属性数组
            string str = "[";
            foreach (DataRow dr in dt1.Rows)
            {
                str += GetNextTree(int.Parse(dr["id"].ToString()), dt, dr, attr);
            }

            if (str != "[")
            {
                str = str.Substring(0, str.Length - 1) + "]";
            }
            else { str = "[{\"id\":-1,\"text\":\"没有数据\"}]"; }
            return str;
        }

        public string GetNextTree(int id, DataTable dtjg, DataRow dr, string[] attr)
        {
            string attr_str = "";
            for (int j = 0; j < attr.Length; j++)
            {
                if (attr[j].IndexOf("attr_") == 0)
                {
                    attr_str += "\"" + attr[j].Replace("attr_", "") + "\":\"" + dr[attr[j]].ToString().Trim() + "\",";
                }
            }
            if (attr_str != "") { attr_str = attr_str.Substring(0, attr_str.Length - 1); }
            string myrs = "{\"id\":" + dr["id"].ToString() + ",\"text\":" + "\"" + MyTy.Utils.HtmlCha(dr["text"].ToString()) + "\", \"attributes\":{" + attr_str + "} , \"iconCls\":\"icon-sum\" ";
            DataRow[] mydr = dtjg.Select("ssid=" + id);
            string mychil = "";
            if (mydr.Length > 0)
            {
                foreach (DataRow dr1 in mydr)
                {
                    mychil += GetNextTree(int.Parse(dr1["id"].ToString()), dtjg, dr1, attr);
                }
            }
            else
            {//如果一级菜单没有下级                 
            }
            string utmp = mychil;
            return myrs + (utmp == string.Empty ? "" : ",\"children\":[ " + utmp.Substring(0, utmp.Length - 1) + "] ") + "},";
        }

        /// <summary>
        /// 更新查询区字段
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpSYJLayout(string data)
        {
            StringBuilder sql = new StringBuilder();
            SYJLayoutEdit root = JsonConvert.DeserializeObject<SYJLayoutEdit>(data);
            foreach (SYJLayout layout in root.row)
            {
                if (layout.Id == 0)
                {
                    //如果是隐藏控件那么只有htmlid
                    //如果是占位那么只有Qwidth
                    //如果是查询按钮只有mc
                    if (layout.Htmlid.Length == 0 && layout.Qwidth == 0 && layout.Mc.Length == 0)
                    {

                    }
                    else
                    {
                        sql.Append(" insert v_wid_layout ( css0,css, mrz,bds,webid,lx, mc,ord,mobileord, width,qwidth, westwidth,eastwidth, northheight, southheight, dwidth, dheight, visible, readonly, type,   bz,nwebid, event, yy,zb,session,naspx,htmlid) ");
                        sql.Append("values('" + layout.Css0 + "','" + layout.Css + "','" + layout.Mrz + "','" + layout.Bds + "','" + layout.Webid + "','" + layout.Lx + "', '" + layout.Mc + "',  '" + layout.Ord + "','"+layout.MobileOrd+"', '" + layout.Width + "','" + layout.Qwidth + "','" + layout.Westwidth + "','" + layout.Eastwidth + "', '" + layout.Northheight + "', '" + layout.Southheight + "', '" + layout.Dwidth + "', '" + layout.Dheight + "' ,'" + layout.Visible + "', '" + layout.Readonly + "', '" + layout.Type + "', '" + layout.Bz + "','" + layout.Nwebid + "','" + layout.Event + "','" + layout.Yy + "','" + layout.Zb + "','" + layout.Session + "','" + layout.Naspx + "','" + layout.Htmlid + "');");
                    }

                }
                else
                {
                    if (layout.Htmlid.Length == 0 && layout.Qwidth == 0 && layout.Mc.Length == 0)
                    {
                        sql.Append(" delete v_wid_layout  where id='" + layout.Id + "'; ");
                    }
                    else
                    {
                        sql.Append(" update v_wid_layout  set css0='" + layout.Css0 + "', css='" + layout.Css + "',mrz='" + layout.Mrz + "',bds='" + layout.Bds + "',mc='" + layout.Mc + "', htmlid='" + layout.Htmlid + "',ord='" + layout.Ord + "',mobileord='"+layout.MobileOrd+"', width='" + layout.Width + "',qwidth='" + layout.Qwidth + "',westwidth='" + layout.Westwidth + "',eastwidth='" + layout.Eastwidth + "', northheight='" + layout.Northheight + "',southheight='" + layout.Southheight + "' ,dwidth='" + layout.Dwidth + "', dheight='" + layout.Dheight + "', visible='" + layout.Visible + "', readonly='" + layout.Readonly + "', type='" + layout.Type + "',   bz='" + layout.Bz + "',nwebid='" + layout.Nwebid + "',  event='" + layout.Event + "', yy='" + layout.Yy + "',zb='" + layout.Zb + "',session='" + layout.Session + "',naspx='" + layout.Naspx + "'   where id='" + layout.Id + "'; ");
                    }
                }
            }
            int r = this.execObj.SubmitTextInt(sql.ToString());
            return "true";
        }

        public Result<string> UpSYJZdwh(string data)
        {
            StringBuilder sql = new StringBuilder();
            
            SYJZdwhEdit root = JsonConvert.DeserializeObject<SYJZdwhEdit>(data);
            string mlLink = this.connstr.GetMbLinkname();
            foreach (SYJZdwh zd in root.row)
            {
                if (zd.Mark == 1)
                {
                    if (zd.Id == 0)
                    {
                        sql.Append(" insert v_tbzd (ywname, zwname, ord, width, webid, visible, readonly, type,  sx, bz, showzero, event, btnvalue, showmrrq, hj, hbltname, px, format,prtname) ");
                        sql.Append("select '" + zd.Ywname + "', '" + zd.Zwname + "', '" + zd.Ord + "', '" + zd.Width + "', a.id, '" + zd.Visible + "', '" + zd.Readonly + "', '" + zd.Type + "','" + zd.Sx + "', '" + zd.Bz + "','" + zd.Showzero + "','" + zd.Event + "','" + zd.Btnvalue + "','" + zd.Showmrrq + "','" + zd.Hj + "','" + zd.Hbltname + "','" + zd.Px + "','"+zd.Format+"','" + zd.Prtname + "' from "+ mlLink + "v_wid a where a.id='" + zd.Wid + "';");
                    }
                    else
                    {
                        if (zd.Ywname.Length == 0 && zd.Zwname.Length == 0)
                        {
                            sql.Append(" delete v_tbzd  where id='" + zd.Id + "'; ");
                        }
                        else
                        {
                            sql.Append(" update v_tbzd set ywname='" + zd.Ywname + "', zwname='" + zd.Zwname + "', ord='" + zd.Ord + "', width='" + zd.Width + "', visible='" + zd.Visible + "', readonly='" + zd.Readonly + "', type='" + zd.Type + "',  sx='" + zd.Sx + "', bz='" + zd.Bz + "', showzero='" + zd.Showzero + "', event='" + zd.Event + "', btnvalue='" + zd.Btnvalue + "', showmrrq='" + zd.Showmrrq + "', hj='" + zd.Hj + "', hbltname='" + zd.Hbltname + "', px='" + zd.Px + "', format='"+zd.Format+"',prtname='" + zd.Prtname + "'   where id='" + zd.Id + "'; ");
                        }
                    }
                }
            }
            if (sql.Length>0)
            {
                int r = this.execObj.SubmitTextInt(sql.ToString());
                return ResultUtil<string>.success("success");
            }else
            {
                return ResultUtil<string>.error(1001, "没有要更新的数据");
            }
            
        }


    }

}
