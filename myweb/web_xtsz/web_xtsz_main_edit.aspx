<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrl:DefaultHeader ID="sysHead" runat="server" />
    <style type="text/css">
        #textarea {
            display: block;
            margin: 0 auto;
            overflow: hidden;
            width: 550px;
            font-size: 14px;
            height: 18px;
            line-height: 24px;
            padding: 2px;
        }

        textarea {
            outline: 0 none;
            border-color: rgba(82, 168, 236, 0.8);
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1), 0 0 8px rgba(82, 168, 236, 0.6);
        }
    </style>
</head>
<body id="editbody" runat="server" class="easyui-layout">
</body>
<input type="hidden" id="wid" runat="server" />
<script src="js/web_xtsz_main_edit.js"></script>
</html>

