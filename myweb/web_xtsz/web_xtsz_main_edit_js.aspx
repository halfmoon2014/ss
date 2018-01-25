<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_js.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_js" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
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
                      <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                      <button type="button" class="btn btn-default" id="showtitp"  aria-label="提示" >提示</button>
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <div class="form-group" id="formts" style="display:none" >
            <label for="ts">提示</label>
            <textarea class="form-control" rows="2" runat="server" id="ts">
                    //打开模态窗口
                    openModal(url, "", "", function (r) {                      
                        if (r == "ok") { 
                            //查询
                            myCheckSessionQuery(); 
                        }
                    });
                    //关窗模态窗口
                    myWindowClose("ok"); 
                    //树加载成功后执行的函数                    
                    onLoadSuccessTree(node, data, treeId);
                    //单击树
                    onClickTree(node,treeId)
            </textarea>
        </div>
        <div class="form-group">
            <label for="tbjs">js语句</label>
            <textarea class="form-control" rows="2" runat="server" id="tbjs"></textarea>
        </div>
    </div>
    <!-- /container -->
    <input type="hidden" id="wid" runat="server" />  
</body>
<script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
<script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
<script src="../javascripts/sweetalert/sweetalert.min.js"></script>
<script data-main="js/web_xtsz_main_edit_js" src="../javascripts/require.js"></script>
</html>