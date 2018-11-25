<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_zdwh.aspx.cs" Inherits="web_xtsz_main_edit_zdwh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->    
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
                      <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                      <button type="button" class="btn btn-default" id="fz"  aria-label="复制" >复制</button>
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>

    <div class="container-fluid">
        <ul>
            <li>筛选,打印名称 功能还没有完成;</li>
            <li>合计=11会显示合计二字</li>
            <li>bz现在只用于作为select数据源</li>
            <li>显示默认日期只判断日期年份是否等于1900</li>
        </ul>
        <div class="table-responsive">
            <table id="zdwhtb" class="table table-bordered">
                <tr class="tbth" rownum="1">
                    <td field="id" style="display: none">id
                    </td>
                    <td field="ywname">字段
                    </td>
                    <td field="zwname">名称
                    </td>
                    <td field="ord">顺序
                    </td>
                    <td field="width">宽度
                    </td>
                    <td field="visible">显示
                    </td>
                    <td field="readonly">只读
                    </td>
                    <td field="type">类型
                    </td>
                    <td field="sx">筛选
                    </td>
                    <td field="bz">备注
                    </td>
                    <td field="showzero">显示数字0
                    </td>
                    <td field="event">事件
                    </td>
                    <td field="btnvalue">按钮名称
                    </td>
                    <td field="showmrrq">显示默认日期
                    </td>
                    <td field="hj">合计
                    </td>
                    <td field="hbltname">合并列头名称
                    </td>
                    <td field="px">排序
                    </td>
                    <td field="format">format
                    </td>
                    <td field="prtname">打印名称
                    </td>
                    <td field="mark" style="display: none">mark
                    </td>
                </tr>
                <%=trList.ToString()%>
            </table>
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
<script data-jscdn="<%=GetJsCDN()%>"  data-csscdn="<%=GetCssCDN()%>" data-from="web_xtsz_main_edit_zdwh" data-ver="<%=GetJsVer()%>"  data-main="<%=GetJsCDN()+"/app"%>" defer async="true" src="<%=GetRequireJs()%>" id="jsApp"  ></script> 
</html>

