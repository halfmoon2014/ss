using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PageHtml
    {

        private string html;        

        private int columnCount;

        private int tableWidth;
        /// <summary>
        /// html
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
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return columnCount;
            }

            set
            {
                columnCount = value;
            }
        }
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
    }
}
