using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Service.DAL;
using DTO;
using Service.Util;

namespace FM.Business
{
    public class ProcPager
    {
        DALInterface execObj;
        SqlCommandString sqlstring;
        public ProcPager()
        {
            this.sqlstring = new SqlCommandString();
            ConnetString connstr = new ConnetString();
            this.execObj = new DALInterface(null, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));
        }

        /// <summary>
        /// 获取业务数据
        /// </summary>
        /// <param name="pagerArguments"></param>
        /// <param name="detailsColumns"></param>
        /// <param name="recordCount"></param>
        /// <param name="columnDataType"></param>
        /// <param name="totalDetailsData"></param>
        /// <param name="detailsSql"></param>
        /// <param name="detailsData"></param>
        /// <param name="cmDetailsData"></param>
        public PageContent GetProcList( FMPagerArguments pagerArguments, List<string> detailsColumns)
        {
            PageContent pageContent = new PageContent();

            DataSet details = this.execObj.SubmitTextDataSet(this.sqlstring.GetWidSql(pagerArguments.Wid));
            pageContent.DetailsSql = ReplaceSqlCommandVar(details.Tables[0].Rows[0]["sql"].ToString().Trim() + " " + details.Tables[0].Rows[0]["sql_2"].ToString().Trim(), pagerArguments.FormParm);
            DataSet detailsDataSet = this.execObj.SubmitTextDataSet(pageContent.DetailsSql);

            if (detailsDataSet.Tables.Count > 1)
            { //如果明细包含了尺码
                if (details.Tables[0].Columns.Contains("mxly"))
                {
                    //旧系统没有mxly字段
                    //尺码内容直接放在主表的第二个表中
                    if (details.Tables[0].Rows[0]["mxly"].ToString().Trim() == "主表")
                    {
                        pageContent.CmDetailsData = detailsDataSet.Tables[1];
                    }
                }
            }
            //复制一个
            DataTable detailsDataSetCopy = detailsDataSet.Tables[0].Clone();
            if (string.IsNullOrEmpty(pagerArguments.FilterRow))
            {//如果没有筛选条件
                detailsDataSetCopy = detailsDataSet.Tables[0];
            }
            else
            {
                DataRow[] drtp = detailsDataSet.Tables[0].Select("1=1 and " + pagerArguments.FilterRow.Replace("!=", "<>"));
                if (drtp.Length > 0)
                {//如果筛选后还有数据
                    detailsDataSetCopy = DataRow2DataTable(drtp);
                }
            }

            pageContent.RecordCount = detailsDataSetCopy.Rows.Count;
            //排序
            DataView dv = new DataView(detailsDataSetCopy, "", pagerArguments.OrderBy, DataViewRowState.CurrentRows);
            //取指定页            
            //把得到查询字段的值分开,取指定列
            string[] sArray = detailsColumns.ToArray();
            MyTy.Utils us = new MyTy.Utils();
            pageContent.DetailsData = us.GetPagedTable(dv.ToTable("pager", false, sArray), pagerArguments.CurrentPageIndex, pagerArguments.PageSize);
            pageContent.ColumnDataType= GetColumnDataType(pageContent.DetailsData);
            pageContent.TotalDetailsData = dv.ToTable();
            return pageContent;
        }

        /// <summary>
        /// DataRow[]转换DataTable
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable DataRow2DataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return new DataTable();
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中
            return tmp;
        }

        public DataTable GetColumnDataType( DataTable detailsData)
        {
            DataTable columnDataType = new DataTable();
            //获取字段属性
            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "column_name";
            columnDataType.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "data_type";
            columnDataType.Columns.Add(column);

            foreach (DataColumn mycolumn in detailsData.Columns)
            {
                row = columnDataType.NewRow();
                row["data_type"] = mycolumn.DataType.ToString();
                row["column_name"] = mycolumn.ColumnName; ;
                columnDataType.Rows.Add(row);
            }

            return columnDataType;
        }


        /// <summary>
        /// 获取分页存储过程返回的数据
        /// </summary>
        public DataSet GetProcListold(string tableName, string columns, string where, string orderBy, int pageIndex, int pageSize, ref int recordCount)
        {

            System.Data.SqlClient.SqlParameter[] lisParams = new System.Data.SqlClient.SqlParameter[6];
            lisParams[0].ParameterName = "@TABLE_NAME";
            lisParams[0].Value = tableName;

            lisParams[1].ParameterName = "@COLUMNS";
            lisParams[1].Value = columns;

            lisParams[2].ParameterName = "@WHERE";
            lisParams[2].Value = where;

            lisParams[3].ParameterName = "@ORDER_BY";
            lisParams[3].Value = orderBy;

            lisParams[4].ParameterName = "@PAGE_INDEX";
            lisParams[4].Value = pageIndex;

            lisParams[5].ParameterName = "@PAGE_SIZE";
            lisParams[5].Value = pageSize;

            lisParams[6].ParameterName = "@RECORD_COUNT";
            lisParams[6].Value = recordCount;
            lisParams[6].Direction = ParameterDirection.Output;

            
            DataSet ds = this.execObj.SubmitStoredProcedureDataSet("P_PAGER", lisParams);


            recordCount = (int)lisParams[6].Value;
            //获取返回值
            /*foreach (IDataParameter par in map.GetDataAdapter().SelectCommand.Parameters)
            {
                if (par.ParameterName == "@RECORD_COUNT")
                {
                    recordCount = (int)par.Value;
                    break;
                }
            }*/

            return ds;
        }

        /// <summary>
        /// 查找表记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetTableRecord(string tableName, string where)
        { 
            return this.execObj.SubmitTextDataSet(this.sqlstring.GetTablename(tableName, (where == string.Empty ? "" : " where " + where)));

        }
        /// <summary>
        /// 得到页面表头字段信息
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="columnLimit"></param>
        /// <param name="headlineData"></param>
        /// <param name="detailsColumns"></param>
        /// <param name="cmDetailsTag"></param>
        public PageDetail GetHeadlineData(int wid, string columnLimit)
        {
            PageDetail pageDetail = new PageDetail();
            using (DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.Gettbzdinfo(wid, (columnLimit == string.Empty ? " " : " AND  '" + columnLimit + "' not like  '%'+Ltrim(Rtrim(a.ywname)) + ',%'"))))
            {
                pageDetail.Data = ds.Tables[0];
                foreach (DataRow i in pageDetail.Data.Rows)
                {
                    //如果是尺码，那么不用放到列信息(detailsColumns)中
                    if (i["type"].ToString().Trim() == "mx")
                    {                        
                        pageDetail.IsDetail = true;
                        continue;
                    }
                    //如果是明细没有数据的情况下,这个值要用来构造空行
                    pageDetail.Columns.Add(i["ywname"].ToString().Trim());
                }                             
            }
            return pageDetail;
        }

        /// <summary>
        /// 得到明细和尺码
        /// </summary>
        /// <param name="pagerArguments"></param>
        /// <param name="detailsSql"></param>
        /// <param name="cmDetailsData">尺码数据如果来源主表，那么这个参数有值</param>
        /// <returns></returns>
        public PageCmContent GetCmDetails( FMPagerArguments pagerArguments,string detailsSql,DataTable cmDetailsData)
        {
            PageCmContent pageCmContent = new PageCmContent();
            DataSet widConfig = this.execObj.SubmitTextDataSet(this.sqlstring.ContEditSql(pagerArguments.Wid));

            string tmpSqlCommand = "";
            string detailDataSourceTag = "";//明细来源,明细有可能存在主表            

            if (widConfig.Tables[0].Columns.Contains("mxly"))
            {
                //旧系统没有mxly这个字段
                detailDataSourceTag = widConfig.Tables[0].Rows[0]["mxly"].ToString();
            }

            //明细与主表的关联
            pageCmContent.MasterSlaveKey = widConfig.Tables[0].Rows[0]["mxgl"].ToString();
            string masterSlaveRelation = "";
            for (int i = 0; i < pageCmContent.MasterSlaveKey.Split(',').Length; i++)
            {
                if (pageCmContent.MasterSlaveKey.Split(',')[i] != "")
                {
                    masterSlaveRelation += " zb." + pageCmContent.MasterSlaveKey.Split(',')[i].ToString() + "=mx." + pageCmContent.MasterSlaveKey.Split(',')[i];
                    if (i < pageCmContent.MasterSlaveKey.Split(',').Length - 1)
                    {
                        masterSlaveRelation += " and ";
                    }
                }
            }
            //明细与尺码的关联
            pageCmContent.DetailCmRelation = widConfig.Tables[0].Rows[0]["mxhgl"].ToString();
            //主表与尺码的关联
            pageCmContent.MasterCmRelation = widConfig.Tables[0].Rows[0]["mxhord"].ToString();
            //尺码SQL
            string cmSql = widConfig.Tables[0].Rows[0]["mxhsql"].ToString();
            //明细SQL
            string detailSql = widConfig.Tables[0].Rows[0]["mxsql"].ToString();
            if (detailDataSourceTag != "主表")
            {
                //DataMx已经在GetProcList函数中得到内容            
                if (detailSql != "" && masterSlaveRelation != "" && detailsSql != "")
                {
                    tmpSqlCommand = "select mx.* from (" + detailSql + ") mx inner join (select a.* from (" + detailsSql + ") a ) zb on " + masterSlaveRelation;
                    tmpSqlCommand = ReplaceSqlCommandVar(tmpSqlCommand, pagerArguments.FormParm);
                    using (DataSet ds = this.execObj.SubmitTextDataSet(tmpSqlCommand))
                    {
                        if (ds.Tables.Count != 0)
                        {
                            pageCmContent.CmDetailsData = ds.Tables[0];
                            pageCmContent.CmDetailsData.TableName = "DataMx";
                        }
                    }                   
                    
                }
            }
            else
            {
                pageCmContent.CmDetailsData = cmDetailsData;
                pageCmContent.CmDetailsData.TableName = "DataMx";
            }
            if (pageCmContent.CmDetailsData != null)
            {
                using (DataSet ds = execObj.SubmitTextDataSet(cmSql))
                {
                    if (ds.Tables.Count != 0)
                    {
                        pageCmContent.CmHeadlineData = ds.Tables[0];
                    }
                }               
                
            }

            return pageCmContent;

        }

        /// <summary>
        /// 表头左击 筛选---没完成
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="column"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable ghl(string tbname, string column, string where)
        {

            System.Data.SqlClient.SqlParameter[] lisParams = new System.Data.SqlClient.SqlParameter[2];
            lisParams[0].ParameterName = "@tablename";
            lisParams[0].Value = tbname;

            lisParams[1].ParameterName = "@column";
            lisParams[1].Value = column;

            lisParams[2].ParameterName = "@where";
            lisParams[2].Value = where == string.Empty ? "1=1" : where;
            
            return this.execObj.SubmitStoredProcedureDataSet("p_GHL", lisParams).Tables[0];


        }

        /// <summary>
        /// 将sql语名中的变量替换为具体值
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="FormParameters"></param>
        /// <returns></returns>
        public string ReplaceSqlCommandVar(string sqlCommand, Dictionary<string, string> FormParameters)
        {
            foreach (string key in FormParameters.Keys)
            {
                sqlCommand = sqlCommand.Replace(key.ToString().Trim(), FormParameters[key].ToString().Trim());
            }
            return sqlCommand;
        }


        /// <summary>
        /// post配置
        /// </summary>
        /// <param name="tzid"></param>
        /// <returns></returns>
        public DataSet GetPosConfig(string tzid)
        {
            return this.execObj.SubmitTextDataSet(this.sqlstring.GetZMDConfig(tzid));
        }
    }
}
