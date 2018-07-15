using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TotalHtml
    {
        private string html;
        private int tableWidth;
        /// <summary>
        /// 表格显示宽度
        /// </summary>
        public int TableWidth
        {
            get
            {
                return tableWidth;
            }

            set
            {
                tableWidth = value;
            }
        }
        /// <summary>
        /// 合计表格
        /// </summary>
        public string Html
        {
            get
            {
                return html;
            }

            set
            {
                html = value;
            }
        }
    }
}
