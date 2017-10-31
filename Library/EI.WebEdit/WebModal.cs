namespace EI.Web.Modal
{
    public class Html
    {
        private int isEasyLayout;
        private string htmlMark;

        /// <summary>
        /// 简单布局
        /// </summary>
        public int IsEasyLayout
        {
            get
            {
                return isEasyLayout;
            }

            set
            {
                isEasyLayout = value;
            }
        }

        public string HtmlMark
        {
            get
            {
                return htmlMark;
            }

            set
            {
                htmlMark = value;
            }
        }
    }
}
