using System.Security.Policy;
using System.Text;
namespace Service.Util
{
    public class SqlCommandString
    {
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="usr">用户名</param>
        /// <param name="psw">密码</param>
        /// <returns>SQL查询语句</returns>
        public string User(string usr, string psw)
        {
            return string.Format("select id from v_user where usr='{0}' and psw='{1}'", usr, psw);
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>SQL查询语句</returns>
        public string User(string userid)
        {
            return string.Format("select name,usr,platform_edit_permission from v_user where id={0}", userid);
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name">用户中文名</param>
        /// <param name="psw">用户密码</param>
        /// <returns>SQL查询语句</returns>
        public string ReloadUser(string name, string psw)
        {
            return string.Format("select id from v_user where name='{0}' and psw='{1}'", name, psw);
        }

        public string UpdataUserPsw(string usr, string psw)
        {
            return string.Format("update v_user set psw='{0}' where usr='{1}'", psw, usr);
        }

        public string TzData(string userid)
        {
            return string.Format(@"SELECT a.*,c.menu,c.sm,tz.tzmc 
             FROM   v_usertz a 
             inner join v_tz tz on tz.id=a.tzid           
             inner join v_usertzmenu c on a.id=c.usertzid
             WHERE  a.userid ={0}", userid);
        }

        public string TopLM(string userid)
        {
            return string.Format(@"SELECT  a.text,a.id,a.mj 
            from v_menu  a 
            inner join  v_usermenu v on  a.id=v.menuid and v.userid={0}
            where a.ty=0 and  a.jb = 1  order by a.xh", userid);
        }

        public string UserMenu(string userid)
        {
            return string.Format(@"SELECT a.text,a.id,a.mj,ssid,a.ty,a.jb,a.xh FROM v_menu a where a.ty=0;SELECT v.menuid,v.userid FROM v_usermenu v WHERE v.userid={0}", userid);
        }

        public string Menu(string userid, string id)
        {
            return string.Format(@"SELECT a.text,a.id,a.mj,isnull(b.mj,0) as xjmj 
            from v_menu a 
            inner join  v_usermenu v on  a.id=v.menuid and v.userid={0} 
            left join (select  ssid,max(mj) as mj from v_menu where mj=1 group by ssid) b  on a.id=b.ssid 
            where a.ty=0 and a.ssid ={1} order by a.xh", userid, id);
        }

        public string TzInfo(string tzid)
        {
            return string.Format(@"select b.tzmc,a.* from v_conn a inner join v_tz b on a.id=b.connid where b.id={0}", tzid);
        }

        public string LeftTree()
        {
            return string.Format(@"
            select a.* from v_v_userdpt a ;
            select * from v_dpt ;
            /*返回第一级,由于权限问题,第一级可能不是总部*/
            SELECT A.* FROM v_v_dpt1 A ;");
        }

        public string CTable(int id, string lx, string js, string sql, string wid, string myname)
        {
            StringBuilder sqlStr = new StringBuilder();
            if (sql.Length > 0)
            {
                sqlStr.Append(string.Format(@" and (sql like '%{0}%' or fwsql like '%{0}%'  or mxsql like '%{0}%'  or mxhsql like '%{0}%' ) ", sql));
            }
            if (wid.Trim() != string.Empty && wid != "0")
            {
                sqlStr.Append(" and id='" + wid + "'");
            }

            if (myname.Trim() != "")
            {
                sqlStr.Append(" and a.name like '%" + myname + "%'");
            }
            return string.Format(@"
            select '发布' as fb,'处理' as cl,a.name,a.id as wid,a.lx  
            from v_wid a   
            where a.lx like '%{0}%' and a.js like '%{1}%' {2} and a.userid={3};", lx, js, sqlStr, id);
        }

        public string CTableLx()
        {
            //return "select distinct a.lx  from v_v_websj a   ";
            return " SELECT  distinct a.lx  FROM dbo.v_wid AS a  ";
        }

        public string ContEditSql(int wid)
        {
            //return "select a.* from v_v_widconfig a  where a.webid=" + wid;
            return string.Format(@"
                SELECT a.fwsql, a.sql, a.name , a.mrcx, a.PageSize, a.orderby, a.myadd, a.id AS webid, a.mxgl, a.mxhgl, a.mxhord, a.mxhsql, a.mxsql, a.mxly, a.sql_2 
                FROM v_wid AS a where a.id= {0};", wid);
        }

        public string ContEdit(int wid, string type)
        {
            return string.Format(@"select a.{0} from  v_wid a  where a.id={1}", type, wid);
        }

        public string Tbzd(int wid)
        {
            return string.Format(@"select a.* from v_v_webzd a where a.webid={0} order by a.visible ,cast(a.ord as varchar);", wid);
        }

        public string TbLayOut(int wid, string lx, string ord)
        {
            return string.Format(@"select a.* from v_wid_layout a  where a.webid={0} and a.lx='{1}' order by visible,{2};", wid, lx, ord);
        }


        public string Layout(int wid, string wz)
        {
            if (wz == "z")
                //取得wid中的布局面板
                return string.Format(@"select * from v_wid_layout where (nwebid<>0 or naspx<>'') and  lx='z' and  webid='{0}';", wid);
            else if (wz == "allwz")
                //所有DIV
                return string.Format(@"select id, webid, lx, mc, ord, type,
                                qwidth, width, visible, readonly, event, bz,
                                sysdel, sysdeltime, nwebid, htmlid, eastwidth,
                                westwidth, northheight, southheight, dwidth,
                                dheight, naspx, yy, session, zb, mrz, bds, css,
                                css0 from v_wid_layout where  webid='{0}' order by lx,ord ;", wid);
            else if (wz == "allmobilewz")
                //所有DIV
                return string.Format(@"select id, webid, lx, mc, ord=mobileord, type,
                                qwidthm as qwidth, widthm as width, visible, readonly, event, bz,
                                sysdel, sysdeltime, nwebid, htmlid, eastwidthm as eastwidth,
                                westwidthm as westwidth, northheightm as northheight, southheightm as southheight, dwidth,
                                dheight, naspx, yy, session, zb, mrz, bds, css,
                                css0 from v_wid_layout where  webid='{0}' order by lx,mobileord ;", wid);
            else
                //取得wid中DIV布局
                return string.Format(@"select * from v_wid_layout where   lx = '{0}' and  webid='{1}' order by lx,ord ;", wz, wid);
        }

        public string WebFwSql(int wid)
        {
            //return "select a.fwsql from v_v_webfwsql a where a.webid='" + wid + "';";
            return string.Format(@"select a.fwsql from v_wid a where (DATALENGTH(a.fwsql) <> 0) and a.id='{0}' ", wid);
        }

        /// <summary>
        /// 返回页面脚本
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public string GetJavaScript(string webId)
        {
            return string.Format(@"SELECT  js from v_wid  a where  id={0}", webId);
        }

        public string Getywname(string userid, int Wid)
        {
            return string.Format(@" 
            select c.ywname from _v_userzd a 
            inner join v_menu b on a.menuid=b.id 
            inner join v_tbzd c on a.zdid= c.id  
            where a.userid={0} and b.webid={1}", userid, Wid);
        }

        public string GetLog(string userid, string action, string ip)
        {
            return string.Format(@"insert _v_msg (userid,wid,action,msg,time,memo) values('{0}',-1,'{1}','登陆主菜单',getdate(),'{2}')", userid, action, ip);
        }

        public string GetSsidMenu(string userid, string ssid)
        {
            //第一个字母表示列位置,第二个排序 
            return string.Format(@"
            SELECT SUBSTRING(CONVERT(VARCHAR(10),a.xh),1,1) AS ls,a.* 
            FROM v_menu  a 
            inner join  v_usermenu v on  a.id=v.menuid and v.userid={0} 
            WHERE a.ty=0 and  isnull(a.text,'')<>'' and  a.ssid={1}
            ORDER BY SUBSTRING(CONVERT(VARCHAR(10),a.xh),1,1),xh ", userid, ssid);
        }

        public string GetLog(string userid, string wid, string action, string msg, string memo)
        {
            return string.Format(@"insert _v_msg_d (userid,wid,action,msg,time,memo) values('{0}','{1}','{2}','{3}',getdate(),'{4}')", userid, wid, action, msg, memo);
        }

        public string GetZMDConfig(string tzid)
        {
            return string.Format(@"select * from v_sp_zmdconfig where tzid={0} ", tzid);
        }

        public string GetLSDlxx(string lx)
        {
            return string.Format(@"
                select a.*,case when b.ssid is null then 0 else 1 end as xjbs 
                from v_ls_xxdmb a 
                left join (select distinct ssid from v_ls_xxdmb ) b on a.id=b.ssid 
                where  a.lx='{0}'", lx);
        }

        public string GetLSDl(string lx)
        {
            return string.Format(@" 
            select a.*,case when b.ssid is null then 0 else 1 end as xjbs 
            from v_ls_xxdmb a 
            left join (select distinct ssid from v_ls_xxdmb ) b on a.id=b.ssid 
            where  a.lx='{0}' and a.ssid=0", lx);
        }

        public string GetSPDLxx(string lx)
        {
            return string.Format(@"select a.*,case when b.ssid is null then 0 else 1 end as xjbs 
            from v_sp_xxdmb a 
            left join (select distinct ssid from v_sp_xxdmb ) b on a.id=b.ssid  where  a.lx='{0}'", lx);
        }

        public string GetSPDl(string lx)
        {
            return string.Format(@"
            select a.*,case when b.ssid is null then 0 else 1 end as xjbs 
            from v_sp_xxdmb a 
            left join (select distinct ssid from v_sp_xxdmb ) b on a.id=b.ssid where  a.lx='{0}' and a.ssid=0", lx);
        }

        public string GetHelp(string id)
        {
            return string.Format(@"SELECT  help  from  v_menu   a  where  id={0}", id);
        }

        public string GetWidSql(int wid)
        {
            //return "SELECT TOP 1 a.SQL,a.mxly,a.sql_2 FROM v_v_widsql a  WHERE a.wid=" + wid + ";  ";
            return string.Format(@" select top 1 a.sql,a.mxly,a.sql_2 from v_wid a WHERE     (DATALENGTH(a.sql) <> 0) and a.id={0}", wid);
        }

        public string GetTablename(string Tablename, string where)
        {
            return "select * from " + Tablename + where;
        }

        public string Gettbzdinfo(int wid, string otherwhere)
        {
            return "SELECT a.* FROM   v_v_tbzdinfo a WHERE  a.wid = " + wid + otherwhere + " ORDER  BY a.visible desc ,cast(a.ord as varchar) ;";
        }

        /// <summary>
        /// 查找入口服务器是否配置了模版和主服务器
        /// </summary>
        /// <returns></returns>
        public string GetCtlServers()
        {
            return "select a.* from v_conn a where a.mbtag=1 or a.systag=1 ;";
        }

        /// <summary>
        /// 获取要同步到业务服务器上的对象
        /// </summary>
        /// <param name="tagret"></param>
        /// <returns></returns>
        public string Getptoclient(string tagret)
        {
            return "select a.ctime,a.pname,REPLACE(a.definition,' mb.dbo.',' " + tagret + "') definition ,a.type from v_v_ptoclient a  ;";
        }

        /// <summary>
        /// 业务服务器上已存在的对象
        /// </summary>
        /// <returns></returns>
        public string GetBLLptoclient()
        {
            return " SELECT C.PNAME,C.CTIME FROM _V_ptoclient  C;" +
            " select a.name as pname  from sys.all_objects a inner join sys.sql_modules b on a.object_id = b.object_id where a.is_ms_shipped=0  ; ";
        }

        /// <summary>
        /// 创建链接服务器
        /// </summary>
        /// <param name="linkName"></param>
        /// <param name="ds"></param>
        /// <param name="port"></param>
        /// <param name="ui"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public string GetCreateLink(string linkName, string ds, string port, string ui, string pw)
        {
            string strSql = "exec  sp_addlinkedserver '" + linkName + "',' ','SQLOLEDB','" + ds + "" + (port == "0" ? "" : "," + port) + "' ";
            strSql += " exec sp_addlinkedsrvlogin '" + linkName + "','false',null,'" + ui + "','" + pw + "'";
            return strSql;
        }

        /// <summary>
        /// 查询链接服务器
        /// </summary>
        /// <returns></returns>
        public string GetSearchLink()
        {            
            return "exec  sp_linkedservers";
        }
        /// <summary>
        /// 删除存储过程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetDropProcedure(string name)
        {
            return "Drop procedure " + name;
        }
        /// <summary>
        /// 删除视图
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetDropView(string name)
        {
            return "Drop view " + name;
        }
        /// <summary>
        /// 创建存储过程
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="linkName"></param>
        /// <returns></returns>
        public string GetCreateProcedure(string definition,string linkName) {
            return "/*此存储过程由外部服务器(" + linkName + ")创建,不能直接修改*/"
                                + definition;
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="linkName"></param>
        /// <returns></returns>
        public string GetCreateView(string pname, string linkName)
        {
            return "create view  " + pname
                                + " as /*此存储过程由外部服务器(" + linkName + ")创建,不能直接修改*/ "
                                + " select * from  " + linkName + pname;
        }

        /// <summary>
        /// 更新业务服务器上的日志
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetUpBLLPtoclient(string name,string time) {
            return " ; delete from _V_ptoclient where pname='" + name
                                 + "'; insert _V_ptoclient(ctime,pname) values(CONVERT(DATETIME, '" + time + "'),'" + name + "');";

        }
        /// <summary>
        ///POS基础配置
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetPosInfo(string userid) {
            return " select a.id,a.mc,a.lx,a.mrz,a.kzx from v_sp_poslx  a ;select a.id,a.bb from v_sp_posbb a; select a.id,a.mc from v_sp_ckb a inner join  v_sp_user_ck b on a.id=b.ckid where a.ty=0 and  b.userid= " + userid;
        }
        /// <summary>
        /// POS 类型
        /// </summary>
        /// <returns></returns>
        public string GetPosSklx() { 
            return " select a.id,a.sklx from v_sp_possklx  a ;";
        }

        /// <summary>
        /// POS单据信息
        /// </summary>
        /// <param name="mykey"></param>
        /// <param name="tzid"></param>
        /// <returns></returns>
        public string GetPosBill(string mykey,string tzid)
        {
            string str2 = " select lx.mc as lxmc,a.id,a.xskhid,a.zdr, convert(varchar(10),a.zdrq ,120) as zdrq ,a.djh,a.bz,a.ckbs,a.djlx,a.ckid,convert(varchar(10),a.rq ,120) rq,a.djbs,a.bb,a.yyy,a.thid,a.lx, substring(convert(varchar(10),b.rq ,112),1,6)+b.djh as thh,substring(convert(varchar(10),a.rq ,112),1,6)+a.djh as rqdjh  from _v_sp_cpposb a ";
            str2 += " inner join v_sp_poslx lx on lx.id=a.lx left join _v_sp_cpposb b on a.thid=b.id  where a.id=" + mykey;
            str2 += " select a.*,b.pm,b.jhj,b.pfj,b.kh,b.fzkh,b.cmzbid,b.shid,sh.ysmc from _v_sp_cpposmxb a inner join v_sp_cpda b on a.cpid=b.id inner join  v_sp_shdmb sh on sh.id=b.shid where a.id=" + mykey;
            str2 += " select a.*,d.mc as cmmc from _v_sp_cpposcmmxb a inner join v_sp_cmxxb d on d.id=a.cmid  where a.id=" + mykey;
            str2 += " select a.skje,b.sklx from _v_sp_cpposjlb a inner join v_sp_possklx b on a.sklx=b.id where a.id=" + mykey;
            str2 += " select     a.pos_prt,a.tzid from v_sp_zmdconfig  a  where a.tzid=" + tzid;
            return str2;
        }

        public string GetMasterSql() { 
            return "select a.ds,a.m_port,a.ic,a.m_ui,a.m_pw,a.linkname from v_conn a   where systag=1";
        }

        public string GetMbSql()
        {
            return "select a.ds,a.m_port,a.ic,a.m_ui,a.m_pw,a.linkname from v_conn a   where mbtag=1";
        }

    }
}
