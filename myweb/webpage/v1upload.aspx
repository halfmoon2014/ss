<%@ Page Language="C#" AutoEventWireup="true" CodeFile="v1upload.aspx.cs"
    Inherits="upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/myjs/utils.js" type="text/javascript"></script>
    <style type="text/css" media="all">
        * {
            margin: 0;
            padding: 0;
        }

        img {
            border: none;
        }

        ul {
            list-style-type: none;
        }

            ul li {
                padding: 2px 4px;
            }

        body {
            font-family: 宋体, 黑体,verdana, Arial;
            font-size: 12px;
            color: #333;
            background: #DDDDDD;
            margin: 30px;
            padding: 0;
        }

        .box {
            border: 1px solid #CCC;
            background: #FFF;
            padding: 8px;
            margin: 5px;
            clear: both;
        }

        .title {
            background: #F0F0F0;
            padding: 5px;
            font-weight: bold;
        }

        .tooltip {
            background: #F0F0F0;
            border-color: #bbb;
        }

            .tooltip h1 {
                color: #A8DF00;
                font-family: 微软雅黑,黑体,宋体,verdana, Arial;
            }

        .titlebutton {
            float: right;
        }

        .uploading {
            background: #FFF;
            color: #444;
            text-align: left;
            width: 500px;
            padding: 4px;
        }

        .image {
            border: 1px solid #ddd;
            margin: 2px;
            padding: 1px;
            display: inline;
            width: 300px;
        }

        .uploadcontrol {
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
        var bizId, bizKey, groupId, size, create;
        var sucessList = [];
        var errorList = [];
        $(document).ready(function () {
            size = request("size") || 1;//默认1个
            create = request("create") || false;//是否允许用户自己添加上传个数
            groupId = request("groupId") || 0;
            bizId = request("bizId") || 0;
            bizKey = request("bizKey") || "";
            var modal = request("modal") || "search"; //||add=新增修改 ,pre=先上传然后再保存
            if (modal == "search") {

            } else if (modal == "add" || modal == "pre") {
                if (modal == "add" && (groupId == 0 || (bizId == 0 && bizKey == ""))) {
                    $("#uploadbox").append("缺少必要的groupId,bizId,bizKey ");
                } else if (modal == "pre" && groupId == 0) {
                    $("#uploadbox").append("缺少必要的groupId");
                } else {
                    if (create) {
                        document.getElementById("toolbox").style.display = "block";
                    }                    
                    var piclist = document.getElementById("piclist").value.split("|");
                    for (var i = 0; i < size; i++) {
                        if (piclist.length > i && piclist[i] != "") {
                            uploadcreate($("#uploadbox"), randomKey(), { id: piclist[i].split(",")[1], newpath: piclist[i].split(",")[0], remark: piclist[i].split(",")[2] });
                        } else
                            uploadcreate($("#uploadbox"), randomKey(), null);
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
        var uploadinit = function (itemid, id) {
            //currentItemID++;
            var msg = "";
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/DelPic',
                async: false,
                data: { id: id },
                error: function (e) {
                    alert("删除失败" + e.message)
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        $("#uploading" + itemid).fadeOut("fast", function () {
                            $("#ifUpload" + itemid).fadeIn("fast");
                            $("#panelViewPic" + itemid).fadeOut("fast");

                            //只有一个的时候
                            if (!create && size == 1) {
                                sucessList = [];                               
                            } else {
                                sucessList.remove(id);                                
                            }

                        });
                    } else {
                        alert("删除失败" + r.r)
                    }
                }
            });

        };

        Array.prototype.indexOf = function (val) {
            for (var i = 0; i < this.length; i++) {
                if (this[i].id == val) return i;
            }
            return -1;
        };
        Array.prototype.remove = function (val) {
            var index = this.indexOf(val);
            if (index > -1) {
                this.splice(index, 1);
            }
        };
        //上传时程序发生错误时，给提示，并返回上传状态
        var uploaderror = function (itemid) {
            alert("程序异常，" + itemid + "项上传未成功。");
            uploadinit(itemid,0);
        };

        //上传成功后的处理
        var uploadsuccess = function (newpath, itemid, id, remark) {
            if (!create && size == 1) {
                sucessList = [];
                sucessList.push({ id: id, path: newpath,remark:remark });
            } else {
                var isExists = false;
                for (var i = 0; i < sucessList.length; i++) {
                    if (sucessList[i].id == id) {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists) sucessList.push({ id: id, path: newpath,remark:remark });
            }
            $("#uploading" + itemid).html("文件上传成功. <a href='javascript:void(0);' onclick='uploadinit(\"" + itemid + "\"," + id + ");'>[删除且重新上传]</a>说明：<input readonly value='" + remark + "' type='text' id='remark'" + itemid +"' />");
            //$("#uploading" + itemid).html("文件上传成功.");
            if (isshowpic) {
                $("#panelViewPic" + itemid).html("<a href='" + newpath + "' title='点击查看大图' target='_blank'><img src='" + newpath + "' alt='' style='width:300px;' /></a>");
                $("#panelViewPic" + itemid).fadeIn("fast");
            }
            if ($("#sucess_close").is(":checked"))   closeWindow();
        };


        //var currentItemID = 0;  //用于存放共有多少个上传控件了
        //创建一个上传控件
        var uploadcreate = function (el, itemid, picObj) {
            //currentItemID++;
            if (itemid == null) {
                itemid = randomKey();
            }
            var strContent = "<div class='uploadcontrol'><iframe src=\"v1UPLOADITEM.aspx?id=" + itemid + "&groupId=" + groupId + "&bizId=" + bizId + "&bizKey=" + bizKey + "\" id=\"ifUpload" + itemid + "\" frameborder=\"no\" scrolling=\"no\" style=\"width:400px; height:68px; " + (picObj != null ? "display:none" : "") + " \"></iframe>";
            if (picObj == null) {
                strContent += "<div class=\"uploading\" id=\"uploading" + itemid + "\" style=\"display:none;\" ></div>";
                strContent += "<div class=\"image\" id=\"panelViewPic" + itemid + "\" style=\"display:none;\"></div></div>";
            }
            else {
                strContent += "<div class=\"uploading\" id=\"uploading" + itemid + "\"><a href='javascript:void(0);' onclick='uploadinit(\"" + itemid + "\"," + picObj.id + ");'>[删除且重新上传]</a>说明：<input readonly value='" + picObj.remark+"' type='text' id='remark'" + itemid+"' /></div>";
                strContent += "<div class=\"image\" id=\"panelViewPic" + itemid + "\" ><a href='" + picObj.newpath + "' title='点击查看大图' target='_blank'><img src='" + picObj.newpath + "' alt='' style='width:300px;' /></a></div></div>";
            }
            el.append(strContent);
        };


        /*
*用于使用平台脚本打开窗口的情况
*兼容chrome 因为没有模态窗口,chrome 使用open 打开新窗口,所以在关闭的时候注意执行父窗口的函数
*子窗口在调用
*/
        function closeWindow() {
            var returnvalue = {};            
            returnvalue["sucessList"] = sucessList;            
            if (browser.versions.webKit) {
                //用来关闭chrome窗口时标识关闭的动作是否使用浏览器自带的关闭按钮
                //任何关闭的动作都会响应onunload事件
                if (browser.versions.mobile) {
                    window.onunloadtag = true;
                    (window.opener && window.opener.callback != undefined) ? window.opener.callback(returnvalue) : "";
                    window.close();
                } else {
                    parent.document.getElementById("platDialog").close();
                    (parent.window.platDialogCallback != undefined) ? parent.window.platDialogCallback(returnvalue) : "";
                    parent.document.getElementById("platIframe").src = "about:blank";
                }
            } else {
                window.returnValue = returnvalue;
                window.close();
            }
        }

        window.onunload = function () {
            if (browser.versions.webKit) {
                if (window.onunloadtag != true) {                   
                    (window.opener && window.opener.callback != undefined) ? window.opener.callback(null) : "";
                }
            }
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="piclist" />
        <div class="tooltip box">
            <a href="#" onclick="closeWindow();">关闭</a><input type="checkbox" id="sucess_close" style="margin-left:30px" checked title ="上传成功后关闭" />上传成功后关闭
        </div>
        <div id="toolbox" style="display: none" class="tooltip box">
            <a href="#" onclick="uploadcreate($('#uploadbox'));">添加一个新上传控件</a> <a href="#" style="display: none" onclick="uploadshowpic($(this));">图片显示关闭</a>
        </div>
        <div id="uploadbox" class="box">
        </div>
    </form>
</body>
</html>
