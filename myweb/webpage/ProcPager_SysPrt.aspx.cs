using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.DataBase;

public partial class main : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FM.Controls.FMPager fm = new FM.Controls.FMPager();
        fm = new FM.Controls.Pager.ProcPager();

        fm.GetDate();
        MyDivTable.InnerHtml = fm.Html();
        string parmUrl = "";
        foreach (string key in Request.Params.Keys)
        {
            if (key.IndexOf("@")>=0)
            {
                parmUrl += "\"" + key + "\":\"" + Request.Params[key] + "\",";
            }
            else if (key == "filterRow")
            {
                sysFindSortRow.Value = Request.Params[key];
            }
            else if (key == "pageSize")
            {
                pageSize.Value = Request.Params[key];
            }
            else if (key == "orderBy")
            {
                orderBy.Value = Request.Params[key];
            }
            else if (key == "title")
            {
                printTitle.InnerHtml = Request.Params[key];
            }
            else if (key == "currentPageIndex")
            {
                currentPageIndex.Value = Request.Params[key];
            }
        }
        if (parmUrl.Length > 0)
        {
            parm.Value = parmUrl.Substring(0,parmUrl.Length-1);
        }
        wid.Value = Request.QueryString["wid"];
        sysHead.Title = "打印:" + printTitle.InnerHtml;
 
    }

}