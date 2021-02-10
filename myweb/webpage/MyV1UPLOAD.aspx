<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyV1UPLOAD.aspx.cs"
    Inherits="upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/myjs/utils.js" type="text/javascript"></script>
    <style type="text/css" media="all">
        *
        {
            margin: 0;
            padding: 0;
        }
        
        img
        {
            border: none;
        }
        
        ul
        {
            list-style-type: none;
        }
        
        ul li
        {
            padding: 2px 4px;
        }
        
        body
        {
            font-family: 宋体, 黑体,verdana, Arial;
            font-size: 12px;
            color: #333;
            background: #DDDDDD;
            margin: 30px;
            padding: 0;
        }
        
        .box
        {
            border: 1px solid #CCC;
            background: #FFF;
            padding: 8px;
            margin: 5px;
            clear: both;
        }
        
        .title
        {
            background: #F0F0F0;
            padding: 5px;
            font-weight: bold;
        }
        
        .tooltip
        {
            background: #F0F0F0;
            border-color: #bbb;
        }
        
        .tooltip h1
        {
            color: #A8DF00;
            font-family: 微软雅黑,黑体,宋体,verdana, Arial;
        }
        
        .titlebutton
        {
            float: right;
        }
        
        .uploading
        {
            background: #FFF;
            color: #444;
            text-align: left;
            width: 500px;
            padding: 4px;
        }
        
        .image
        {
            border: 1px solid #ddd;
            margin: 2px;
            padding: 1px;
            display: inline;
            width: 300px;
        }
        
        .uploadcontrol
        {
            margin: 4px 0;
            border-bottom: 1px solid #F0F0F0;
        }
    </style>
    <script type="text/javascript">
        var request = function (paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return null;
            } else {
                return returnValue;
            }
        };
        var bizId;
        var bizKey;
        var groupId;
        $(document).ready(function () {
            var size = request("size") || 1;
            var add = request("add") || false;
            groupId = request("groupId") || 0;
            bizId = request("bizId") || 0;
            bizKey = request("bizKey") || "";
            var modal = request("modal") || "search"; //||add
            if (modal =="search") {

            } else if (modal == "add") {
                if (groupId == 0 || (bizId == 0 && bizKey == "")) {
                    $("#uploadbox").append("缺少必要的groupId,bizId,bizKey ");
                } else {
                    if (add) {
                        document.getElementById("toolbox").style.display = "block";
                    }
                    var piclist = document.getElementById("piclist").value.split("|");
                    for (var i = 0; i < size; i++) {
                        if (piclist.length > i && piclist[i]!="") {                            
                            uploadcreate($("#uploadbox"), i, { id: piclist[i].split(",")[1], newpath: piclist[i].split(",")[0]} );
                        }else
                            uploadcreate($("#uploadbox"), i,null);
                    }
                }
            }
        });

        var hideDiv = function (idName) {
            $("#" + idName).fadeOut("fast");
        };

        //是否显示上传后的图片
        var isshowpic = true;
        var uploadshowpic = function (el) {
            isshowpic = !(isshowpic);
            if (isshowpic) {
                el.html("图片显示关闭");
            }
            else {
                el.html("图片显示开启");
            }
        };

        //载入中的GIF动画
        var loadingUrl = "../images/loading.gif";

        //选择文件后的事件,iframe里面调用的
        var uploading = function (imgsrc, itemid) {
            var el = $("#uploading" + itemid);
            $("#ifUpload" + itemid).fadeOut("fast");
            el.fadeIn("fast");
            el.html("<img src='" + loadingUrl + "' align='absmiddle' /> 上传中");
            return el;
        };

        //重新上传方法    
        var uploadinit = function (itemid,id) {
            currentItemID++;
            var msg = "";
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/DelPic',
                async: false,
                data: { id: id },
                error: function (e) {
                    alert("删除失败"+e.message)
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {                        
                        $("#uploading" + itemid).fadeOut("fast", function () {
                            $("#ifUpload" + itemid).fadeIn("fast");
                            $("#panelViewPic" + itemid).fadeOut("fast");
                        });
                    } else {
                        alert("删除失败" + r.r)                        
                    }
                }
            });           

        };

        //上传时程序发生错误时，给提示，并返回上传状态
        var uploaderror = function (itemid) {
            alert("程序异常，" + itemid + "项上传未成功。");
            uploadinit();
        };

        //上传成功后的处理
        var uploadsuccess = function (newpath, itemid,id) {
            $("#uploading" + itemid).html("文件上传成功. <a href='javascript:void(0);' onclick='uploadinit(" + itemid + ","+id+");'>[删除且重新上传]</a>");
            //$("#uploading" + itemid).html("文件上传成功.");
            if (isshowpic) {
                $("#panelViewPic" + itemid).html("<a href='" + newpath + "' title='点击查看大图' target='_blank'><img src='" + newpath + "' alt='' style='width:300px;' /></a>");
                $("#panelViewPic" + itemid).fadeIn("fast");
            }
        };


        var currentItemID = 0;  //用于存放共有多少个上传控件了
        //创建一个上传控件
        var uploadcreate = function (el, itemid, picObj) {
            currentItemID++;
            if (itemid == null) {
                itemid = currentItemID;
            }           
            var strContent = "<div class='uploadcontrol'><iframe src=\"Myv1UPLOADITEM.aspx?id=" + itemid + "&groupId=" + groupId + "&bizId=" + bizId + "&bizKey=" + bizKey + "\" id=\"ifUpload" + itemid + "\" frameborder=\"no\" scrolling=\"no\" style=\"width:400px; height:28px; " + (picObj !=null  ? "display:none" : "") + " \"></iframe>";
            if (picObj == null) {
                strContent += "<div class=\"uploading\" id=\"uploading" + itemid + "\" style=\"display:none;\" ></div>";
                strContent += "<div class=\"image\" id=\"panelViewPic" + itemid + "\" style=\"display:none;\"></div></div>";
            }
            else {    
                strContent += "<div class=\"uploading\" id=\"uploading" + itemid + "\"><a href='javascript:void(0);' onclick='uploadinit(" + itemid + "," + picObj.id+");'>[删除且重新上传]</a></div>";
                strContent += "<div class=\"image\" id=\"panelViewPic" + itemid + "\" ><a href='" + picObj.newpath + "' title='点击查看大图' target='_blank'><img src='" + picObj.newpath + "' alt='' style='width:300px;' /></a></div></div>";
            }
            el.append(strContent);
        };
     
    </script>
</head>
<body>
    <form id="form1" runat="server">    
    <input type="hidden" runat="server" id="piclist" />
    <div id="toolbox" style="display:none" class="tooltip box">
        <a href="#" onclick="uploadcreate($('#uploadbox'));">添加一个新上传控件</a> <a href="#" style="display:none" onclick="uploadshowpic($(this));">
            图片显示关闭</a>
    </div>
    <div id="uploadbox" class="box">
    </div>
    </form>
</body>
</html>
