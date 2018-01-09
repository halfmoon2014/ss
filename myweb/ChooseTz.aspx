<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseTz.aspx.cs" Inherits="ChooseTz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- sysHead -->
    <ctrl:DefaultHeader Title="账套选择" ID="sysHead" runat="server" />
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

    <%--    <!-- Libraries -->
    <script src="javascripts/choosetz/ChooseTz.js" type="text/javascript"></script>
    <!-- End of Libraries -->	--%>
    <style type="text/css">
        body {
            background: none transparent scroll repeat 0% 0%;
            overflow: hidden;
            background-image: url('images/BG2.png');
            background-repeat: repeat;
            font-family: Tahoma, Arial, sans-serif;
        }

        a {
            text-decoration: none; /*超链接无下划线*/
        }

        #chosstz {
            font-size: 13px;
        }
    </style>
</head>
<body>
    <form id="F" runat="server">
        <div id="chosstz" runat="server"></div>
        <input type="hidden" id="menu" runat="server" />
        <input type="hidden" id="tzid" runat="server" />
    </form>
    <!-- Libraries -->
    <script src="javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
    <script src="javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
    <script data-main="javascripts/chooseTz/ChooseTz3.js" src="javascripts/require.js"></script>
    <!-- End of Libraries -->
</body>
</html>

