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

        .wh {
            white-space: nowrap;
        }
    </style>
    <style type="text/css">
        .wh {
            white-space: nowrap;
        }

        table, table tr th, table tr td {
            border: 1px solid #252525;
        }

        table {
            border-collapse: collapse;
            padding: 2px;
        }

        .hiddenTD {
            border-left: 1px solid #fffafa00;
            border-right: 1px solid #fffafa00;
        }

        .hiddenTR {
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

            if (detailDT.Select("khid='" + khDR["id"] + "'", "djlxmc").Length == 0)
            {//没记录
                continue;
            }
            DataTable singDetailDT = detailDT.Select("khid='" + khDR["id"] + "'", "djlxmc,BizDate").CopyToDataTable();
    %>
    <div class="break">
        <table style="font-size: 12pt; width: 1000px">
            <tr style="text-align: center; font-weight: bold; font-size: 24pt; line-height: 30px;">
                <td colspan="8" class="hiddenTR">晋丰五金电镀挂镀对账单</td>
            </tr>
            <tr style="font-weight: bold; font-size: 18pt; line-height: 30px;">
                <td colspan="6" class="hiddenTR">客户名称:<%=khDR["khmc"].ToString()+"("+khDR["khdm"].ToString()+")" %></td>

                <td colspan="2" class="hiddenTR">单位:元</td>
            </tr>
        </table>
        <table style="font-size: 12pt; width: 1000px">
            <tr style="font-weight: bold; line-height: 30px;">
                <td style="width: 100px;" class="wh">日期</td>
                <td style="width: 70px;" class="wh">单号</td>
                <td style="text-align: center">产品型号</td>
                <td style="width: 150px;" class="wh">颜色</td>
                <td style="width: 80px; text-align: center" class="wh">重量(斤)</td>
                <td style="width: 70px; text-align: center" class="wh">斤个数</td>
                <td style="width: 70px; text-align: center" class="wh">总个数</td>
                <td style="width: 70px; text-align: center" class="wh">单价</td>
                <td style="width: 100px; text-align: center" class="wh">金额</td>
            </tr>
            <%
                foreach (DataRow dr in singDetailDT.Rows)
                {
            %>

            <tr>
                <td><%=dr["BizDate"]%></td>
                <td><%=dr["number"]%></td>
                <td style="vnd.ms-excel.numberformat:@"><%=dr["product"]%></td>
                <td><%=dr["colour"]%></td>
                <td style="text-align: right"><%=string.Format("{0:0.##}",dr["weight"])%></td>
                <td style="text-align: right"><%=(decimal.Parse(dr["count_pre_jin"].ToString())==0?"":string.Format("{0:0.###}",dr["count_pre_jin"]) ) %></td>
                <td style="text-align: right"><%=(decimal.Parse(dr["count_pre_jin"].ToString())==0?"":string.Format("{0:0.###}",dr["after_quantity"]) ) %></td>
                <td style="text-align: right"><%=(decimal.Parse(dr["price"].ToString())==0?"":string.Format("{0:0.###}",dr["price"]))%></td>
                <td style="text-align: right"><%=(decimal.Parse(dr["Amount"].ToString())==0?"":string.Format("{0:0.##}",dr["Amount"]))%></td>
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
    </div>
    <%
        }
    %>
</body>
</html>
