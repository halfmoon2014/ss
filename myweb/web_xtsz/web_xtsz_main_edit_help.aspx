<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_help.aspx.cs" Inherits="web_xtsz_main_edit_help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />   
</head>
<body runat="server">
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#xtsznavbar" aria-expanded="false" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#"></a>
            </div>
            <div id="xtsznavbar" class="navbar-collapse collapse">        
                <form class="navbar-form navbar-left">
                    <div class="btn-group" role="toolbar" id="btnGroup" runat="server" aria-label="操作按钮">
                      <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                    </div>
                </form>
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <div class="form-group">
            <label for="fwsql">说明文档</label>
            <textarea class="form-control" rows="30" cols="120" runat="server" id="tbhelp"></textarea>
        </div>
    </div>
    <input type="hidden" id="wid" runat="server" />
</body>
<script data-cdn="<%=GetJsCDN()%>" data-from="web_xtsz_main_edit_help" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>


