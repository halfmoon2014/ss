<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_sp_cppos_prt.aspx.cs"
    Inherits="web_sp_web_sp_cppos_prt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .contcss{ border: 0px; border-collapse:collapse; width:100% }
    .skfscss{ border: 0px;  border-collapse:collapse;width:100%}
    .cont_pmcss{ display:none;}
    </style>
    <script type="text/javascript" language="javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            //window.document.body.innerHTML = bdhtml;
            window.close();
            //prnform.htext.value=prnhtml;
            //prnform.submit();
            //alert(prnhtml);
        }
    </script>
</head>
<body style="margin-left: 18px; margin-top: 0px">
    <form id="Form1" method="post" runat="server">
    <input type="button" name="print" value="预览并打印" onclick="preview()" />
    <!--startprint-->
    <table style="width: 5.5cm; background-color: White; border: 0px; border-collapse:collapse; font-size:9pt">
        <tr style="height: 20px">
            <td align="center">
                <image style=" width:60%;hegith:50px " src="posgif.gif"></image>
            </td>
        </tr>
        <tr style="height: 18px">
            <td>
                编号：<label id="Djh" runat="server">
                </label>
                &nbsp;&nbsp;&nbsp;&nbsp;销售别：<label id="Djlbmc" runat="server"></label>
            </td>
        </tr>
        <tr style="height: 18px">
            <td>
                日期：<label id="rq" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td>
                ----------------------------------
            </td>
        </tr>
        <tr>
            <td id="content" runat="server">
            </td>
        </tr>
        <tr>
            <td>
                ----------------------------------
            </td>
        </tr>
        <tr>
            <td>
                合计：<span style="width: 70px"></span><label id="hjsl" runat="server"></label>
                <span style="width: 30px"></span>￥&nbsp;
                <label id="ysk" runat="server">
                </label>
            </td>
        </tr>
        <tr>
            <td>
                大写：
                <label id="lsje_dx" runat="server">
                </label>
            </td>
        </tr>
        <tr>
            <td>
                ----------------------------------
            </td>
        </tr>
        <tr>
            <td id="skfs" runat="server">
                
            </td>
        </tr>

        <tr>
            <td>
                备注：<label id="bz" runat="server"></label>  
            </td>
        </tr>
        <tr>
            <td>
                ----------------------------------
            </td>
        </tr>
        <tr>
            <td>
                开单：<label id="zdr" runat="server"></label>
            </td>
        </tr>
        <tr style=" display:none" >
            <td>
                售后服务电话：
                <label id="lxdh" runat="server">
                </label>
            </td>
        </tr>
        <tr  >
            <td>
                <label id="sm" runat="server">
                </label>
            </td>
        </tr>
    </table>
    <!--endprint-->  
    </form>
</body>
</html>
