using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
namespace FM.Controls
{
    public class FMPagerArguments
    {

        [Description("是否是第一次加载调用")]
        public int loadMark
        {
            get
            {
                return _loadMark;
            }
            set
            {
                _loadMark = value;
            }
        }
        private int _loadMark = 0;
        [Description("当前页")]
        public int currentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
            }
        }
        private int _currentPageIndex = 0;

        [Description("页wid")]
        public int wid
        {
            get
            {
                return _wid;
            }
            set
            {
                _wid = value;
            }
        }
        private int _wid = 0;

        [Description("用于填充SQL的@参数")]
        public Dictionary<string, string> formParm = new Dictionary<string, string> ();

        [Description("过滤行条件")]
        public string filterRow
        {
            get
            {
                return _filterRow;
            }
            set
            {
                _filterRow = value;
            }
        }
        private string _filterRow = "";

        [Description("过滤列条件")]
        public string filterColumn
        {
            get
            {
                return _filterColumn;
            }
            set
            {
                _filterColumn = value;
            }
        }
        private string _filterColumn = "";

        [Description("排列顺序")]
        public string orderBy
        {
            get
            {

                return _orderBy;
            }
            set
            {
                _orderBy = value;
            }
        }
        private string _orderBy = "";

        [Description("其他信息")]
        public string otherMsg
        {
            get
            {

                return _otherMsg;
            }
            set
            {
                _otherMsg = value;
            }
        }
        private string _otherMsg = "";


        [Description("打印信息")]
        public bool prtFlag
        {
            get
            {

                return _prtFlag;
            }
            set
            {
                _prtFlag = value;
            }
        }
        private bool _prtFlag = false;
        [Description("导EXCLE标识")]
        public bool excelFlag
        {
            get
            {

                return _excelFlag;
            }
            set
            {
                _excelFlag = value;
            }
        }
        private bool _excelFlag = false;


        [Description("每页显示记录数")]
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        private int _pageSize = 0;

        [Description("JS分页函数")]
        public string pagerJs
        {
            get
            {
                return _pagerJs;
            }
            set
            {
                _pagerJs = value;
            }
        }
        //ajax导航分布函数
        private string _pagerJs = "pagerObj.Page";



        [Description("总记录数")]
        public int recordCount
        {
            get
            {
                return _recordCount;
            }
            set
            {
                _recordCount = value;
            }
        }
        private int _recordCount = 0;

        [Description("窗口可见高")]
        public int clientHeight
        {
            get
            {
                return _clientHeight;
            }
            set
            {
                _clientHeight = value;
            }
        }
        private int _clientHeight = 0;

        [Description("窗口可见宽")]
        public int clientWidth
        {
            get
            {
                return _clientWidth;
            }
            set
            {
                _clientWidth = value;
            }
        }
        private int _clientWidth = 0;
    }
}
