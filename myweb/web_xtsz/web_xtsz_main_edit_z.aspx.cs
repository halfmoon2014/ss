using System;
using Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class web_xtsz_main_edit_z : System.Web.UI.Page
{    
    public StringBuilder formgroupBuilder = new StringBuilder();
    public StringBuilder detailHeadBuilder = new StringBuilder();
    public StringBuilder detailDataBuilder = new StringBuilder();
    public string detailTextMode = "<td {2}><input type='text'  field='{0}' value=\"{1}\" /></td> ";//值 一定要引号,有些数据本身就有单引号
    public string detailCKMode = "<td {2}><input field='{0}' type='checkbox' {1} /></td> ";
    public List<string> detailSelecttypeList = new List<string>();
    public List<FWDetail> detailList = new List<FWDetail>();
    protected void Page_Load(object sender, EventArgs e)
    {
        int intWid = int.Parse(Request.QueryString["wid"].ToString().Trim());
        string strLx = Request.QueryString["lx"].ToString().Trim();
        string dataOrd = "ord";
        if (Request.QueryString["ismobile"]!=null && Request.QueryString["ismobile"].ToString().Equals("1"))
        {
            dataOrd = "mobileord";
            ismobile.Value = "1";
        }
        Business ei = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
        if (string.Compare(ei.GetDataTag(), "true") != 0)
            btnGroup.Controls.Remove(fb);
        List<FW> formgroupList = new List<FW>();
        formgroupList.Add(new FW("s", "southheight", "Div布局-下高", "southheightm", "mobile-下高"));
        formgroupList.Add(new FW("n", "northheight", "Div布局-上高", "northheightm", "mobile-上高"));
        formgroupList.Add(new FW("e", "eastwidth", "Div布局-右宽", "eastwidthm", "mobile-右宽"));
        formgroupList.Add(new FW("w", "westwidth", "Div布局-左宽", "westwidthm", "mobile-左宽"));
        if (!strLx.Equals("c"))
        {
            foreach (FW f in formgroupList)
            {
                if (strLx == "t" && f.key == "n")
                    f.isShow = true;
                else if (strLx == "b" && f.key == "s")
                    f.isShow = true;
                else if (strLx == "l" && f.key == "w")
                    f.isShow = true;
                else if (strLx == "r" && f.key == "e")
                    f.isShow = true;
            }
        }

        #region 查询条件
        DataTable detail = ei.GetTbLayOut(intWid, strLx, dataOrd).Tables[0];        
        string formgroupMode = @"<label for='{0}' field='td_{0}' class='col-md-3 control-label' style='display:{6}' >{1}</label>
                    <div class='col-md-3' style='display:{6}'>
                        <input type='text' class='form-control' id='{0}' field='{0}' value='{2}'>
                    </div>
                    <label for='{3}' field='td_{3}' class='col-md-3 control-label' style='display:{6}'>{4}</label>
                    <div class='col-md-3' style='display:{6}'>
                        <input type='text' class='form-control' id='{3}' field='{3}' value='{5}'>
                    </div>";
        foreach (FW f in formgroupList)
            formgroupBuilder.Append(string.Format(formgroupMode,
                f.f, f.fName, HtmlCha(detail.Rows.Count == 0 ? "0" : detail.Rows[0][f.f].ToString()),
                f.m, f.mName, HtmlCha(detail.Rows.Count == 0 ? "0" : detail.Rows[0][f.m].ToString()),
                (f.isShow ? "block" : "none")));
        #endregion 

        #region 列表
        detailList.Add(new FWDetail("id", "id", false,"text"));
        detailList.Add(new FWDetail("ord", "排布", true,"text"));
        detailList.Add(new FWDetail("mobileord", "mobile", true, "text"));
        detailList.Add(new FWDetail("mc", "标题名称", true, "text"));
        detailList.Add(new FWDetail("qwidth", "标题宽度", true, "text"));
        detailList.Add(new FWDetail("qwidthm", "mobile标题宽度", true, "text"));
        detailList.Add(new FWDetail("htmlid", "htmlid", true, "text"));
        detailList.Add(new FWDetail("width", "html宽度", true, "text"));
        detailList.Add(new FWDetail("widthm", "mobileHtml宽度", true, "text"));
        detailList.Add(new FWDetail("visible", "可见", true,"checkbox"));
        detailList.Add(new FWDetail("readonly", "只读", true, "checkbox"));
        detailList.Add(new FWDetail("type", "类型", true, "select"));
        detailList.Add(new FWDetail("event", "事件", true, "text"));
        detailList.Add(new FWDetail("yy", "引用", true, "text"));
        detailList.Add(new FWDetail("bds", "表达式", true, "text"));
        detailList.Add(new FWDetail("mrz", "默认值", true, "text"));
        detailList.Add(new FWDetail("zb", "后台字段", true, "checkbox"));
        detailList.Add(new FWDetail("session", "session", true, "text"));
        detailList.Add(new FWDetail("css0", "css0", true, "text"));
        detailList.Add(new FWDetail("css", "css", true, "text"));
        detailList.Add(new FWDetail("bz", "备注", true, "text"));
        detailList.Add(new FWDetail("nwebid", "下级webid", true, "text"));
        detailList.Add(new FWDetail("naspx", "下级aspx", true, "text"));
        detailList.Add(new FWDetail("dwidth", "复-宽度", true, "text"));
        detailList.Add(new FWDetail("dheight", "复-高度", true, "text"));
        detailList.Add(new FWDetail("mark", "mark", false,"text"));

        string detailHeadMode = @"<td field='{0}' class='field_{0}'  {2} >{1}</td>";        
        foreach (FWDetail f in detailList)
            detailHeadBuilder.Append(string.Format(detailHeadMode, f.f, f.fName, (f.isShow ? "" : "style='display:none'")));    

        detailSelecttypeList.Add("text");
        detailSelecttypeList.Add("select");
        detailSelecttypeList.Add("button");
        detailSelecttypeList.Add("checkbox");
        detailSelecttypeList.Add("textarea");
        detailSelecttypeList.Add("td");
        detailSelecttypeList.Add("a");
        detailSelecttypeList.Add("date");
        detailSelecttypeList.Add("tree");
        detailSelecttypeList.Add("");

        if (detail.Rows.Count > 0)
        {
            for(int i=0;i< detail.Rows.Count; i++)
            {
                StringBuilder row = getDetailTR( detail.Rows[i]);
                detailDataBuilder.Append( string.Format("<tr class='tbbody' rownum='{0}'>{1}</tr>", i, row.ToString()));
            }
        }else
        {
            StringBuilder row = getDetailTR(null);
            detailDataBuilder.Append(string.Format("<tr class='tbbody' rownum='{0}'>{1}</tr>", 0, row.ToString()));
        }
        #endregion

        wid.Value = intWid.ToString();
        lx.Value = strLx;
    }

    public StringBuilder getDetailTR(DataRow dr)
    {
        StringBuilder row = new StringBuilder();
        
        foreach (FWDetail fd in detailList)
        {
            string value = "";
            if (dr != null && !fd.f.Equals("mark") )
                value=dr[fd.f].ToString();

            if (fd.type == "text")
                row.Append(string.Format(detailTextMode, fd.f, HtmlCha(value), (fd.isShow ? "" : "style='display:none'")));
            else if (fd.type == "checkbox")
                row.Append(string.Format(detailCKMode, fd.f, (dr == null || value == "0" ? "" : "checked"), (fd.isShow ? "" : "style='display:none'")));
            else if (fd.type == "select")
            {
                StringBuilder detailSelectTypeMode = new StringBuilder();
                if (fd.f == "type")
                    detailSelecttypeList.ForEach(delegate (string s) {
                        detailSelectTypeMode.Append(string.Format("<option value='{0}' {1}>{0}</option>", s, (value == s  ? "selected" : "")));
                    });
                row.Append(string.Format("<td><select field='{0}' mrz=''>{1}</select></td>", fd.f, detailSelectTypeMode.ToString()));
            }
        }
        return row;
    }

    public string HtmlCha(string str)
    {
        return MyTy.Utils.HtmlCha(str).Trim();
    }

}

public class FW
{
    public string f;
    public string fName;
    public string m;
    public string mName;
    public string key;
    public bool isShow = false;
    public FW(string key, string f, string fn, string m, string mn)
    {
        this.key = key;
        this.f = f;
        this.fName = fn;
        this.m = m;
        this.mName = mn;
    }
    public FW(string f, string fn, bool isShow)
    {        
        this.f = f;
        this.fName = fn;
        this.isShow = isShow;
    }
}

public class FWDetail
{
    public string f;
    public string fName;  
    public bool isShow = false;
    public string type;
    public FWDetail(string f, string fn, bool isShow, string type)
    {
        this.f = f;
        this.fName = fn;
        this.isShow = isShow;
        this.type = type;
    }
}