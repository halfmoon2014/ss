using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.DataBase;
using System.Data;


public partial class web_lsy_prtgo : System.Web.UI.Page
{
 
    public DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string jsonStr = Request.QueryString["d"].ToString();
        string cxmysql="";
        string upmysql = "";
        string id="";
        string col="";
        for (int i = 0; i < jsonStr.Split('|').Length; i++)
        {
            id=jsonStr.Split('|')[i].Split('/')[0].ToString();
            col=jsonStr.Split('|')[i].Split('/')[1].ToString();

            cxmysql += " select a.id,c.xmmc,a.zh,a.fh,a.xm, b." + "zmc" + col.Substring(1, col.Length - 1) + " as title ,a." + col + " as value from xm_t_lhmxb a inner join xm_t_xmmcmx b on a.mxid=b.mxid inner join xm_t_xmmczb c on b.id=c.id   where a.id=" + id;
            if (i != jsonStr.Split('|').Length - 1)
            {
                cxmysql += " union all ";
            }
            upmysql += " if not exists(select * from xm_t_prted where id="+id+" and az='"+col+"')";
            upmysql += " begin ";
            upmysql += "  insert xm_T_prted(id,az) values ("+id+",'"+col+"') ";
            upmysql += " end ";

        }       

        FM.Business.Help hp = new FM.Business.Help();
        hp.ExecuteNonQuery(upmysql);
        DataSet ds = hp.ExecuteDataset(cxmysql);

        

    }

    #region 人民币小写金额转大写金额
    /// <summary>
    /// 小写金额转大写金额
    /// </summary>
    /// <param name="Money">接收需要转换的小写金额</param>
    /// <returns>返回大写金额</returns>
    public string ConvertMoney(Decimal Money)
    {
        //金额转换程序
        string MoneyNum = "";//记录小写金额字符串[输入参数]
        string MoneyStr = "";//记录大写金额字符串[输出参数]
        string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
        string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾元角分";//模

        MoneyNum = ((long)(Money * 100)).ToString();
        for (int i = 0; i < MoneyNum.Length; i++)
        {
            string DVar = "";//记录生成的单个字符(大写)
            string UnitVar = "";//记录截取的单位
            for (int n = 0; n < 10; n++)
            {
                //对比后生成单个字符(大写)
                if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                {
                    DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                    //给生成的单个大写字符加单位
                    UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                    n = 10;//退出循环
                }
            }
            //生成大写金额字符串
            MoneyStr = MoneyStr + DVar + UnitVar;
        }
        //二次处理大写金额字符串
        MoneyStr = MoneyStr + "整";
        while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
            || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零元")
            || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整"))
        {
            MoneyStr = MoneyStr.Replace("零分", "零");
            MoneyStr = MoneyStr.Replace("零角", "零");
            MoneyStr = MoneyStr.Replace("零拾", "零");
            MoneyStr = MoneyStr.Replace("零佰", "零");
            MoneyStr = MoneyStr.Replace("零仟", "零");
            MoneyStr = MoneyStr.Replace("零万", "万");
            MoneyStr = MoneyStr.Replace("零亿", "亿");
            MoneyStr = MoneyStr.Replace("亿万", "亿");
            MoneyStr = MoneyStr.Replace("零零", "零");
            MoneyStr = MoneyStr.Replace("零元", "元零");
            MoneyStr = MoneyStr.Replace("零整", "整");
            MoneyStr = MoneyStr.Replace("分整", "分");
        }
        if (MoneyStr == "整")
        {
            MoneyStr = "零元整";
        }
        return MoneyStr;
    }
    #endregion
}