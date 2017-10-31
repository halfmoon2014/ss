using System;
using Service.Util;
using System.DataBase;
using System.Data;
public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s_fl = "<h2> 分类</h2>   ";

        ConnetString db = new ConnetString();

        string conn = db.GetMasterConn();
        string sql = "select * from v_wx_fz where ty=0 order by ord ;select * from v_wx_xxfb;select * from v_wx_spxx;select * from v_wx_sptp;";
        DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
        string c = CreatS("0", ds.Tables[0], ds.Tables[1], ds.Tables[2], ds.Tables[3]);   

        fl.InnerHtml = s_fl+"<ul>"+c+"</ul>";
        content.InnerHtml = "welcome";

    }
    /// <summary>
    /// 构造HTML
    /// </summary>
    /// <param name="startid"></param>
    /// <param name="wx_fz"></param>
    /// <param name="xxfb"></param>
    /// <param name="spxx"></param>
    /// <param name="sptp"></param>
    /// <returns></returns>
    public string CreatS(string startid, DataTable wx_fz, DataTable xxfb, DataTable spxx, DataTable sptp)
    {
        string r = "";
        foreach (DataRow dr in wx_fz.Select("ssid=" + startid))
        {

            //r += "<ul data-role=\"listview\" data-split-theme=\"d\" data-inset=\"true\">";
            //r += "<li data-role=\"list-divider\">" + dr["mc"].ToString().Trim() + "</li>";
            

            if (dr["mj"].ToString().Trim() == "1")
            {
                r += "<li > <a href=\"#\" onclick=\"s('" + dr["id"].ToString().Trim() + "')\"> " + dr["mc"].ToString().Trim() + "</a></li>";
            }
            else
            {
                r += "<li>" + dr["mc"].ToString().Trim() + "<ul>" + CreatS(dr["id"].ToString().Trim(), wx_fz, xxfb, spxx, sptp) + "</ul></li>";
            }            
        }
        return r;
    }  
}