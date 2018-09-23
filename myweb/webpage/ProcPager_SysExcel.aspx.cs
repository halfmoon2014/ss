using MyTy;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using DTO;
public partial class webpage_ProcPager_SysExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FM.Controls.FMPager fm = new FM.Controls.Pager.ProcPager();
        fm.GetDate();
        Result<PageHtml> result = fm.Html();
        printForm.InnerHtml = "<div><table class=\"title_prt\"><tr><td colspan=\""+result.Data.ColumnCount+"\" id=\"excelTitle\">" + Request.Params["title"] + "</td></tr></table></div>" +
            "<div id=\"divPager\"  >" + result.Data.Html + "</div>";

        Response.Clear();
        Response.Buffer = true;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.
        Now.ToString("yyyyMMdd") + ".xls");
        Response.ContentEncoding =Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        this.EnableViewState = false;
    }    
}