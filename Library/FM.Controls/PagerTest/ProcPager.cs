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
            pagerArguments.loadMark = HTMLHelper.QueryStringInt("loadmark");
            pagerArguments.currentPageIndex = HTMLHelper.QueryStringInt("p");
            pagerArguments.clientHeight = HTMLHelper.QueryStringInt("clientHeight");
            pagerArguments.clientWidth = HTMLHelper.QueryStringInt("clientWidth");
            pagerArguments.wid = HTMLHelper.QueryStringInt("wid");
            if (pagerArguments.currentPageIndex <= 0)
                pagerArguments.currentPageIndex = 1;
            
            foreach (string key in requestParameters.Keys)
            {
                if (key != null)
                {
                    if (key.IndexOf("@") >= 0)
                        pagerArguments.formParm.Add(key, requestParameters[key]);
                    else if (key == "filterRow")
                        pagerArguments.filterRow = requestParameters[key];
                    else if (key == "filterColumn")
                        pagerArguments.filterColumn = requestParameters[key];
                    else if (key == "orderBy")
                        pagerArguments.orderBy = requestParameters[key];
                    else if (key == "otherMsg")
                        pagerArguments.otherMsg = requestParameters[key];
                    else if (key == "pageType")
                    {
                        pagerArguments.prtFlag = (requestParameters[key] == "sysPrint" ? true : pagerArguments.prtFlag);
                        pagerArguments.excelFlag = (requestParameters[key] == "sysExcel" ? true : pagerArguments.excelFlag);
                    }
                    else if (key == "loadmark")
                        pagerArguments.loadMark = int.Parse(requestParameters[key]);
                    else if (key == "pageSize")
                    {
                        if (requestParameters[key] == "")
                            pagerArguments.pageSize = 0;
                        else
                            pagerArguments.pageSize = int.Parse(requestParameters[key]);
                    }
                }
            }
            if (pagerArguments.excelFlag)
            {//如果是导出EXCEL那么就显示全部记录
                pagerArguments.currentPageIndex = 1;
                pagerArguments.pageSize = 999999999;
            }
            pagerArguments.formParm.Add("@userid", MySession.SessionHandle.Get("userid").ToString().Trim());
            pagerArguments.formParm.Add("@tzid", MySession.SessionHandle.Get("tzid").ToString().Trim());
            Business.Login lg = new Business.Login();
            pagerArguments.formParm.Add("@username", lg.GetUser(MySession.SessionHandle.Get("userid")).Tables[0].Rows[0]["name"].ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GetDate();
            base.DataBind();
        }

        /// <summary>
        /// 获取相关数据源
        /// </summary>
        public override void GetDate()
        {
            Business.ProcPager inf = new Business.ProcPager();
            int mxcxTag = 0;// 是否默认查询
            DataSet widConfig = inf.GetTableRecord("v_wid", "id=" + pagerArguments.wid);//得到wid 对应信息

            if (pagerArguments.orderBy == string.Empty)
            {
                pagerArguments.orderBy = widConfig.Tables[0].Rows[0]["orderby"].ToString().Trim();
            }
            if (pagerArguments.pageSize == 0)
            {
                pagerArguments.pageSize = Convert.ToInt32(widConfig.Tables[0].Rows[0]["PageSize"]);//每页显示的记录数
            }

            if (pagerArguments.pageSize == 0)
            {
                pagerArguments.pageSize = 10;
            }

            mxcxTag = Convert.ToInt32(widConfig.Tables[0].Rows[0]["mrcx"]);
            addNewRowPermission = Convert.ToInt32(widConfig.Tables[0].Rows[0]["myadd"]) == 0 ? false : true;

            //是否存在尺码标识;
            int cmDetailsTag = 0;
            inf.GetHeadlineData(pagerArguments.wid, pagerArguments.filterColumn, ref this.headlineData, ref this.detailsColumns, ref cmDetailsTag);

            //mrcx=0 首次不查
            //loadMark=0 不是第一次加载
            if (mxcxTag == 1 || pagerArguments.loadMark == 0)
            {
                int recordCount = 0;//总记录数
                inf.GetProcList(pagerArguments.wid, this.detailsColumns, pagerArguments.orderBy, pagerArguments.currentPageIndex, pagerArguments.pageSize, pagerArguments.formParm, pagerArguments.filterRow, ref recordCount, ref this.columnDataType, ref this.totalDetailsData, ref this.detailsSql, ref this.detailsData, ref this.cmDetailsData);
                //获取分页存储过程返回的数据
                pagerArguments.recordCount = recordCount;
            }

            if (cmDetailsTag == 1)
                inf.GetCmDetails(pagerArguments.wid, this.detailsSql, pagerArguments.formParm, ref this.cmDetailsData, ref this.cmHeadlineData, ref this.masterCmRelation, ref this.masterSlaveKey, ref this.detailCmRelation);

        }
    }
}