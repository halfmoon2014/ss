﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2_wx_zxdp.aspx.cs"
    Inherits="Default2_wx_zxdp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    

    <link rel="stylesheet" href="../css/jquery/jquery.mobile-1.4.2.min.css" />
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/jquery/jquery.mobile-1.4.2.min.js" type="text/javascript"></script>
    <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1">
    <!--PhotoSwipe 插件 -->
    <link href="../javascripts/photoswipe1.0.11/photoswipe.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../javascripts/photoswipe1.0.11/simple-inheritance.min.js"></script>
    <script type="text/javascript" src="../javascripts/photoswipe1.0.11/code-photoswipe-1.0.11.min.js"></script>
    <!--PhotoSwipe 插件 -->
	<style type="text/css">
		div.gallery-row:after { clear: both; content: "."; display: block; height: 0; visibility: hidden; }
		div.gallery-item { float: left; width: 33.333333%; }
		div.gallery-item a { display: block; margin: 5px; border: 1px solid #3c3c3c; }
		div.gallery-item img { display: block; width: 100%; height: auto; }
		#Gallery1 .ui-content, #Gallery2 .ui-content { overflow: hidden; }
	</style>
    <script type="text/javascript">
        function showp(spid) {
            $.mobile.changePage("Default2_wx_zxdp_tp.aspx" , { type: "post", data: {spid:spid} }, "pop")

            $('#mypic').live('pageshow', function (event, ui) {                
              //  $("div.gallery a", $('div.gallery-page')[0]).photoSwipe();
            });

        }     
    </script>
</head>
<body>
    <div id="mp" data-role="page">
        <div data-role="header">
            <h1>
                最新单品</h1>
        </div>
        <!-- /header -->
        <div data-role="content">
            <div id="banner">
                <h2>
                    窝窝声明</h2>
            </div>
            <p>
                <ul>
                    <li>有理想</li>
                    <li>有道德</li>
                    <li>有文化</li>
                    <li>有纪律</li>
                </ul>
            </p>
            <div id="div_tj" runat="server">
            </div>
        </div>
        <!-- /content -->
        <div data-role="footer">
            <h4 class="m_footer">
                亲,你来啦...</h4>
        </div>
        <!-- /footer -->
    </div>
    <!-- /page -->
</body>
</html>
