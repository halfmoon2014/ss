<%@ Page Language="C#" AutoEventWireup="true" CodeFile="content_menu3.aspx.cs" Inherits="content_menu3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" title="菜单内容"/>
     
</head>
<body style="background-color: transparent;">
    <div id="content_menu3_mydiv" class="container-fluid" runat="server">
    </div>
        <div id="overlay" style="background: rgb(222, 222, 222); width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 99999; opacity: 1;">
        <div class="sk-cube-grid">
            <div class="sk-cube sk-cube1"></div>
            <div class="sk-cube sk-cube2"></div>
            <div class="sk-cube sk-cube3"></div>
            <div class="sk-cube sk-cube4"></div>
            <div class="sk-cube sk-cube5"></div>
            <div class="sk-cube sk-cube6"></div>
            <div class="sk-cube sk-cube7"></div>
            <div class="sk-cube sk-cube8"></div>
            <div class="sk-cube sk-cube9"></div>
        </div>
    </div>
    <script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="content_menu3" data-ver="<%=GetJsVer()%>" data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"></script>
</body>
</html>
