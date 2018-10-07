<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_add.aspx.cs" Inherits="web_xtsz_web_xtsz_main_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="新增" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="../css/bootstrap/userplatform/sticky-footer-navbar.css" rel="stylesheet" />
    <!-- End of Libraries -->
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
</head>
<body>
    <div class="container-fluid">
        <form class="form-horizontal">
            <div class="form-group">
                <label for="mc" class="col-md-2 control-label">名称</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="mc" runat="server" >
                </div>
                <label for="lx" class="col-md-2 control-label">类型</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="lx" runat="server">
                </div>           
            </div>
        </form>
        <div class="btn-group" role="toolbar" id="btnGroup" runat="server" aria-label="操作按钮">
            <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="确定">确定(S)</button>
        </div>
    </div>
    <input type="hidden" id="userid" runat="server" />
    <input type="hidden" id="zt" runat="server" />
    <input type="hidden" id="wid" runat="server" />
 
</body>
<script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
<script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
<script src="../javascripts/sweetalert/sweetalert.min.js"></script>
<script data-main="js/web_xtsz_main_add" src="../javascripts/require.js"></script>
</html>
