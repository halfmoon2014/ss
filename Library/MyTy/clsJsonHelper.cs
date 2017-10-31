using System;
using System.Collections.Generic;
using System.Collections;


namespace MyTy
{
    /// <summary>
    /// 用于Json格式的文本操作
    /// </summary>
    public class clsJsonHelper : IDisposable
    {
        public string jSon = "";
        private const string ErrText = "err";

        private Hashtable htItems = new Hashtable();
        private Hashtable varNode = new Hashtable();

        public clsJsonHelper()
        {

        }

        public static clsJsonHelper CreateJsonHelper(string strJson)
        {
            clsJsonHelper Jh = new clsJsonHelper();
            Jh.SetjSon(strJson);
            return Jh;
        }

        //找到字符串成对匹配符的结束位置
        private int FindToEnd(ref string sourceString, char beginFlag, char endFlag)
        {
            int begincount = 0;
            int endcount = 0;
            int i;
            for (i = 0; i < sourceString.Length; i++)
            {
                if (sourceString[i].Equals(beginFlag)) begincount++;
                else if (sourceString[i].Equals(endFlag)) endcount++;

                if (begincount > 0 && begincount == endcount)
                {
                    break;
                }
            }
            return i;
        }

        //查找字符串的第一个字符
        private char FindFirstFlag(ref string sourceString, int IndexBegin)
        {
            string findString = sourceString.Substring(IndexBegin);

            findString = findString.Trim();
            if (findString.Length > 0) return findString[0];
            else return ' ';
        }

        /// <summary>
        /// 使用字符串初始化Json对象
        /// </summary>
        /// <param name="initJson"></param>
        private void SetjSon(string initJson)
        {
            try
            {
                initJson = initJson.Trim();
                initJson = initJson.Substring(1, initJson.Length - 2);

                string myStr;
                int myIndex;
                string htName, htValue;
                //List<string> strList = new List<string>(initJson.Split(','));
                List<string> strList = new List<string>();

                int flg2PointIndex;
                int findOver;
                string AddString;
                int tmp1, tmp2;
                while (initJson.Contains("\":"))
                {
                    initJson = initJson.Trim();
                    flg2PointIndex = initJson.IndexOf("\":");   //肯定能取得数据
                    flg2PointIndex += 2;

                    if (FindFirstFlag(ref initJson, flg2PointIndex) == '{')
                    {
                        findOver = FindToEnd(ref initJson, '{', '}');
                    }
                    else if (FindFirstFlag(ref initJson, flg2PointIndex) == '[')
                    {
                        findOver = FindToEnd(ref initJson, '[', ']');
                    }
                    else
                    {
                        tmp1 = initJson.IndexOf('"', flg2PointIndex + 1);
                        tmp2 = initJson.IndexOf(',', flg2PointIndex + 1);

                        if (tmp1 > tmp2 && initJson[flg2PointIndex] == '"')
                        {
                            findOver = tmp1;
                        }
                        else if (tmp2 > 0)
                        {
                            findOver = tmp2 - 1;
                        }
                        else
                        {
                            findOver = initJson.Length - 1;
                        }


                        //if (FindFirstFlag(ref initJson, flg2PointIndex) == '"')
                        //{ 
                        //    findOver = FindToEnd(ref initJson, '"', '"');
                        //}
                        //else if (initJson.IndexOf(',') > 0)
                        //{
                        //    findOver = initJson.IndexOf(',') - 1;
                        //}
                        //else
                        //{
                        //findOver = initJson.Length - 1;
                        //}
                    }

                    AddString = initJson.Substring(0, findOver + 1);
                    strList.Add(AddString);
                    initJson = initJson.Remove(0, AddString.Length);
                    initJson = initJson.Trim();
                    if (initJson.Length > 0 && initJson[0] == ',')
                    {
                        initJson = initJson.Trim().Remove(0, 1);
                    }
                }

                foreach (string strJs in strList)
                {
                    myStr = strJs.Trim();
                    if (myStr != "")
                    {
                        myStr = myStr.Remove(0, 1);
                        myIndex = myStr.IndexOf("\":");
                        htName = myStr.Substring(0, myIndex);

                        myStr = myStr.Substring(myIndex + 2);
                        myStr = myStr.Trim();

                        //myLastIndex = myStr.LastIndexOf("\"");
                        //if (myLastIndex == -1)
                        //{
                        //    htValue = myStr;
                        //}
                        //else
                        //{
                        //    htValue = myStr.Substring(1, myLastIndex - 1);
                        //}
                        htValue = myStr;
                        htValue = htValue.Trim();
                        htItems.Add(htName, htValue);
                        if (htValue.IndexOf('{') > -1)       //必须要有大括号，才创建子数据
                        {
                            CreateNodeJson(htName, htValue);
                        }
                    }
                }

                CreateJsonString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建节点json对象 
        /// </summary>
        /// <param name="jname"></param>
        /// <param name="jvalue"></param>
        private void CreateNodeJson(string jname, string jvalue)
        {
            int Beginindex;
            int findOver;

            Beginindex = jvalue.IndexOf('{');
            findOver = FindToEnd(ref jvalue, '{', '}');

            List<clsJsonHelper> jhList = new List<clsJsonHelper>();
            clsJsonHelper jh;
            while (Beginindex > -1 && findOver > Beginindex)
            {
                jh = clsJsonHelper.CreateJsonHelper(jvalue.Substring(Beginindex, findOver - Beginindex + 1));
                jhList.Add(jh);
                jvalue = jvalue.Substring(findOver + 1);

                Beginindex = jvalue.IndexOf('{');
                findOver = FindToEnd(ref jvalue, '{', '}');
            }
            if (jhList.Count > 0)
            {
                varNode.Add(jname, jhList);
            }
        }

        public void AddJsonVar(string jname, string jvalue)
        {
            if (htItems.ContainsKey(jname))
            {
                htItems.Remove(jname);
            }

            htItems.Add(jname, string.Concat("\"", jvalue, "\""));
            CreateJsonString();
        }

        /// <summary>
        /// 添加一个变量，并指定是否为值追加双引号。By:xlm 20151016追加此方法 
        /// </summary>
        /// <param name="jname">变量名</param>
        /// <param name="jvalue">变量值</param>
        /// <param name="AddQuotes">是否追加双引号</param>
        public void AddJsonVar(string jname, string jvalue, bool AddQuotes)
        {
            if (AddQuotes)
            {
                AddJsonVar(jname, jvalue);
            }
            else
            {
                if (htItems.ContainsKey(jname))
                {
                    htItems.Remove(jname);
                }
                htItems.Add(jname, jvalue);
                CreateJsonString();
            }
        }

        public void ClearJsonVar()
        {
            htItems.Clear();
            CreateJsonString();
        }

        public void RemoveJsonVar(string jname)
        {
            if (htItems.ContainsKey(jname))
            {
                htItems.Remove(jname);
                CreateJsonString();
            }
        }

        ///// <summary>
        ///// 返回Json值
        ///// </summary>
        ///// <param name="jname"></param>
        ///// <returns></returns>
        //public string GetJsonValue(string jname)
        //{
        //    if (htItems.ContainsKey(jname))
        //    {                 
        //        string value = htItems[jname].ToString();
        //        if (value[0] == '\"')
        //        {
        //            value = value.Substring(1, value.Length - 2);
        //        }
        //        return value;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        /// <summary>
        /// 返回Json值,支持 使用 / 寻径。比如：  节点1/子节点2/子节点3
        /// </summary>
        /// <param name="jname"></param>
        /// <returns></returns>
        public string GetJsonValue(string jname)
        {
            try
            {
                string[] jnamelist = jname.Split('/');
                int j = jnamelist.Length;
                clsJsonHelper tmpJh = this;
                for (int i = 0; i < j; i++)
                {
                    if (i == j - 1)
                    {
                        if (tmpJh.htItems.ContainsKey(jnamelist[i]))
                        {
                            string value = tmpJh.htItems[jnamelist[i]].ToString();
                            if (value[0] == '\"')
                            {
                                value = value.Substring(1, value.Length - 2);
                            }
                            return value;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        tmpJh = tmpJh.GetJsonNodes(jnamelist[i])[0];
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "Json解析错误：" + ex.Message;
            }
        }

        /// <summary>
        /// 返回Json节点
        /// </summary>
        /// <param name="jname"></param>
        /// <returns></returns>
        public List<clsJsonHelper> GetJsonNodes(string jname)
        {
            try
            {
                string[] jnamelist = jname.Split('/');
                int j = jnamelist.Length;
                clsJsonHelper tmpJh = this;
                for (int i = 0; i < j; i++)
                {
                    if (i == j - 1)
                    {
                        if (tmpJh.varNode.ContainsKey(jnamelist[i]))
                        {
                            List<clsJsonHelper> jhList = (List<clsJsonHelper>)tmpJh.varNode[jnamelist[i]];

                            return jhList;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        tmpJh = tmpJh.GetJsonNodes(jnamelist[i])[0];
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

        /// <summary>
        /// 创建Json字符串
        /// </summary>
        /// <returns></returns>
        private string CreateJsonString()
        {
            jSon = "";

            foreach (DictionaryEntry de in htItems)
            {
                if (jSon == "")
                {
                    jSon += "{";
                }
                else
                {
                    jSon += ",\n";
                }

                //jSon += "\"" + de.Key + "\":\"" + de.Value + "\"";

                jSon += "\"" + de.Key + "\":" + de.Value;
            }
            jSon += "}";

            return jSon;
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="ErrorInfo"></param>
        public void SetJsonErr(string ErrorInfo)
        {
            AddJsonVar(ErrText, ErrorInfo);
        }

        public void Dispose()
        {
            int j;
            foreach (DictionaryEntry de in varNode)
            {
                List<clsJsonHelper> jhList = (List<clsJsonHelper>)de.Value;
                j = jhList.Count;
                for (int i = j - 1; i > -1; i--)
                {
                    jhList[i].Dispose();
                }
            }
            htItems.Clear();
            varNode.Clear();
            jSon = "";
        }
    }
}
