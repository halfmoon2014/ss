<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_creattb.aspx.cs"
    Inherits="web_xtsz_web_xtsz_creattb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="../javascripts/jquery-1.8.0.min.js" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <ctrl:DefaultHeader  ID="sysHead" runat="server" />
</head>
<script language="javascript" type="text/javascript">
    function mypic() {
        var path, name, newpath, targetWidth, targetHeight,msg="";
        $('#btn_pic').linkbutton('disable');
        for (var i = 0; i < 10;i++ ) {
            path = document.getElementById("path_" + i).value;
            newpath = path + "/cl"
            name = document.getElementById("name_" + i).value;
            targetWidth = document.getElementById("width_" + i).value; if (targetWidth == "") {targetWidth = 0; }
            targetHeight = document.getElementById("height_" + i).value; if (targetHeight == "") { targetHeight = 0; }
            if (path.length > 0 && name.length > 0) {
                $.ajax({ type: 'post',
                    url: '../webuser/ws.asmx/pzoom',
                    async: false,
                    data: { ppath: path, pname: name, newpath: newpath, targetWidth: targetWidth, targetHeight: targetHeight, watermarkText: '', watermarkImage: '' },
                    error: function (e) {
                        msg += name + "连接失败<br>"
                    },
                    success: function (data) {
                        var r = myAjaxData(data);
                        if (r.r == 'true') {
                            msg += name + "处理成功<br>"
                        } else {
                            msg += name + "处理失败,原因:"+r.r+"<br>"
                        }
                    }
                })
            }
        }
        $.messager.alert('提示信息', msg, 'info', function () {
            $('#btn_pic').linkbutton('enable'); 
        });
        
    }

    //生成表
    function mydone() {
        var tbname = mySysDate(document.getElementById("tbname").value);
        if (tbname.toLowerCase().indexOf("tb") < 0) {            
            $.messager.alert('提示信息', '表名必须有tb字样', 'info');
            return false;
        }
        var reg = new RegExp("\r\n", "g");  
        $('#ok').linkbutton('disable');
        $.ajax({ type: 'post',
            url: '../webuser/ws.asmx/autoview',
            data: { value1: tbname },

            error: function (e) {
                $('#ok').linkbutton('enable');
                $.messager.alert('提示信息', '连接失败', 'info');
            },
            success: function (data) {
                var r = myAjaxData(data);
                if (r.r == 'true') {
                    $.messager.alert('提示信息', '处理成功', 'info');
                    $('#ok').linkbutton('enable');

                } else {
                    $.messager.alert('提示信息', '处理失败', 'info');
                    $('#ok').linkbutton('enable');
                }
            }
        })
    }
    $.extend($.fn.validatebox.defaults.rules, {
        tbshow: {
            validator: function (value, param) {
                if (value.toLowerCase().indexOf("tb") >= 0) {                    
                    return true;
                }
                else {
                    return false;
                }

            },
            message: '必须包含一个tb字样'
        }
    });
    window.onload = function () {
        var td = "";
        for (var i = 0; i < 10;i++ ) {
            td += "<tr><td><input style=\" width:400px\" type=\"text\" id=\"path_" + i + "\" /></td><td><input style=\" width:150px\" type=\"text\" id=\"name_" + i + "\" /></td><td><input style=\" width:40px\" type=\"text\" id=\"width_" + i + "\" /></td><td><input style=\" width:40px\"  type=\"text\" id=\"height_" + i + "\" /></td></tr>  "
        }
        document.getElementById("tb_pic")
        $("#tb_pic").append(td);

    }
</script>
<body>
    <div  class="easyui-accordion" >
    <div title="生成表">
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%" class="mytform" height="100%">
                        <tr align="left">
                            <td colspan="3" style="color: Red; font-size:14pt; text-align: left">
                                此功能将生成增加视图字段与生成视图,</br>
                                只针对业务上的实体表,</br>
                                [业务表名定义了_tb开头],</br>
                                表名替换顺序为tb_,_tb,tb,只替换一次                                
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width:40px" class='td' align="left">
                                表名:
                            </td>
                            <td  style="width:80px">
                                <input type="text" class="easyui-validatebox" validtype="tbshow" id="tbname" />
                            </td>

                            <td>
                                &nbsp;
                            </td>
                        </tr>

                        <tr align="right">
                            <td>
                                &nbsp;
                            </td>
                            <td   >
                                <a href="javascript:void(0)" onclick="javascript:mydone()" class="easyui-linkbutton"
                                    id="ok">处理</a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div title="生成图片缩略图">
    <table id="tb_pic">
    <tr id="tr_h"><td  >路径</td><td>图片名称</td><td >宽度</td><td>高度</td></tr>      
    </table>
    <a href="javascript:void(0)" onclick="javascript:mypic()" class="easyui-linkbutton"
        id="btn_pic">生成图片缩略图,新图片自动放在对应cl文件夹下</a>
    </div>
    </div>    
</body>
</html>
