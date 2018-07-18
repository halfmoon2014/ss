using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DTO
{
    public class PageCmContent
    {
        private DataTable cmDetailsData=new DataTable();
        private DataTable cmHeadlineData;
        private string masterCmRelation;
        private string masterSlaveKey;
        private string detailCmRelation;

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

        /// <summary>
        /// 尺码标题
        /// </summary>
        public DataTable CmHeadlineData
        {
            get
            {
                return cmHeadlineData;
            }

            set
            {
                cmHeadlineData = value;
            }
        }

        /// <summary>
        /// 详情与尺码标题的关联
        /// </summary>
        public string MasterCmRelation
        {
            get
            {
                return masterCmRelation;
            }

            set
            {
                masterCmRelation = value;
            }
        }

        /// <summary>
        /// 详情和尺码的关联
        /// </summary>
        public string MasterSlaveKey
        {
            get
            {
                return masterSlaveKey;
            }

            set
            {
                masterSlaveKey = value;
            }
        }

        /// <summary>
        /// 尺码与尺码标题的关联
        /// </summary>
        public string DetailCmRelation
        {
            get
            {
                return detailCmRelation;
            }

            set
            {
                detailCmRelation = value;
            }
        }
    }
}
