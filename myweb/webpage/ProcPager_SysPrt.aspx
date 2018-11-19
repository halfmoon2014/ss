<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcPager_SysPrt.aspx.cs" Inherits="main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader PageType="print" ID="sysHead" runat="server" Title="打印" />
    <style media="print" type="text/css">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div class='divallclass' id="divall">
        <div class="divtotalclass">
            <div>
                <table class="title_prt">
                    <tr>
                        <td id="printTitle" runat="server">
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_title" runat="server" class="mydiv_titleclass  ">
                <table style="width: 98%; margin: 0 auto;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td width="54" valign="middle">
                            <input type="button" class="Noprint" value=" 设置 " name="Findsort" onclick='javascript:mySysPrtConfig()' />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divPager" runat="server">
                <div id="MyDivTable" class="MyDivTableClass" runat="server">
                </div>
            </div>
        </div>
    </div>
    <%   
        FM.Business.Help hp = new FM.Business.Help();        
    %>
    <%=hp.GetWait()%>

    <div id="div_SysPrtConfigSort" style=" width: 100%; height: 100%; overflow: auto; display:none;">
        <div id="SysFindSortMask" style="top: 0; left: 0; position: absolute; z-index: 1000;" class="SysFindSortMaskclass">
        </div>
        <div id='div_SysWindow' inline="true" title="设置" class="easyui-window" data-options="collapsible:false,minimizable:false,maximizable:false,onClose:function(){hideMySysPrtConfigSort();} "
            style="width: 700px; height: 300px;">
            <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">            
                <tr style=" height:35px;">
                    <td style="width: 110px">
                        每页显示行数:
                    </td>
                    <td style="width: 30px">
                        <input style="width: 30px" type="text" id="SysPrtCount" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr><td colspan='3'>&nbsp;</td></tr>
                <tr style=" height:35px;" > 
                    <td colspan='3' style=" text-align:right; " >
                        <input type="button" onclick="execSysPrtConfigt()" value="确定" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <input type="hidden" runat="server" id="wid" value="0" />
    <input type="hidden" runat="server" id="sysFindSortRow" value="" />
    <input type="hidden" runat="server" id="parm" value="" />
    <input type="hidden" id="pageSize" name="pageSize" value="" runat="server" />
    <input type="hidden" id="orderBy" name="orderBy" value="" runat="server" />
    <input type="hidden" id="currentPageIndex" name="currentPageIndex" value="" runat="server" />
</body>
</html>
<script type="text/javascript">

    $(function () {
        var loadMark = 1;
        waitOff(loadMark);
    })

    function loadInfo(loadMark) {
        var url = 'ProcPager.aspx';

        var filterRow = "";
        if (document.getElementById("sysFindSortRow") != null) {
            filterRow = document.getElementById("sysFindSortRow").value;            
        }

        var orderBy = "";
        if (document.getElementById("orderBy") != null) {
            orderBy = document.getElementById("orderBy").value;

        }

        var pageSize = "";
        if (document.getElementById("pageSize") != null) {
            pageSize = document.getElementById("pageSize").value;
        }

        var parm = document.getElementById("parm").value;
        if (parm != "") {
            parm = parm+","
        }

        pagerObj = new PagerObj({
            wid: document.getElementById("wid").value,
            loadMark: 0, //手动赋值,因为一定要有数据, 
            url: url,
            extendParams: eval("({" + parm + "filterRow:\"" + filterRow + "\",pageType:\"sysPrint\",orderBy:\"" + orderBy + "\",pageSize:\"" + pageSize + "\"})"),
            callbackFn: function () { loadInfoCallback(loadMark); }
        });

        pagerObj.Page(document.getElementById("currentPageIndex").value); 
    }

    function loadInfoCallback(loadMark) {        
        if (getContentTableId() != null) {
            var key = "table_Content_" + document.getElementById("wid").value;
           
            $("#" + key + " tr").click(function () {
                $("#" + key + " tr").removeClass("selected");
                $(this).addClass("selected");

            })
        }
        //打印的时候给分页的提示加上一个不打印
        $("[prtprtNoprint='true']", $(".pageClass")).addClass("Noprint");
        $("[prtdisappear='true']", $(".pageClass")).hide();

        if (loadMark == 1) {
            //执行外部的ONLOAD事件
            if (typeof (window_onload_b) == "function") {
                window_onload_b();
            }
        }
        if (loadMark == 0) {
            //执行外部的ONLOAD事件
            if (typeof (window_onload_b_cx) == "function") {
                window_onload_b_cx();
            }
        }
        waitOff(loadMark);

    }

    /*等待框*/
    function waitOn(fn) {

        $("#Loading").fadeTo("fast", 0.5, function () {
            if (fn) {
                fn();
            }
        });

        //$("#Loading").show();

    }
    function waitOff(loadMark) {
        $("#Loading").fadeOut("normal", function () {
            if (loadMark == 1) {
                //执行外部的ONLOAD事件
                if (typeof (window_onload_a) == "function") {
                    window_onload_a();
                }
            }
            if (loadMark == 0) {
                //执行外部的ONLOAD事件
                if (typeof (window_onload_a_cx) == "function") {
                    window_onload_a_cx();
                }
            }
        });
    }
</script>
