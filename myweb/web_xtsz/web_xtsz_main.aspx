<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main.aspx.cs" Inherits="web_xtsz_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="页面管理" />
</head>
<body id="mainbody" runat="server" class="easyui-layout">
</body>
<script data-cdn="<%=GetJsCDN()%>" data-from="web_xtsz_main" data-ver="<%=GetJsVer()%>" data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"></script>
</html>
