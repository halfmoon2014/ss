<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseTz.aspx.cs" Inherits="ChooseTz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- sysHead --> 	
    <ctrl:DefaultHeader  TITLE="账套选择" ID="sysHead" runat="server"  />
    <!-- End of sysHead -->	
    <!-- Libraries -->
    <script src="javascripts/choosetz/ChooseTz.js" type="text/javascript"></script>
    <!-- End of Libraries -->	
    <style type="text/css" >
        body
        {   
            background: none transparent scroll repeat 0% 0%;
            overflow: hidden;
            background-image: url('images/BG2.png'); background-repeat: repeat;
            font-family:Tahoma, Arial, sans-serif;
        }
        a
        {
            text-decoration: none; /*超链接无下划线*/
        }
        #chosstz{font-size: 13px;}
    </style>
</head>
<body  >
    <form id="F"  runat="server"  >    
    <div id="chosstz" runat="server"></div>
    <input type="hidden" id="menu"   runat="server" />
    <input type="hidden" id="tzid"   runat="server" />
    </form>
</body>
</html>

