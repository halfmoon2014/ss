<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hang_plate_dz.aspx.cs" Inherits="web_sp_hang_plate_prt" %>

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
        .hiddenTD{
            border-left: 1px solid #fffafa00; border-right: 1px solid #fffafa00;
        }
        .hiddenTR{
            border: 1px solid #fffafa00;
        }
    </style>
    <style type="text/css">
        table, table tr th, table tr td {
            border: 1px solid #252525;
        }

        table {          
            border-collapse: collapse;
            padding: 2px;
        }
                .hiddenTD{
            border-left: 1px solid #fffafa00; border-right: 1px solid #fffafa00;
        }
        .hiddenTR{
            border: 1px solid #fffafa00;
        }
    </style>
</head>
<body>
    <%        
        DataTable khDT = ds.Tables[1];
        DataTable detailDT = ds.Tables[0];
        foreach (DataRow khDR in khDT.Rows)
        {
            decimal totalAmount = 0;
            if(detailDT.Select("khmc='" + khDR["khmc"] + "'", "djlxmc").Length == 0)
            {
                continue;
            }
            DataTable singDetailDT = detailDT.Select("khmc='" + khDR["khmc"] + "'", "djlxmc").CopyToDataTable();
    %>
    <table style="font-size: 12pt;">
        <tr align="center" style="font-weight: bold; font-size: 24pt; line-height: 30px;">
            <td colspan="8" class="hiddenTR">晋丰五金电镀挂镀对账单</td>
        </tr>
        <tr style="font-weight: bold; font-size: 18pt; line-height: 30px;">
            <td colspan="4" class="hiddenTD">客户名称:<%=khDR["khmc"] %></td>
            <td class="hiddenTD">&nbsp;</td>
            <td class="hiddenTD">&nbsp;</td>
            <td class="hiddenTD">&nbsp;</td>
            <td class="hiddenTD">单位:元</td>
        </tr>
        <tr style="font-weight: bold; line-height: 30px;">
            <td style="width: 100px;">日期</td>
            <td style="width: 100px;">单号</td>
            <td width="200" align="center">产品型号</td>
            <td width="150">颜色</td>
            <td width="80" align="center">重量(斤)</td>
            <td width="80" align="center">斤个数</td>
            <td width="80" align="center">单价</td>
            <td width="100" align="center">金额</td>
        </tr>
        <%
            foreach (DataRow dr in singDetailDT.Rows)
            {
        %>

        <tr>
            <td><%=dr["BizDate"]%></td>
            <td><%=dr["number"]%></td>
            <td><%=dr["product"]%></td>
            <td><%=dr["colour"]%></td>
            <td><%=string.Format("{0:#.##}",dr["weight"])%></td>
            <td><%=(decimal.Parse(dr["count_pre_jin"].ToString())==0?"":string.Format("{0:0.###}",dr["count_pre_jin"]) ) %></td>
            <td><%=(decimal.Parse(dr["price"].ToString())==0?"":string.Format("{0:0.###}",dr["price"]))%></td>
            <td style="text-align: right"><%=(decimal.Parse(dr["Amount"].ToString())==0?"":string.Format("{0:0.###}",dr["Amount"]))%></td>
        </tr>
        <%
                if (singDetailDT.Rows.IndexOf(dr) + 1 == singDetailDT.Rows.Count)
                {//最后一行
                 //输出合计
                    totalAmount += decimal.Parse(dr["Amount"].ToString());
                    Response.Write(WriteKHDJTotal(dr["djlxmc"].ToString(), totalAmount));
                    Response.Write(WriteKHTotal(decimal.Parse(singDetailDT.Compute("sum(Amount)", "").ToString())));
                }
                else
                {
                    if (string.Compare(dr["djlxmc"].ToString(), singDetailDT.Rows[singDetailDT.Rows.IndexOf(dr) + 1]["djlxmc"].ToString(), true) != 0)
                    {//当前这么与下一行不是相同的类别
                     //输出当前类别合计
                        totalAmount += decimal.Parse(dr["Amount"].ToString());
                        Response.Write(WriteKHDJTotal(dr["djlxmc"].ToString(), totalAmount));
                        totalAmount = 0;
                    }
                    else
                    {
                        totalAmount += decimal.Parse(dr["Amount"].ToString());
                    }
                }
            }
        %>
    </table>
    <%
      }
    %>
</body>
</html>
