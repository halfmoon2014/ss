using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace MyTy
{
    public class Utils
    {
        #region 转义字符
        /// <summary>
        /// 转义字符
        /// 只转义引号,这样页面才可以正常显示引号
        /// 不能转义空格,因为保存会变问号
        /// </summary>
        /// <param name="stringValue">需转义字符</param>
        /// <returns></returns>
        public static string HtmlCha(string stringValue)
        {
            return stringValue.Replace("\"", "&quot;");
        }
        #endregion
        /// <summary>
        /// 查找某个字符串出现的字符个数
        /// </summary>
        /// <param name="inputString">字串</param>
        /// <param name="value">特定字符</param>
        /// <param name="ignoreCase">不区分大小写</param>
        /// <returns></returns>
        public int ContainCount(string inputString, char value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                inputString = inputString.ToLower();
                if (Char.IsUpper(value))
                {
                    value = Char.ToLower(value);
                }
            }

            int count = 0;

            for (int i = 0; (i = inputString.IndexOf(value, i)) >= 0; i++)
            {
                count++;
            }

            return count;
        }
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        /// <summary>
        /// 是否无符号整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
        /// <summary> 
        /// DataTable分页 
        /// </summary> 
        /// <param name="dt">DataTable</param> 
        /// <param name="PageIndex">页索引,注意：从1开始</param> 
        /// <param name="PageSize">每页大小</param> 
        /// <returns>分好页的DataTable数据</returns>
        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        /// <summary>
        /// web应用所在的绝对路径
        /// </summary>
        /// <returns></returns>
        public string GetWebPath()
        {
            //return MyTy.ConfigReader.Read(HttpContext.Current.Server.MapPath("~/config.xml"), "/Root/appSettings/LogPath", "");
            return System.Web.Hosting.HostingEnvironment.MapPath("~");
        }

        #region 人民币小写金额转大写金额
        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="Money">接收需要转换的小写金额</param>
        /// <returns>返回大写金额</returns>
        public string ConvertMoney(Decimal Money)
        {
            //金额转换程序
            string MoneyNum ;//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾元角分";//模

            MoneyNum = ((long)(Money * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr += "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零元")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零元", "元零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            return MoneyStr;
        }
        #endregion  

        /// <summary>
        /// datatable 与datatable 关联
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="FJC"></param>
        /// <param name="SJC"></param>
        /// <returns></returns>
        public static DataTable Join(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
        {

            //创建一个新的DataTable  

            DataTable table = new DataTable("Join");


            // Use a DataSet to leverage DataRelation  

            using (DataSet ds = new DataSet())
            {

                //把DataTable Copy到DataSet中  

                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                DataColumn[] parentcolumns = new DataColumn[FJC.Length];

                for (int i = 0; i < parentcolumns.Length; i++)
                {

                    parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];

                }

                DataColumn[] childcolumns = new DataColumn[SJC.Length];

                for (int i = 0; i < childcolumns.Length; i++)
                {

                    childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];

                }


                //创建关联  

                DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);

                ds.Relations.Add(r);


                //为关联表创建列  

                for (int i = 0; i < First.Columns.Count; i++)
                {

                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);

                }

                for (int i = 0; i < Second.Columns.Count; i++)
                {

                    //看看有没有重复的列，如果有在第二个DataTable的Column的列明后加_Second  

                    if (!table.Columns.Contains(Second.Columns[i].ColumnName))

                        table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);

                    else

                        table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);

                }


                table.BeginLoadData();

                foreach (DataRow firstrow in ds.Tables[0].Rows)
                {

                    //得到行的数据  

                    DataRow[] childrows = firstrow.GetChildRows(r);

                    if (childrows != null && childrows.Length > 0)
                    {

                        object[] parentarray = firstrow.ItemArray;

                        foreach (DataRow secondrow in childrows)
                        {

                            object[] secondarray = secondrow.ItemArray;

                            object[] joinarray = new object[parentarray.Length + secondarray.Length];

                            Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);

                            Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                            table.LoadDataRow(joinarray, true);

                        }

                    }

                }

                table.EndLoadData();

            }


            return table;

        }

        public static DataTable Join(DataTable First, DataTable Second, DataColumn FJC, DataColumn SJC)
        {

            return Join(First, Second, new DataColumn[] { FJC }, new DataColumn[] { SJC });

        }

        public static DataTable Join(DataTable First, DataTable Second, string FJC, string SJC)
        {

            return Join(First, Second, new DataColumn[] { First.Columns[FJC] }, new DataColumn[] { Second.Columns[SJC] });

        }

        public static DataTable Join(DataTable First, DataTable Second, string FJC, char Fsplit, string SJC, char Ssplit)
        {

            DataTable f = new DataTable();
            int fcount = 0;
            for (int i = 0; i < FJC.Split(Fsplit).Length; i++)
            {
                if (FJC.Split(Fsplit)[i] != null)
                {
                    DataColumn dc = new DataColumn(First.Columns[FJC.Split(Fsplit)[i]].ColumnName);
                    f.Columns.Add(dc);
                    fcount += 1;
                }
            }
            DataColumn[] f1 = new DataColumn[fcount];
            f.Columns.CopyTo(f1, 0);

            fcount = 0;
            DataTable s = new DataTable();
            for (int i = 0; i < SJC.Split(Ssplit).Length; i++)
            {
                if (SJC.Split(Ssplit)[i] != null)
                {
                    DataColumn dc = new DataColumn(First.Columns[SJC.Split(Ssplit)[i]].ColumnName);
                    s.Columns.Add(dc);
                    fcount += 1;
                }
            }
            DataColumn[] s1 = new DataColumn[fcount];
            s.Columns.CopyTo(s1, 0);
            return Join(First, Second, f1, s1);

        }

        /// <summary>
        /// 
        /// 测试网线路
        /// </summary>
        /// <param name="ipStr"></param>
        /// <returns></returns>
        public static bool TestNet(string ipStr)
        {            
                        
            //构造Ping实例
            Ping pingSender = new Ping();
            //Ping 选项设置
            PingOptions options = new PingOptions
            {
                DontFragment = true
            };
            //测试数据
            string data = "test data";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //设置超时时间
            int timeout = 120;
            //调用同步 send 方法发送数据,将返回结果保存至PingReply实例
            try
            {
                PingReply reply = pingSender.Send(ipStr, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                    return false;
            }catch(SystemException )
            {
                return false;
            }
        }

    }
}
