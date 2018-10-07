<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- sysHead -->
    <ctrlHeader:DefaultHeader ID="sysHead" Title="Nexts - Login" runat="server" />
    <!-- End of sysHead -->
    <!-- Libraries -->
    <link href="css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/login3/signin.css" rel="stylesheet" />   
    <link href="css/loading/loading.css" rel="stylesheet" /> 
</head>
<body>
    <div id="container">
        <div class="form-signin" >
            <h2 class="form-signin-heading">Please sign in</h2>
            <label for="inputName" class="sr-only">用户名</label>
            <input type="text" name="usr" id="usr" class="form-control" placeholder="用户名"  autofocus>
            <label for="inputPassword" class="sr-only">密码</label>
            <input type="password" name="psw" id="psw" class="form-control" placeholder="密码" >
            <div class="checkbox">
                <label>
                    <input type="checkbox" id="rememberme" name="rememberme" value="rememberme">记住我的账号
                </label>
            </div>
            <div class="alert alert-warning" style="display:none" role="alert">
                <strong>Warning!</strong><span id="msg"></span> 
            </div>
            <button class="btn btn-lg btn-primary btn-block" disabled="disabled"  id="ok">登 陆</button>
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

    <!-- Libraries -->
    <script data-baseurl="javascripts" data-from="login" data-ver="" data-main="javascripts/app" defer async="true" src="javascripts/require.js" id="jsApp"  ></script>
    <!-- End of Libraries -->
</body>
</html>
