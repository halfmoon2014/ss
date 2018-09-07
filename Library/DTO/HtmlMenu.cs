using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HtmlMenu
    {
        private String htmlmark;
        private bool isOnlyOne;

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

        public bool IsOnlyOne
        {
            get
            {
                return isOnlyOne;
            }

            set
            {
                isOnlyOne = value;
            }
        }
    }
}
