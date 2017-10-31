﻿using System.Collections.Generic;
using System.Data;
namespace FM.Business
{
    public class Pos
    {
        /// <summary>
        /// 返回POS中的类型
        /// </summary>
        /// <returns></returns>
        public string[] Pos_Load(ref int thbs)
        {
            string[] str = new string[3];
            //销售别
            str[0] = "";
            //班别
            str[1] = "<select id=\"select_bb\" class=\"p100\">";
            //仓库
            str[2] = "<select id=\"select_ck\" class=\"p100\">";

            //第一个字母表示列位置,第二个排序 
            string str2 = " select a.id,a.mc,a.lx,a.mrz,a.kzx from v_sp_poslx  a ;select a.id,a.bb from v_sp_posbb a; select a.id,a.mc from v_sp_ckb a inner join  v_sp_user_ck b on a.id=b.ckid where a.ty=0 and  b.userid= " + MySession.SessionHandle.Get("userid").ToString();
            FM.Business.Help hp = new FM.Business.Help();
            DataSet ds = hp.ExecuteDataset(str2);

            string nowv = "";//记录下拉框值改变前的值
            string fv = "";//第一个值
            int f_thbs = 0;//第一个值是否是退货
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string selected = "";
                if (dr["mrz"].ToString().Trim() == "1")
                {
                    nowv = dr["id"].ToString().Trim();
                    selected = "selected='true' ";
                    thbs = dr["lx"].ToString().Trim() == "-1" ? 1 : 0;
                }
                if (ds.Tables[0].Rows.IndexOf(dr) == 0)
                {
                    fv = dr["id"].ToString().Trim();
                    f_thbs = int.Parse(dr["lx"].ToString().Trim());
                }
                str[0] += "<option " + selected + " lx='" + dr["lx"].ToString().Trim() + "' value=\"" + dr["id"].ToString().Trim() + "\" kzx='" + dr["kzx"].ToString().Trim() + "' >" + dr["mc"].ToString().Trim() + "</option>";
            }

            if (nowv == "")
            {//如果没有默认值,就取第一个值
                nowv = fv;
                thbs = f_thbs;
            }
            str[0] = "<select id=\"select_xsb\" nowv='" + nowv + "'  class=\"p100\">" + str[0] + "</select>";

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                str[1] += "<option value=\"" + dr["id"].ToString().Trim() + "\">" + dr["bb"].ToString().Trim() + "</option>";
            }
            str[1] += "</select>";

            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                str[2] += "<option value=\"" + dr["id"].ToString().Trim() + "\">" + dr["mc"].ToString().Trim() + "</option>";
            }
            str[2] += "</select>";

            return str;
        }
        public string[] Pos_Sk()
        {

            string[] str = new string[1];
            str[0] = "<table>";
            //第一个字母表示列位置,第二个排序 
            string str2 = " select a.id,a.sklx from v_sp_possklx  a ;";
            FM.Business.Help hp = new FM.Business.Help();
            DataSet ds = hp.ExecuteDataset(str2);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int pkey = ds.Tables[0].Rows.IndexOf(dr) + 1;
                str[0] += "<tr><td >" + pkey + "</td><td pkey='pkey_" + pkey + "' key='" + dr["id"].ToString().Trim() + "'>" + dr["sklx"].ToString() + "</td></tr>";
            }
            str[0] += "</table>";
            return str;
        }
        public string[] Pos_MyLoad(string mykey, ref DataSet ds)
        {
            FM.Business.Help hp = new FM.Business.Help();
            string[] rstring = new string[2];
            Dictionary<string, string> zbstring = new Dictionary<string, string>();//主表
            Dictionary<string, Dictionary<string, string>> mx = new Dictionary<string, Dictionary<string, string>>();//明细
            Dictionary<string, Dictionary<string, string>> cm = new Dictionary<string, Dictionary<string, string>>();//尺码        
            Dictionary<string, Dictionary<string, string>> skjl = new Dictionary<string, Dictionary<string, string>>();//尺码 
            //读取数据
            //sj.Pos_MyLoad(mykey, ref zbstring, ref mx, ref cm);            
            string str2 = " select lx.mc as lxmc,a.id,a.xskhid,a.zdr, convert(varchar(10),a.zdrq ,120) as zdrq ,a.djh,a.bz,a.ckbs,a.djlx,a.ckid,convert(varchar(10),a.rq ,120) rq,a.djbs,a.bb,a.yyy,a.thid,a.lx, substring(convert(varchar(10),b.rq ,112),1,6)+b.djh as thh,substring(convert(varchar(10),a.rq ,112),1,6)+a.djh as rqdjh  from _v_sp_cpposb a ";
            str2 += " inner join v_sp_poslx lx on lx.id=a.lx left join _v_sp_cpposb b on a.thid=b.id  where a.id=" + mykey;
            str2 += " select a.*,b.pm,b.jhj,b.pfj,b.kh,b.fzkh,b.cmzbid,b.shid,sh.ysmc from _v_sp_cpposmxb a inner join v_sp_cpda b on a.cpid=b.id inner join  v_sp_shdmb sh on sh.id=b.shid where a.id=" + mykey;
            str2 += " select a.*,d.mc as cmmc from _v_sp_cpposcmmxb a inner join v_sp_cmxxb d on d.id=a.cmid  where a.id=" + mykey;
            str2 += " select a.skje,b.sklx from _v_sp_cpposjlb a inner join v_sp_possklx b on a.sklx=b.id where a.id=" + mykey;
            str2 += " select     a.pos_prt,a.tzid from v_sp_zmdconfig  a  where a.tzid=" + MySession.SessionHandle.Get("tzid").ToString().Trim();

            ds = hp.ExecuteDataset(str2);

            //主表  Dictionary<string, string> zbstring = new Dictionary<string, string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    zbstring.Add(dc.ColumnName, ds.Tables[0].Rows[0][dc.ColumnName].ToString());
                }
            }
            //明细表  Dictionary<string, Dictionary<string, string>> mx = new Dictionary<string, Dictionary<string, string>>();
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                //明细中的某一行
                Dictionary<string, string> mx_col = new Dictionary<string, string>();
                foreach (DataColumn dc in ds.Tables[1].Columns)
                {
                    mx_col.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                mx.Add(ds.Tables[1].Rows.IndexOf(dr).ToString(), mx_col);
            }

            //尺码 Dictionary<string, Dictionary<string, string>> cm = new Dictionary<string, Dictionary<string, string>>();

            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                //尺码中的某一行
                Dictionary<string, string> cm_col = new Dictionary<string, string>();
                foreach (DataColumn dc in ds.Tables[2].Columns)
                {
                    cm_col.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                cm.Add(ds.Tables[2].Rows.IndexOf(dr).ToString(), cm_col);
            }
            //收款记录
            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                //尺码中的某一行
                Dictionary<string, string> cm_col = new Dictionary<string, string>();
                foreach (DataColumn dc in ds.Tables[3].Columns)
                {
                    cm_col.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                skjl.Add(ds.Tables[3].Rows.IndexOf(dr).ToString(), cm_col);
            }
            //
            string zb_str = "{";
            foreach (var item in zbstring)
            {
                zb_str += item.Key + ":'" + item.Value + "',";
            }
            if (zb_str == "{")
            {
                zb_str = "zb_str:''";
            }
            else
            {
                zb_str = "zb_str:" + zb_str.Substring(0, zb_str.Length - 1) + "}";
            }

            string mx_str = "{";
            foreach (var item in mx)
            {
                string _mx_str = "{";
                foreach (var itemmx in item.Value)
                {
                    _mx_str += itemmx.Key + ":'" + itemmx.Value + "',";
                }
                _mx_str = _mx_str.Substring(0, _mx_str.Length - 1) + "}";

                mx_str += item.Key + ":" + _mx_str + ",";
            }
            if (mx_str == "{")
            {
                mx_str = "mx_str:''";
            }
            else
            {
                mx_str = "mx_str:" + mx_str.Substring(0, mx_str.Length - 1) + "}";
            }
            string cm_str = "{";
            foreach (var item in cm)
            {
                string _cm_str = "{";
                foreach (var itemcm in item.Value)
                {
                    _cm_str += itemcm.Key + ":'" + itemcm.Value + "',";
                }
                _cm_str = _cm_str.Substring(0, _cm_str.Length - 1) + "}";

                cm_str += item.Key + ":" + _cm_str + ",";
            }
            if (cm_str == "{")
            {
                cm_str = "cm_str:''";
            }
            else
            {
                cm_str = "cm_str:" + cm_str.Substring(0, cm_str.Length - 1) + "}";
            }

            string skjl_str = "{";
            foreach (var item in skjl)
            {
                string _skjl_str = "{";
                foreach (var itemcm in item.Value)
                {
                    _skjl_str += itemcm.Key + ":'" + itemcm.Value + "',";
                }
                _skjl_str = _skjl_str.Substring(0, _skjl_str.Length - 1) + "}";

                skjl_str += item.Key + ":" + _skjl_str + ",";
            }
            if (skjl_str == "{")
            {
                skjl_str = "skjl_str:''";
            }
            else
            {
                skjl_str = "skjl_str:" + skjl_str.Substring(0, skjl_str.Length - 1) + "}";
            }
            rstring[0] = "{" + zb_str + "," + mx_str + "," + cm_str + " }";
            rstring[1] = "{" + skjl_str + "}";
            return rstring;
        }
    }
}
