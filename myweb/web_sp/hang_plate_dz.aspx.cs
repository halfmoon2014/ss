using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class web_sp_hang_plate_prt : System.Web.UI.Page
{
    public DataSet ds;
    public string ksrq;
    public string jsrq;
    public string khmc;
    protected void Page_Load(object sender, EventArgs e)
    {
        string khmcs = Request.Form["khmcs"].ToString();
        ksrq = Request.Form["ksrq"].ToString();
        jsrq = Request.Form["jsrq"].ToString();
        string where = "";
        if ( string.Compare(Request.Form["isCash"].ToString() , "@",true)!=0)
        {
            where = " and a.iscash=" + Request.Form["isCash"].ToString();
        }
        int isExcel = 0;
        if (!string.IsNullOrEmpty(Request.Form["isExcel"]))
        {
            isExcel =int.Parse( Request.Form["isExcel"].ToString());
        }
        if (isExcel == 1)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.
            Now.ToString("yyyyMMdd") + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
        }
        string str_sql = "";
        if (string.IsNullOrEmpty(khmcs))
        {
            str_sql = " SELECT khmc INTO #khmc  FROM v_sp_xskhda ; ";
        }
        else
        {
            str_sql = " SELECT value AS khmc INTO #khmc  FROM SplitToTable('{0}',','); ";
        }
        str_sql += "select a.djlx,a.id,x.id khid,a.remark,r.name createor, convert(varchar(10),a.BizDate ,120) BizDate,a.create_date,a.number,a.customer_id,a.createor_id,x.khdm,a.Audit_Status into #zb" +
        " from _v_hang_plate a" +
        " inner join v_sp_xskhda x on a.customer_id = x.id" +
        " INNER JOIN v_user r ON r.id = a.createor_id" +
        " inner join #khmc k on k.khmc=x.khmc " +
        " where DATEDIFF(DAY,'{1}',a.BizDate)>=0 and DATEDIFF(DAY,a.BizDate,'{2}')>=0 {3} ;" +
        " SELECT zb.khid,zb.BizDate,zb.number,a.product+case a.memory when '' then '' else '['+a.memory+']' end  as product,a.colour,a.weight,a.count_pre_jin ,a.price,case zb.djlx when 410 then '滚镀' else '挂镀' end djlxmc, CASE ZB.DJLX WHEN  410 THEN CONVERT( DECIMAL(12,2), a.price*a.weight,2) ELSE  CONVERT( DECIMAL(12,2), a.price*a.weight*a.count_pre_jin,2) END  Amount," +
        " a.remark,a.after_finish,after_quantity = a.weight * a.count_pre_jin ,a.after_price,CONVERT(DECIMAL(12, 2), a.after_price * a.weight * a.count_pre_jin, 2) after_amt" +
        " FROM _v_hang_plate_detail a INNER JOIN #zb ZB ON ZB.ID=A.ID; " +
        " select a.khmc,b.khdm,b.id from #khmc a inner join v_sp_xskhda b on a.khmc=b.khmc order by case  when  ISNUMERIC(b.khdm) = 1 AND CHARINDEX(',', b.khdm) = 0 AND CHARINDEX('\', b.khdm) = 0 then  CAST(b.khdm AS INT) else 0 end; " +
        " drop table #khmc ;drop table #zb;";
        FM.Business.Help hp = new FM.Business.Help();
        ds = hp.ExecuteDataset(string.Format(str_sql,khmcs,ksrq,jsrq, where));
        //List <Control> trCtl = new List<Control>();
        //List<Control> tdCtl = new List<Control>();
        //foreach (Control control in this.Controls)
        //{
        //    AttributeCollection htmlControl = ((System.Web.UI.HtmlControls.HtmlControl)control).Attributes;
        //    foreach (string key in htmlControl.Keys)
        //    {
        //        if (htmlControl[key] == "hiddenTR")
        //        {
        //            trCtl.Add(control);
        //        }else if (htmlControl[key] == "hiddenTD")
        //        {
        //            tdCtl.Add(control);
        //        }
        //    }
        //}
        //foreach(Control control in trCtl)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlControl)control).Style["border"] = "1px solid #fffafa00";
        //}
        //foreach (Control control in tdCtl)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlControl)control).Style["border-left"] = "1px solid #fffafa00";
        //    ((System.Web.UI.HtmlControls.HtmlControl)control).Style["border-right"] = "1px solid #fffafa00";
        //}
    }
    public string WriteKHDJTotal(string djlxmc,decimal totalAmount)
    {
        return string.Format("<tr><td>{0}</td><td  style='text-align: center' colspan='6'>{1}合计</td><td  style='text-align: right' >{2}</td></tr>", jsrq,djlxmc, (totalAmount == 0 ? "" : string.Format("{0:0.###}", totalAmount)))+
            "<tr><td colspan='8'>&nbsp;</td></tr>";
    }
    public string WriteKHTotal( decimal totalAmount)
    {
        return string.Format("<tr><td>{0}</td><td  style='text-align: center' colspan='6'>本月合计</td><td style='text-align: right'>{1}</td></tr>", jsrq,(totalAmount == 0 ? "" : string.Format("{0:0.###}", totalAmount)))+
            "<tr><td class='hiddenTR' colspan='8'>&nbsp;</td></tr>" +
            "<tr><td style='font-size:18pt;' class='hiddenTR' colspan ='8'>中国农业银行.石狮市支行营业部 6228.4606.8800.7017.771 王宏强</td></tr>" +
            "<tr><td style='font-size:18pt;' class='hiddenTR' colspan='8'>中国建设银行.石狮宝岛支行 6227.0018.3362.0132.965  王宏强</td></tr>" +
            "<tr><td style='font-size:18pt;' class='hiddenTR' colspan='8'>福建省农村信用社联合社 6230.3611.0703.2408.372  王宏强</td></tr>" +
            "<tr><td class='hiddenTR' colspan='8'>&nbsp;</td></tr>" +
            "<tr><td class='hiddenTR' colspan='8'>&nbsp;</td></tr>" +
            "<tr><td class='hiddenTR' colspan='8'>&nbsp;</td></tr>";
    }
}