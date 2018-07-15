using DTO;
using FM.Components;
using System;
using System.Collections.Specialized;
using System.Data;

namespace FM.Controls.Pager
{
    /// <summary>
    /// 存储过程分页
    /// </summary>
    public class ProcPager : FMPager
    {
        public ProcPager()
        {
            pagerArgumentsInit();
        }

        /// <summary>
        /// 初始请求参数
        /// </summary>
        public void pagerArgumentsInit()
        {
            NameValueCollection requestParameters = HTMLHelper.GetParameters();
            pagerArguments.LoadMark = HTMLHelper.QueryStringInt("loadmark");
            pagerArguments.CurrentPageIndex = HTMLHelper.QueryStringInt("p");
            pagerArguments.ClientHeight = HTMLHelper.QueryStringInt("clientHeight");
            pagerArguments.ClientWidth = HTMLHelper.QueryStringInt("clientWidth");
            pagerArguments.Wid = HTMLHelper.QueryStringInt("wid");
            if (pagerArguments.CurrentPageIndex <= 0)
                pagerArguments.CurrentPageIndex = 1;
            
            foreach (string key in requestParameters.Keys)
            {
                if (key != null)
                {
                    if (key.IndexOf("@") >= 0)
                        pagerArguments.FormParm.Add(key, requestParameters[key]);
                    else if (key == "filterRow")
                        pagerArguments.FilterRow = requestParameters[key];
                    else if (key == "filterColumn")
                        pagerArguments.FilterColumn = requestParameters[key];
                    else if (key == "orderBy")
                        pagerArguments.OrderBy = requestParameters[key];
                    else if (key == "otherMsg")
                        pagerArguments.OtherMsg = requestParameters[key];
                    else if (key == "pageType")
                    {
                        pagerArguments.IsPrint = (requestParameters[key] == "sysPrint" ? true : pagerArguments.IsPrint);
                        pagerArguments.IsExcel = (requestParameters[key] == "sysExcel" ? true : pagerArguments.IsExcel);
                    }
                    else if (key == "loadmark")
                        pagerArguments.LoadMark = int.Parse(requestParameters[key]);
                    else if (key == "pageSize")
                    {
                        if (requestParameters[key] == "")
                            pagerArguments.PageSize = 0;
                        else
                            pagerArguments.PageSize = int.Parse(requestParameters[key]);
                    }
                }
            }
            if (pagerArguments.IsExcel)
            {//如果是导出EXCEL那么就显示全部记录
                pagerArguments.CurrentPageIndex = 1;
                pagerArguments.PageSize = 999999999;
            }
            pagerArguments.FormParm.Add("@userid", MySession.SessionHandle.Get("userid").ToString().Trim());
            pagerArguments.FormParm.Add("@tzid", MySession.SessionHandle.Get("tzid").ToString().Trim());
            Business.Login lg = new Business.Login();
            pagerArguments.FormParm.Add("@username", lg.GetUser(MySession.SessionHandle.Get("userid")).Tables[0].Rows[0]["name"].ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GetDate();
            base.DataBind();
        }

        /// <summary>
        /// 获取业务相关数据源
        /// </summary>
        public override void GetDate()
        {
            Business.ProcPager inf = new Business.ProcPager();            
            DataSet widConfig = inf.GetTableRecord("v_wid", "id=" + pagerArguments.Wid);//得到wid 对应信息

            if (string.IsNullOrEmpty(pagerArguments.OrderBy))
                pagerArguments.OrderBy = widConfig.Tables[0].Rows[0]["orderby"].ToString().Trim();
            
            if (pagerArguments.PageSize == 0)            
                pagerArguments.PageSize = Convert.ToInt32(widConfig.Tables[0].Rows[0]["PageSize"]);//每页显示的记录数            

            if (pagerArguments.PageSize == 0)            
                pagerArguments.PageSize = 10;            
                        
            addNewRowPermission = Convert.ToInt32(widConfig.Tables[0].Rows[0]["myadd"]) == 0 ? false : true;
                      
            PageDetail pageDetail= inf.GetHeadlineData(pagerArguments.Wid, pagerArguments.FilterColumn);
            this.detailHeadData = pageDetail.Data;
            this.detailsColumns = pageDetail.Columns;
         
            //mrcx=0 首次不查
            //loadMark=0 不是第一次加载
            if (Convert.ToInt32(widConfig.Tables[0].Rows[0]["mrcx"]) == 1 || pagerArguments.LoadMark == 0)
            {   
                PageContent pageContent=inf.GetProcList(pagerArguments,this.detailsColumns);
                //获取分页存储过程返回的数据
                pagerArguments.RecordCount = pageContent.RecordCount;
                this.columnDataType = pageContent.ColumnDataType;
                this.totalDetailsData = pageContent.TotalDetailsData;
                this.detailsSql = pageContent.DetailsSql;
                this.detailContentData = pageContent.DetailsData;
                this.cmDetailsData = pageContent.CmDetailsData;
            }

            if (pageDetail.IsDetail)
            {
                PageCmContent pageCmContent=inf.GetCmDetails(pagerArguments, this.detailsSql, cmDetailsData);
                this.cmDetailsData = pageCmContent.CmDetailsData;
                this.cmHeadlineData = pageCmContent.CmHeadlineData;
                this.masterCmRelation = pageCmContent.MasterCmRelation;
                this.masterSlaveKey = pageCmContent.MasterSlaveKey;
                this.detailCmRelation = pageCmContent.DetailCmRelation;
            }

        }
    }
}