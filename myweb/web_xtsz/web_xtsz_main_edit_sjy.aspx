<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_sjy.aspx.cs" Inherits="web_xtsz_web_xtsz_main_edit_sjy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrl:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="css/sticky-footer-navbar.css" rel="stylesheet" />

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
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#"></a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">        
                <ul class="nav navbar-nav" id="btnGroup" runat="server">
                    <li><a href="#" id="ok" accesskey="s" >保存(S)</a></li>
                    <li id="li_fb" runat="server"><a href="#" id="fb">发布</a></li>            
                </ul>
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
                <div class="col-md-1 checkbox">
                    <label>
                        <input type="checkbox" id="mrcx" runat="server">
                        默认查询
                    </label>
                </div>
                <div class="col-md-1 checkbox">
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
            <textarea class="form-control" ondblclick="fd(this)" rows="5"  runat="server" id="fwsql"></textarea>                
        </div>
        <div class="form-group">
            <label for="tbsql">sql语句</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="10" runat="server" id="tbsql"></textarea>
            <label class="sr-only" for="tbsql2">sql语句附加语句</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="10" runat="server" id="tbsql2"></textarea>
        </div>
        <form class="form-horizontal">
            <div class="form-group">
                <label for="mxgl" class="col-md-1 control-label">明细关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxgl" runat="server">
                </div>
                <label for="mxly" class="col-md-1 control-label">明细来源</label>
                <div class="col-md-4">
                    <input type="text" class="form-control" id="mxly" runat="server">
                    <p class="help-block">可输入:主表,明细数据源,存储过程</p>                    
                </div>
               
            </div>
        </form>
        <div class="form-group">
            <label for="mxsql">明细数据源</label>
            <textarea class="form-control" ondblclick="fd(this)" rows="10" runat="server" id="mxsql"></textarea>            
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
</body>

</html>
<script type="text/javascript">

    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#fb").bind("click", function () { fb_click(); });
    });
    function salert(title, text, type, fn) {
        swal({
            title: title,
            text: text,
            type: type,
        }, fn);
    }
    function ok_click() {
        $('#ok').attr('disable','disable');
        var sql = document.getElementById("tbsql").value;
        var fwsql = document.getElementById("fwsql").value;
        var name = document.getElementById("name").value;
        //var zwname = document.getElementById("zwname").value;
        var mxgl = document.getElementById("mxgl").value;
        var mxsql = document.getElementById("mxsql").value;
        var mxhgl = document.getElementById("mxhgl").value;
        var mxhord = document.getElementById("mxhord").value;
        var mxhsql = document.getElementById("mxhsql").value;
        var sql_2 = document.getElementById("tbsql2").value;

        if (name.length == 0) {
            salert('提示信息', '中文名称一定要输入!', 'info', function () {                
                $('#ok').removeAttr("disable")
            });

        } else {
            var wid = document.getElementById("wid").value;
            var mrcx = (document.getElementById("mrcx").checked ? "1" : "0");
            var myadd = (document.getElementById("myadd").checked ? "1" : "0");
            var orderby = document.getElementById("orderby").value;
            var pagesize = document.getElementById("pagesize").value;
            var mxly = document.getElementById("mxly").value;
            if (pagesize == "") { pagesize = "0"; }

            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/sjy_up',
                data: { wid: wid, value1: name, value3: sql, value4: fwsql, mrcx: mrcx, myadd: myadd, orderby: orderby, pagesize: pagesize, mxgl: mxgl, mxsql: mxsql, mxhgl: mxhgl, mxhord: mxhord, mxhsql: mxhsql, mxly: mxly, sql_2: sql_2 },
                error: function (e) {
                    salert('提示信息', '连接失败!', 'error', function () {
                        $('#ok').removeAttr("disable")
                    });
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        salert('提示信息', '保存成功!', 'success', function () {
                            $('#ok').removeAttr("disable")
                        });
                    } else {
                        salert('提示信息', '保存失败!', 'error', function () {
                            $('#ok').removeAttr("disable")
                        });
                    }
                }
            })
        }

    }

    //放大
    function fd(obj) {
        return false;
        if (typeof ($(obj).attr("oldw")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldw", $(obj).css("width"));
        }
        if (typeof ($(obj).attr("oldh")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldh", $(obj).css("height"));
        }
        if ($(obj).css("width") == $(obj).attr("oldw")) {
            $(obj).css("height", "600");
            $(obj).css("width", "90%");
        } else {
            $(obj).css("height", $(obj).attr("oldh"));
            $(obj).css("width", $(obj).attr("oldw"));
        }
    }

</script>
