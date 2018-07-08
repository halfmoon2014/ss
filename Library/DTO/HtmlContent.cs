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
