<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_sjy.aspx.cs" Inherits="web_xtsz_web_xtsz_main_edit_sjy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <form class="form-horizontal">
            <div class="form-group">
                <label for="name" class="col-md-1 control-label">名称</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="name" runat="server" readonly> 
                </div>
                <label for="orderby" class="col-md-1 control-label">排序字段</label>
                <div class="col-md-1">
                    <input type="text" class="form-control" id="orderby" runat="server">
                </div>
                <label for="pagesize" class="col-md-1 control-label">页大小</label>
                <div class="col-md-1">
                    <input type="text" class="form-control" id="pagesize" runat="server">
                </div>
                <div class="col-md-2 checkbox">
                    <label>
                        <input type="checkbox" id="mrcx" runat="server">
                        默认查询
                    </label>
                </div>
                <div class="col-md-2 checkbox">
                    <label>
                        <input type="checkbox" id="myadd" runat="server">
                        允许新增
                    </label>
                </div>
            </div>         
        </form>
        <!-- Main component for a primary marketing message or call to action -->
        <div class="form-group">
            <label for="fwsql">方位sql</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="2"  runat="server" id="fwsql"></textarea>                
        </div>
        <div class="form-group">
            <label for="tbsql">sql语句</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="2" runat="server" id="tbsql"></textarea>
            <label class="sr-only" for="tbsql2">sql语句附加语句</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="2" runat="server" id="tbsql2"></textarea>
        </div>
        <form class="form-horizontal">
            <div class="form-group">
                <label for="mxgl" class="col-md-2 control-label">明细关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxgl" runat="server">
                </div>
                <label for="mxly" class="col-md-2 control-label">明细来源</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxly" runat="server">
                    <p class="help-block">可输入:主表,明细数据源,存储过程</p>                    
                </div>
               
            </div>
        </form>
        <div class="form-group">
            <label for="mxsql">明细数据源</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="2" runat="server" id="mxsql"></textarea>            
        </div>

        <form class="form-horizontal">
            <div class="form-group">
                <label for="mxhgl" class="col-md-2 control-label">明细数据源-表头关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxhgl" runat="server">
                </div>
                <label for="mxhord" class="col-md-2 control-label">sql语句-表头关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxhord" runat="server">                    
                </div>

            </div>
        </form>

        <div class="form-group">
            <label for="mxhsql">明细头数据源</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="2" runat="server" id="mxhsql"></textarea>
        </div>
    </div>
    <!-- /container -->

    <input type="hidden" id="wid" runat="server" />
    <script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
    <script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
    <script src="../javascripts/sweetalert/sweetalert.min.js"></script>
    <script data-main="js/web_xtsz_main_edit_sjy" src="../javascripts/require.js"></script>
</body>
</html>
 