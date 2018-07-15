using System.Collections.Generic;
using System.ComponentModel;
namespace DTO
{
    public class FMPagerArguments
    {
        /// <summary>
        /// 是否是第一次加载调用
        /// </summary>
        [Description("是否是第一次加载调用")]
        public int LoadMark
        {
            get
            {
                return loadMark;
            }
            set
            {
                loadMark = value;
            }
        }
        private int loadMark = 0;

        /// <summary>
        /// 当前页
        /// </summary>
        [Description("当前页")]
        public int CurrentPageIndex
        {
            get
            {
                return currentPageIndex;
            }
            set
            {
                currentPageIndex = value;
            }
        }
        private int currentPageIndex = 0;

        /// <summary>
        /// 页wid
        /// </summary>
        [Description("页wid")]
        public int Wid
        {
            get
            {
                return wid;
            }
            set
            {
                wid = value;
            }
        }
        private int wid = 0;

        /// <summary>
        /// 用于填充SQL的@参数
        /// </summary>
        [Description("用于填充SQL的@参数")]
        public Dictionary<string, string> FormParm 
        {
            get
            {
                return formParm;
            }
            set
            {
                formParm = value;
            }
        }
        private Dictionary<string, string> formParm = new Dictionary<string, string>();

        /// <summary>
        /// 过滤行条件
        /// </summary>
        [Description("过滤行条件")]
        public string FilterRow
        {
            get
            {
                return filterRow;
            }
            set
            {
                filterRow = value;
            }
        }
        private string filterRow = "";

        /// <summary>
        /// 过滤列条件
        /// </summary>
        [Description("过滤列条件")]
        public string FilterColumn
        {
            get
            {
                return filterColumn;
            }
            set
            {
                filterColumn = value;
            }
        }
        private string filterColumn = "";

        /// <summary>
        /// 排列顺序
        /// </summary>
        [Description("排列顺序")]
        public string OrderBy
        {
            get
            {

                return orderBy;
            }
            set
            {
                orderBy = value;
            }
        }
        private string orderBy = "";

        /// <summary>
        /// 其他信息
        /// </summary>
        [Description("其他信息")]
        public string OtherMsg
        {
            get
            {

                return otherMsg;
            }
            set
            {
                otherMsg = value;
            }
        }
        private string otherMsg = "";

        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印信息")]
        public bool IsPrint
        {
            get
            {

                return isPrint;
            }
            set
            {
                isPrint = value;
            }
        }
        private bool isPrint = false;

        /// <summary>
        /// EXCLE标识
        /// </summary>
        [Description("导EXCLE标识")]
        public bool IsExcel
        {
            get
            {

                return isExcel;
            }
            set
            {
                isExcel = value;
            }
        }
        private bool isExcel = false;

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        [Description("每页显示记录数")]
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        private int pageSize = 0;

        /// <summary>
        /// JS分页函数
        /// </summary>
        [Description("JS分页函数")]
        public string PagerJs
        {
            get
            {
                return pagerJs;
            }
            set
            {
                pagerJs = value;
            }
        }
        //ajax导航分布函数
        private string pagerJs = "pagerObj.Page";

        /// <summary>
        /// 总记录数
        /// </summary>
        [Description("总记录数")]
        public int RecordCount
        {
            get
            {
                return recordCount;
            }
            set
            {
                recordCount = value;
            }
        }
        private int recordCount = 0;

        /// <summary>
        /// 窗口可见高
        /// </summary>
        [Description("窗口可见高")]
        public int ClientHeight
        {
            get
            {
                return clientHeight;
            }
            set
            {
                clientHeight = value;
            }
        }
        private int clientHeight = 0;

        /// <summary>
        /// 窗口可见宽
        /// </summary>
        [Description("窗口可见宽")]
        public int ClientWidth
        {
            get
            {
                return clientWidth;
            }
            set
            {
                clientWidth = value;
            }
        }
        private int clientWidth = 0;
    }
}
