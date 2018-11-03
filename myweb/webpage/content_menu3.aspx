<%@ Page Language="C#" AutoEventWireup="true" CodeFile="content_menu3.aspx.cs" Inherits="content_menu3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" title="菜单内容"/>
     
</head>
<body style="background-color: transparent;">
    <div id="content_menu3_mydiv" class="container-fluid" runat="server">
    </div>
    <script data-cdn="<%=GetJsCDN()%>" data-from="content_menu3" data-ver="<%=GetJsVer()%>" data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"></script>
</body>
</html>
