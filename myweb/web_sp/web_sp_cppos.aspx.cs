using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
public partial class web_ls_web_ls_cpdaxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //加载一些只用加载 一次的东西        

        FM.Business.Pos em = new FM.Business.Pos();
        string[] str = new string[3];
        int thbs = 0;//标识初始值是否是退货还是正常销售
        str = em.Pos_Load(ref thbs);
        //销售别
        tb_select_xsb.InnerHtml = str[0];
        if (thbs == 0)
        {
            thh.Disabled = true;
        }

        //班别
        bb.InnerHtml = str[1];
        //仓库
        ck.InnerHtml = str[2];
        FM.Business.Login lg = new FM.Business.Login();
        zdr.Value = lg.GetUser(MySession.SessionHandle.Get("userid")).Tables[0].Rows[0]["name"].ToString();
        DateTime dt = DateTime.Now;
        rq.Value = dt.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        if (Request.QueryString["title"] != null)
        {
            sysHead.TITLE = Request.QueryString["title"].ToString().Trim();
        }
        
    }
}