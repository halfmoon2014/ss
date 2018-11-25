﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_add.aspx.cs" Inherits="web_xtsz_main_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="新增" />   
</head>
<body>
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#zdnavbar" aria-expanded="false" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#"></a>
            </div>
            <div id="zdnavbar" class="navbar-collapse collapse">  
                <form class="navbar-form navbar-left">
                    <div class="btn-group" role="toolbar" id="Div1" runat="server" aria-label="操作按钮">                      
                        <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="确定">确定(S)</button>
                        <button type="button" class="btn btn-default" id="close" aria-label="关闭">关闭</button>                    
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>

    <div class="container-fluid">
        <form class="form-horizontal">
            <div class="form-group">
                <label for="mc" class="col-md-2 control-label">名称</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="mc" runat="server">
                </div>
                <label for="lx" class="col-md-2 control-label">类型</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="lx" runat="server">
                </div>
            </div>
        </form>
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

    <input type="hidden" id="userid" runat="server" />
    <input type="hidden" id="zt" runat="server" />
    <input type="hidden" id="wid" runat="server" />

</body>
<script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="web_xtsz_main_add" data-ver="<%=GetJsVer()%>" data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"></script>
</html>
