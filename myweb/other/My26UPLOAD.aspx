<%@ Page Language="C#" AutoEventWireup="true" CodeFile="My26UPLOAD.aspx.cs"
    Inherits="Default26" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>    
</head>
<script type="text/javascript">
    function TestUp() {
        ajaxFileUpload("FileUpload1");
    }
    function ajaxFileUpload(obfile_id) {
        //准备提交处理
        $("#loading_msg").html("<img src=/images/DotAjax.gif />");
        alert($("#" + obfile_id).val());
        alert($("#" + obfile_id+"2").val());
        //开始提交
        $.ajax({
            type: "POST",
            url: "webuser/ajaxUpFile.ashx",
            data: "upfile=" + $("#" + obfile_id).val(),
            success: function (data, status) {
                //alert(data);
                var stringArray = data.split("|");
                if (stringArray[0] == "1") {
                    //stringArray[0]    成功状态(1为成功，0为失败)
                    //stringArray[1]    上传成功的文件名
                    //stringArray[2]    消息提示
                    $("#divmsg").html("<img src=/images/note_ok.gif />" + stringArray[2] + "  文件地址：" + stringArray[1]);
                    $("#filepreview").attr({ src: stringArray[1] });
                }
                else {
                    //上传出错
                    $("#divmsg").html("<img src=/images/note_error.gif />" + stringArray[2] + "");
                }

                $("#loading_msg").html("");
            },
            error: function (data, status, e) {
                alert("上传失败:" + e.toString());
            }
        });
        return false; //.NET按钮控件取消提交

    }
    </script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload12" runat="server" />
        <input type="file" id="FileUpload1" />
        <input id="Button2" type="button" value="HTML按钮Ajax提交" onclick="TestUp();" />
        <div id="divmsg">
        </div>
    </div>
    </form>
</body>
</html>
