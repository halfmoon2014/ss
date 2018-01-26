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
                string div = 
                    "<div class=\"form-signin\">" +
                        "<div class=\"panel panel-default\">" +
                            "<div class=\"panel-heading\">套账选择</div>" +
                            "<div class=\"list-group tzlist\">{0}</div>" +
                            "<div class=\"panel-footer\">" +
                                    "<div class=\"checkbox\" style=\"text-align:right\">" +
                                        "<label><input name=\"updata\" id=\"updata\" type = \"checkbox\" >权限更新</label >" +
                                    "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div> ";


                if (ds.Tables[0].Rows.Count <= 0)
                {//没有找到单据
                    //rstring[1] = "<table><tr><td>'无可选套账'</td></tr></table>";
                    rstring[1] = string.Format(div, "<a href=\"#\" class=\"list-group-item\">无可选套账</a>");
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == -1)
                    {//只有一个套账的时候,直接进入,停用这个方式,后面会有影响
                        string tm = ds.Tables[0].Rows[0]["menu"].ToString();
                        rstring[1] = "webpage/" + (tm == string.Empty ? mrmenu : tm) + ".aspx";
                        AddTzid(ds.Tables[0].Rows[0]["tzid"].ToString());
                        rstring[0] = "Response";
                    }
                    else
                    {

                        //string str = "<table width='100%' height='100%' border='0' cellpadding='0' cellspacing='0' >";
                        //str += "<tr>";
                        //str += "<td align='center' align='center'>";
                        //str += "<table width='737' height='356' border='0' cellspacing='0' cellpadding='0'>";
                        //str += "<tr>";
                        //str += "<td width='40'>";
                        //str += "</td>";
                        //str += "<td align='center' align='center'>";
                        //str += "<br>";
                        //str += "<br>";
                        //str += "<br>";
                        //str += "<br>";

                        //str += "<table  align='center'>";
                        //str += "<tr style='height:30px'><td style='width: 14px;' > &nbsp;</td><td align='right'>&nbsp;</td><td align='left' style='width:80px'>可选套账</td><td align='left'>说明</td><td>&nbsp;</td></tr>";

                        //foreach (DataRow dr in ds.Tables[0].Rows)
                        //{
                        //    string lm = dr["menu"].ToString().Trim();
                        //    //默认主页为menu_
                        //    lm = (lm == string.Empty ? mrmenu : lm);

                        //    str += "<tr align='center'>";
                        //    str += "<td style='width: 14px'> &nbsp;</td>";
                        //    str += "<td align='right'>" + (ds.Tables[0].Rows.IndexOf(dr) + 1).ToString().Trim() + "</td>";
                        //    str += "<td align='left'><a href='#' mylink=1  t=\"" + dr["tzid"].ToString().Trim() + "\" m=\"" + lm + "\" > " + dr["tzmc"].ToString().Trim() + "</a></td>";
                        //    str += "<td align='left'>" + dr["sm"].ToString().Trim() + "</td>";
                        //    str += "<td>&nbsp;</td>";
                        //    str += "</tr>";
                        //}
                        //str += "</table></td></tr></table></td></tr> </table>";
                        string tzHtml = "";
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string lm = dr["menu"].ToString().Trim();
                            //默认主页为menu_
                            lm = (lm == string.Empty ? mrmenu : lm);

                            tzHtml += "<a href=\"#\" mylink=1  t=\"" + dr["tzid"].ToString().Trim() + "\" m=\"" + lm + "\" class=\"list-group-item\" accesskey=\""+(ds.Tables[0].Rows.IndexOf(dr)+1)+"\" >";
                            tzHtml += "<h4 class=\"list -group-item-heading\">" + dr["tzmc"].ToString().Trim() + "<span class=\"label label-default\" style=\"float: right;\">" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</span></h4>";
                            tzHtml += "<p class=\"list -group-item-text\" style=\"text-align:right\">" + dr["sm"].ToString().Trim() + "</p>";
                            tzHtml += "</a>";
                        }
                        rstring[1] = string.Format(div, tzHtml); ;
                    }
                }
            }
            return rstring;

        }
    }
}
