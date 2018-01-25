<%@ Page Language="C#" AutoEventWireup="true" CodeFile="content_menu3.aspx.cs" Inherits="webpage_content_menu3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <script src="../javascripts/bootstrap/ie-emulation-modes-warning.js"></script>
    <!--[if lt IE 9]>
        <script src="../javascripts/bootstrap/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="../javascripts/bootstrap/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- End of Libraries -->
    <style type="text/css">
        #content_menu3_mydiv li{
            background-color:transparent;
            border:none;
        }
     
    </style>
</head>
<body style="background-color: transparent;">
    <form id="content_menu3_form1" runat="server">
        <div id="content_menu3_mydiv" runat="server">
        </div>
    </form>
    <script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
    <script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.list-group-item').delegate('a', 'click', function (e) {
                if ($(e.target).is("a")) {
                    var cmd = $(e.target).parent().attr("cmd");                    
                    parent.addTab($(e.target).html(), cmd, $(e.target).parent())
                }
            });
            $('.list-group-item').delegate('span', 'click', function (e) {
                if ($(e.target).is("span")) {
                    var menuID = $(e.target).parent().attr("menuID");
                    parent.myhelp(menuID)
                }
            });
        });
    </script>
</body>
</html>
