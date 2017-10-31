using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;

public partial class web_lsy_lhb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
        string str_sql = "select id,lh from xm_T_lhb";
        FM.Business.Help hp = new FM.Business.Help();
        DataTable dt = hp.ExecuteDataset(str_sql).Tables[0];
        
        string outstr = "";
        int rn = 0;
        foreach (DataRow dr in dt.Rows)
        {
            rn = dt.Rows.IndexOf(dr);
            outstr += "<tr row=" + rn + "><td class='lh' id=\"lh" + rn + "\">" + dr["lh"].ToString() + "</td>"+
                " <td><a href=\"javascript:void(0)\" onclick=\"xg("+rn+")\" >修改</a></td>"+
                "<td> <a href=\"javascript:void(0)\" onclick=\"del("+rn+")\" >删除</a></td>" +
                "<td style=\" display:none\" ><input type=\"hidden\" id=\"mykey" + rn+
                "\" value=" + dr["id"].ToString() + " /></td></tr>";
        }
        content.InnerHtml = "<table>" + outstr + "</table>";
        
    }
}