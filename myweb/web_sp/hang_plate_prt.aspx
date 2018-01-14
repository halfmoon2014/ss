<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hang_plate_prt.aspx.cs" Inherits="web_sp_hang_plate_prt" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css" media="print">
    
        .break
        {
            page-break-after: always;
        }
 
    </style>
</head>
<body>
    <%        
        int pageSize = 6;
        int pageTotal = ds.Tables[1].Rows.Count;
        double f = pageTotal * 1.0 / pageSize;

        int pageCount = (int)Math.Ceiling(f);

        for (int w = 0; w < pageCount; w++)
        {
            int z = w;

    %>

    <div style="width: 18cm" class="break">
        <div id="title">
            <table style="margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr align="center">
                    <td style="font-weight: bold; font-size: 10pt; width: 8%; text-decoration: underline"></td>
                    <td style="font-weight: bold; font-size: 16pt; width: 28%; text-decoration: underline" align="center">晋&nbsp;丰&nbsp;五&nbsp;金&nbsp;电&nbsp;镀</td>
                    <td style="font-weight: bold; font-size: 10pt; width: 5%; text-decoration: underline">单据号:<%=ds.Tables[0].Rows[0]["number"]%></td>
                    <td width="60" style="font-weight: bold; width: 5%; text-decoration: underline"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">电话:0595-83288176 传真:0595-83289176</td>
                    <td colspan="2">【送货单】</td>
                </tr>
            </table>

        </div>

        <div id="head">
            <table style="font-size: 9pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr height="25">
                    <td align="left" width="230">客户名称:<%=ds.Tables[0].Rows[0]["khmc"]%></td>
                    <td align="left" width="200">日期:<%=ds.Tables[0].Rows[0]["BizDate"]%></td>
                </tr>
            </table>
        </div>

        <div id="content">
           
            <table cellspacing="0" bordercolor="black" border="1" style="font-size: 9pt; border-left-color: black; border-bottom-color: black; border-top-color: black; border-collapse: collapse; border-right-color: black; margin-left: 1px; font-size: 9pt; width: 18cm">
                <tr align="center">
                    <td width="170" align="center">产品型号</td>
                    <td width="150">颜色</td>
                    <td width="80" align="center">重量(斤)</td>
                    <td width="80" align="center">斤个数</td>
                    <td width="80" align="center">单价</td>
                    <td width="60" align="center">金额</td>
                   <%-- <td width="50" align="center">其他</td>
                    <td width="60" align="center">总个数</td>
                    <td width="60" align="center">油单价</td>
                    <td width="130" align="center">油金额</td>--%>
                    <td width="160" align="center">备注</td>
                </tr>
                <%
                    decimal pageCurrentTotal = 0;
                    for (int t = w * pageSize; t <= w * pageSize + 5; t++)
                    {
                        if (t + 1 <= pageTotal)
                        {
                            DataRow dr = ds.Tables[1].Rows[t];
                            pageCurrentTotal += decimal.Parse(dr["Amount"].ToString());
                %>
                <tr>
                    <td><%=dr["product"]%></td>
                    <td><%=dr["colour"]%></td>
                    <td><%=dr["weight"]%></td>
                    <td><%=dr["count_pre_jin"]%></td>
                    <td><%=dr["price"]%></td>
                    <td><%=dr["Amount"]%></td>
              <%--      <td><%=dr["after_finish"]%></td>
                    <td><%=dr["after_quantity"]%></td>
                    <td><%=dr["after_price"]%></td>
                    <td><%=dr["after_amt"]%></td>--%>
                    <td><%=dr["remark"]%></td>
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
                <tr>
                    <td colspan="5" style="text-align:right" >合计金额:</td>
                    
                    <td colspan="2"><%=pageCurrentTotal %></td>                  
                   
                </tr>
                <tr>
                <td colspan="7" style="height:80px;">
                    <table>
                        <tr>
                            <td style="width:10px;">工件示意图</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
        </div>

        <div id="footer" >
            <table style="font-size: 9pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr height="25">
                    <td align="left" width="120">制单人:<%=ds.Tables[0].Rows[0]["createor"]%></td>
                    <td align="left" width="120">送货人:</td>
                    <td align="left" width="120">收货人:</td>
                    <td>当前<%=w+1 %>页/总<%=pageCount %>页&nbsp;  一联存根(白) 二联客户(红) 三联结算(蓝)</td>
                </tr>
            </table>
        </div>
    </div>
    <%
        }
    %>
</body>
</html>
