using MySession;
using System.Data;
using Service.Util;
using Service.DAL;
namespace FM.Business
{
    public class ChooseTz
    {
        

        
        /// <summary>
        ///将当前选择的套账放入session 
        /// </summary>
        /// <param name="value1"></param>
        public void AddTzid(string value1)
        {
            MySession.SessionHandle.Add("tzid", value1);
        }
        /// <summary>
        /// 得到TZ选择
        /// </summary>
        /// <returns></returns>
        public string[] GetTzMenu(string mrmenu)
        {
            string[] rstring = new string[2];
            rstring[0] = "";

            //user登陆名
            if (!string.IsNullOrEmpty(SessionHandle.Get("userid")))
            {
                SqlCommandString sqlstring = new SqlCommandString();
                ConnetString connstr = new ConnetString();
                DALInterface execObj = new DALInterface(null, connstr.GetConnString());
                
                DataSet ds = execObj.SubmitTextDataSet(sqlstring.TzData(SessionHandle.Get("userid")));
                if (ds.Tables[0].Rows.Count <= 0)
                {//没有找到单据
                    rstring[1] = "<table><tr><td>'无可选套账'</td></tr></table>";
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {//只有一个套账的时候,直接进入,停用这个方式,后面会有影响
                        string tm = ds.Tables[0].Rows[0]["menu"].ToString();
                        rstring[1] = "webpage/" + (tm == string.Empty ? mrmenu : tm) + ".aspx";
                        AddTzid(ds.Tables[0].Rows[0]["tzid"].ToString());
                        rstring[0] = "Response";
                    }
                    else
                    {
                        string str = "<table width='100%' height='100%' border='0' cellpadding='0' cellspacing='0' >";
                        str += "<tr>";
                        str += "<td align='center' align='center'>";
                        str += "<table width='737' height='356' border='0' cellspacing='0' cellpadding='0'>";
                        str += "<tr>";
                        str += "<td width='40'>";
                        str += "</td>";
                        str += "<td align='center' align='center'>";
                        str += "<br>";
                        str += "<br>";
                        str += "<br>";
                        str += "<br>";

                        str += "<table  align='center'>";
                        str += "<tr style='height:30px'><td style='width: 14px;' > &nbsp;</td><td align='right'>&nbsp;</td><td align='left' style='width:80px'>可选套账</td><td align='left'>说明</td><td>&nbsp;</td></tr>";

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string lm = dr["menu"].ToString().Trim();
                            //默认主页为menu_
                            lm = (lm == string.Empty ? mrmenu : lm);

                            str += "<tr align='center'><td style='width: 14px'> &nbsp;</td><td align='right'>" + (ds.Tables[0].Rows.IndexOf(dr) + 1).ToString().Trim() + "</td><td align='left'><a href='#' mylink=1  t=\"" + dr["tzid"].ToString().Trim() + "\" m=\"" + lm + "\" > " + dr["tzmc"].ToString().Trim() + "</a></td><td align='left'>" + dr["sm"].ToString().Trim() + "</td><td>&nbsp;</td></tr>";
                        }
                        str += "</table></td></tr></table></td></tr> </table>";
                        rstring[1] = str;
                    }
                }
            }
            return rstring;

        }
    }
}
