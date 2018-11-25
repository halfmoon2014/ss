<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_z.aspx.cs"    Inherits="web_xtsz_main_edit_z" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />    
</head>
<body id="bodyzdwh">
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
                      <button type="button" class="btn btn-default" id="btnmb" runat="server" aria-label="手机">手机</button>
                      <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                      <button type="button" class="btn btn-default" id="showtitp"  aria-label="提示" >提示</button>
                      <button type="button" class="btn btn-default" id="fz"  aria-label="复制" >复制</button>
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <div class="form-group" id="formts" style="display: none">
            <label for="ts">提示</label>
            <textarea style="width: 100%;" id="helpTextarea" rows="1">
            button类型的宽度取html宽度;css0样式指定[标题名称];css样式指定[htmlid]             
            session字典 处理顺序
            1.URL
            2.默认值
                A.带select时,先将@key用URL夫换
                B.固定项@tzid @username @userid            
            3.替换主表字段中的@key
            
            在进行构造表头时取值
            1.主表字段
                A.主表有记录
                B.其它取默认值
            2.默认值
            默认值=session字段存在于session字典中
                    =默认字段存在于session字典中
                    =select 语句(@key可被session字典替换)
                    =某个值 
            [引用][表达式]用于ajax请求数据的时候,也只作用于此
        </textarea>
        </div>    
        <div id="cxtj" class="container-fluid">
            <form class="form-horizontal">
                <div class="form-group">                 
                    <%=formgroupBuilder.ToString() %>
                </div>
            </form>
        </div>

        <div class="table-responsive">
            <table id="zdwhtb" class="table table-bordered">
                <tr class="tbth" rownum="1">
                    <%=detailHeadBuilder.ToString() %>
                </tr><%=detailDataBuilder.ToString() %>
            </table>
        </div>
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
    <input type="hidden" id="wid" runat="server" />
    <input type="hidden" id="lx" runat="server" />
    <input type="hidden" id="ismobile" runat="server" />
</body>
<script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="web_xtsz_main_edit_z" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>
