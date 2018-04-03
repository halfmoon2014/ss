using System;
using System.Data;
public partial class web_sp_hang_plate_prt : System.Web.UI.Page
{
    public DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        int key = int.Parse(Request.QueryString["key"].ToString());
        string str_sql = "select a.id,x.khmc+' '+x.khdm khmc,a.remark,r.name createor, convert(varchar(10),a.BizDate ,120) BizDate,a.create_date,a.number,a.customer_id,a.createor_id,x.khdm,a.Audit_Status" +
        " from _v_hang_plate a" +
        " inner join v_sp_xskhda x on a.customer_id = x.id" +
        " INNER JOIN v_user r ON r.id = a.createor_id" +
        " where a.id = {0};" +
        " SELECT a.unit,a.name, a.mxid,a.product,a.colour,a.weight,a.count_pre_jin ,a.price ,  a.price*a.weight Amount" +        
        " FROM _v_hang_plate_detail a INNER JOIN _v_hang_plate ZB ON ZB.ID=A.ID " +
        " where a.id = {0}";
        FM.Business.Help hp = new FM.Business.Help();
        ds = hp.ExecuteDataset(string.Format(str_sql, key));     
        
    }
}