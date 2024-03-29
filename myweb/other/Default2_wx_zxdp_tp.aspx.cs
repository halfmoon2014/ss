﻿using System;
using Service.Util;
using System.DataBase;
using System.Data;
public partial class Default2_wx_zxdp_tp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string spid = Request.Form["spid"].ToString().Trim();
        ConnetString db = new ConnetString();

        string conn = db.GetMasterConn();
        string sql = "select * from v_wx_sptp where spid=" + spid;
        DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sql);
        string picsyt = "style=\"width: 40px; height: 40px\"";
        int rno = 0;
        string tr = "";

        foreach (DataRow dr2 in ds.Tables[0].Rows)
        {
            if (rno % 3 == 0)
            {
                tr += "<div class=\"gallery-row\">";
            }
            tr += "<div class=\"gallery-item\"><a href=\"" + dr2["lj"].ToString().Trim() + "\" target='_blank' rel=\"external\"><img " + picsyt + " src=\"" + Tolst(dr2["lj"].ToString().Trim()) + "\" alt=\"" + dr2["bz"].ToString().Trim() + "\" /></a></div>";

            //tr += "<div class=\"ui-block-" + (rno % 3 == 0 ? "a" : (rno % 3 == 1 ? "b" : "c")) + "\">";
            //tr += "<div>" + dr2["mc"].ToString().Trim() + "</div><div><img  src=\"" + Tolst(dr2["lj"].ToString().Trim()) + "\" onclick='showp(this)'  " + picsyt + " spid='" + spid + "' sr='" + dr2["lj"].ToString().Trim() + "' /></div>";
            //tr += "</div>";
            if (rno % 3 == 2 || rno == ds.Tables[0].Rows.Count - 1)
            {
                tr += "</div>";
            }
            rno += 1;
        }
        content.InnerHtml = " <div class=\"gallery\">"+tr+"</div>"; ;
    }
    /// <summary>
    /// 从真实图地址,返回略缩图
    /// </summary>
    /// <param name="titlepic">真实地址</param>
    /// <returns></returns>
    public string Tolst(string titlepic)
    {
        int fg = titlepic.LastIndexOf("/");
        //return titlepic;
        return titlepic.Substring(0, fg) + "/cl" + titlepic.Substring(fg, titlepic.Length - fg);
    }
}