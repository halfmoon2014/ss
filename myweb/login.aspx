<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- sysHead -->
    <ctrlHeader:DefaultHeader ID="sysHead" Title="Nexts - Login" runat="server" />
    <!-- End of sysHead -->

    <!-- Libraries -->
    <link href="css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="css/login3/signin.css" rel="stylesheet" />
    <script src="javascripts/bootstrap/ie-emulation-modes-warning.js"></script>    
    <!--[if lt IE 9]>
        <script src="javascripts/bootstrap/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="javascripts/bootstrap/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- End of Libraries -->
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
            <button class="btn btn-lg btn-primary btn-block" id="ok">登 陆</button>
        </div>

    </div>
    <!-- Libraries -->
    <script src="javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
    <script src="javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
    <script data-main="javascripts/login3/login3" src="javascripts/require.js"></script>
    <!-- End of Libraries -->
</body>
</html>
