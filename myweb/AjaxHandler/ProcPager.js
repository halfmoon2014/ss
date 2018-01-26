//全局数组
//二维数组表示（wid,内容）
//PUBLICWEBARRAY 表头信息
if (typeof (PUBLICWEBARRAY) == "undefined") {
    var PUBLICWEBARRAY = new Array();
}
function getPublicWebArray(v) {
    var rev;
    if (v != null) {
        if (PUBLICWEBARRAY.length > 0) {
            for (var i = 0; i < PUBLICWEBARRAY.length; i++) {
                if (PUBLICWEBARRAY[i][0] == v) {
                    rev = PUBLICWEBARRAY[i][1];
                    break;
                }
            }
        }
    }
    return rev;
}
function addPublicWebArray(v1, v2) {
    if (v1 != null) {
        if (getPublicWebArray(v1) == null) {
            PUBLICWEBARRAY.push([v1, v2])
        } else {
            for (var i = 0; i < PUBLICWEBARRAY.length; i++) {
                if (PUBLICWEBARRAY[i][0] == v1) {
                    PUBLICWEBARRAY[i][1] = v2;
                }
            }
        }

    }
}
//全局数组end

//得到表头元素
function getHeadList(headerTableId) {
    if (getPublicWebArray(getHeaderTableId()) != null && getPublicWebArray(getHeaderTableId()) != "") { return getPublicWebArray(getHeaderTableId()); }
    var myJson = "";
    $.each($("#" + headerTableId + " tr"), function (i, n) {//只有一行,双表头就有2行
        var tdName = "table_header_td_" + $("#wid").val() + "_";
        $.each($(n).children(), function (i2, n2) {
            if (n2.style.display != "none" && $.trim(n2.innerHTML) != "&nbsp;") {
                if ($(n2).attr("HeadName") != null) {
                    if ($(n2).attr("cmstring") == undefined) {//不是尺码，尺码列不筛选
                        myJson += $.trim(n2.id.replace(tdName, "")) + ":" + $.trim($(n2).attr("HeadName")) + ",";
                    }
                }
            }
        });


    });

    if (myJson != "") {
        myJson = myJson.substring(0, myJson.length - 1);
    }
    addPublicWebArray(getHeaderTableId(), myJson);

    return myJson;
}

//键盘事件
//当表格可保存时,向下新增一行
//下移一行
function fmOnKey(e, id) {
    var keyn;
    if (browser.versions.trident) {
        keyn = e.keyCode;
    } else if (e.which) {
        keyn = e.which;
    }
    var rownum = Number($("#" + id).parent().attr("rownum"));
    if (keyn == 40) {//向下箭头
        var qkey = "_" + $("#wid").val() + "_" + rownum;

        var hkey = "_" + $("#wid").val() + "_" + (rownum + 1);
        if (myFormRowsTotal() == rownum + 1) {//最后一行就是新增
            var cc = $("#" + id).parent().clone(true);
            $(cc).attr("rownum", (rownum + 1));

            $.each(cc.find("[id]"), function (i, n) {
                var mykey = $(n).attr("id").replace(qkey, hkey)
                if ($(n).val() != undefined) {
                    if ($(n).attr("type") == undefined || $(n).attr("type") != "button") {
                        $(n).val("");
                    }
                }
                if ($(n).is(':checked')) {
                    $(n).removeAttr("checked");
                }
                if (n.childNodes[0] != undefined && n.childNodes[0].nodeName == "#text" && n.getElementsByTagName('*').length == 0) {
                    //n.childNodes[0].textContent.trim() != "" ie下有问题!
                    //td字段！                                        
                    n.innerHTML = "";
                }
                $(n).attr("id", mykey);
            });

            $.each(cc.find("[selected]"), function (i, n) {
                $(n).removeAttr("selected");
            });

            cc.appendTo($("#" + id).parent().parent());
            $("#" + id.replace(qkey, hkey) + ">input").focus();
            //删除新行中的over
            $("#" + id.replace(qkey, hkey)).parent().removeClass("over");
            //新增一行样式已经复制,只需要清除上一行样式
            $("#" + id).parent().removeClass("selected");


        } else {//非最后一行
            $("#" + id.replace(qkey, hkey) + ">input").focus();
            //清除上一行样式 
            $("#" + id).parent().removeClass("selected");
            //下一行样式加入样式
            $("#" + id.replace(qkey, hkey)).parent().addClass("selected");
        }

    } else if (keyn == 38) {
        //向上键头
        if (rownum > 0) {//如果不是第一行
            var qkey38 = "_" + $("#wid").val() + "_" + rownum;
            var hkey38 = "_" + $("#wid").val() + "_" + (rownum - 1);
            $("#" + id.replace(qkey38, hkey38) + ">input").focus();
            $("#" + id).parent().removeClass("selected");
            $("#" + id.replace(qkey38, hkey38)).parent().addClass("selected");
        }
    }
}

//提取查找字符串前面所有的字符
function getFront(mainStr, searchStr) {
    foundOffset = mainStr.indexOf(searchStr);
    if (foundOffset == -1) {
        return null;
    }
    return mainStr.substring(0, foundOffset);
}

//提取查找字符串后面的所有字符
function getEnd(mainStr, searchStr) {
    foundOffset = mainStr.indexOf(searchStr);
    if (foundOffset == -1) {
        return null;
    }
    return mainStr.substring(foundOffset + searchStr.length, mainStr.length);
}

//将字符串 searchStr 修改为 replaceStr
function replaceString(mainStr, searchStr, replaceStr) {
    var front = getFront(mainStr, searchStr);
    var end = getEnd(mainStr, searchStr);
    if (front != null && end != null) {
        return front + replaceStr + end;
    }
    return null;
}

//表格更新标识
function sysFmmyChange(key, cn) {

    if ($("#" + replaceString(key, cn, "flag")).length > 0) {

        $("#" + replaceString(key, cn, "flag")).val(1);
    }

}

//得到表头ID
function getHeaderTableId(v) {
    if (v == null) {
        if ($("#wid").length == 0) {
            return null;
        } else {
            return ("table_Header_" + $("#wid").val());
        }
    } else {
        return ("table_Header_" + v);
    }
}

//得到表体ID
function getContentTableId(v) {
    if (v == null) {
        if ($("#wid").length == 0) {
            return null;
        } else {
            return ("table_Content_" + $("#wid").val());
        }
    } else {
        return ("table_Content_" + v);
    }
}

//刷新本页面
function myFormRefresh() {
    loadInfo();
}

function myFormRowsTotal() {
    return $("#" + getContentTableId() + " tr").length;
}

function myFormCellsTotal() {
    //表格列数    
    return $("#" + getContentTableId() + " tr").eq(0).children("td").length - 1; //有一列是&nbsp;
}

function myHj(key, v, mx, col) {
    if (v == undefined || v == null) {
        //抓取控件值
        return myHjGet(key, v, mx, col);
    } else {
        //赋控件值
        return myHjSet(key, v, mx, col);
    }
}

function myHjGet(key, v, mx, col) {
    var tdskey;
    if (mx == "cm") {
        tdskey = "table_hj_td_" + $("#wid").val() + "_" + key + "_" + col;
    } else {
        //TD控件
        tdskey = "table_hj_td_" + $("#wid").val() + "_" + key;
    }
    if ($("#" + tdskey).length == 0) {
        return null;
    } else {
        return $.trim($("#" + tdskey).html());
    }

}

function myHjSet(key, v, mx, col) {
    var tdskey;
    if (mx == "cm") {
        tdskey = "table_hj_td_" + $("#wid").val() + "_" + key + "_" + col;
    } else {
        //TD控件
        tdskey = "table_hj_td_" + $("#wid").val() + "_" + key;
    }
    if ($("#" + tdskey).length == 0) {
        //return null;
    } else {
        $("#" + tdskey).html(v);
    }
}

//取值 页面表格的值 myForm("id",0) 或  myForm("mx",0,null,"cm",0)
//或者赋值  myForm("cmzbid",0,"")
//m行号 v 赋值值 ,mx 尺码取值标记 col 尺码列序
function myForm(key, m, v, mx, col) {
    if (v == undefined || v == null) {
        //抓取控件值
        return myFormGet(key, m, mx, col);
    } else {
        //赋控件值
        return myFormSet(key, m, v, mx, col);
    }
}
//取控件对象
function getForm(key, m, mx, col) {
    //抓取控件值
    return myFormGet(key, m, mx, col,'getObj');
}
//赋值
function myFormSet(key, m, v, mx, col) {
    var skey; var tdskey;
    if (mx == "cm") {
        skey = "table_" + $("#wid").val() + "_" + m + "_" + key + "_" + col;
        tdskey = "table_td_" + $("#wid").val() + "_" + m + "_" + key + "_" + col;
    } else {
        //TD间控件
        skey = "table_" + $("#wid").val() + "_" + m + "_" + key;
        //TD控件
        tdskey = "table_td_" + $("#wid").val() + "_" + m + "_" + key;
    }
    if ($("#" + skey).length == 0) {
        if ($("#" + tdskey).length == 0) {
            //return null;
        } else {
            $("#" + tdskey).html(v);
        }
    } else {

        if (document.getElementById(skey).type != undefined) {
            if (document.getElementById(skey).type == "checkbox") {//赋值的时候
                if (v == 1) {
                    $("#" + skey).prop("checked", true);
                } else {
                    $("#" + skey).removeAttr("checked");
                }
                //return ($(document.getElementById(skey)).attr("checked") == undefined ? 0 : 1);
            } else if ($("#" + skey).parent().attr("innerctrl") == 'a') {//A类型赋值
                $("#" + skey).html(v);
            } else {
                $("#" + skey).val(v);
            }
        } else {
            $("#" + skey).val(v);
        }


    }
}

//获取值
//v=null or v=undefined
//mx="mx"获取尺码控件值,col=列序
function myFormGet(key, m, mx, col,type) {
    var skey; var tdskey; var returnValue; var returnObj;
    if (mx == "cm") {
        skey = "table_" + $("#wid").val() + "_" + m + "_" + key + "_" + col;
        tdskey = "table_td_" + $("#wid").val() + "_" + m + "_" + key + "_" + col;
    } else {
        //TD间控件
        skey = "table_" + $("#wid").val() + "_" + m + "_" + key;
        //TD控件
        tdskey = "table_td_" + $("#wid").val() + "_" + m + "_" + key;
    }
    if ($("#" + skey).length == 0) {
        if ($("#" + tdskey).length == 0) {
            returnValue = null; returnObj == null;
        } else {
            returnValue = $.trim($("#" + tdskey).html());
            returnObj = $("#" + tdskey);
        }
    } else {
        if (document.getElementById(skey).type != undefined) {
            if (document.getElementById(skey).type == "checkbox") {//取值的时候,只有0或1,
                returnValue = ($("#" + skey).is(':checked') == false ? 0 : 1);
                returnObj = $("#" + skey);
            } else {
                returnValue = $("#" + skey).val();
                returnObj = $("#" + skey);
            }
        } else {
            returnValue = $("#" + skey).val();
            returnObj = $("#" + skey);
        }
    }
    return (type=="getObj"?returnObj:returnValue);
}

//分页过程
function PagerObj(op) {
    var obj = new Object;
    obj.Page = page;

    var settings = $.extend({
        wid: 0, //页面ID
        containerId: 'divPager',    //容器编号
        loadMark: 0, //是否页面第一次打开
        url: '', //分页请求URL地址
        extendParams: '', //URL地址扩展参数
        callbackFn: null  //加载完毕后的回调函数        
    }, op);

    function page(currentIndex) {
        if (1 == 2 /*checkSession() == false*/) {
            $.messager.alert('提示信息', '超时,请按F5刷新!', 'info', function () {
                execCallbackFn();
            });
        } else {
            var finalUrl = settings.url;
            if (finalUrl.indexOf('?') == -1) {
                finalUrl += '?';
            }

            finalUrl += '&p=' + currentIndex;
            finalUrl += '&clearBuffer=' + randomKey(); //消除浏览器缓存            
            finalUrl += '&wid=' + parseInt(settings.wid);
            finalUrl += '&loadmark=' + parseInt(settings.loadMark);
            $.ajax({
                type: "POST",
                async: true,
                url: finalUrl,
                data: settings.extendParams,
                success: function (html) {
                    pageCallback(html);
                }
            });
        }
    }
    //回调函数
    function pageCallback(html) {
        $('#' + settings.containerId).html(html); //alert(html)
        execCallbackFn();
    }
    //自定义回调函数
    function execCallbackFn() {
        if (settings.callbackFn != null) {
            settings.callbackFn();
        }
    }
    return obj;
}

//带session检查的查询事件
function myCheckSessionQuery() {
    if (checkSession() == false) {
        reLoad(function () {
            waitOn(myFormRefresh);
        });
    } else {
        waitOn(myFormRefresh);
    }
}

//json格式
//{
// 'myinfo' : 
// [ 
//   {'tbywname':'_v_wy_khda',
//    'tbzwname':'%u7269%u4E1A%u5BA2%u6237%u6863%u6848%u89C6%u56FE',
//    'ywname':'bh',
//    'zwname':'%u5BA2%u6237%u7F16%u53F7',
//    'hj':'0','bz':'','id':'80','tbid':'','nbsp':''
//   }
//   ,
//   {'wid':'1','tbywname':'a','tbzwname':'b'}
// ]
//}
//保存事件

//col代表列名 如mx
//返回尺码的数组
function getCm(col) {
    var header = getHeaderTableId();
    var h_arr = new Array();
    var cmstring = "";
    var ord = 0;
    $.each($("[headname='" + col + "']", $("#" + header)), function (i, n) {

        cmstring = $(n).attr("cmstring");
        var t_arr = new Array();
        for (i = 0; i < cmstring.split("|").length; i++) {
            if (cmstring.split("|")[i] != "") {
                t_arr.push([cmstring.split("|")[i].split("/")[0], cmstring.split("|")[i].split("/")[1]]);
            }
        }
        h_arr.push([$(n).attr("ord"), t_arr]);
    });
    return h_arr;
}

//根据尺码组,列位置,尺码组名 得对应的尺码id
function getCmid(h_arr, cmzbid, colorder) {
    var r = -1;
    for (i = 0; i < h_arr.length; i++) {
        if (colorder == h_arr[i][0]) {
            for (j = 0; j < h_arr[i][1].length; j++) {
                if (cmzbid == h_arr[i][1][j][0]) {
                    r = h_arr[i][1][j][1];
                    break;
                }
            }
        }
        if (r != -1) { break; }
    }
    return r;
}

//筛选事件
function mySysFindSort() {
    if (typeof (myFindSort) == "function") {//如果用户自定义MyFindSort函数!否则默认函数
        myFindSort();
    } else {
        var myjson = getHeadList(getHeaderTableId());
        if (myjson != "") {
            var jsarr = myjson.split(",");
            if ($("#SysFindSort_key").length == 0) {
                $("#platformbody").append(createFindSort()); $.parser.parse($('#div_SysWindow').parent());
            }
            for (var i = 0; i < jsarr.length; i++) {
                jsAddItemToSelect(document.getElementById("SysFindSort_key"), jsarr[i].split(":")[1], jsarr[i].split(":")[0]);
            }
        }

        var div_SysFindSort = $("#div_SysFindSort");
        $("#UserShowRows").val($("#pageSize").val());
        div_SysFindSort.show();
        $('#div_SysWindow').window('open');
        $("#SysFindSort_val").focus();

    }
}

//筛选事件-隐藏筛选
function hideMySysFindSort() {
    $("#div_SysFindSort").hide();
}


//筛选事件-筛选代码
function createFindSort() {
    var rs = "";
    rs = "    <div id=\"div_SysFindSort\" style=\"display: none;\">  <div id=\"SysFindSortMask\" style=\"top: 0; left: 0; position: absolute; z-index: 1000;\"  class=\"SysFindSortMaskclass\"></div><div class=\"SysFindSort_beian_winBG\">  <div  style=\"z-index: 1002; width: 100%; height: 300px; background: #fff; position: absolute; vertical-align: middle;\"><div style=\"width: 100%; background: #f1f1f1; height: 15px; light-height: 16px; border-bottom: #666666 1px solid;   text-align: right;\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">   <tr>   <td align=\"right\" style=\"background-color:  #EDF3E4\"><a href=\"JavaScript:hideMySysFindSort();\">   <img alt=\"img\" width=\"15\" height=\"15\" border=\"0\" src=\"../images/login_08.gif\" /></a></td></tr></table>  &nbsp;</div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">  <tr>   <td class=\"td1\"> 查询条件:</td></tr><tr><td><table width=\"100%\"><tr><td class=\"td1\">  关键字:  </td>  <td>   <select id=\"SysFindSort_key\">  </select></td><td class=\"td1\">条件:</td><td><select id=\"SysFindSort_tj\">   <option selected=\"selected\">=</option>  <option>!= </option>  <option>包含 </option>   <option>不包 </option><option>&gt; </option>  <option>&gt;= </option> <option>&lt; </option>      <option>&lt;= </option>  </select>   </td>  <td class=\"td1\">值:</td><td><input type=\"text\" id=\"SysFindSort_val\" /><a href=\"#\" onclick=\"document.getElementById('SysFindSort_val').value='{空值}'\">空值</a></td><td><input type=\"button\"  id=\"SysFindSort_add\" onclick=\"sysFindSortAdd()\" value=\"加入\" />  <input type=\"button\"  id=\"SysFindSort_and\" onclick=\"sysFindSortAndOr('并且')\" value=\"并且\" /> <input type=\"button\" id=\"SysFindSort_or\" onclick=\"sysFindSortAndOr('或者')\" value=\"或者\" />   </td>   </tr> </table>  </td> </tr> <tr>  <td> <table>  <tr>  <td>    <table>   <tr>  <td>  <textarea id=\"SysFindSort_jg\" readonly rows=\"10\" cols=\"10\" runat=\"server\" style=\"width: 565px;  height: 120px\"></textarea>  </td>   <td>     <table> <tr style=\"height: 75px\">  <td>   &nbsp;   </td>   <td>   &nbsp;</td> </tr>  <tr> <td> <input type=\"button\"  id=\"SysFindSort_go\"  onclick=\"execSysFindSort()\" value=\"筛选\" /> </td> <td>   <input type=\"button\" id=\"SysFindSort_clr\" onclick=\"sysFindSortclear()\" value=\"清除\" /> </td>   </tr>  <tr>  <td> <input type=\"button\"  id=\"SysFindSort_save\" onclick=\"SysFindSort_save()\" disabled value=\"保存\"   title=\"保存当前筛选条件\" /> </td>  <td> <input type=\"button\"  id=\"SysFindSort_load\" onclick=\"SysFindSort_load()\" disabled value=\"加载\" title=\"加载已保存的筛选条件\" />  </td>  </tr> </table>  </td>  </tr>  </table> </td> </tr> </table></td>  </tr> <tr> <td>  <table width=\"100%\">   <tr>  <td style=\"width:75px\" class=\"td1\">   显示行数: </td> <td style=\"width:30px\">  <input style=\"width:30px\" type=\"text\" id=\"UserShowRows\" />  </td>  <td style=\"width:50px\">  <input type=\"button\"  onclick=\"SysSaveUserShowRows()\" disabled  value=\"保存\" title=\"设置当前显示行数\" /> </td>    <td>    &nbsp;  </td>  </tr> </table>  </td>  </tr> </table>  </div> </div> <input type=\"hidden\" id=\"sysFindSortRow\" name=\"sysFindSortRow\" value=\"\"  /></div>"
    //return rs;
    rs = " <div id=\"div_SysFindSort\" style=\"position: relative; width:100%;height:100%; overflow: auto;display: none;\">  "

    rs += "  <div id=\"SysFindSortMask\" style=\"top: 0; left: 0; position: absolute; z-index: 1000;\"  class=\"SysFindSortMaskclass\"></div>"
    rs += "    "
    rs += "   <div  id='div_SysWindow' inline=\"true\" title=\"筛选\" class=\"easyui-window\" data-options=\"collapsible:false,minimizable:false,maximizable:false,onClose:function(){hideMySysFindSort();} \" style=\"width: 700px; height: 300px;\">"
    rs += "     <table style=\" width:100%;height:100% \" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">  <tr>   <td class=\"td1\"> 查询条件:</td></tr><tr><td><table width=\"100%\"><tr><td class=\"td1\">  关键字:  </td>  <td>   <select id=\"SysFindSort_key\">  </select></td><td class=\"td1\">条件:</td><td><select id=\"SysFindSort_tj\">   <option selected=\"selected\">=</option>  <option>!= </option>  <option>包含 </option>   <option>不包 </option><option>&gt; </option>  <option>&gt;= </option> <option>&lt; </option>      <option>&lt;= </option>  </select>   </td>  <td class=\"td1\">值:</td><td><input type=\"text\" id=\"SysFindSort_val\" /><a href=\"#\" onclick=\"document.getElementById('SysFindSort_val').value='{空值}'\">空值</a>  </td>   <td>    <input type=\"button\"  id=\"SysFindSort_add\" onclick=\"sysFindSortAdd()\" value=\"加入\" />  <input type=\"button\"  id=\"SysFindSort_and\" onclick=\"sysFindSortAndOr('并且')\" value=\"并且\" /> <input type=\"button\" id=\"SysFindSort_or\" onclick=\"sysFindSortAndOr('或者')\" value=\"或者\" />   </td>   </tr> </table>  </td> </tr> <tr>  <td> <table style=\"width:100%;height:100%\"><tr><td>表达式:</td></tr>   <tr>  <td>  <textarea id=\"SysFindSort_jg\" readonly rows=\"10\" cols=\"10\" runat=\"server\" style=\"width: 519px;  height: 120px\"></textarea>  </td>   <td>     <table> <tr style=\"height: 75px\">  <td>   &nbsp;   </td>   <td>   &nbsp;</td> </tr>  <tr> <td> <input type=\"button\"  id=\"SysFindSort_go\"  onclick=\"execSysFindSort()\" value=\"筛选\" /> </td> <td>   <input type=\"button\" id=\"SysFindSort_clr\" onclick=\"sysFindSortclear()\" value=\"清除\" /> </td>   </tr>  <tr>  <td> <input type=\"button\"  id=\"SysFindSort_save\" onclick=\"SysFindSort_save()\" disabled value=\"保存\"   title=\"保存当前筛选条件\" /> </td>  <td> <input type=\"button\"  id=\"SysFindSort_load\" onclick=\"SysFindSort_load()\" disabled value=\"加载\" title=\"加载已保存的筛选条件\" />  </td>  </tr> </table>  </td>  </tr>  </table></td>  </tr> <tr> <td>  <table width=\"100%\">   <tr>  <td style=\"width:55px\" class=\"td1\">   显示行数: </td> <td style=\"width:30px\">  <input style=\"width:30px\" type=\"text\" id=\"UserShowRows\" />  </td>  <td style=\"width:50px\">  <input type=\"button\"  onclick=\"SysSaveUserShowRows()\" disabled  value=\"保存\" title=\"设置当前显示行数\" /> </td>    <td>    &nbsp;  </td>  </tr> </table>  </td>  </tr> </table> "
    rs += "   </div> "

    rs += "    "

    rs += "<input type=\"hidden\" id=\"sysFindSortRow\" name=\"sysFindSortRow\" value=\"\"  />"
    rs += " </div>"
    return rs;
}

//筛选事件-执行筛选条件
function execSysFindSort() {
    var jg = $.trim($("#SysFindSort_jg").val());
    if (jg != "") {
        if (sysFindSortao(jg) == 1) {
            jg = jg.substring(0, js.length - 2);
            jg = $.trim(jg);
        }
        var tj = jg.split(" ");
        var thli = getPublicWebArray(getHeaderTableId()).split(",");
        thli.sort(function dd(a1, a2) { return a2.split(":")[1].length - a1.split(":")[1].length }); //排序
        myjg = "";
        var lsbs = 0; //临时标记,用来标记是不是包含或者不包
        for (var i = 0; i < tj.length; i++) {
            if (i % 4 == 0) {
                for (var j = 0; j < thli.length; j++) {
                    if (thli[j].split(":")[1] == tj[i]) { tj[i] = thli[j].split(":")[0]; break; }
                }

            } else if (i % 4 == 1) {
                if (tj[i] == "包含") {
                    tj[i] = "like"; lsbs = 1;
                } else if (tj[i] == "不包") {
                    tj[i] = "not like"; lsbs = 1;
                } else {
                    tj[i] = replacetsf(tj[i]);
                }
            } else if (i % 4 == 2) {
                var reg = new RegExp("'", "g");//g,表示全部替换。
                if (lsbs == 1) {
                    tj[i] = "'%" +  tj[i].replace(reg, "''"); + "%'";
                    lsbs = 0;
                } else {
                    tj[i] = "'" + ( tj[i]=="{空值}"?"": tj[i].replace(reg, "''")) + "'";
                }
            } else {
                if (tj[i] == "并且") {
                    tj[i] = "and";
                } else {
                    tj[i] = "or";

                }
            }
            myjg += tj[i] + " ";
        }

        $("#sysFindSortRow").val(myjg);

    }
    hideMySysFindSort();
    waitOn(myFormRefresh);
    //MyFormRefresh();


}

//筛选事件-加入
function sysFindSortAdd() {
    var key = document.getElementById("SysFindSort_key").options[window.document.getElementById("SysFindSort_key").selectedIndex].text;
    var tj = document.getElementById("SysFindSort_tj").options[window.document.getElementById("SysFindSort_tj").selectedIndex].text;
    var val = $("#SysFindSort_val").val();
    var jg = $("#SysFindSort_jg").val();
    if (val != "") {
        if (jg == "" || (jg != "" && sysFindSortao(jg) == 1)) {
            $("#SysFindSort_jg").val($("#SysFindSort_jg").val() + " " + key + " " + tj + " " + val);
            $("#SysFindSort_val").val("");
        }
    }
}

//筛选事件-筛选里的关系事件
function sysFindSortAndOr() {
    var args = sysFindSortAndOr.arguments;
    if (args.length == 1) {

        var key = document.getElementById("SysFindSort_key").options[window.document.getElementById("SysFindSort_key").selectedIndex].text;
        var tj = document.getElementById("SysFindSort_tj").options[window.document.getElementById("SysFindSort_tj").selectedIndex].text;
        var val = $("#SysFindSort_val").val()
        var jg = $("#SysFindSort_jg").val();
        if (val == "") {
            if (jg != "") {
                if (sysFindSortao(jg) == -1) {
                    $("#SysFindSort_jg").val($("#SysFindSort_jg").val() + " " + args[0]);
                }
            }
        }
        else {
            if (jg == "" || (jg != "" && sysFindSortao(jg) == 1)) {
                $("#SysFindSort_jg").val($("#SysFindSort_jg").val() + " " + key + " " + tj + " " + val + " " + args[0]);
                $("#SysFindSort_val").val("");
            } else if (jg != "" && sysFindSortao(jg) == -1) {
                $("#SysFindSort_jg").val($("#SysFindSort_jg").val() + " " + args[0] + " " + key + " " + tj + " " + val);
                $("#SysFindSort_val").val("");
            }

        }
    }
}

//筛选事件-判断最后一个是否是并且或者而且
function sysFindSortao(jg) {
    var reg = new RegExp(" ", "g"); //创建正则RegExp对象
    if (jg.replace(reg, "").lastIndexOf("并且") == jg.replace(reg, "").length - 2 || jg.replace(reg, "").lastIndexOf("或者") == jg.replace(reg, "").length - 2)
    { return 1; } else { return -1; }
}

//筛选事件-清除
function sysFindSortclear() {
    $("#SysFindSort_jg").val("");
    $("#sysFindSortRow").val("");
    //execSysFindSort();
}

//默认操作 打印,导出EXCEL
function myDefaultOperate(Etype) {
    var wid = $("#wid").val();
    var title = document.title;
    var filterRow = "";
    if ($("#sysFindSortRow").length > 0) {
        filterRow = $("#sysFindSortRow").val();       
    }
    var orderBy = "";
    if ($("#orderBy").length > 0) {
        orderBy = $("#orderBy").val();

    }
    var pageSize = "";
    if ($("#pageSize").length > 0) {
        pageSize = $("#pageSize").val();
    }
    var printParmObj = eval("({" + getUploadParameters("json") + ",\"filterRow\":\"" + filterRow + "\",\"title\":\"" + title + "\",\"orderBy\":\"" + orderBy + "\",\"pageSize\":\"" + pageSize + "\"" + "})");
    var excelParm = getUploadParameters("string");
    var currentPageIndex = ($(".pageactive").length==0?1: $(".pageactive")[0].innerHTML);
    /*页面上必要参数*/
    var url = "";
    if (Etype == "excel") {
        url = "../webpage/ProcPager_SysExcel.aspx?pageType=sysExcel" + "&clearBuffer=" + randomKey() + "&wid=" + wid + "&p=" + currentPageIndex;
        postNewWin(url, printParmObj);
    } else if (Etype == "print") {
        url = "../webpage/ProcPager_SysPrt.aspx?pageType=sysPrint" + "&clearBuffer=" + randomKey() + "&wid=" + wid + "&p=" + currentPageIndex;
        postNewWin(url, printParmObj);
    }
    /*var eurl = "";
    if (Etype == "excel") {
    eurl = "../webpage/ProcPager_SysExcel.aspx?pageType=sysExcel&" + excelParm;
    } else if (Etype == "print") {        
    eurl = "../webpage/ProcPager_SysPrt.aspx?pageType=sysPrint&" + excelParm;
    }
    var url = eurl + "&clearBuffer=" + randomKey() + "&wid=" + wid + "&filterRow=" + filterRow + "&currentPageIndex=" + currentPageIndex + "&title=" + title;
    window.open(url);*/

}

//打印-页面
function mySysPrtConfig() {
//    var div_SysFindSort = $("#div_SysFindSort");
//    $("#UserShowRows").val($("#pageSize").val());
//    div_SysFindSort.show();
//    $('#div_SysWindow').window('open');
//    $("#SysFindSort_val").focus();

    //打印设置
    var div_SysPrtConfigdSort = $("#div_SysPrtConfigSort");
    div_SysPrtConfigdSort.show();
    $("#SysPrtCount").val($("#pageSize").val());
    $('#div_SysWindow').window('open');
    $("#SysPrtCount").focus();
}

//打印-隐藏打印设置
function hideMySysPrtConfigSort() {
    $("#div_SysPrtConfigSort").hide();
}

function execSysPrtConfigt() {
    if (IsNum($("#SysPrtCount").val()) == true && $("#SysPrtCount").val() != '0') {
        $("#pageSize").val($("#SysPrtCount").val());
        hideMySysPrtConfigSort();
        myFormRefresh();

    }
}

//字段排序
function mySysPx(obj, zd) {

    if ($("#orderBy").length == 0) {
        var order = document.createElement("input");
        order.setAttribute("type", "hidden");
        order.setAttribute("id", "orderBy"); //设置Id属性
        order.setAttribute("value", zd + " desc");
        platformbody.appendChild(order);
    } else {
        if ($("#orderBy").val() == zd + " desc" || $("#orderBy").val() == zd) {
            if ($("#orderBy").val() == zd + " desc") {
                $("#orderBy").val(zd);
            } else {
                $("#orderBy").val(zd + " desc");
            }

        } else {
            $("#orderBy").val(zd + " desc");
        }
    }
    waitOn(myFormRefresh);
}