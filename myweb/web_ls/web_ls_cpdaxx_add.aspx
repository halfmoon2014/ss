<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_ls_cpdaxx_add.aspx.cs" Inherits="web_ls_cpdaxx_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="新增" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="../css/bootstrap/userplatform/sticky-footer-navbar.css" rel="stylesheet" />

    <script src="../javascripts/bootstrap/ie-emulation-modes-warning.js"></script>
    <!--[if lt IE 9]>
        <script src="../javascripts/bootstrap/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="../javascripts/bootstrap/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- End of Libraries -->
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
</head>
<body>
    <div>
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
                        <div class="btn-group" role="toolbar" id="btnGroup" runat="server" aria-label="操作按钮">                      
                          <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                          <button type="button" class="btn btn-default" id="close"  aria-label="关闭" >关闭</button>                      
                        </div>
                    </form>  
                </div><!--/.nav-collapse -->
            </div>
        </nav>
        <div class="container-fluid">
            <form class="form-horizontal">
                <div class="form-group">
                    <label for="mc" class="col-md-1 control-label">名称</label>
                    <div class="col-md-12">
                        <input type="text" class="form-control" id="mc" runat="server">
                    </div>
                </div>
            </form>
        </div>
    </div>
    <input type="hidden" id="myid" runat="server" />
    <input type="hidden" id="lx" runat="server" />
    <input type="hidden" id="zt" runat="server" />
</body>
<script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
<script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
<script src="../javascripts/sweetalert/sweetalert.min.js"></script>
<script data-main="js/web_ls_cpdaxx_add.js" src="../javascripts/require.js"></script>
</html>

