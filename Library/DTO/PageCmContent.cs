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

        public DataTable CmDetailsData { get => cmDetailsData; set => cmDetailsData = value; }
        public DataTable CmHeadlineData { get => cmHeadlineData; set => cmHeadlineData = value; }
        public string MasterCmRelation { get => masterCmRelation; set => masterCmRelation = value; }
        public string MasterSlaveKey { get => masterSlaveKey; set => masterSlaveKey = value; }
        public string DetailCmRelation { get => detailCmRelation; set => detailCmRelation = value; }
    }
}
