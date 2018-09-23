﻿namespace DTO
{
    public class PageConfiguration
    {
        /// <summary>
        /// 页面样式
        /// </summary>
        private string pageThemes;
        /// <summary>
        /// 页面样式主题
        /// </summary>
        private string themes;
        /// <summary>
        /// JSCDN
        /// </summary>
        private string jsCDN;
        /// <summary>
        /// CSSCDN
        /// </summary>
        private string cssCDN;

        public string PageThemes
        {
            get
            {
                return pageThemes;
            }

            set
            {
                pageThemes = value;
            }
        }

        public string Themes
        {
            get
            {
                return themes;
            }

            set
            {
                themes = value;
            }
        }

        public string JsCDN
        {
            get
            {
                return jsCDN;
            }

            set
            {
                jsCDN = value;
            }
        }

        public string CssCDN
        {
            get
            {
                return cssCDN;
            }

            set
            {
                cssCDN = value;
            }
        }
    }
}