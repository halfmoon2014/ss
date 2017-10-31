using System;
using System.DataBase;
using System.Data;
using Service.Util;
public partial class Default2_wx_zxdp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //dbstring db = new dbstring();
        ConnetString db = new ConnetString();
        string conn = db.GetMasterConn(); 

        string sql = "select * from v_wx_fz where ty=0 order by ord ;select * from v_wx_xxfb;select * from v_wx_spxx;select * from v_wx_sptp;";
        DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
        string c = CreatS("0", ds.Tables[0], ds.Tables[1], ds.Tables[2], ds.Tables[3]);         
        div_tj.InnerHtml = c;
    }
    /// <summary>
    /// 返回末级构造STRING
    /// </summary>
    /// <param name="fzkey">末级id</param>
    /// <param name="ms"></param>
    /// <param name="xxfb"></param>
    /// <param name="spxx"></param>
    /// <param name="sptp"></param>
    /// <returns></returns>
    public string GroupStr(string fzkey, string ms, DataTable xxfb, DataTable spxx, DataTable sptp)
    {
         
        string tr = "";
        foreach (DataRow dr in xxfb.Select("ssid=" + fzkey) )
        {
            string spid = dr["spid"].ToString().Trim();
            DataRow spxxdr = spxx.Select("id=" + spid)[0];
            DataRow[] sptpdr = sptp.Select("spid=" + spid);
            string picsyt = "style=\"width: 40px; height: 40px\"";
            string titlepic = "";
            if (sptpdr.Length > 0) {
                titlepic = sptpdr[0]["lj"].ToString().Trim();
            }
            tr += " <div data-role=\"collapsible\" > ";
            tr += " <h3> ";
            tr += spxxdr["mc"].ToString().Trim()+ "<img " + picsyt + " src=\" " + Tolst(titlepic) + " \" />";
            tr += " </h3> ";
            tr += " <p style=\"white-space:pre-wrap;  \">";
            tr +=  spxxdr["ms"].ToString().Trim() ;
            /*
            int rno = 0;
            foreach (DataRow dr2 in sptpdr)
            {
                if ( rno%3==0)
                {
                    tr += "<div class=\"ui-grid-b\">";
                }
                tr += "<div class=\"ui-block-" + (rno % 3 == 0 ? "a" : (rno % 3 == 1?"b":"c" ) ) + "\">";
                tr += "<div>" + dr2["mc"].ToString().Trim() + "</div><div><img  src=\"" + Tolst(dr2["lj"].ToString().Trim()) + "\" onclick='showp(this)'  " + picsyt + " spid='" + spid + "' fzkey=" + fzkey + " sr='" + dr2["lj"].ToString().Trim() + "' /></div>";
                tr += "</div>";
                if (rno % 3 == 2 || rno==sptpdr.Length-1 )
                {
                    tr += "</div>";
                }
                rno += 1;
            } */
            tr += "</p><p><a class=\"ui-link\" onclick=showp(" + spid + ")  \">查看图片</a>";
            tr += " </p> ";
            tr += " </div> ";
        }

        if (ms == "1")
        {
            tr = "<div data-role=\"collapsible-set\">" + tr + "</div>";
        }
        return tr;
    }    
    /// <summary>
    /// 从真实图地址,返回略缩图
    /// </summary>
    /// <param name="titlepic">真实地址</param>
    /// <returns></returns>
    public string  Tolst(string titlepic){
       int fg= titlepic.LastIndexOf ("/");
       //return titlepic;
       return titlepic.Substring(0, fg) + "/cl" + titlepic.Substring(fg, titlepic.Length - fg);
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
    public string CreatS( string startid, DataTable wx_fz, DataTable xxfb, DataTable spxx, DataTable sptp)
    {
        string r="";
        foreach (DataRow dr in wx_fz.Select("ssid=" + startid))
        {

            //r += "<ul data-role=\"listview\" data-split-theme=\"d\" data-inset=\"true\">";
            //r += "<li data-role=\"list-divider\">" + dr["mc"].ToString().Trim() + "</li>";
            r+="<div class=\"ui-body ui-body-a ui-corner-all\"><h3>"+dr["mc"].ToString().Trim()+"</h3>";

            if (dr["mj"].ToString().Trim() == "1")
            {
               // r += "<li>";
                r += GroupStr(dr["id"].ToString().Trim(), dr["ms"].ToString().Trim(), xxfb, spxx, sptp);
               // r += " </li>"; 
            }
            else
            {
                r += CreatS(dr["id"].ToString().Trim(), wx_fz, xxfb, spxx, sptp);
            }
            //r += "</ul>";
            r+="</div>";
        }
        return r;
    }
}