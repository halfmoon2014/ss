using Comet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webpage_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Complex> clienConnetList = LongSataMrg.clienConnetList;
        string html = "";
        for (int i = clienConnetList.Count - 1; i >= 0; i--)
        {   
            Complex complex = clienConnetList[i];
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='tag_"+i+"' >");
            sb.Append("<div>Name:" + complex.Name+ " <a href=\"#\" onclick=\"del('"+ complex.Guid + "','tag_"+i+"')\" >清除</a> </div>");
            sb.Append("<div>Guid:" + complex.Guid + "</div>");
            sb.Append("<div>Ip:" + complex.Ip + "</div>");
            sb.Append("<div>CreateTime:" + complex.CreateTime + "</div>");
            sb.Append("<div>IsClientConnected:" + complex.CometResult.Context.Response.IsClientConnected.ToString() + "</div>");
            sb.Append("</div>");
            html += sb.ToString();
        }
        list.InnerHtml = html;
    }
}