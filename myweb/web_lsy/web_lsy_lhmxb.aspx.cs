using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;

public partial class web_lsy_lhmxb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string mxid = "-1";
        
        if (Request.Form["xmhidden"] != null)
        {            
            mxid = Request.Form["xmhidden"].ToString(); 
        }

        string str_sql = "select a.id,a.mxid, a.zh,a.fh,a.xm,a.a1,a.a2,a.a3,a.a4,a.a5,a.a6,b.dh from xm_t_lhmxb a inner join xm_t_xmmcmx b on a.mxid=b.mxid where b.mxid= " + mxid + " ;" +
            "select a.mxid,b.xmmc+'/'+a.dh  as dh from xm_t_xmmcmx a inner join xm_t_xmmczb b on a.id=b.id;select * from xm_t_xmmcmx where mxid=" + mxid;
        FM.Business.Help hp = new FM.Business.Help();
        DataSet ds = hp.ExecuteDataset(str_sql);

        
        DataTable dt = ds.Tables[0];
        string outstr = "<tr class='style_head '><td>幢号</td><td>房号</td><td>姓名</td>" +
            "<td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc1"].ToString()) +
            "</td><td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc2"].ToString()) +
            "</td><td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc3"].ToString()) +
            "</td><td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc4"].ToString()) +
            "</td><td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc5"].ToString()) +
            "</td><td>" + (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc6"].ToString()) +
            "</td><td>代号</td><td>修改</td><td>删除</td></tr>";

        tda1.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc1"].ToString());
        tda2.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc2"].ToString());
        tda3.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc3"].ToString());
        tda4.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc4"].ToString());
        tda5.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc5"].ToString());
        tda6.InnerHtml = (ds.Tables[2].Rows.Count == 0 ? "/" : ds.Tables[2].Rows[0]["zmc6"].ToString());

        int rn = 0;
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            outstr += "<tr row=" + rn + ">"+   
                "<td class='zh' id=\"zh_" + rn + "\">" + dr["zh"].ToString() + "</td>" +
                "<td class='fh' id=\"fh_" + rn + "\">" + dr["fh"].ToString() + "</td>" +
                "<td class='xm' id=\"xm_" + rn + "\">" + dr["xm"].ToString() + "</td>" +
                "<td class='a1' id=\"a1_" + rn + "\">" + dr["a1"].ToString() + "</td>" +
                "<td class='a2' id=\"a2_" + rn + "\">" + dr["a2"].ToString() + "</td>" +
                "<td class='a3' id=\"a3_" + rn + "\">" + dr["a3"].ToString() + "</td>" +
                "<td class='a4' id=\"a4_" + rn + "\">" + dr["a4"].ToString() + "</td>" +
                "<td class='a5' id=\"a5_" + rn + "\">" + dr["a5"].ToString() + "</td>" +
                "<td class='a6' id=\"a6_" + rn + "\">" + dr["a6"].ToString() + "</td>" +
                "<td class='dh' id=\"dh_" + rn + "\">" + dr["dh"].ToString() + "</td>" +                
                " <td><a href=\"javascript:void(0)\" onclick=\"xg("+rn+")\" >修改</a></td>"+
                "<td> <a href=\"javascript:void(0)\" onclick=\"del("+rn+")\" >删除</a></td>" +
                "<td style=\" display:none\" >"+
                "<input type=\"hidden\" id=\"mykey_" + rn + "\" value=" + dr["id"].ToString() + " />" +
                "<input type=\"hidden\" id=\"mxid_" + rn + "\" value=" + dr["mxid"].ToString() + " />" +
                "</td>"+
                "</tr>";
        }

        string xh = "";
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            xh += "<option value=\"" + dr["mxid"].ToString()+"\""+ (mxid==dr["mxid"].ToString()?" selected ":"") + ">" + dr["dh"].ToString() + "</option>";
        }
        xh = "<select  onchange=\"xmchang()\" id=\"mxid\" >" + "<option value=\"-1\">请选择</option>" + xh + "</select>";
        xmselect.InnerHtml = xh;
        xmhidden.Value = mxid;
        content.InnerHtml = "<table class='style_Content'>" + outstr + "</table>";
        
    }
}