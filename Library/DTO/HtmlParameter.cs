using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace DTO
{
    public class HtmlParameter
    {
        private NameValueCollection queryString;
        private NameValueCollection form;

        public NameValueCollection QueryString
        {
            get
            {
                return queryString;
            }

            set
            {
                queryString = value;
            }
        }

        public NameValueCollection Form
        {
            get
            {
                return form;
            }

            set
            {
                form = value;
            }
        }
    }
}
