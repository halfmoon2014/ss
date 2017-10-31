<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <!-- sysHead --> 			
    <ctrl:DefaultHeader  ID="sysHead" TITLE="Nexts - Login" runat="server" />
    <!-- End of sysHead -->	
    <!-- Libraries -->
	<link type="text/css" href="css/login2/login.css" rel="stylesheet" />			        
    <script src="javascripts/login2/login2.js" type="text/javascript"></script>
	<!-- End of Libraries -->	
</head>
	<body>
	<div id="container">
		<div class="logo">
			<a href="#"><img src="images/login2/assets/logo.png" alt="" /></a>
		</div>
		<div id="box">
			<p class="main">
				<label>用户名: </label>
				<input id="usr" value="" /> 
				<label>密&#12288;码: </label>
				<input type="password" id="psw" value="" />	
			</p>
			<p class="space">
				<span><input id="rememberme" type="checkbox" />记住用户名</span>
                <input type="button" id="ok" value="登&#12288;录" class="login" />
                <span  id="msg"></span>				
			</p>
		
		</div>
	</div>

	</body>
</html>
