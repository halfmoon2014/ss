﻿using System.Data;
namespace DTO
{
    public class PageContent
    {
        private int recordCount;
        private DataTable columnDataType;
        private DataTable totalDetailsData;
        private string detailsSql;
        private DataTable detailsData;
        private DataTable cmDetailsData=new DataTable();

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return recordCount;
            }

            set
            {
                recordCount = value;
            }
        }

        /// <summary>
        /// 业务详情各项的数据类型
        /// </summary>
        public DataTable ColumnDataType
        {
            get
            {
                return columnDataType;
            }

            set
            {
                columnDataType = value;
            }
        }

        /// <summary>
        /// 完整的业务详情
        /// </summary>
        public DataTable TotalDetailsData
        {
            get
            {
                return totalDetailsData;
            }

            set
            {
                totalDetailsData = value;
            }
        }

        /// <summary>
        /// 详情数据SQL
        /// </summary>
        public string DetailsSql
        {
            get
            {
                return detailsSql;
            }

            set
            {
                detailsSql = value;
            }
        }

        /// <summary>
        /// 当前页要显示的业务详情
        /// </summary>
        public DataTable DetailsData
        {
            get
            {
                return detailsData;
            }

            set
            {
                detailsData = value;
            }
        }

        /// <summary>
        /// 业务详情对应的尺码数据
        /// </summary>
        public DataTable CmDetailsData
        {
            get
            {
                return cmDetailsData;
            }

            set
            {
                cmDetailsData = value;
            }
        }
    }
}
