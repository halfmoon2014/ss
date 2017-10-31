using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_sp_web_sp_cppos_js : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //加载一些只用加载 一次的东西        
        
        FM.Business.Pos em = new FM.Business.Pos();
        string[] str = new string[1];        
        str = em.Pos_Sk();
        div_fkfs.InnerHtml = str[0];

        zje.InnerText = Request.QueryString["zje"].ToString();
        ysje.InnerText = "0";
        zl.InnerText = "-" + Request.QueryString["zje"].ToString();
    }
}