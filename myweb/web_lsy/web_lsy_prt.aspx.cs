using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;

public partial class web_lsy_prt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        
        string mxid = "-1";
        if (Request.Form["xmhidden"] != null)
        {
            mxid = Request.Form["xmhidden"].ToString();
        }
        string zh = "";// 幅号
        if(Request.Form["cxzh"] !=null){
            zh = Request.Form["cxzh"].ToString();
        }
        string fh="";
        
        if(Request.Form["cxfh"] !=null){
            fh = Request.Form["cxfh"].ToString();
        }
                
        string xm = "";//姓名
        if (Request.Form["cxxm"] != null)
        {
            xm = Request.Form["cxxm"].ToString();
        }
        string where = " where a.mxid="+mxid+" ";
        if (zh.Length > 0 || xm.Length > 0 || fh.Length>0)
        {
            if (zh.Length > 0)
            {
                where += " and  a.zh like '%" + zh + "%'";
            }
            if (xm.Length > 0)
            {
                where += " and  a.xm like '%" + xm + "%'";
            }
            if (fh.Length > 0)
            {
                where += " and  a.fh like '%" + fh + "%'";
            }            
        }
        cxzh.Value = zh; cxxm.Value = xm; cxfh.Value=fh;
        string str_sql = " select a.Id,MAX(a.a1) a1ed,MAX(a.a2) a2ed,MAX(a.a3) a3ed,MAX(a.a4) a4ed,MAX(a.a5) a5ed,MAX(a.a6) a6ed into #prted from ( ";
        str_sql += " select id, ";
        str_sql += " case ISNULL(az,'') when 'a1' then 1 else 0 end as a1, ";
        str_sql += " case ISNULL(az,'') when 'a2' then 1 else 0 end as a2, ";
        str_sql += " case ISNULL(az,'') when 'a3' then 1 else 0 end as a3, ";
        str_sql += " case ISNULL(az,'') when 'a4' then 1 else 0 end as a4, ";
        str_sql += " case ISNULL(az,'') when 'a5' then 1 else 0 end as a5, ";
        str_sql += " case ISNULL(az,'') when 'a6' then 1 else 0 end as a6 ";
        str_sql += " from xm_T_prted) a  group by a.id  ";
        str_sql += " select  a.*,isnull(b.a1ed,0) a1ed,isnull(b.a2ed,0) a2ed,isnull(b.a3ed,0) a3ed,isnull(b.a4ed,0) a4ed,isnull(b.a5ed,0) a5ed,isnull(b.a6ed,0) a6ed "+
            "from [xm_t_lhmxb] a left join #prted b on a.id=b.id " + where + " ; select a.mxid,b.xmmc+'/'+a.dh  as dh from xm_t_xmmcmx a inner join xm_t_xmmczb b on a.id=b.id;"+
            " select * from xm_t_xmmcmx where mxid="+mxid+";drop table #prted;";
        FM.Business.Help hp = new FM.Business.Help();
        DataSet ds = hp.ExecuteDataset(str_sql);

        
        DataTable dt = ds.Tables[0];
        string tda1; string tda2; string tda3; string tda4; string tda5; string tda6;
        tda1 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc1"].ToString());
        tda2 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc2"].ToString());
        tda3 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc3"].ToString());
        tda4 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc4"].ToString());
        tda5 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc5"].ToString());
        tda6 = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc6"].ToString());

        string outstr = "<tr class='style_head '><td>幢号</td><td>房号</td><td>姓名</td><td>"+tda1+"</td>" +
            "<td>" + tda2 + "</td><td>" + tda3 + "</td><td>" + tda4 + "</td><td>" + tda5+ "</td><td>"+tda6+"</td><td>合计</td></tr>";
        int rn = 0;
        string a1ed = ""; string a2ed = ""; string a3ed = ""; string a4ed = ""; string a5ed = ""; string a6ed = "";
        decimal v1 = 0; decimal v2 = 0; decimal v3 = 0; decimal v4 = 0; decimal v5 = 0; decimal v6 = 0;decimal v7 = 0;decimal xv7 = 0;
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            a1ed = dr["a1ed"].ToString() == "0" ? "" : "prted";
            a2ed = dr["a2ed"].ToString() == "0" ? "" : "prted";
            a3ed = dr["a3ed"].ToString() == "0" ? "" : "prted";
            a4ed = dr["a4ed"].ToString() == "0" ? "" : "prted";
            a5ed = dr["a5ed"].ToString() == "0" ? "" : "prted";
            a6ed = dr["a6ed"].ToString() == "0" ? "" : "prted";
			xv7= decimal.Parse( dr["a1"].ToString())+
				decimal.Parse( dr["a2"].ToString())+
				decimal.Parse( dr["a3"].ToString())+
				decimal.Parse( dr["a4"].ToString())+
				decimal.Parse( dr["a5"].ToString())+
				decimal.Parse( dr["a6"].ToString()) ;            

            outstr += "<tr  row=" + rn + ">" +
             
                "<td class='zh' id=\"zh_" + rn + "\">" + dr["zh"].ToString() + "</td>" +
                "<td class='fh' id=\"fh_" + rn + "\">" + dr["fh"].ToString() + "</td>" +
                "<td class='xm' id=\"xm_" + rn + "\">" + dr["xm"].ToString() + "</td>" +

                "<td  class='a1 " + "' id=\"a1_" + rn + "\"><div class='" + a1ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka1_" + rn + "\"  /> " + dr["a1"].ToString() + "</div></td>" +
                "<td class='a2 " + "' id=\"a2_" + rn + "\"> <div class='" + a2ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka2_" + rn + "\"  /> " + dr["a2"].ToString() + "</div></td>" +
                "<td class='a3 " + "' id=\"a3_" + rn + "\"> <div class='" + a3ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka3_" + rn + "\"  /> " + dr["a3"].ToString() + "</div></td>" +
                "<td class='a4 " + "' id=\"a4_" + rn + "\"> <div class='" + a4ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka4_" + rn + "\"  /> " + dr["a4"].ToString() + "</div></td>" +
                "<td class='a5 " + "' id=\"a5_" + rn + "\"> <div class='" + a5ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka5_" + rn + "\"  /> " + dr["a5"].ToString() + "</div></td>" +
                "<td class='a6 " + "' id=\"a6_" + rn + "\"> <div class='" + a6ed + "'><input prt=\"yes\" type=\"checkbox\" id=\"cka6_" + rn + "\"  /> " + dr["a6"].ToString() + "</div></td>" +
				"<td class='hj' id=\"hj_" + rn + "\">" + xv7+ "</td>" +
				
                "<td style=\" display:none\" ><input type=\"hidden\" id=\"mykey_" + rn +
                "\" value=" + dr["id"].ToString() + " /></td></tr>";
                v1 +=decimal.Parse( dr["a1"].ToString());
                v2 += decimal.Parse(dr["a2"].ToString());
                v3 += decimal.Parse(dr["a3"].ToString());
                v4 += decimal.Parse(dr["a4"].ToString());
                v5 += decimal.Parse(dr["a5"].ToString());
                v6 += decimal.Parse(dr["a6"].ToString());
				v7+=xv7;
               
        }
        string ends = "<tr  class='style_head '><td>合计:</td><td style='text-align:right'>总行数</td><td text-align:left>" + dt.Rows.Count + "</td><td>" + v1 + "</td><td>" + v2 + "</td><td>" + v3 + "</td><td>" + v4 + "</td><td>" + v5 + "</td><td>" + v6 + "</td><td>"+v7+"</td></tr>";

        string xh = "";
        
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            xh += "<option value=\"" + dr["mxid"].ToString() + "\"" + (mxid == dr["mxid"].ToString() ? " selected " : "") + ">" + dr["dh"].ToString() + "</option>";
        }
        xh = "<select  onchange=\"xmchang()\" id=\"mxid\" >" + "<option value=\"-1\">请选择</option>" + xh + "</select>";
        xmselect.InnerHtml = xh;
        xmhidden.Value = mxid;
        content.InnerHtml = "<table class='style_Content'>" + outstr + ends + "</table>";

    }
}