(function (win) {
    //先获得页面中baseUrl数值
    var baseUrl = document.getElementById('jsApp').getAttribute('data-baseurl');
    var from = document.getElementById('jsApp').getAttribute('data-from');
    var ver = document.getElementById('jsApp').getAttribute('data-ver') ||"regular";
    
    var config = {
        baseUrl: baseUrl,
        urlArgs: 'ver=' + ver,
        paths: {
            jquery: "jquery/1.12.4/jquery.min",
            utils: "utilsA",
            myweb: "myjs/mywebA",
            bootstrap: 'bootstrap/3.3.7/bootstrap.min',
            easyui: "jey/jquery.easyui.min",
            zhCN: "jey/easyui-lang-zh_CN",
            sweetalert: "sweetalert/sweetalert.min",
            progressDefender: "progressDefenderA",

            login: 'login3/login3',
            choosetz3: 'choosetz/choosetz3',
            web_ls_cpdaxx:"web_ls/web_ls_cpdaxx"
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
            'easyui': ['jquery']
        }
    };
    require.config(config);
    require([from], function (entry) {
        entry.start();
    });

})(window);