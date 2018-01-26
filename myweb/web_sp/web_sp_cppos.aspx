<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_sp_cppos.aspx.cs" Inherits="web_ls_web_ls_cpdaxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="Default" />
    <link href="../css/mycss/myweb.css" rel="stylesheet" type="text/css" />
    <style>
        .mytable
        {
            table-layout: fixed;
        }
        .chh
        {
            width: 120px;
        }
        .cpm
        {
            width: 120px;
        }
        .cdj
        {
            width: 80px;
        }
        .cje
        {
            width: 80px;
        }
        .csl
        {
            width: 80px;
        }
        .cys
        {
            width: 80px;
        }
        .ccm
        {
            width: 80px;
        }
        .clsj
        {
            width: 80px;
        }
        .czk
        {
            width: 80px;
        }
        
        .ccontent
        {
            height: 300px;
            overflow-y: auto;
        }
        .p100
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="mytable">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                日期
                            </td>
                            <td>
                                <input class="p100" type="text" id="rq" runat="server" />
                            </td>
                            <td>
                                销售别
                            </td>
                            <td id="tb_select_xsb" runat="server">
                            </td>
                            <td>
                                退货号
                            </td>
                            <td>
                                <input type="text" id="thh" class="p100" runat="server" />
                                <input type="hidden" id="thh_id" />
                            </td>
                            <td>
                                班别
                            </td>
                            <td id="bb" runat="server">
                            </td>
                            <td>
                                仓库
                            </td>
                            <td id="ck" runat="server">
                            </td>
                            <td>
                                营业员
                            </td>
                            <td>
                                <input type="text" id="yyy" class="p100" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                备注
                            </td>
                            <td colspan="11">
                                <input type="text" id="bz" class="p100" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="content" class="MyDivTableClass">
        <div >
            <table class="style_head">
                <tr>
                    <td class="chh">
                        货号
                    </td>
                    <td class="cpm">
                        品名
                    </td>
                    <td class="cys">
                        颜色
                    </td>
                    <td class="ccm">
                        尺码
                    </td>
                    <td class="csl">
                        数量
                    </td>
                    <td class="clsj">
                        零售价
                    </td>
                    <td class="czk">
                        折扣
                    </td>
                    <td class="cdj">
                        单价
                    </td>
                    <td class="cje">
                        金额
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="ccontent">
            <table id="table_content" class="style_Content">
            </table>
        </div>
        <div >
            <table class="style_hj">
                <tr>
                    <td class="chh">
                        合计
                    </td>
                    <td class="cpm">
                        &nbsp;
                    </td>
                    <td class="cys">
                        &nbsp;
                    </td>
                    <td class="ccm">
                        &nbsp;
                    </td>
                    <td class="csl" id="hj_sl">
                        &nbsp;
                    </td>
                    <td class="clsj">
                        &nbsp;
                    </td>
                    <td class="czk">
                        &nbsp;
                    </td>
                    <td class="cdj">
                        &nbsp;
                    </td>
                    <td class="cje" id="hj_je">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <table class="mytable">
            <tr>
                <td>
                    货号
                </td>
                <td>
                    <input type="text" id="hh" />
                </td>
                <td>
                    折扣
                </td>
                <td>
                    <input type="text" id="zk" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">结算</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="del">清除所有货号</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="add">重新开单</a>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="mykey" runat="server" />
    <input type="hidden" id="zdr" runat="server" />
    <input type="hidden" id="zt" runat="server" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        if ($.browser.msie) {
            //alert('IE');
        } else {
            $("#hh").bind("change", function (event) { hh_change(event); });
        }

        $("#ok").bind("click", function () { ok_click(); });
        $("#add").bind("click", function () { add_click(); });
        $("#del").bind("click", function () { del_click(); });
        $("#select_xsb").bind("change", function (event) { xsb_click(event); });
        $("#thh").bind("change", function (event) { thh_change(event); });

        /*$('#xx').tree({
        url: 'web_sp_main.ashx?lx=dl',
        onBeforeLoad: function (node, param) {

        }
        });*/
        if (request("mykey") == null) {
            $("#mykey").attr("value", "0");
        } else {
            $("#mykey").attr("value", request("mykey"));
        }
        var mykey = $("#mykey").attr("value");
        if (mykey == null || mykey == "") { mykey = 0; }

        if (mykey == 0) {
            myLoad(0, "add");
        } else {
            myLoad(mykey, "xg");
        }

        document.body.onkeydown = function () {
            if (browser.versions.trident) {
                if (window.event.keyCode == 13) {
                    var srcid = "";
                    if (window.event.srcElement != null && window.event.srcElement.id != null) {
                        srcid = window.event.srcElement.id;
                    }
                    if (srcid == "hh") {
                        //货号输入,不换成TAB

                        hh_change(event);

                    } else {
                        window.event.keyCode = 9;
                    }

                }
            }
        }
    });
    //重新开单按钮
    function add_click() {
        if ($("#add").linkbutton("options").disabled) {
            //由于JQUERY EASYUI LINKBUTTON 对动态加载的事件不支持失效
            return false;
        }
        myLoad(0, "add");
    }
    //选择退货号
    function thh_change(event) {
        var djh = event.target.value;
        var url = "../webpage/lss.aspx?wid=130&djh=" + djh;
        var r = openModal(url, "", "dialogWidth=1000px;dialogHeight=700px");

        if (r != null && r != "") {
            myLoad(r[0], "th");
        } else {
            $.messager.alert('提示信息', '没有选择销售单!', 'info', function (r) {
                myLoad(0, "add");
            });
        }
    }

    function ok_check(rq) {
        if (rq.length != 10) {
            $.messager.alert('提示信息', '日期格式不正确!', 'info', function () {
                return false;
            });
        } else if (getRowCount() == 0) {
            $.messager.alert('提示信息', '无保存数据!', 'info', function () {
                return false;
            });
        } else {
            return true;
        }
    }
    function ok_click() {
        if ($("#ok").linkbutton("options").disabled) {
        //由于JQUERY EASYUI LINKBUTTON 对动态加载的事件不支持失效
            return false;
        }
        var rq = $("#rq").attr("value");
        rq = rq.replace(/\b(\w)\b/g, '0$1');
        if (!ok_check(rq)) {
            return false;
        }
        $('#ok').linkbutton('disable');
        $('#del').linkbutton('disable');
        $('#add').linkbutton('disable');
        //结算
        var zje = $.trim(document.getElementById("hj_je").innerHTML);
        if (zje == "" || zje == "&nbsp;") {
            zje = "0";
        }
        var url = "web_sp_cppos_js.aspx?zje=" + zje;
        var r = openModal(url, "", "dialogWidth=600px;dialogHeight=700px");
        if (r != null) {
            if (r == "") {
                //收款金额是0
            } else {
                r = eval("(" + r + ")");
            }
            var str = "";
            var mykey = document.getElementById("mykey").value;
            var djlx = 505;
            var bz = document.getElementById("bz").value;
            var ckid = document.getElementById("select_ck").value;
            var lx = document.getElementById("select_xsb").value;
            var bb = document.getElementById("select_bb").value;
            var zdr = document.getElementById("zdr").value;
            var yyy = document.getElementById("yyy").value;
            var thh_id = document.getElementById("thh_id").value;
            if (thh_id == "" || thh_id == null) { thh_id = "0"; }
            str += "declare @id int ;declare @mxid int;"
            if (mykey == "" || mykey == "0") {
                str += " declare @djh varchar(max);select @djh= case isnull(max(a.djh),'') when '' then '100001' else max(a.djh)+1 end  from _V_sp_cpposb a where a.djlx=" + djlx + " and CONVERT(varchar(7),a.rq,120)=CONVERT(varchar(7),'" + rq + "',120) "
                str += " insert into _V_sp_cpposb  ";
                str += "([xskhid] ";
                str += ",[zdr]";
                str += ",[zdrq]";
                str += ",[djh]";
                str += ",[bz]";
                str += ",[ckbs]";
                str += ",[djlx]";
                str += ",[ckid]";
                str += ",[rq]";
                str += ",[djbs]";
                str += ",[bb]";
                str += ",[yyy]";
                str += ",[thid]";
                str += ",[lx])";
                str += "VALUES";
                str += "(0 ";
                str += ",'" + zdr + "'";
                str += ",getdate() ";
                str += ",@djh ";
                str += ",'" + bz + "' ";
                str += ",1"; //扣库存标识
                str += "," + djlx + " ";
                str += "," + ckid + " ";
                str += ",'" + rq + "' ";
                str += ", 1"; //有效单,无效单标识
                str += "," + bb + " ";
                str += ",'" + yyy + "' ";
                str += ",'" + thh_id + "' ";
                str += "," + lx + ") ;  ";
                str += " set @id=SCOPE_IDENTITY();"
            } else {
                str += " select @id=" + mykey;
                str += " update _V_sp_cpposb set thid='" + thh_id + "', yyy='" + yyy + "', bz='" + bz + "',ckid=" + ckid + ",rq='" + rq + "',bb=" + bb + ",lx=" + lx + " where id=@id"
                str += " select a.cpid,b.ckid,c.cmid,c.sl*poslx.lx as sl,b.ckbs,b.djlx into #kcjs from _v_sp_cpposmxb a  inner join _V_sp_cpposb b on a.id=b.id  inner join _v_sp_cpposcmmxb c on c.id=b.id and c.mxid=a.mxid inner join v_sp_poslx poslx on poslx.id=b.lx where a.id=@id; ";

            }
            var cpid = ""; var cmid = ""; var mxid = ""; var thmxid = "";
            var sl = 0; var zk = 0; var dj = 0;
            var zk = 0; var lsj = 0;
            var fsbs = 0; //负数标识
            var jlbs = 0; //明细记录都没了
            for (var i = 0; i < getRowCount(); i++) {
                mxid = getValue(i, "mxid"); if (mxid.length == 0) { mxid = "0"; }
                thmxid = getValue(i, "thmxid"); if (thmxid.length == 0) { thmxid = "0"; }
                if ($(document.getElementById("tr_" + i)).attr("zfbs") == "0") {
                    cpid = getValue(i, "cpid");
                    cmid = getValue(i, "cmid");
                    sl = getValue(i, "sl"); if (sl.length == 0) { sl = "0"; }
                    dj = getValue(i, "dj"); if (dj.length == 0) { dj = "0"; }

                    zk = getValue(i, "zk"); if (zk.length == 0) { zk = "0"; }
                    lsj = getValue(i, "lsj"); if (lsj.length == 0) { lsj = "0"; }
                    if (sl < 0) { fsbs = 1; }
                    if (sl > 0) { jlbs += 1; }
                    if (mxid > 0) {
                        if (sl == 0) {
                            str += " delete _v_sp_cpposcmmxb  where id=@id and mxid=" + mxid;
                            str += " delete _v_sp_cpposmxb  where id=@id and mxid= " + mxid;
                        } else {
                            str += " update _v_sp_cpposmxb set zk=" + zk + ",lsj=" + lsj + ", cpid='" + cpid + "',dj='" + dj + "' where id=@id and mxid= " + mxid;
                            str += " update _v_sp_cpposcmmxb set cmid='" + cmid + "',sl='" + sl + "' where id=@id and mxid=" + mxid;
                        }
                    } else {
                        if (sl != 0) {
                            str += " insert _v_sp_cpposmxb (id,thmxid,cpid,dj,zk,lsj) values (@id,'" + thmxid + "','" + cpid + "','" + dj + "','" + zk + "','" + lsj + "') ";
                            str += " set @mxid=SCOPE_IDENTITY();"
                            str += " insert _v_sp_cpposcmmxb (id,mxid,cmid,sl) values(@id,@mxid,'" + cmid + "','" + sl + "')";
                        }
                    }
                } else {
                    //做了删除识的
                    if (mxid > 0) {
                        str += " delete _v_sp_cpposcmmxb  where id=@id and mxid=" + mxid;
                        str += " delete _v_sp_cpposmxb  where id=@id and mxid= " + mxid;
                    }
                }
            }

            //付款记录
            str += " delete from _v_sp_cpposjlb where id=@id";
            for (var i = 0; i < 50; i++) {
                if (r[i] != undefined) {
                    str += " insert _v_sp_cpposjlb (id,sklx,skje) values(@id,'" + r[i].id + "','" + r[i].je + "')";
                }
            }
            str += " execute p_SPKCJS_DJLX @id," + djlx + "  ; ";
            str += " select @id;"
            //document.getElementById("bz").value = str;return false;
            //alert(str);return false;
            if (fsbs == 1) {
                $.messager.alert('提示信息', '不允许开负数!', 'info', function () {
                    $('#ok').linkbutton('enable');
                    $('#del').linkbutton('enable');
                    $('#add').linkbutton('enable');
                });
            } else if (jlbs == 0) {
                $.messager.alert('提示信息', '没有可保存的记录!', 'info', function () {
                    $('#ok').linkbutton('enable');
                    $('#del').linkbutton('enable');
                    $('#add').linkbutton('enable');
                });
            } else {
                var r = myAjax(str, "on");
                if (r == -1) {
                    $.messager.alert('提示信息', '连接失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                        $('#del').linkbutton('enable');
                        $('#add').linkbutton('enable');
                    });
                } else {
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '保存成功!', 'info', function () {
                            $('#ok').linkbutton('enable');
                            $('#del').linkbutton('enable');
                            $('#add').linkbutton('enable');
                            if ($("#zt").attr("value") == "xg") {
                                $.messager.confirm('Confirm', '是否打印小票?', function (j) {
                                    if (j) {
                                        var xp_url = "web_sp_cppos_prt.aspx?mykey=" + r.msg;
                                        openModal(xp_url, "", "dialogWidth=600px;dialogHeight=700px");
                                    }
                                    window.returnValue = "ok";
                                    window.close();
                                });

                            } else {
                                $.messager.confirm('Confirm', '是否打印小票?', function (j) {
                                    if (j) {
                                        var xp_url = "web_sp_cppos_prt.aspx?mykey=" + r.msg;
                                        openModal(xp_url, "", "dialogWidth=600px;dialogHeight=700px");
                                    }
                                    myLoad(0, "add");
                                });
                            }
                        });
                    } else {
                        $.messager.alert('提示信息', r.msg, 'info', function () {
                            $('#ok').linkbutton('enable');
                            $('#del').linkbutton('enable');
                            $('#add').linkbutton('enable');
                        });
                    }
                }
            }



        } else {
            //直接退出或点取消
            $('#ok').linkbutton('enable');
            $('#del').linkbutton('enable');
            $('#add').linkbutton('enable');
        }
    }

    //清除所有记录,但并不清除主表记录    
    function del_click() {
        if ($("#del").linkbutton("options").disabled) {
            //由于JQUERY EASYUI LINKBUTTON 对动态加载的事件不支持失效
            return false;
        }
        var key = "table_content";
        var tab = document.getElementById(key);
        var b = "";
        tab.innerHTML = b;
        mytotal();
    }

    //beign选择销售别  
    function xsb_click() {
        if ($("#select_xsb").find("option:selected").attr("lx") == "-1" && $("#select_xsb").find("option:selected").attr("kzx") == "1") {
            //销售转退货
            if (getRowCount() != 0) {
                //如果有数据
                $.messager.confirm('提示信息', '确定要清除当前数据吗?', function (r) {
                    if (r) {                        
                        ChangCtrl("th")
                        $("#select_xsb").attr("nowv", $("#select_xsb").val()); ;
                    } else {
                        $("#select_xsb").val($("#select_xsb").attr("nowv"));
                    }
                });
            } else {
                ChangCtrl("th")
                $("#select_xsb").attr("nowv", $("#select_xsb").val());
            }
        } else {
            //退货转销售
            if (getRowCount() != 0) {
                //如果有数据
                $.messager.confirm('提示信息', '确定要清除当前数据吗?', function (r) {
                    if (r) {                       
                        ChangCtrl("add")
                        $("#select_xsb").attr("nowv", $("#select_xsb").val());
                    } else {
                        $("#select_xsb").val($("#select_xsb").attr("nowv"));                        
                    }
                });
            } else {
                ChangCtrl("add")
                $("#select_xsb").attr("nowv", $("#select_xsb").val());
            }
        }        

    }
    //end  选择销售别


    //除去控制根据BS来重置页面
    function ChangCtrl(bs) {
        $('#ok').linkbutton('enable');
        $('#del').linkbutton('enable');        
        document.getElementById("mykey").value = 0;
        $("#zt").attr("value",bs);
        if (bs == "add") {
            $('#add').linkbutton('enable');
            document.getElementById("thh").disabled = true;
            document.getElementById("hh").disabled = false;
            document.getElementById("thh").value = "";
            document.getElementById("thh_id").value = "";
            document.getElementById("select_xsb").disabled = false;
            if ($($("#select_xsb").find("option:selected")).attr("lx") != 1) {
                var cv = $("[LX=1]", $("#select_xsb"))[0].value; //随便找一个LX=1的值给他
                if ($($("#select_xsb").find("option:selected")).attr("kzx") == 0) {
                //新增情况，如果选择手工退货，那么就使用这个值
                    cv = $("#select_xsb ").val();
                }
                $("#select_xsb ").val(cv);
                $("#select_xsb").attr("nowv", cv);
            }
        } else if (bs == "xg") {
            $('#add').linkbutton('disable');
            document.getElementById("thh").disabled = true;
            document.getElementById("hh").disabled = false;
            document.getElementById("thh").value = "";
            document.getElementById("thh_id").value = "";
            document.getElementById("select_xsb").disabled = true;

        } else if (bs == "th") {
            $('#add').linkbutton('enable');
            document.getElementById("thh").disabled = false;
            document.getElementById("hh").disabled = true;
            document.getElementById("thh").value = "";
            document.getElementById("thh_id").value = "";
            document.getElementById("select_xsb").disabled = false;
        }
        del_click();
    }

    //bs==null ||bs=="" || bs ==undefined
    //mykey单据ID =0时,新增页面 
    //有值时,就是取单
    //bs="th" 开退货单! 只加载mykey 对应的明细,
    //bs=add 新增
    //bs=xg 修改
    function myLoad(mykey, bs) {
        //alert(mykey);alert(bs);
        if (bs == "add") {                      
            if ($("[LX=1]", $("#select_xsb")).length > 0) {
            //判断下拉框架中有没有LX=1的可选项
                ChangCtrl("add");                
            } else {
                $.messager.alert('提示信息', '[销售别]参数错误!', 'info', function () {
                    return false;
                });
            }
        } else {
            ChangCtrl(bs);
            var r;
            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/Pos_MyLoad',
                async: false,
                data: { mykey: mykey },
                error: function (e) { r = -1; },
                success: function (data) {
                    r = myAjaxData(data);
                }
            });

            if (r == -1) {
                $.messager.alert('提示信息', '参数错误!', 'info', function () {
                    $('#ok').linkbutton('disable');
                });
            } else {
                //alert(r.zb_str);
                //alert(r.mx_str);
                //alert(r.cm_str);
                if (r.zb_str == "" || r.mx_str == "" || r.cm_str == "") {
                    $.messager.alert('提示信息', '数据错误!', 'info', function () {
                        $('#ok').linkbutton('disable');
                    });
                } else {
                    if (r.zb_str != "" && bs == "xg") {
                        //主表有数据
                        document.getElementById("mykey").value = r.zb_str.id;
                        document.getElementById("thh_id").value = r.zb_str.thid;
                        document.getElementById("thh").value = r.zb_str.thh;
                        document.getElementById("yyy").value = r.zb_str.yyy;
                        document.getElementById("bz").value = r.zb_str.bz;
                        document.getElementById("rq").value = r.zb_str.rq;
                        $("#select_xsb").val(r.zb_str.lx);
                        $("#select_xsb").val(r.zb_str.lx);
                        $("#select_bb").val(r.zb_str.bb);
                        $("#select_ck").val(r.zb_str.ckid);
                    }
                    if (r.zb_str != "" && bs == "th") {
                        //如果是退货,
                        $("#thh").attr("value", r.zb_str.rqdjh); //退货的RQ+单据号
                        $("#thh_id").attr("value", mykey); //退货ID
                    }
                    if (r.mx_str != "") {
                        //明细有数据
                        var thml_str = "";
                        for (var z = 0; z < 100; z++) {
                            if (r.mx_str[z] == undefined) { continue; }
                            var w = "";
                            w += "cpid:'" + r.mx_str[z].cpid + "',";
                            w += "kh:'" + r.mx_str[z].kh + "',";
                            w += "pm:'" + r.mx_str[z].pm + "',";
                            w += "jhj:'" + r.mx_str[z].jhj + "',";
                            w += "lsj:'" + r.mx_str[z].lsj + "',";
                            w += "dj:'" + r.mx_str[z].dj + "',";
                            w += "pfj:'" + r.mx_str[z].pfj + "',";
                            w += "fzkh:'" + r.mx_str[z].fzkh + "',";
                            w += "cmzbid:'" + r.mx_str[z].cmzbid + "',";
                            w += "shid:'" + r.mx_str[z].shid + "',";
                            w += "ysmc:'" + r.mx_str[z].ysmc + "',";
                            var cmid; var cmmc; var sl;
                            if (r.cm_str != "") {
                                //尺码有数据
                                for (var h = 0; h < 100; h++) {
                                    if (r.cm_str[h] == undefined) { continue; }
                                    if (r.cm_str[h].id = r.zb_str.id && r.cm_str[h].mxid == r.mx_str[z].mxid) {
                                        cmid = r.cm_str[h].cmid;
                                        sl = r.cm_str[h].sl;
                                        cmmc = r.cm_str[h].cmmc;
                                        break;
                                    }
                                }
                            }
                            w += "cmid:'" + cmid + "',";
                            w += "cmmc:'" + cmmc + "',";
                            if (bs == "xg") {
                                w += "mxid:'" + r.mx_str[z].mxid + "',";
                            } else if (bs == "th") {
                                w += "mxid:0,";
                                w += "thmxid:'" + r.mx_str[z].mxid + "',";
                            }
                            w += "sl:'" + sl + "'";
                            w = "{" + w + "}";
                            thml_str += CreateTr(eval("(" + w + ")"), z, r.mx_str[z].zk, bs);
                        }

                        $("#table_content").append(thml_str);
                        mytotal();

                    } else {
                    }


                }
            }
        }

    }
    //货号选择
    function hh_change(event) {
        var kh = document.getElementById("hh").value;
        if (kh != null && kh != "") {
            var url = "../webpage/lss.aspx?wid=117&kh=" + kh;
            var r = openModal(url, "", "dialogWidth=600px;dialogHeight=700px;titlebar=0;toolbar=0;status=0");
            if (r != null && r != "") {
                var zk = document.getElementById("zk").value;
                if (zk == "") { zk = 10; }
                var tr = CreateTr(eval("(" + r + ")"), getRowCount(), zk, "add");
                $("#table_content").append(tr);
                document.getElementById("hh").value = "";
                mytotal();

            } else {
                $.messager.alert('提示信息', '没有选择产品!', 'info', function () {
                    document.getElementById("hh").value = "";
                    $("#hh").focus();
                });
            }
        }

    }
    //创建一TABLE中的一行
    //bs=dj 说明是退货

    //bs="th" 开退货单! 
    //bs=xg 修改
    //bs=add 新增
    function CreateTr(r, row, zk, bs) {
        //r=选择的某个货号的某个尺码
        //row 行号
        var dj;
        var readonly = "";
        var thmxid = 0;
        if (bs == "add") {
            dj = r.lsj * zk / 10;
            thmxid = 0;
        } else if (bs == "xg") {
            dj = r.dj;
            thmxid = 0;
        } else if (bs == "th") {
            dj = r.dj;
            thmxid = r.thmxid;
            readonly = " readonly ";
        }

        var str_r = "<tr id=\"tr_" + row + "\" row=" + row + " zfbs=0>";
        str_r += "<td class='chh' id=\"td_" + row + "_kh\"  >" + r.kh + "</td>";
        str_r += "<td class='cpm'>" + r.pm + "</td>";
        str_r += "<td class='cys'>" + r.ysmc + "</td>";
        str_r += "<td class='ccm'>" + r.cmmc + "</td>";
        str_r += "<td class='csl'><input class='p100' " + readonly + " onchange=\"myc('" + row + "','sl')\" type=\"text\" id=\"" + row + "_sl\" value=" + r.sl + "  /></td>";
        str_r += "<td class='clsj' id=\"td_" + row + "_lsj\" >" + r.lsj + "</td>";
        str_r += "<td class='czk'><input class='p100' " + readonly + " onchange=\"myc('" + row + "','zk')\" type=\"text\" id=\"" + row + "_zk\" value=" + zk + "  /></td>";
        str_r += "<td class='cdj' id=\"td_" + row + "_dj\" ><input class='p100' " + readonly + " onchange=\"myc('" + row + "','dj')\" type=\"text\" id=\"" + row + "_dj\" value=" + dj + "  /> </td>";

        str_r += "<td class='cje' id=\"td_" + row + "_je\" >" + r.sl * dj + "</td>";
        str_r += "<td style='text-align:left'><a href=\"#\" onclick=\"godel(" + row + ",this)\" >删除</a>";
        str_r += "<input type=\"hidden\" id=\"" + row + "_cpid\" value=\"" + r.cpid + "\" />";
        str_r += "<input type=\"hidden\" id=\"" + row + "_cmid\" value=\"" + r.cmid + "\" />";
        str_r += "<input type=\"hidden\" id=\"" + row + "_mxid\" value=" + r.mxid + " />";
        str_r += "<input type=\"hidden\" id=\"" + row + "_thmxid\" value=" + thmxid + " />";
        str_r += "</td>";
        str_r += "</tr>";
        return str_r;
    }

    //zd 字段 row 行  
    //修改数量,或单价  
    function myc(row, zd) {
        if (zd == "sl" || zd == "zk" || zd == "dj") {
            if (zd == "dj") {
                setValue(row, 'zk', ForDight(Number(getValue(row, 'dj')) / Number(getValue(row, 'lsj')) * 10, 2));
            } else {
                setValue(row, 'dj', ForDight(Number(getValue(row, 'lsj')) * Number(getValue(row, 'zk')) * 0.1, 2));
            }
            setValue(row, 'je', ForDight(Number(getValue(row, 'sl')) * Number(getValue(row, 'dj')), 2));
            mytotal();
        }
    }

    //计算总金额总数量
    function mytotal() {
        var sl = 0; var je = 0;
        for (var i = 0; i < getRowCount(); i++) {
            if ($(document.getElementById("tr_" + i)).attr("zfbs") == "0") {
                sl += Number(getValue(i, 'sl'));
                je += Number(getValue(i, 'je'));
            }
        }
        document.getElementById("hj_sl").innerHTML = sl;
        document.getElementById("hj_je").innerHTML = je;
    }
    //计算总行数
    function getRowCount() {
        var key = "table_content";
        var tab = document.getElementById(key);
        //表格行数
        return tab.rows.length;
    }

    //删除
    function godel(row, obj) {
        if (obj.innerHTML == "删除") {
            $.messager.confirm('提示信息', '确定要删除吗?', function (r) {
                if (r) {
                    //将货号后面加上[做废]
                    setValue(row, 'kh', getValue(row, 'kh') + "[做废]");
                    obj.innerHTML = "恢复";
                    $(document.getElementById("tr_" + row)).attr("zfbs", 1);
                    mytotal();
                }
            });
        } else {
            //恢复
            $.messager.confirm('提示信息', '确定要恢复吗?', function (r) {
                if (r) {
                    //将货号后面加上[做废]
                    setValue(row, 'kh', getValue(row, 'kh').replace("[做废]", ""));
                    obj.innerHTML = "删除";
                    $(document.getElementById("tr_" + row)).attr("zfbs", 0);
                    mytotal();
                }
            });
        }
    }

    //根据行号和字段来取值
    function getValue(row, zd) {
        var r = "";
        if (zd == "dj" || zd == "sl" || zd == "zk" || zd == "cpid" || zd == "mxid" || zd == "cmid") {
            r = document.getElementById(row + "_" + zd).value;
        } else if (zd == "je" || zd == "lsj" || zd == "kh") {
            r = document.getElementById("td_" + row + "_" + zd).innerHTML;
        }
        return r;
    }

    //根据行号字段来设置值
    function setValue(row, zd, v) {
        if (zd == "dj" || zd == "sl" || zd == "zk") {
            document.getElementById(row + "_" + zd).value = v;
        } else if (zd == "je" || zd == "lsj" || zd == "kh") {
            document.getElementById("td_" + row + "_" + zd).innerHTML = v;
        }
    }
</script>
