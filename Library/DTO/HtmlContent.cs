using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HtmlContent
    {
        private String htmlmark;
        private int height;
        private int width;

        /// <summary>
        /// html
        /// </summary>
        public string Htmlmark
        {
            get
            {
                return htmlmark;
            }

            set
            {
                htmlmark = value;
            }
        }

        /// <summary>
        /// 所需要的高
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        /// <summary>
        /// 所需要的宽
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
    }
}
