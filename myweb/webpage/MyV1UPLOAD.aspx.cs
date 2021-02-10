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
        Service.Util.Business business = new Service.Util.Business(SessionHandle.Get("tzid"), SessionHandle.Get("userid"));
        Result<DataSet> result =business.GetPicList(int.Parse(Request.QueryString["groupId"]), int.Parse(Request.QueryString["bizId"]), Request.QueryString["bizKey"]);
        String html = "";
        if (result.Data.Tables[0].Rows.Count > 0)
        {
            for(int i=0;i< result.Data.Tables[0].Rows.Count; i++)
            {
                html += result.Data.Tables[0].Rows[i]["pic"]+","+ result.Data.Tables[0].Rows[i]["id"]+ "|";
            }
            
        }
        piclist.Value = html;
    }
}