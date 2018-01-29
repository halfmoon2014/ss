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
        int pageSize = 12;
        int pageTotal = ds.Tables[1].Rows.Count;
        double f = pageTotal * 1.0 / pageSize;

        int pageCount = (int)Math.Ceiling(f);

        for (int w = 0; w < pageCount; w++)
        {
            int z = w;

    %>
    <div style="width: 20.5cm" class="break">
        <div id="title" style="margin-top:30px;">
            <table style="margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr align="center">
                    <td style="font-weight: bold; font-size: 10pt; width: 8%; text-decoration: underline"></td>
                    <td style="font-weight: bold; font-size: 24pt; width: 28%;" align="center">晋&nbsp;丰&nbsp;五&nbsp;金&nbsp;电&nbsp;镀</td>
                    <td style="font-weight: bold; font-size: 12pt; width: 5%; ">单据号:<%=ds.Tables[0].Rows[0]["number"]%></td>
                    <td width="60" style="font-weight: bold; width: 5%; text-decoration: underline"></td>
                </tr>
                <tr style="padding-top:10px">
                    <td colspan="2" style="text-align: center;font-size:18px;">电话:0595-83288176 传真:0595-83289176</td>
                    <td colspan="2" style="text-align: center; font-size: 18px;">【送货单】</td>
                </tr>
            </table>
        </div>

        <div id="head" style="margin-top:10px;">
            <table style="font-size: 14pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0" >
                <tr>
                  
                    <td align="left" width="600">客户名称:<%=ds.Tables[0].Rows[0]["khmc"]%></td>
                    <td align="left" width="200">日期:<%=ds.Tables[0].Rows[0]["BizDate"]%></td>
                </tr>
            </table>
        </div>

        <div id="content" style="margin-top:10px;">
            <table cellspacing="0" bordercolor="black" border="1" style="font-size: 12pt; border-left-color: black; border-bottom-color: black; border-top-color: black; border-collapse: collapse; border-right-color: black; margin-left: 1px; ">
                <tr align="center" style="font-weight: bold; line-height: 30px;">
                    <td width="200" align="center">产品型号</td>
                    <td width="150">颜色</td>
                    <td width="80" align="center">重量(斤)</td>
                    <td width="80" align="center">斤个数</td>
                    <td width="80" align="center">单价</td>
                    <td width="100" align="center">金额</td>                  
                    <td width="160" align="center">备注</td>
                </tr>
                <%
                    decimal pageCurrentTotal = 0;
                    for (int t = w * pageSize; t <= w * pageSize + pageSize-1; t++)
                    {
                        if (t + 1 <= pageTotal)
                        {
                            DataRow dr = ds.Tables[1].Rows[t];
                            pageCurrentTotal += decimal.Parse(dr["Amount"].ToString());
                %>
                <tr>
                    <td><%=dr["product"]%></td>
                    <td><%=dr["colour"]%></td>
                    <td><%=string.Format("{0:#.##}",dr["weight"])%></td>
                    <td><%=(decimal.Parse(dr["count_pre_jin"].ToString())==0?"":string.Format("{0:0.###}",dr["count_pre_jin"]) ) %></td>
                    <td><%=(decimal.Parse(dr["price"].ToString())==0?"":string.Format("{0:0.###}",dr["price"]))%></td>
                    <td style="text-align:right"><%=(decimal.Parse(dr["Amount"].ToString())==0?"":string.Format("{0:0.###}",dr["Amount"]))%></td>              
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
                <tr style="line-height:30px; font-weight:bold;">
                    <td colspan="5" style="text-align:right" >合计金额:</td>
                    
                    <td colspan="2"><%=string.Format("{0:0.###}",pageCurrentTotal) %></td>                  
                   
                </tr>
 <%--               <tr>
                <td colspan="7" style="height:80px;">
                    <table>
                        <tr>
                            <td style="width:10px;">工件示意图</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                </tr>--%>
            </table>
        </div>

        <div id="footer" style="margin-top:10px;" >
            <table style="font-size: 11pt; margin-left: 1px;" cellspacing="0" cellpadding="0" border="0">
                <tr height="25">
                    <td align="left" width="120">制单人:<%=ds.Tables[0].Rows[0]["createor"]%></td>
                    <td align="left" width="120">送货人:</td>
                    <td align="left" width="120">收货人:</td>
                    <td>当前<%=w+1 %>页/总<%=pageCount %>页&nbsp;  一联存根(白) 二联客户(红) 三联结算(黄)</td>
                </tr>
            </table>
        </div>
    </div>
    <%
        }
    %>
</body>
</html>
