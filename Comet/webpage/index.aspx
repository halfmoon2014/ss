<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="webpage_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../javascripts/jquery/jquery-1.8.0.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>clienConnetList</title>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="list">
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function del(g,tag) {
        $.ajax({
            type: 'post',
            url:  '../webuser/longPollingDel.ashx?&g=' + g,            
            data: { "timed": new Date().getTime() },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (textStatus == "timeout") { // 请求超时
                    alert("删除超时");
                } else {
                    alert("删除失败，" + textStatus);
                }
            },
            success: function (data) {
                alert(data);
                $("." + tag).remove();
            }
        });
    }
</script>
