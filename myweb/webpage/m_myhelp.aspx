<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_myhelp.aspx.cs" Inherits="webpage_m_myhelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" title="帮助文档" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />    
    <link href="../css/bootstrap/userplatform/sticky-footer-navbar.css" rel="stylesheet" />
    <!-- End of Libraries -->
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
</head>
<body>
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#jsnavbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#"></a>
            </div>
            <div id="jsnavbar" class="navbar-collapse collapse">
                <form class="navbar-form navbar-left">
                    <div class="btn-group" role="toolbar" id="btnGroup" runat="server" aria-label="操作按钮">
                        <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存">保存(S)</button>
                        <button type="button" class="btn btn-default" id="esc" aria-label="退出">退出</button>                        
                    </div>
                </form>
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
    
        <div class="form-group">
            <label for="helpText">manual</label>
            <textarea class="form-control" rows="2" runat="server" id="helpText"></textarea>
        </div>
    </div>
    <!-- /container -->
    <input type="hidden" id="myid" runat="server" />    
    <script data-cdn="<%=GetJsCDN()%>" data-from="userhelp" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</body>
</html>
