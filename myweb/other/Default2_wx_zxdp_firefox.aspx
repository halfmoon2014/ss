<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2_wx_zxdp_firefox.aspx.cs"
    Inherits="Default2_wx_zxdp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="css/jqm/jquery.mobile-1.4.2.min.css" />
    <script src="javascripts/jquery-1.8.0.min.js"></script>
    <script src="javascripts/jqm/jquery.mobile-1.4.2.min.js"></script>
    <meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1">

    <!--PhotoSwipe 插件 -->
	<link href="javascripts/photoswipe1.0.11/photoswipe.css" type="text/css" rel="stylesheet" />	
	<script type="text/javascript" src="javascripts/photoswipe1.0.11/simple-inheritance.min.js"></script>
	<script type="text/javascript" src="javascripts/photoswipe1.0.11/code-photoswipe-1.0.11.min.js"></script>
     <!--PhotoSwipe 插件 -->
     <script  type="text/javascript">
         function showp(pobj) {
             var fzkey = $(pobj).attr("fzkey");
             var spid = $(pobj).attr("spid");
             var p = "";
             var op = $("[spid='" + spid + "'][fzkey='" + fzkey + "']");
             var arr_p = new Array()
             $.each(op, function (i, n) {             
                 arr_p.push(eval("({url:'" + $(n).attr("sr") + "',caption:''})"));
             })

             Code.PhotoSwipe.Current.setOptions({
                 preventHide: false,
                 getImageSource: function (obj) {
                     return obj.url;
                 },
                 getImageCaption: function (obj) {
                     return obj.caption;
                 }
             });

              Code.PhotoSwipe.Current.setImages(arr_p);
              Code.PhotoSwipe.Current.show(0);
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
