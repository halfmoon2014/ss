﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using EI.Web;

public partial class web_xtsz_main : FM.Controls.Page
{
    protected new  void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
        int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
        FM.Business.Login lg = new FM.Business.Login();
        string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();
        EI.Web.WebEdit em = new EI.Web.WebEdit(tzid.ToString(), userid.ToString(), username, Request.Browser.Browser);
        mainbody.InnerHtml = em.GetCont();
    }
}