<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_sp_cppos_js.aspx.cs"
    Inherits="web_sp_web_sp_cppos_js" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="收款" />
    <link href="../css/mycss/myweb.css" rel="stylesheet" type="text/css" />
    <style>
        .info td
        {
            font-size: 20px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    收款方式
                </td>
                <td>
                    <input type="text" id="fkfs" onchange="mydmxz('fkfs')" />
                </td>
            </tr>
            <tr>
                <td>
                    方式名称
                </td>
                <td id="fkfsmc">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    金额
                </td>
                <td>
                    <input type="text" id="fkje" onchange="mydmxz('fkje')" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    收款记录
                </td>
            </tr>
            <tr>
                <td>
                    <table id="fkjl">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div runat="server">
        <table>
            <tr>
                <td>
                    快捷键
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div_fkfs" runat="server">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="info">
        <table>
            <tr>
                <td style="width: 100px">
                    应收金额
                </td>
                <td id="zje" runat="server" style="width: 80px; text-align: right">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    已收金额
                </td>
                <td id="ysje" runat="server" style="width: 80px; text-align: right">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    找零
                </td>
                <td id="zl" runat="server" style="width: 80px; text-align: right">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">确定(+)</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="esc">取消(-)</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script language="javascript">
        /*
        * 用下面的代码就不会发生悲剧了
        * 最终结论
        * E(e).stop(); 阻止时间冒泡
        * E(e).prevent();阻止时间默认行为
        */
        var E = function (e) {
            e = (browser.versions.trident) || e;
            return {
                stop: function () {
                    if (e && e.stopPropagation) e.stopPropagation();
                    else e.cancelBubble = true
                },
                prevent: function () {
                    if (e && e.preventDefault) e.preventDefault();
                    else e.returnValue = false
                }
            }
        }
        $(function () {
            $("#ok").bind("click", function () { ok_click(); });
            $("#esc").bind("click", function () { esc_click(); });
            $("#fkfs").focus();
            $(document).keypress(function (e) {
                var key = "";
                if (browser.versions.trident) {
                    key = e.keyCode;
                } else if (e.which) {
                    key = e.which;
                }


                if (key == 43) {
                    ok_click();
                    E(e).stop();
                    E(e).prevent();
                } else if (key == 45) {
                    esc_click();
                    E(e).stop();
                    E(e).prevent();
                }

            });
            /*
            $("#fkfs").keydown(function (e) {
            var key = "";
            if (window.event) {
            key = e.keyCode;
            } else if (e.which) {
            key = e.which;
            }            
            alert(key)
            return false;
            });

            $("#fkje").keydown(function (e) {
            var key = "";
            if (window.event) {
            key = e.keyCode;
            } else if (e.which) {
            key = e.which;
            }            
            alert(key)
            return false;
            });*/

        });
        function ok_click() {
             
            var zl = $.trim(document.getElementById("zl").innerHTML);            
            var r = "";
            if (zl >= 0) {
                $.each($("tr", $("#fkjl")), function (i, n) {
                    var row = $(n).attr("row");
                    r += "" + row + ":{id:'" + $(n).attr("key") + "',je:'" + document.getElementById("fkje_" + row).innerHTML + "'},";
                });
                if (r == "") {
                    window.returnValue = "";
                } else {
                    window.returnValue = "{" + r.substring(0, r.length - 1) + "}";
                }
                window.close();
            } else {
                $.messager.alert('提示信息', '收款不足!', 'info');
            }
        }
        function esc_click() {
            window.close();
        }
        function mydmxz(bs) {
            tobj = document.getElementById(bs);
            if (bs == "fkfs") {
                //如果是选择了收款方式
                var p = $("td[pkey='pkey_" + tobj.value + "']");
                if (p.length == 1) {
                    $.each(p, function (i, n) {
                        document.getElementById("fkfsmc").innerHTML = $(n).text();
                        if ($.trim(document.getElementById("fkje").value) != "" && $.trim(document.getElementById("fkje").value) != "0") {
                            //付款方式和金额都正确
                            gojl();
                        } else {
                            $("#fkje").focus();
                        }
                    });
                } else {
                    document.getElementById("fkfsmc").innerHTML = "";
                    document.getElementById("fkfs").value = "";
                }
            } else if (bs == "fkje") {
                //如果选择的金额
                if (document.getElementById("fkfsmc").innerHTML != "" && $.trim(document.getElementById("fkje").value) != "" && $.trim(document.getElementById("fkje").value) != "0") {
                    gojl();
                } else {
                    if ($.trim(document.getElementById("fkje").value) == "" || $.trim(document.getElementById("fkje").value) == "0") {
                    } else {
                        $("#fkfs").focus();
                    }
                }
            }
        }
        //根据当前选择的收款方式和金额,生成收款记录
        function gojl() {
            var fkfs = $.trim(document.getElementById("fkfs").value);
            var fkje = $.trim(document.getElementById("fkje").value);
            var key = "";
            var fkfsmc = document.getElementById("fkfsmc").innerHTML;
            var row = getRowCount();
            if (fkfs != "" && fkje != "" && fkje != "0") {
                var p = $("td[pkey='pkey_" + fkfs + "']");
                if (p.length == 1) {
                    $.each(p, function (i, n) {
                        key = $(n).attr("key");
                    });
                }
                var tr = "<tr row='" + row + "' key='" + key + "' ><td>" + fkfsmc + "</td><td id='fkje_" + row + "'>" + fkje + "</td></tr>";
                $("#fkjl").append(tr);
                clear1();
                CountZl();
            }
        }
        function clear1() {
            document.getElementById("fkfsmc").innerHTML = "";
            document.getElementById("fkfs").value = "";
            document.getElementById("fkje").value = "";
            $("#fkfs").focus();
        }
        //计算总行数
        function getRowCount() {
            var key = "fkjl";
            var tab = document.getElementById(key);
            //表格行数
            return tab.rows.length;
        }
        //计算找零
        function CountZl() {
            var t = getRowCount();
            var ysje = 0;
            if (t > 0) {
                for (var i = 0; i < t; i++) {
                    ysje += Number(document.getElementById("fkje_" + i).innerHTML);
                }
            }
            document.getElementById("ysje").innerHTML = ysje;
            document.getElementById("zl").innerHTML =ysje- Number(document.getElementById("zje").innerHTML) ;
        }

    </script>
</body>
</html>
