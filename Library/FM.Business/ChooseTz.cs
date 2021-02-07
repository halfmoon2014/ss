using MySession;
using System.Data;
using Service.Util;
using Service.DAL;
using DTO;
using MyTy;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace FM.Business
{
    public class ChooseTz
    {
        DALInterface execObj;
        SqlCommandString sqlstring;
        ConnetString connstr;

        public ChooseTz()
        {
            this.sqlstring = new SqlCommandString();
            this.connstr = new ConnetString();
            this.execObj = new DALInterface(null, connstr.GetConnString());
        }
        /// <summary>
        ///将当前选择的套账放入session 
        /// </summary>
        /// <param name="value1"></param>
        public void AddTzid(string value1)
        {
            SessionHandle.Add("tzid", value1);
        }

        public DataTable GetTzMenuJson(int userid) {
            return execObj.SubmitTextDataSet(sqlstring.TzData(userid.ToString())).Tables[0];
        }
        /// <summary>
        /// 得到TZ选择
        /// </summary>
        /// <returns></returns>
        public HtmlMenu GetTzMenu(string path, string mrmenu)
        {
            HtmlMenu htmlMenu = new HtmlMenu();

            DataTable dt = GetTzMenuJson(int.Parse(SessionHandle.Get("userid")));
            
          
            string div = ExtUtil.GetHtml(path, "\\webpage\\chooseExt\\mobile");            
            if (dt.Rows.Count <= 0)
                //没有找到单据
                //rstring[1] = "<table><tr><td>'无可选套账'</td></tr></table>";
                htmlMenu.Htmlmark = string.Format(div, "<a href=\"#\" class=\"list-group-item\">无可选套账</a>");
            else
            {
                if (dt.Rows.Count == -1)
                {
                    //只有一个套账的时候,直接进入,停用这个方式,后面会有影响
                    string tm = dt.Rows[0]["menu"].ToString();
                    htmlMenu.Htmlmark = "webpage/" + (tm == string.Empty ? mrmenu : tm) + ".aspx";
                    AddTzid(dt.Rows[0]["tzid"].ToString());
                    htmlMenu.IsOnlyOne = true;
                }
                else
                {
                    string tzHtml = ExtUtil.GetHtml(path, "\\webpage\\chooseExt\\tzlist");
                    StringBuilder tzHtmlBuer = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                        tzHtmlBuer.Append(string.Format(tzHtml, dr["tzid"].ToString().Trim(), (dr["menu"].ToString().Trim() == string.Empty ? mrmenu : dr["menu"].ToString().Trim()), dt.Rows.IndexOf(dr) + 1, dr["tzmc"].ToString().Trim(), dr["sm"].ToString().Trim()));

                    htmlMenu.Htmlmark = string.Format(div, tzHtmlBuer.ToString());
                }
            }

            return htmlMenu;
        }
    }
}
