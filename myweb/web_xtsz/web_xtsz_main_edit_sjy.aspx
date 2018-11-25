<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_sjy.aspx.cs" Inherits="web_xtsz_main_edit_sjy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />     
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
            <label for="fwsql">单据SQL</label>
            <textarea class="form-control"   rows="2"  runat="server" id="fwsql"></textarea>                
        </div>
        <div class="form-group">
            <label for="tbsql">详情SQL</label>
            <textarea class="form-control"   rows="2" runat="server" id="tbsql"></textarea>
            <label class="sr-only" for="tbsql2">sql语句附加语句</label>
            <textarea class="form-control"   rows="2" runat="server" id="tbsql2"></textarea>
        </div>
        <form class="form-horizontal">
            <div class="form-group">
                <label for="mxgl" class="col-md-2 control-label">明细SQL-尺码关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxgl" runat="server">
                </div>
                <label for="mxly" class="col-md-2 control-label">尺码来源</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxly" runat="server">
                    <p class="help-block">可输入:主表,尺码数据源,存储过程</p>                    
                </div>
               
            </div>
        </form>
        <div class="form-group">
            <label for="mxsql">尺码</label>
            <textarea class="form-control" rows="2" runat="server" id="mxsql"></textarea>            
        </div>

        <form class="form-horizontal">
            <div class="form-group">
                <label for="mxhgl" class="col-md-2 control-label">尺码数据源-尺码标题关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxhgl" runat="server">
                </div>
                <label for="mxhord" class="col-md-2 control-label">明细SQL-尺码标题关联</label>
                <div class="col-md-3">
                    <input type="text" class="form-control" id="mxhord" runat="server">                    
                </div>

            </div>
        </form>

        <div class="form-group">
            <label for="mxhsql">尺码标题</label>
            <textarea class="form-control" rows="2" runat="server" id="mxhsql"></textarea>
        </div>
    </div>
    <!-- /container -->
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
    <input type="hidden" id="wid" runat="server" />    
</body>
<script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="web_xtsz_main_edit_sjy" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>
 