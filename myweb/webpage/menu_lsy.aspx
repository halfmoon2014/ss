<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_lsy.aspx.cs" Inherits="webpage_menu_3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/jey/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../css/jey/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/mycss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/pepper-grinder/easyui.css" rel="stylesheet" type="text/css" />
    <title>小区</title>
</head>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body class="easyui-layout">
	<div data-options="region:'north',split:true"  style="height:100px;padding:10px;">
		<p>welcome...</p>
	</div>
	<div data-options="region:'south',split:true"   style="height:100px;padding:10px;background:#efefef;">
        <p>welcome...</p>
	</div>


	<div data-options="region:'center'"  style="overflow:hidden;">
		<div class="easyui-accordion" data-options="fit:true,border:false">
			<div title="功能表" style="padding:10px;overflow:auto;">
            <!--
				<p><a href="#" onclick="myclick(1)">楼号表</a></p>-->			
				
				<p><a href="#" onclick="myclick(3)">项目名称</a></p>
				<p><a href="#" onclick="myclick(4)">项目名称明细</a></p>	
                <p><a href="#" onclick="myclick(2)">楼号明细表</a></p>
                <p><a href="#" onclick="myclick(5)">打印</a></p>	
                <p><a href="#" onclick="myclick(6)">查询</a></p>
			</div>

		</div>
	</div>
</body>
<script language="javascript" type="text/javascript">
    function myclick(v) {
        var url = "";
        if (v == 1) {
            url = "../web_lsy/web_lsy_lhb.aspx";
        } else if (v == 2) {
            url = "../web_lsy/web_lsy_lhmxb.aspx";        
        } else if (v == 3) {
            url = "../web_lsy/web_lsy_xmmc.aspx"; 
        } else if (v == 4) {
            url = "../web_lsy/web_lsy_xmmcmx.aspx"; 
        } else if (v == 5) {
            url = "../web_lsy/web_lsy_prt.aspx";
        } else if (v == 6) {
            url = "../web_lsy/web_lsy_cx.aspx";
        }
        window.open(url);
    }
</script>
</html>
