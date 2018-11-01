<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_lsy_cx.aspx.cs" Inherits="web_lsy_cx" %>

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

    <style>
        .lh
        {
            width: 100px;
        }
        .prted
        {
            color:Red;
        }
        .a1
        {
            width:150px;
        }
        .a2
        {
            width:150px;
        }
        .a3
        {
            width:150px;
        }
        .a4
        {
            width:150px;
        }
        .a5
        {
            width:150px;
        }
        .a6
        {
            width:150px;
        }                                        
    </style>
    <title>查询</title>
</head>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body>
    <form id="myform" runat="server">
    <table>
        <tr>
            <td>代号<input type="hidden" runat="server" id="xmhidden" /></td>
            <td id="xmselect" runat="server" style=" width:80px;">
            </td>
            <td>            
                幢号<input id="cxzh" runat="server" type="text" />
                房号<input id="cxfh" runat="server" type="text" />
                姓名<input id="cxxm" runat="server" type="text" />
                收款情况<select id="skqk" runat="server">
                <option value="all">全部</option>
                <option value="ysk">已收款</option>
                <option value="wsk">未收款</option>
                </select>
            </td>
            <td>
                <a href="javascript:void(0)" onclick="cx_click()" class="easyui-linkbutton" id="cx">
                    查询</a>
            </td>
            <td>
                <a href="javascript:void(0)" onclick="prt_click()" class="easyui-linkbutton" id="prt">
                    打印</a>
            </td>

            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div style=" height:10px; "></div>
    <table>
        <tr>
            <td>
                查询列表
            </td>
        </tr>
    </table>
    <div id="content" runat="server">
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    function cx_click() {
        $('#cx').linkbutton('disable');
        myform.submit();
    }
    function prt_click() {
        $.messager.confirm("打印提示", "确定要打印吗?", function (b) {
            if (b == true) {
                $('#prt').linkbutton('disable');
                var kids = "";
                var row = 0; //行号
                var key = 0;
                var selectcol = ""; //选择中了哪一列
                var jsonc = ""//JSON中间数据
                var oldcss = "";
                $.each($("input[prt='yes']"), function (i, n) {
                    if (n.checked == true) {
                        //alert(n);
                        row = $(n.parentNode.parentNode.parentNode).attr("row");
                        key = document.getElementById("mykey_" + row).value;
                        selectcol = $(n.parentNode.parentNode).attr("id").replace("_" + row, "");
                        jsonc += key + "/" + selectcol + "|";

                        if ($(n.parentNode).attr("class") != undefined) {
                            oldcss = $(n.parentNode).attr("class");
                        }
                        if (oldcss.indexOf("prted") >= 0) {
                            //防止已经有了样式
                            oldcss = oldcss.replace("prted", "");
                        }
                        $(n.parentNode).addClass("prted " + oldcss);

                        $(n).removeAttr("checked");
                    }
                });

                if (jsonc == "") {
                    $.messager.alert('提示信息', '没选中数据', 'info', function () {
                        $('#prt').linkbutton('enable');                        
                    });

                } else {
                    jsonc = jsonc.substring(0, jsonc.length - 1);
                    url = "web_lsy_prtgo.aspx?d=" + jsonc;
                    window.open(url);
                    //var r = openModal(url, "", "dialogWidth=800px;dialogHeight=600px;");
                    //window.showModalDialog('url', WebBrowser, 'dialogHight:500px;dialogWidth:650px;center:yes;resizeable:no;help:no;status;no');
                    $('#prt').linkbutton('enable');
                }
            }
        });

       
    }
    function xmchang() {
        document.getElementById("xmhidden").value = document.getElementById("mxid").value;
        myform.submit();
    }
   
    
</script>
</html>
