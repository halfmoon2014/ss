<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hang_plate_prt.aspx.cs" Inherits="web_sp_hang_plate_prt" %>
<%@ Import Namespace=System.Data %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <div style="width:18cm">
        <div id="title" runat="server">
            <table style=" margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr align="center">
                    <td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; WIDTH: 8%; TEXT-DECORATION: underline"></td>
                    <td style="FONT-WEIGHT: bold; FONT-SIZE: 16pt; WIDTH: 28%; TEXT-DECORATION: underline" align="center">挂&nbsp;镀&nbsp;车&nbsp;间</td>
                    <td style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; WIDTH: 5%; TEXT-DECORATION: underline">打印</td>
                    <td width="60" style="FONT-WEIGHT: bold; WIDTH: 5%; TEXT-DECORATION: underline"></td>
                </tr>
            </table>

        </div>
        <div id="head" runat="server">

            <table style="FONT-SIZE: 9pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr height="25">
                    <td align="left" width="180">单据号:<%=ds.Tables[0].Rows[0]["number"]%></td>
                    <td align="left" width="200">日期:<%=ds.Tables[0].Rows[0]["BizDate"]%></td>
                    <td align="left" width="130">客户:<%=ds.Tables[0].Rows[0]["khmc"]%></td>

                </tr>
                <tr height="25">
                    <td align="left" colspan="2" width="110">备注:<%=ds.Tables[0].Rows[0]["remark"]%></td>
                    <td align="left" width="200">制单人:<%=ds.Tables[0].Rows[0]["createor"]%></td>
                </tr>
            </table>
        </div>
        <div id="content" runat="server">
            <table cellspacing="0" bordercolor="black" border="1" style="FONT-SIZE: 9pt; BORDER-LEFT-COLOR: black; BORDER-BOTTOM-COLOR: black; BORDER-TOP-COLOR: black; BORDER-COLLAPSE: collapse; BORDER-RIGHT-COLOR: black; margin-left: 1px; FONT-SIZE: 9pt; WIDTH: 18cm">
                <tr align="center">
                    <td width="70" align="center">产品型号</td>
                    <td width="150">颜色</td>
                    <td width="80" align="center">重量(斤)</td>                  
                    <td width="80" align="center">斤个数</td>
                    <td width="30" align="center">单价</td>                  
                    <td width="60" align="center">金额</td>                  
                    <td width="50" align="center">其他</td>
                    <td width="60" align="center">总个数</td>
                    <td width="60" align="center">油单价</td>
                    <td width="130" align="center">油金额</td>
                    <td width="60" align="center">备注</td>                    
                </tr>
                <%
                foreach(DataRow dr in ds.Tables[1].Rows)
                {
                %>
                <tr>
                    <td><%=dr["product"]%></td>
                    <td><%=dr["colour"]%></td>
                    <td><%=dr["weight"]%></td>
                    <td><%=dr["count_pre_jin"]%></td>
                    <td><%=dr["price"]%></td>
                    <td><%=dr["Amount"]%></td>
                    <td><%=dr["after_finish"]%></td>
                    <td><%=dr["after_quantity"]%></td>
                    <td><%=dr["after_price"]%></td>
                    <td><%=dr["after_amt"]%></td>
                    <td><%=dr["remark"]%></td>
                </tr>
                <%
                }
                %>
            </table>
        </div>
        <div id="footer" runat="server">

        </div>
    </div>
</body>
</html>
