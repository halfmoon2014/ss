(function (win) {
    //先获得页面中baseUrl数值
    var baseUrl = document.getElementById('jsApp').getAttribute('data-cdn');
    var from = document.getElementById('jsApp').getAttribute('data-from');
    var ver = document.getElementById('jsApp').getAttribute('data-ver') ||"regular";
    var config = {
        baseUrl: baseUrl,
        urlArgs: 'ver=' + ver,
        waitSeconds:0,
        paths: {
            jquery: "jquery/1.12.4/jquery.min",
            utils: "utilsA",
            myweb: "myjs/mywebA",
            bootstrap: 'bootstrap/3.3.7/bootstrap.min',
            easyui: "jey/jquery.easyui.min",
            zhCN: "jey/easyui-lang-zh_CN",
            easyuiAdd: "jey/jquery.easyui.addA",
            sweetalert: "sweetalert/sweetalert.min",
            swalProcess: "sweetalert/swalProcessA",
            progressDefender: "progressDefenderA",            
            xtsz: "web_xtsz/xtsz",

            login: 'login3/login3',
            choosetz3: 'choosetz/choosetz3',
            web_ls_cpdaxx: "web_ls/web_ls_cpdaxx",
            web_ls_cpdaxx_add: "web_ls/web_ls_cpdaxx_add",
            userhelp: "userhelp/userhelp",
            web_xtsz_main: "web_xtsz/web_xtsz_main",
            web_xtsz_main_add: "web_xtsz/web_xtsz_main_add",
            web_xtsz_main_edit: "web_xtsz/web_xtsz_main_edit",
            web_xtsz_main_edit_help: "web_xtsz/web_xtsz_main_edit_help",
            web_xtsz_main_edit_sjy: "web_xtsz/web_xtsz_main_edit_sjy",
            web_xtsz_main_edit_zdwh: "web_xtsz/web_xtsz_main_edit_zdwh",
            web_xtsz_main_edit_js: "web_xtsz/web_xtsz_main_edit_js",
            web_xtsz_main_edit_z: "web_xtsz/web_xtsz_main_edit_z"
        },
        shim: {
            'jquery': {
                exports: '$'
            },
            'bootstrap': {
                deps: ['jquery'],
                exports: '$'
            },
            'zhCN': ['jquery'],
            'easyui': ['jquery', 'zhCN'],            
            'swalProcess': {
                deps: ['sweetalert']                
            }
        }
    };
    require.config(config);
    require([from], function (entry) {
        entry.start();
    });

})(window);