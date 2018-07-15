using System.Data;
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
        public int RecordCount { get => recordCount; set => recordCount = value; }
        
        /// <summary>
        /// 业务数据类型
        /// </summary>
        public DataTable ColumnDataType { get => columnDataType; set => columnDataType = value; }

        /// <summary>
        /// 全部业务数据
        /// </summary>
        public DataTable TotalDetailsData { get => totalDetailsData; set => totalDetailsData = value; }

        /// <summary>
        /// 业务数据sql
        /// </summary>
        public string DetailsSql { get => detailsSql; set => detailsSql = value; }

        /// <summary>
        /// 当前页显示的业务数据
        /// </summary>
        public DataTable DetailsData { get => detailsData; set => detailsData = value; }

        /// <summary>
        /// 如果尺码关联=主表，可以直接取出尺码数据
        /// </summary>
        public DataTable CmDetailsData { get => cmDetailsData; set => cmDetailsData = value; }
    }
}
