using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Service.DAL;

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

        public void GetProcList(int wid, string detailsColumns, string orderBy, int pageIndex, int pageSize, Dictionary<string, string> formParm, string filterRow, 
            ref int recordCount, ref DataTable columnDataType, ref DataTable totalDetailsData, ref string detailsSql, ref DataTable detailsData ,ref DataTable cmDetailsData)
        {           
            DataSet details = this.execObj.SubmitTextDataSet(this.sqlstring.GetWidSql(wid));
            
            detailsSql = ReplaceSqlCommandVar(details.Tables[0].Rows[0]["sql"].ToString().Trim() + " " + details.Tables[0].Rows[0]["sql_2"].ToString().Trim(), formParm);
            DataSet detailsDataSet = this.execObj.SubmitTextDataSet(detailsSql);

            if (detailsDataSet.Tables.Count > 1)
            { //如果明细包含了尺码
                if (details.Tables[0].Columns.Contains("mxly"))
                {//旧系统没有mxly字段
                    if (details.Tables[0].Rows[0]["mxly"].ToString().Trim() == "主表")
                    {
                        cmDetailsData = detailsDataSet.Tables[1];
                    }
                }
            }
            //复制一个
            DataTable detailsDataSetCopy = detailsDataSet.Tables[0].Clone();
            if (filterRow == string.Empty)
            {//如果没有筛选条件
                detailsDataSetCopy = detailsDataSet.Tables[0];
            }
            else
            {
                DataRow[] drtp = detailsDataSet.Tables[0].Select("1=1 and " + filterRow.Replace("!=", "<>"));
                if (drtp.Length > 0)
                {//如果筛选后还有数据
                    detailsDataSetCopy = DataRow2DataTable(drtp);
                }
            }

            recordCount = detailsDataSetCopy.Rows.Count;
            //排序
            DataView dv = new DataView(detailsDataSetCopy, "", orderBy, DataViewRowState.CurrentRows);
            //取指定页            
            //把得到查询字段的值分开,取指定列
            string[] sArray = Regex.Split(detailsColumns, ",", RegexOptions.IgnoreCase);
            MyTy.Utils us = new MyTy.Utils();
            detailsData = us.GetPagedTable(dv.ToTable("pager", false, sArray), pageIndex, pageSize);
            GetColumnDataType(columnDataType, detailsData);
            totalDetailsData = dv.ToTable();
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

        public void GetColumnDataType(DataTable columnDataType, DataTable detailsData)
        {
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
        public void GetHeadlineData(int wid, string columnLimit,ref DataTable headlineData, ref string detailsColumns, ref int cmDetailsTag)
        { 
            using (DataSet ds = this.execObj.SubmitTextDataSet(this.sqlstring.Gettbzdinfo(wid, (columnLimit == string.Empty ? " " : " AND  '" + columnLimit + "' not like  '%'+Ltrim(Rtrim(a.ywname)) + ',%'"))))
            {
                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    //返回是否存在尺码标识
                    if (i["type"].ToString().Trim() == "mx")
                    {
                        cmDetailsTag = 1;
                        continue;
                    }
                    detailsColumns += i["ywname"].ToString().Trim() + ",";
                }
                //如果是明细没有数据的情况下,这个值要用来构造空行
                detailsColumns = detailsColumns.Substring(0, detailsColumns.Length - 1);
                headlineData = ds.Tables[0];
            }

        }
        /// <summary>
        /// 得到明细和尺码
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="detailsSql"></param>
        /// <param name="formParm"></param>
        /// <param name="cmDetailsData"></param>
        /// <param name="cmHeadlineData"></param>
        /// <param name="masterCmRelation"></param>
        /// <param name="masterSlaveKey"></param>
        /// <param name="detailCmRelation"></param>
        public void GetCmDetails(int wid, string detailsSql, Dictionary<string, string> formParm, ref DataTable cmDetailsData, ref DataTable cmHeadlineData, ref string masterCmRelation, ref string masterSlaveKey, ref string detailCmRelation)
        {
            DataSet widConfig = this.execObj.SubmitTextDataSet(this.sqlstring.ContEditSql(wid));

            string tmpSqlCommand = "";
            string detailDataSourceTag = "";//明细来源,明细有可能存在主表            

            if (widConfig.Tables[0].Columns.Contains("mxly"))
            {//旧系统没有mxly这个字段
                detailDataSourceTag = widConfig.Tables[0].Rows[0]["mxly"].ToString();
            }

            //明细与主表的关联
            masterSlaveKey = widConfig.Tables[0].Rows[0]["mxgl"].ToString();
            string masterSlaveRelation = "";
            for (int i = 0; i < masterSlaveKey.Split(',').Length; i++)
            {
                if (masterSlaveKey.Split(',')[i] != "")
                {
                    masterSlaveRelation += " zb." + masterSlaveKey.Split(',')[i].ToString() + "=mx." + masterSlaveKey.Split(',')[i];
                    if (i < masterSlaveKey.Split(',').Length - 1)
                    {
                        masterSlaveRelation += " and ";
                    }
                }
            }            
            //明细与尺码的关联
            detailCmRelation = widConfig.Tables[0].Rows[0]["mxhgl"].ToString();
            //主表与尺码的关联
            masterCmRelation = widConfig.Tables[0].Rows[0]["mxhord"].ToString();
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
                    tmpSqlCommand = ReplaceSqlCommandVar(tmpSqlCommand, formParm);
                    using (DataSet ds = this.execObj.SubmitTextDataSet(tmpSqlCommand))
                    {
                        if (ds.Tables.Count != 0)
                        {
                            cmDetailsData = ds.Tables[0];
                            cmDetailsData.TableName = "DataMx";
                        }
                    }                   
                    
                }
            }
            if (cmDetailsData != null)
            {
                using (DataSet ds = execObj.SubmitTextDataSet(cmSql))
                {
                    if (ds.Tables.Count != 0)
                    {
                        cmHeadlineData = ds.Tables[0];
                    }
                }               
                
            }

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
