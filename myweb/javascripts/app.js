﻿(function (win) {
    //先获得页面中baseUrl数值
    var baseUrl = document.getElementById('jsApp').getAttribute('data-jscdn');
    var cssCDN = document.getElementById('jsApp').getAttribute('data-csscdn');
    var from = document.getElementById('jsApp').getAttribute('data-from');
    var ver = document.getElementById('jsApp').getAttribute('data-ver') ||"regular";
    var config = {
        baseUrl: baseUrl,
        urlArgs: 'ver=' + ver,
        waitSeconds:0,
        paths: {
            jquery: "jquery/1.12.4/jquery.min",
            utils: "myjs/utilsA",
            myweb: "myjs/mywebA",
            bootstrap: 'bootstrap/3.3.7/bootstrap.min',
            easyui: "jey/jquery.easyui.min",
            zhCN: "jey/easyui-lang-zh_CN",
            easyuiAdd: "jey/jquery.easyui.addA",
            sweetalert: "sweetalert/sweetalert.min",
            swalProcess: "sweetalert/swalProcessA",
            progressDefender: "myjs/progressDefenderA",            
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
            web_xtsz_main_edit_z: "web_xtsz/web_xtsz_main_edit_z",
            content_menu3: "content_menu3/content_menu3",
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
            },
            'login': {
                deps: [                    
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/loading/loading.css',
                    'css!' + cssCDN+'/login3/signin.css',//注意顺序,最下面这个样式作用最大,会覆盖前面的
                ]
            },
            'choosetz3': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/loading/loading.css',
                    'css!' + cssCDN+'/choosetz/choosetz.css'
                ]
            },
            'web_xtsz_main_edit': {
                deps: [
                    'css!' + cssCDN+'/jey/ui-pepper-grinder/easyui.css',
                    'css!' + cssCDN+'/jey/icon.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/web_xtsz/web_xtsz_main_edit.css',
                ]
            },
            'web_xtsz_main_edit_help': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main_edit_sjy': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main_edit_zdwh': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/web_xtsz/web_xtsz_main_edit_zdwh.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main_edit_js': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main_edit_z': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/web_xtsz/web_xtsz_main_edit_z.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main_add': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',
                    'css!' + cssCDN+'/bootstrap/userplatform/sticky-footer-navbar.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
            'web_xtsz_main': {
                deps: [
                    'css!' + cssCDN+'/jey/ui-pepper-grinder/easyui.css',
                    'css!' + cssCDN+'/jey/icon.css',
                    'css!' + cssCDN+'/sweetalert/sweetalert.css',
                ]
            },
            'content_menu3': {
                deps: [
                    'css!' + cssCDN+'/bootstrap/3.3.7/css/bootstrap.min.css',                
                    'css!' + cssCDN+'/content_menu3/content_menu3.css',
                    'css!' + cssCDN+'/loading/loading.css',
                ]
            },
        },
        map: {
            '*': {
                'css': 'lib/css'
            }
        }
    };
    require.config(config);
    require([from], function (entry) {
        entry.start();
    });

})(window);