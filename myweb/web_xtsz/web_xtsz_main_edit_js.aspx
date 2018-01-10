<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_js.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_js" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <ctrl:DefaultHeader  id="sysHead" runat="server" />
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
</head>
<body runat="server">
    <form runat="server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <a href="javascript:void(0)" class="easyui-linkbutton" accessKey="S" id="ok">保存(S)</a>
                        </td>
                        <td><a href="javascript:void(0)" class="easyui-linkbutton" id="showtitp">提示</a></td>
                        <td id="fb" runat="server"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <ul>
                    <li>js语句</li>
                    <li>
                        <textarea id="tbjs" rows="30" cols="120" runat="server" ></textarea></li>
                </ul>
            </td>
        </tr>
    </table>
    <div id="w" class="easyui-window" data-options="closed:true,title:'titps'"
        style="width: 800px; height: 600px; padding: 5px;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center',border:false" style="padding: 10px; background: #fff;
                border: 1px solid #ccc;">
                <textarea style=" width:95%;height:95%">
                    //打开模态窗口
                    openModal(url, "", "", function (r) {                      
                        if (r == "ok") { 
                            //查询
                            myCheckSessionQuery(); 
                        }
                    });
                    //关窗模态窗口
                    myWindowClose("ok"); 
                    //树加载成功后执行的函数                    
                    onLoadSuccessTree(node, data, treeId);
                    //单击树
                    onClickTree(node,treeId)
                </textarea>
                <br />
                <br />                
            </div>           
        </div>
    </div>
    <input type="hidden" id="wid" runat="server" />
    </form>
</body>
    <script src="../javascripts/sweetalert/sweetalert.min.js"></script>
</html>
<script>

    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#fb").bind("click", function () { fb_click(); });
        $("#showtitp").bind("click", function () { showtitp_click(); });
    });
    function salert(title, text, type, fn) {
        swal({
            title: title,
            text: text,
            type: type,
        }, fn);
    }
    //显示提示
    function showtitp_click() {
        $('#w').window('open');
    }
    function ok_click() {        
        $('#ok').linkbutton('disable');
        var js = mySysDate(document.getElementById("tbjs").value);
        var wid = mySysDate(document.getElementById("wid").value);

        $.ajax({ type: 'post',
            url: '../webuser/ws.asmx/sjy_upjs',
            data: { wid: wid, js: js },
            error: function (e) {
                salert('提示信息', '连接失败!', 'info', function () {
                    $('#ok').linkbutton('enable');
                });
            },
            success: function (data) {
                var r = myAjaxData(data);
                if (r.r == 'true') {
                    salert('提示信息', '保存成功!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                } else {
                    salert('提示信息', '保存失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                }
            }
        })
    }

</script>
