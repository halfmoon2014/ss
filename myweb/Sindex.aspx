<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sindex.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>商城测试版</title>
    <link href="css/home/style.css" rel="stylesheet" type="text/css" media="screen" />    
    <script language="javascript" type="text/javascript">
        function s(id) {
            alert(id);
        }
    </script>
</head>
<body>
    <div id="wrapper">
        <div id="wrapper2">
            <div id="header" class="container">
                <div id="logo">
                    <h1>
                        <a href="#">Z<span>果果</span>7商城</a></h1>
                </div>
                <div id="menu">
                    <ul>
                        <li class="current_page_item"><a href="#">首页</a></li>
                        <li><a href="#">博客</a></li>
                        <li><a href="#">照片</a></li>
                        <li><a href="#">关于</a></li>
                        <li><a href="#">联系我们</a></li>
                    </ul>
                </div>
            </div>
            <!-- end #header -->
            <div id="page">
                <div id="content" runat="server">
 
                </div>
                <!-- end #content -->
                <div id="sidebar">
                    <ul>
                        <li>
                            <div id="search">
                                <form method="get" action="#">
                                <div>
                                    <input type="text" name="s" id="search-text" value="" />
                                    <input type="submit" id="search-submit" value="GO" />
                                </div>
                                </form>
                            </div>
                            <div style="clear: both;">
                                &nbsp;</div>
                        </li>
                        <li id="fl" runat="server">              
                        </li>                      
                        <li>
                            <h2>
                                友情站点</h2>
                            <ul>
                                <li><a href="http://www.sina.com.cn" target="_blank" >新浪</a></li>
                                <li><a href="http://www.163.com" target="_blank">网易</a></li>
                                <li><a href="http://www.renren.com" target="_blank">人人</a></li>
                                <li><a href="http://www.facebook.com" target="_blank">facebook</a></li>
                                <li><a href="http://www.microsoft.com" target="_blank">microsoft</a></li>
                                <li><a href="http://www.nba.com" target="_blank">nba</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!-- end #sidebar -->
                <div style="clear: both;">
                    &nbsp;</div>
            </div>
            <!-- end #page -->
            <div id="footer">
                <p>
                    Copyright (c) 2012 Sitename.com. All rights reserved. Design by <a href="http://www.eesj.net/"
                        rel="nofollow">eesj.net</a>.</p>
            </div>
        </div>
    </div>
    <!-- end #footer -->
</body>
</html>
