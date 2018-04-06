<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hang_plate_wj_prt.aspx.cs" Inherits="web_sp_hang_plate_prt" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css" media="print">
        .break {
            page-break-after: always;
        }
    </style>
</head>
<body>
    <%        
        int pageSize = 12;
        int pageTotal = ds.Tables[1].Rows.Count;
        double f = pageTotal * 1.0 / pageSize;

        int pageCount = (int)Math.Ceiling(f);

        for (int w = 0; w < pageCount; w++)
        {
            int z = w;

    %>
    <div style="width: 20cm" class="break">
        <table>
            <tr>
                <td>
                    <div id="title" style="margin-top: 10px;">
                        <table style="margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                            <tr align="center">
                                <td rowspan="3" style="width: 10%">
                                    <img src="hongjie.jpg" style="width:80px;" />
                                </td>
                                <td colspan="2" style="font-size: 24pt; width: 28%;" align="center">鸿&nbsp;杰&nbsp;五&nbsp;金&nbsp;厂&nbsp;<送货单></td>
                                <td colspan="2" style="font-size: 12pt; width: 5%;">№<%=ds.Tables[0].Rows[0]["number"]%></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="line-height:2px;border-color: #000000; text-align: center; border-bottom-style:solid; border-top-style:solid;border-top-width: medium; border-bottom-width: medium">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; font-size: 13px;">地址:石狮市宝盖镇仑后村一区128号 电话:0595-88982766 传真:0595-88782966 QQ:2796049693</td>
                            </tr>
                        </table>
                    </div>

                    <div id="head" style="margin-top: 10px;">
                        <table style="font-size: 14pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                            <tr>

                                <td align="left" width="600">收货地址:<%=ds.Tables[0].Rows[0]["dz"]%></td>
                                <td align="left" width="200">日期:<%=ds.Tables[0].Rows[0]["BizDate"]%></td>
                            </tr>
                        </table>
                    </div>

                    <div id="content" style="margin-top: 10px;">
                        <table cellspacing="0" bordercolor="black" border="1" style="font-size: 12pt; border-left-color: black; border-bottom-color: black; border-top-color: black; border-collapse: collapse; border-right-color: black; margin-left: 1px;">
                            <tr align="center" style="line-height: 30px;">
                                <td width="80" align="center">货号</td>
                                <td width="300" align="center">名称及规格</td>
                                <td width="120">颜色</td>
                                <td width="80" align="center">单位</td>
                                <td width="80" align="center">数量</td>
                                <td width="80" align="center">单价</td>
                                <td width="100" align="center">金额</td>
                            </tr>
                            <%
                                decimal pageCurrentTotal = 0;
                                for (int t = w * pageSize; t <= w * pageSize + pageSize - 1; t++)
                                {
                                    if (t + 1 <= pageTotal)
                                    {
                                        DataRow dr = ds.Tables[1].Rows[t];
                                        pageCurrentTotal += decimal.Parse(dr["Amount"].ToString());
                            %>
                            <tr>
                                <td><%=dr["product"]%></td>
                                <td><%=dr["name"]%></td>
                                <td><%=dr["colour"]%></td>
                                <td><%=dr["unit"]%></td>
                                <td><%=string.Format("{0:0.##}",dr["weight"])%></td>
                                <td><%=(decimal.Parse(dr["price"].ToString())==0?"":string.Format("{0:0.###}",dr["price"]))%></td>
                                <td style="text-align: right"><%=(decimal.Parse(dr["Amount"].ToString())==0?"":string.Format("{0:0.###}",dr["Amount"]))%></td>
                            </tr>
                            <%
                                }
                                else
                                {
                            %>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>

                            </tr>
                            <%
                                    }

                                }

                            %>
                            <tr style="line-height: 30px;">
                                <td style="text-align: right">合计金额</td>
                                <td colspan="5" style="text-align: right"><%=CmycurD(string.Format("{0:0.###}",pageCurrentTotal)," ") %></td>

                                <td colspan="2"></td>

                            </tr>
                            <tr style="line-height: 30px;">
                                <td colspan="8" style="text-align:left">声明：客户收到产品后，如有质量问题，请于七天内向本公司反应，逾期怒不负责</td>

                            </tr>
                        </table>
                    </div>

                    <div id="footer" style="margin-top: 10px;">
                        <table style="font-size: 11pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                            <tr height="25">
                                <td align="left" width="70">收货单位及经手人</td>
                                <td align="left" width="60">（盖章）</td>
                                <td align="left" width="120"></td>
                                <td align="left" width="70">收货单位及经手人</td>
                                <td align="left" width="60">（盖章）</td>
                                <td align="left" width="120"></td>
                                <td>当前<%=w+1 %>页/总<%=pageCount %>页&nbsp; </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="font-size:10pt;">
                    一<br />
                    联<br />
                    :<br />
                    存<br />
                    根<br />
                    ︵<br />
                    白<br />
                    ︶<br />
                    二<br />
                    联<br />
                    :<br />
                    客<br />
                    户<br />
                    ︵<br />
                    红<br />
                    ︶<br />
                    三<br />
                    联<br />
                    :<br />
                    财<br />
                    务<br />
                    ︵<br />
                    绿<br />
                    ︶<br />
                </td>
            </tr>
        </table>

    </div>
    <%
        }
    %>
</body>
</html>
