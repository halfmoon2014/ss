using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DTO
{
    public class PageDetail
    {
        private DataTable data;
        private bool isDetail;
        private List<string> columns=new List<string>();
        /// <summary>
        /// 字段数据
        /// </summary>
        public DataTable Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;

            }
        }
        /// <summary>
        /// 是否存在尺码
        /// </summary>
        public bool IsDetail
        {
            get
            {
                return isDetail;
            }

            set
            {
                isDetail = value;

            }
        }
        /// <summary>
        /// 要显示的字段
        /// </summary>
        public List<string> Columns
        {
            get
            {
                return columns;
            }

            set
            {
                columns = value;

            }
        }
    }
}
