<%@ Page Language="C#" AutoEventWireup="true" CodeFile="v1uploaditem.aspx.cs" Inherits="uploaditem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
        <style type="text/css">
        *{ margin:0; padding:0; }
        
    </style>
    <script type="text/javascript">

        var uploadSelect = function (el) {
            if (el[0].files.length == 0) {
                alert("请先选择好文件");
                return false;
            }
            el.fadeOut("show");
            parent.uploading(document.getElementById("<%=file1.ClientID %>").value, '<%=itemID %>');
            $("#<%=frmUpload.ClientID %>").submit();
        };
        
    </script>
</head>


<body>
    <form runat="server" id="frmUpload" method="post" enctype="multipart/form-data">
        <input type="button" value="上传" onclick="uploadSelect($('#file1'));" />
        <div>
        说明：<input type="text" id="remark1" runat="server" />
        </div>
        <input type="file" runat="server" id="file1"   />
     </form>
</body>
</html>
