using MySession;
using MyTy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["bizId"] == null && Request.QueryString["bizKey"] == null && Request.QueryString["modal"] != "pre") { 
            //没有传KEY不用查
        }
        else
        {
            Service.Util.Business business = new Service.Util.Business(SessionHandle.Get("tzid"), SessionHandle.Get("userid"));
            Result<DataSet> result;
            if (Request.QueryString["modal"] == "pre")
            {
                result = business.GetPrePicList((Request.QueryString["preIdList"] == null || Request.QueryString["preIdList"].Length == 0 ? "0" : (Request.QueryString["preIdList"])));
            }
            else
            {
                result = business.GetPicList((Request.QueryString["groupId"] == null ? 0 : int.Parse(Request.QueryString["groupId"])),
                    (Request.QueryString["bizId"] == null ? 0 : int.Parse(Request.QueryString["bizId"])),
                    (Request.QueryString["bizKey"] == null ? "" : Request.QueryString["bizKey"]));
            }
            String html = "";
            if (result.Data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < result.Data.Tables[0].Rows.Count; i++)
                {
                    html += result.Data.Tables[0].Rows[i]["pic"] + "," + result.Data.Tables[0].Rows[i]["id"]+"," + result.Data.Tables[0].Rows[i]["remark"] + "|";
                }

            }
            piclist.Value = html;
        }
    }
}