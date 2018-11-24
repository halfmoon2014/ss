using System;
using Service.Util;
using MyTy;
using System.Data;
using System.Collections.Generic;
using System.Text;

public partial class web_xtsz_main_edit_zdwh : FM.Controls.Page
{
    public StringBuilder trList = new StringBuilder();
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        wid.Value = Request.QueryString["wid"].ToString().Trim();
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        if (string.Compare(ei.GetDataTag(), "true") != 0)
            btnGroup.Controls.Remove(fb);
        string trExt = ExtUtil.GetHtml(Request.PhysicalApplicationPath, "\\web_xtsz\\ext\\web_xtsz_main_edit_zdwh_tr");        

        Business business = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        DataTable dt = business.GetTbzd(int.Parse(Request.QueryString["wid"].ToString().Trim())).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tr = trExt;                
                foreach (DataColumn col in dt.Columns)
                {
                    string val = Utils.HtmlCha(dt.Rows[i][col.ColumnName].ToString());
                    if (col.ColumnName== "type")
                        tr = tr.Replace("$" + col.ColumnName + "$", TypeSelect(val));
                    else
                        tr=tr.Replace("$"+col.ColumnName+"$", val);
                }
                tr=tr.Replace("$rownum$", i.ToString());
                trList.Append(tr);
            }
        }else
        {
            string tr = trExt;
            foreach (DataColumn col in dt.Columns)
            {
                string val = "";
                if (col.ColumnName == "type")
                    tr = tr.Replace("$" + col.ColumnName + "$", TypeSelect(val));
                else
                    tr = tr.Replace("$" + col.ColumnName + "$", val);
            }
            tr.Replace("$rownum$", "0");
            trList.Append(tr);
        }
    }
    private string[] selectList = { "text", "select", "button", "checkbox", "textarea", "td", "a", "img", "mx" };
    public string TypeSelect(string defaultValue)
    {        
        StringBuilder select = new StringBuilder();
        foreach (string s in selectList)
            select.Append(string.Format("<option value=\"{0}\" {1}>{0}</option>", s,(s== defaultValue? "selected" : "")));
        select.Append("<option value=\"\" "+ ("" == defaultValue ? "selected" : "") + " ></option>");
        return select.ToString();
    }

 
}