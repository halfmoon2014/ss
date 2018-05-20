using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PageHtml
    {
        /// <summary>
        /// html
        /// </summary>
        private string html;
        /// <summary>
        /// 列数
        /// </summary>
        private int columnCount;

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
    }
}
