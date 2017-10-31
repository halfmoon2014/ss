using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;

public partial class web_lsy_xmmc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string str_sql = "select id,xmmc from xm_t_xmmczb";
        FM.Business.Help hp = new FM.Business.Help();        
        DataTable dt = hp.ExecuteDataset(str_sql).Tables[0];
        
        string outstr = "";
        int rn = 0;
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            outstr += "<tr row=" + rn + "><td class='xmmc' id=\"xmmc_" + rn + "\">" + dr["xmmc"].ToString() + "</td>" +
                " <td><a href=\"javascript:void(0)\" onclick=\"xg("+rn+")\" >修改</a></td>"+
                "<td> <a href=\"javascript:void(0)\" onclick=\"del("+rn+")\" >删除</a></td>" +
                "<td style=\" display:none\" ><input type=\"hidden\" id=\"mykey_" + rn+
                "\" value=" + dr["id"].ToString() + " /></td></tr>";
        }
        content.InnerHtml = "<table>" + outstr + "</table>";
        
    }
}