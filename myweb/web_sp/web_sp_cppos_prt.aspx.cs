using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class web_sp_web_sp_cppos_prt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MyTy.Utils us = new MyTy.Utils();
        string mykey = "";
        if (Request.QueryString["mykey"] != null)
        {
            mykey = Request.QueryString["mykey"].ToString().Trim();
        }        
        
        FM.Business.Pos em = new FM.Business.Pos();
        System.Data.DataSet ds = new System.Data.DataSet();
        em.PosMyLoad(mykey,ref ds);
        Djh.InnerText = ds.Tables[0].Rows[0]["djh"].ToString().Trim();
        Djlbmc.InnerText = ds.Tables[0].Rows[0]["lxmc"].ToString().Trim();
        rq.InnerHtml = ds.Tables[0].Rows[0]["rq"].ToString().Trim();

        string cont = "";
        decimal zje = 0;
        int zsl = 0;
        foreach (System.Data.DataRow dr in ds.Tables[1].Rows)
        {
            string id = dr["id"].ToString().Trim();
            string mxid = dr["mxid"].ToString().Trim();
            string kh = dr["kh"].ToString().Trim();
            string pm = dr["pm"].ToString().Trim();
            string dj = dr["dj"].ToString().Trim();
            string lsj = dr["lsj"].ToString().Trim();
            string zk = dr["zk"].ToString().Trim();

            foreach (System.Data.DataRow drmx in ds.Tables[2].Select("id="+id+" and mxid="+mxid) )
            {
                cont += "<tr><td>" + kh + "</td><td class='cont_pmcss'>" + pm + "</td><td>" + drmx["cmmc"].ToString().Trim() + "</td><td>"
                    + drmx["sl"].ToString().Trim() + "</td><td>" + Math.Round( decimal.Parse(dj),2) + "</td><td>" 
                    + Math.Round(Decimal.Parse(drmx["sl"].ToString().Trim()) * Decimal.Parse(dr["dj"].ToString().Trim()),2 ) + "</td></tr>";
                zje += Math.Round(Decimal.Parse(drmx["sl"].ToString().Trim()) * Decimal.Parse(dr["dj"].ToString().Trim()), 2);
                zsl += int.Parse(drmx["sl"].ToString().Trim());
            }
        }
        content.InnerHtml = "<table class='contcss'><tr><td>款号</td><td class='cont_pmcss'>品名</td><td>规格</td><td>数量</td><td>单价</td><td>金额</td></tr>" + cont + "</table>";
        hjsl.InnerHtml = zsl.ToString();
        ysk.InnerHtml = zje.ToString();
        lsje_dx.InnerHtml = us.ConvertMoney(zje);
        string str_skfs = "";
        foreach (System.Data.DataRow dr in ds.Tables[3].Rows)
        {
            int dex = ds.Tables[3].Rows.IndexOf(dr)+1;
            if (dex % 2 == 0)
            {
            }
            else
            {//第1,3,5,7 ...
                if (dex == ds.Tables[3].Rows.Count)
                {
                    str_skfs += "<tr><td>"+dr["sklx"]+"</td><td>"+dr["skje"]+"</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                }
                else
                {
                    str_skfs += "<tr><td>" + dr["sklx"] + "</td><td>" + dr["skje"] + "</td><td>" + ds.Tables[3].Rows[dex]["sklx"] + "</td><td>" + ds.Tables[3].Rows[dex]["skje"] + "</td></tr>";
                }
                
            }
            
        }
        skfs.InnerHtml = "<table class='skfscss' >" + str_skfs + "</table>";
        bz.InnerHtml = ds.Tables[0].Rows[0]["bz"].ToString();
        zdr.InnerHtml = ds.Tables[0].Rows[0]["zdr"].ToString();
        sm.InnerHtml = ds.Tables[4].Rows[0]["pos_prt"].ToString(); 
    }
}