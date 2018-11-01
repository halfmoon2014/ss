<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_lsy_prtgo.aspx.cs" Inherits="web_lsy_prtgo" %>
<%@ Import Namespace=System.Data %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/jey/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../css/jey/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/mycss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/pepper-grinder/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../javascripts/myjs/myweb.js" type="text/javascript"></script>
    <link href="../css/mycss/myweb.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
       .fy
       {
            page-break-after: always;
       }  

       .myf
       {
           font-size:18px;
       }
       .myf1
       {
           font-size:14px;
       }                              
    </style>
    <title>打印</title>
</head>
    <script type="text/javascript" language="javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;
            //window.close();
            //prnform.htext.value=prnhtml;
            //prnform.submit();
            //alert(prnhtml);
        }
    </script>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body style=" padding:0px;">
    <form id="myform" runat="server">
    
    <!--
    <input type="button" name="print" value="预览并打印" onclick="preview()" />
    -->
    <% 
        string cl="";
        int a = ds.Tables.Count;
        string zycss = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ds.Tables[0].Rows.IndexOf(dr)== ds.Tables[0].Rows.Count-1 ){
                cl="";
            }else{
                cl="fy";
            }
            if (dr["title"].ToString().Length >= 20)
            {
                zycss = "myf1";
            }
            else
            {
                zycss = "myf";
            }
            //Response.Write(dr["title"].ToString().Length);
            %>
<!--startprint-->
    <div  class="<%=cl %>" >
    <table style="  table-layout:fixed;">
    <tr style=" height:2.0cm">
    <td>
    &nbsp;
    </td>
    </tr>    
    <!--日期 -->
    <tr style=" height:0.5cm">
    <td>
    <table style=" table-layout:fixed;" ><tr><td style="width:5.3cm">&nbsp;</td><td class="myf" ><%=DateTime.Now.Year.ToString()%></td> <td style="width:1cm">&nbsp;</td> <td class='myf'><%=DateTime.Now.Month.ToString() %></td><td style="width:1cm">&nbsp;</td><td class='myf' ><%=DateTime.Now.Day.ToString()%></td><td>&nbsp;</td></tr></table>
    </td>
    </tr>
    <!--交款单位 -->
    <tr style=" height:1.1cm">
    <td>
    <table style=" table-layout:fixed;" ><tr><td style="width:4cm">&nbsp;</td><td class="myf"  ><%=dr["xmmc"].ToString()+" "+dr["zh"].ToString()+" "+dr["fh"].ToString()+" "+dr["xm"].ToString() %></td><td>&nbsp;</td></tr></table>
    </td>
    </tr>
    <!--摘要 -->
    <tr style=" height:0.8cm">
    <td>
    <table style=" table-layout:fixed;"><tr><td style="width:4cm">&nbsp;</td><td class="<%=zycss %>"><%=dr["title"].ToString() %></td><td>&nbsp;</td></tr></table>
    </td>
    </tr>

    <!--钱 -->
    <tr style=" height:0.8cm">
    <td>
    <table style=" table-layout:fixed;"><tr><td style="width:4cm">&nbsp;</td><td class="myf" ><%=ConvertMoney(Decimal.Parse(dr["value"].ToString())) %></td> <td>&nbsp;</td></tr></table>
    </td>
    </tr>
    
    <tr style=" height:0.7cm">
    <td >
    &nbsp;    
    </td>
    </tr>

    <!--小写 -->
    <tr style=" height:0.5cm">
    <td>
    <table style=" table-layout:fixed;" ><tr><td style="width:3.1cm">&nbsp;</td><td class="myf" ><%=dr["value"].ToString() %></td><td>&nbsp;</td> </tr></table>
    </td>
    </tr>
    
    <tr >
    <td >
    &nbsp;    
    </td>
    </tr>
    
    </table> 
    </div>
           
            <%
        }
    %>
<!--endprint-->    
    </form>
</body>

</html>
