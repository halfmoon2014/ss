using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;
public partial class web_lsy_cx : System.Web.UI.Page
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
        string skqk = "";
        if (Request.Form["skqk"] != null)
        {
            skqk = Request.Form["skqk"].ToString();
        }
        else
        {
            skqk = "all";
        }


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
            "<td>" + tda2 + "</td><td>" + tda3 + "</td><td>" + tda4 + "</td><td>" + tda5 + "</td><td>" + tda6 + "</td><td>已收款</td><td>未收款</td><td>合计</td></tr>";
        int rn = 0;
        //如果已收款,则显示红色
        string a1ed = ""; string a2ed = ""; string a3ed = ""; string a4ed = ""; string a5ed = ""; string a6ed = "";
        //合计
        decimal v1 = 0; decimal v2 = 0; decimal v3 = 0; decimal v4 = 0; decimal v5 = 0; decimal v6 = 0; decimal zje_hj = 0; decimal ysk_hj = 0; decimal wsk_hj = 0;
        //房号合计
        decimal zje = 0; decimal ysk = 0; decimal wsk = 0;
        
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            a1ed = dr["a1ed"].ToString() == "0" ? "" : "prted";
            a2ed = dr["a2ed"].ToString() == "0" ? "" : "prted";
            a3ed = dr["a3ed"].ToString() == "0" ? "" : "prted";
            a4ed = dr["a4ed"].ToString() == "0" ? "" : "prted";
            a5ed = dr["a5ed"].ToString() == "0" ? "" : "prted";
            a6ed = dr["a6ed"].ToString() == "0" ? "" : "prted";
			zje= decimal.Parse( dr["a1"].ToString())+
				decimal.Parse( dr["a2"].ToString())+
				decimal.Parse( dr["a3"].ToString())+
				decimal.Parse( dr["a4"].ToString())+
				decimal.Parse( dr["a5"].ToString())+
				decimal.Parse( dr["a6"].ToString()) ;

            v1 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a1"].ToString()));
            v2 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a2"].ToString()));
            v3 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a3"].ToString()));
            v4 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a4"].ToString()));
            v5 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a5"].ToString()));
            v6 += skqkclsz(skqk, dr["a1ed"].ToString(), decimal.Parse(dr["a6"].ToString()));
            zje_hj += zje;
            ysk = (dr["a1ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a1"].ToString())) +
                (dr["a2ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a2"].ToString())) +
                (dr["a3ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a3"].ToString())) +
                (dr["a4ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a4"].ToString())) +
                (dr["a5ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a5"].ToString())) +
                (dr["a6ed"].ToString() == "0" ? 0 : decimal.Parse(dr["a6"].ToString()));
            wsk = zje - ysk;
            ysk_hj += ysk;
            wsk_hj += wsk;

            outstr += "<tr  row=" + rn + ">" +
             
                "<td class='zh' id=\"zh_" + rn + "\">" + dr["zh"].ToString() + "</td>" +
                "<td class='fh' id=\"fh_" + rn + "\">" + dr["fh"].ToString() + "</td>" +
                "<td class='xm' id=\"xm_" + rn + "\">" + dr["xm"].ToString() + "</td>" +

                "<td  class='a1 " + "' id=\"a1_" + rn + "\"><div class='" + a1ed + "'> " + skqkcl(skqk, dr["a1ed"].ToString(), dr["a1"].ToString()) + "</div></td>" +
                "<td class='a2 " + "' id=\"a2_" + rn + "\"> <div class='" + a2ed + "'> " + skqkcl(skqk, dr["a2ed"].ToString(), dr["a2"].ToString()) + "</div></td>" +
                "<td class='a3 " + "' id=\"a3_" + rn + "\"> <div class='" + a3ed + "'> " + skqkcl(skqk, dr["a3ed"].ToString(), dr["a3"].ToString()) + "</div></td>" +
                "<td class='a4 " + "' id=\"a4_" + rn + "\"> <div class='" + a4ed + "'> " + skqkcl(skqk, dr["a4ed"].ToString(), dr["a4"].ToString()) + "</div></td>" +
                "<td class='a5 " + "' id=\"a5_" + rn + "\"> <div class='" + a5ed + "'> " + skqkcl(skqk, dr["a5ed"].ToString(), dr["a5"].ToString()) + "</div></td>" +
                "<td class='a6 " + "' id=\"a6_" + rn + "\"> <div class='" + a6ed + "'> " + skqkcl(skqk, dr["a6ed"].ToString(), dr["a6"].ToString()) + "</div></td>" +
                "<td class='ysk' id=\"ysk_" + rn + "\">" + zerodec(ysk) + "</td>" +
                "<td class='wsk' id=\"wsk_" + rn + "\">" + zerodec(wsk) + "</td>" +
                "<td class='hj' id=\"hj_" + rn + "\">" + zerodec(zje) + "</td>" +
				
                "<td style=\" display:none\" ><input type=\"hidden\" id=\"mykey_" + rn +
                "\" value=" + dr["id"].ToString() + " /></td></tr>";

               
        }
        string ends = "<tr  class='style_head '><td>合计:</td><td style='text-align:right'>总行数</td><td text-align:left>" + dt.Rows.Count +
            "</td><td>" + zerodec(v1) + "</td><td>" + zerodec(v2) + "</td><td>" + zerodec(v3) + "</td><td>" + zerodec(v4) + "</td><td>" + zerodec(v5) + "</td><td>" + zerodec(v6) +
            "</td><td>" + zerodec(ysk_hj) + "</td><td>" + zerodec(wsk_hj) + "</td><td>" + zerodec(zje_hj) + "</td></tr>";

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
    public decimal skqkclsz(string skqk, string ed, decimal v)
    {
        if (skqk == "all")
        {
            return v;
        }
        else if (skqk == "ysk")
        {//要显示已收款
            if (ed == "0")
            {
                return 0;
            }
            else
            {
                return v;
            }
        }
        else if (skqk == "wsk")
        {
            if (ed == "0")
            {
                return v;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }

    }
    /// <summary>
    /// 处理表格显示的数据信息
    /// </summary>
    /// <param name="skqk"></param>
    /// <param name="ed"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public string skqkcl(string skqk, string ed, string v)
    {
        if (skqk == "all")
        {
            return zerostr(v);
        }
        else if (skqk == "ysk")
        {//要显示已收款
            if (ed == "0")
            {
                return "&nbsp;";
            }
            else
            {
                return zerostr(v);
            }
        }
        else if (skqk == "wsk")
        {
            if (ed == "0")
            {
                return zerostr(v);
            }
            else
            {
                return "&nbsp;";
            }
        }
        else
        {
            return "error";
        }
    }

    public string zerostr(string v)
    {
        if (decimal.Parse(v) == 0)
        {
            return "&nbsp";
        }
        else
        {
            return v;
        }
    }
    public string zerodec( decimal v)
    {
        if (v == 0)
        {
            return "&nbsp";
        }
        else
        {
            return v.ToString(); 
        }
    }
}