<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_ls_xsprt.aspx.cs" Inherits="web_ls_web_ls_xsprt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售单打印</title>
    <style type="text/css">
        .body
        {
            margin: 0px;
            width: 14.5cm;
        }
        .mxzbbody
        {
            font-size: 9pt;
            border: "0px";
            width: 100%;
        }
        .zbbody
        {
            margin-left: 0px;
            border: "0px";
            width: 100%;
        }
        .mxbody
        {
            font-size: 9pt;
            border-collapse: collapse;
            width: 100%;
        }
        .mxbody td
        {
            border: 1px;
            border: 1px solid gray;
        }
    </style>
    <script language="javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.close();
            //prnform.htext.value=prnhtml;
            //prnform.submit();
            //alert(prnhtml);
        }
    </script>
</head>
<body class="body">
    <%
        int id = int.Parse(Request.QueryString["id"].ToString().Trim());
        int zsl = 0; Decimal zje = 0;
        
        string str = "select convert(varchar(10),a.rq,120) as rq,a.*,b.khmc,b.dz,b.lxdh from _V_ls_cpxsb a inner join _v_ls_xskhda b on a.xskhid=b.id  where a.id=" + id + ";select *,b.dw,b.fzkh,b.kh,case a.cxp when 0 then a.dj*zb.zk*0.01 else a.dj end as zhdj,a.sl*case a.cxp when 0 then a.dj*zb.zk*0.01 else a.dj end as zhje from _V_ls_cpxsmxb a inner join _v_ls_cpda b on a.cpid=b.id inner join _V_ls_cpxsb zb on zb.id=a.id  where a.id=" + id;
        FM.Business.Help hp = new FM.Business.Help();
        System.Data.DataSet ds = hp.ExecuteDataset(str);
        System.Data.DataTable dt_zb = ds.Tables[0];
        System.Data.DataTable dt_mx = ds.Tables[1];
    %>
    <form>
    <div align="left">
    <input type="button" name="print" value="预览并打印" onclick="preview()">
    </div>
    <!--startprint-->
    <div>
        <table class="zbbody">
            <tr align="center">
                <td style="font-weight: bold; font-size: 10pt; width: 10%;">
                    &nbsp;
                </td>
                <td style="font-weight: bold; font-size: 20pt; width: 80%; text-decoration: underline;
                    text-align: center">
                    濠&nbsp;江&nbsp;九&nbsp;牧&nbsp;销&nbsp;售&nbsp;单
                </td>
                <td style="font-weight: bold; font-size: 10pt; width: 10%;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="mxzbbody">
            <tr height="25">
                <td align="left" width="200">
                    单据号:<%=dt_zb.Rows[0]["djh"]%>
                </td>

                <td align="left" width="220">
                    折扣:<%=dt_zb.Rows[0]["zk"]%>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" width="100">
                    日期:<%=dt_zb.Rows[0]["rq"]%>
                </td>
            </tr>
        </table>
        <table class="mxzbbody">
            <tr height="20">
                <td align="left" width="200">
                    客户:<%=dt_zb.Rows[0]["khmc"]%>
                </td>

                <td align="left" width="220">
                    地址:<%=dt_zb.Rows[0]["dz"]%>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" width="150">
                    电话:<%=dt_zb.Rows[0]["lxdh"]%>
                </td>

            </tr>
        </table>
        <table class="mxbody">
            <tr>
            <td width="30" align="center">序号</td>
                <td width="70" align="center">
                    SAP编码
                </td>

                <td align="center">
                    品名
                </td>
                <td width="30" align="center">
                    单位
                </td>
                <td width="45" align="center">
                    数量
                </td>
                <td width="60" align="center">
                    市场价
                </td>
                <td width="60" align="center">
                    折后价
                </td>
                <td width="60" align="center">
                    折后金额
                </td>
            </tr>
            <%
                foreach (System.Data.DataRow dr in dt_mx.Rows)
                {
                    zsl += int.Parse(dr["sl"].ToString());
                    zje += Decimal.Parse(dr["zhje"].ToString());
            %>
            <tr>
                <td width="20" align="center">
                    <%=(dr.Table.Rows.IndexOf(dr)+1) %>
                </td>
                <td width="70" align="center">
                    <%=dr["kh"].ToString() %>
                </td>
                <td align="center">
                    <%=dr["pm"].ToString() %>
                </td>
                <td width="30" align="center">
                    <%=dr["dw"].ToString() %>
                </td>
                <td width="45" align="center">
                    <%=dr["sl"].ToString() %>
                </td>
                <td width="60" align="center">
                    <%=dr["dj"].ToString() %>
                </td>
                <td width="60" align="center">
                    <%=dr["zhdj"].ToString() %>
                </td>
                <td width="60" align="center">
                    <%=dr["zhje"].ToString() %>
                </td>
            </tr>
            <%
                }
            %>
            <!--合计 -->
            <tr>
                <td colspan="4" align="center">
                    合计:
                </td>
                <td width="45" align="center">
                    <%=zsl %>
                </td>
                <td width="60" align="center">
                    &nbsp;
                </td>
                <td width="60" align="center">
                    &nbsp;
                </td>
                <td width="60" align="center">
                    <%=zje %>
                </td>
            </tr>
        </table>
        <table class="mxzbbody">
            <tr>
                <td align="left" width="210">
                    打印时间:<%=DateTime.Now.ToString() %>
                </td>
                <td>
                    &nbsp;
                </td>

            </tr>
            <tr>
                <td align="left" width="110" colspan="3">
                    备注:<%=dt_zb.Rows[0]["bz"]%>
                </td>
            </tr>

        </table>
        <table  class="mxzbbody">
            <tr>
                <td rowspan="4">
                服<br />务<br />
                    承<br />
                    诺
                </td>
                <td colspan=2>
                1.非常感谢您选择了九牧产品。我们承诺为您提供优质的服务，如果您在使用过程中出现故障，请及时与下列单位联系，我们将竭诚为您服务! 
                </td>

            </tr>
            <tr>
                <td colspan=2>
                2.产品保修期及免费维修规定请参照产品包装盒中的《保证书》。
                </td>
            </tr>
            <tr>
                <td colspan=2>
                地址：石狮市濠江路濠江大厦115-116号（光大银行对面）
                </td>
            </tr>
            <tr>
                <td colspan=2>
               服务电话：0595-83139595

                </td>
            </tr>        
        </table>
        <br />
        <table class="mxzbbody">
            <tr>
                <td align="left" width="210">
                    客户签收:
                </td>
                <td align="left" width="210">
                    送货员:
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" width="110">
                    制单:<%=dt_zb.Rows[0]["zdr"]%>
                </td>
            </tr>

        </table>
    </div>
    <!--endprint-->
    </form>
</body>
</html>
