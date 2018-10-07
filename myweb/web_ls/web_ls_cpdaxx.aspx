<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_ls_cpdaxx.aspx.cs" Inherits="web_ls_web_ls_cpdaxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="产品参数" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/userplatform/sticky-footer-navbar.css" rel="stylesheet" />
    <link href="../css/jey/ui-pepper-grinder/easyui.css" rel="stylesheet" />
    <link href="../css/jey/icon.css" rel="stylesheet" />
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
                          <button type="button" class="btn btn-default" id="ok"  aria-label="新增">新增</button>
                          <button type="button" class="btn btn-default" id="edit"  aria-label="修改">修改</button>                      
                          <button type="button" class="btn btn-default" id="del"  aria-label="删除">删除</button>   
                        </div>
                    </form>  
                </div><!--/.nav-collapse -->
            </div>
        </nav>
        <div style="width: 200px">
            <ul id="xx">
            </ul>
        </div>
    </div>
    <dialog id="platDialog" style="border: 3px; padding: 16px;"><iframe style="width: 800px; height: 600px" id="platIframe" frameborder="0" ></iframe></dialog>
</body>
<script data-baseurl="../javascripts" data-from="web_ls_cpdaxx" data-main="../javascripts/app" defer async="true" src="../javascripts/require.js" id="jsApp"></script>
</html>
