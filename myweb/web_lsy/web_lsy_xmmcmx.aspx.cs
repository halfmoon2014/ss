using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;

public partial class web_lsy_xmmcmx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string str_sql = "select a.dh,a.id,a.mxid,a.zmc1,a.zmc2,a.zmc3,a.zmc4,a.zmc5,a.zmc6,b.xmmc  from xm_t_xmmcmx a inner join xm_t_xmmczb  b on a.id=b.id ;select id,xmmc from xm_t_xmmczb;";
        FM.Business.Help hp = new FM.Business.Help();
        DataSet ds = hp.ExecuteDataset(str_sql);        
        DataTable dt = ds.Tables[0];
        string outstr = "<tr class='style_head'><td>名称1</td><td>名称2</td><td>名称3</td><td>名称4</td><td>名称5</td><td>名称6</td><td>项目名称</td><td>代号</td><td>修改</td><td>删除</td></tr>";
        int rn = 0;
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            outstr += "<tr row=" + rn + ">"+
                "<td class='zmc1' id=\"zmc1_" + rn + "\">" + dr["zmc1"].ToString() + "</td>"+
                "<td class='zmc2' id=\"zmc2_" + rn + "\">" + dr["zmc2"].ToString() + "</td>" +
                "<td class='zmc3' id=\"zmc3_" + rn + "\">" + dr["zmc3"].ToString() + "</td>" +
                "<td class='zmc4' id=\"zmc4_" + rn + "\">" + dr["zmc4"].ToString() + "</td>" +
                "<td class='zmc5' id=\"zmc5_" + rn + "\">" + dr["zmc5"].ToString() + "</td>" +
                "<td class='zmc6' id=\"zmc6_" + rn + "\">" + dr["zmc6"].ToString() + "</td>" +
                "<td class='xmmc' id=\"xmmc_" + rn + "\">" + dr["xmmc"].ToString() + "</td>" +
                "<td class='dh' id=\"dh_" + rn + "\">" + dr["dh"].ToString() + "</td>" +
                " <td><a href=\"javascript:void(0)\" onclick=\"xg("+rn+")\" >修改</a></td>"+
                "<td> <a href=\"javascript:void(0)\" onclick=\"del("+rn+")\" >删除</a></td>" +
                "<td style=\" display:none\" >"+
                "<input type=\"hidden\" id=\"mykey_" + rn + "\" value=" + dr["id"].ToString() + " />" +
                "<input type=\"hidden\" id=\"mxid_" + rn + "\" value=" + dr["mxid"].ToString() + " />" +
                "</td>"+
                "</tr>";
        }

        string xmmc = "";
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            xmmc += "<option value=\"" + dr["id"].ToString() + "\">" + dr["xmmc"].ToString() + "</option>";
        }
        xmmc = "<select id=\"mykey\">" + "<option value=\"-1\">请选择</option>" + xmmc + "</select>";
        xmmcselect.InnerHtml = xmmc;
        content.InnerHtml = "<table  class='style_Content'>" + outstr + "</table>";
        
    }
}