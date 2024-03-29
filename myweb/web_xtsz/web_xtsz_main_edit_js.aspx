﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_js.aspx.cs" Inherits="web_xtsz_main_edit_js" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />    
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
                            wQuery(); 
                        }
                    });
                    //关窗模态窗口
                    closeWindow("ok"); 
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
<script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="web_xtsz_main_edit_js" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>