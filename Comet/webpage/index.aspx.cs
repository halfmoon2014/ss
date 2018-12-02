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
            Random rd = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
            int r = rd.Next(1, 10);
            Complex complex = clienConnetList[i];
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='tag_"+i+" color_"+r+"' >");
            sb.Append("<div>Name:" + complex.Name+ " </div>");
            
            sb.Append("<div>ConnID:" + complex.ConnID + "</div>");
            sb.Append("<div>Title:" + complex.Title + "</div>");
            sb.Append("<div>Guid:" + complex.Guid + "</div>");
            sb.Append("<div>Ip:" + complex.Ip + "</div>");
            sb.Append("<div>CreateTime:" + complex.CreateTime + "</div>");
            sb.Append("<div>IsClientConnected:" + complex.CometResult.Context.Response.IsClientConnected.ToString() + "</div>");
            sb.Append("<div><textarea class='textarea_" + i + "'></textarea></div>");
            sb.Append("<div><input type=\"button\" onclick=\"doAction('command','" + complex.Guid + "'," + i + ")\" value=\"send command\" />  <a href=\"#\" onclick=\"doAction('del','" + complex.Guid + "'," + i + ")\" >清除</a>&nbsp;&nbsp;<a href=\"#\" onclick=\"doAction('query','" + complex.Guid + "'," + i + ")\" >刷新</a>&nbsp;&nbsp;<a href=\"#\" onclick=\"doAction('unload','" + complex.Guid + "'," + i + ")\" >用户注销</a></div>");
            sb.Append("</div>");
            html += sb.ToString();
        }
        list.InnerHtml = html;
    }
}