<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->    
    <link href="../css/jey/ui-pepper-grinder/easyui.css" rel="stylesheet" />
    <link href="../css/jey/icon.css" rel="stylesheet" />
    <!-- End of Libraries -->
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
    <link href="../css/web_xtsz/web_xtsz_main_edit.css" rel="stylesheet" />
</head>
<body id="editbody" runat="server" class="easyui-layout">
</body>
<input type="hidden" id="wid" runat="server" />
<script data-cdn="<%=GetJsCDN()%>" data-from="web_xtsz_main_edit" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>

