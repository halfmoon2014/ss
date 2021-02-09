export function getUrlKey(name, url) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(url) || [, ""])[1].replace(/\+/g, '%20')) || null
}
export function loadJS(vue, callBack) {
    // console.log(process)
   
    if (vue.$data.mdata.vueExtJSKey) {
  
        //加载到页面上JS的id
        var vueKey = "vueKey";
        if (process.env.type == '231') 
            var scr = 'http://192.168.35.231/bb/bbjs/vue_' + vue.$data.mdata.vueExtJSKey + '.js';
        else        
            var scr = 'http://tm.lilanz.com/oa/res/js/vueextjs/vue_' + vue.$data.mdata.vueExtJSKey + '.js';
        
        //如果存在,就移除掉
        if (document.getElementById(vueKey))  document.getElementById(vueKey).parentNode.removeChild(document.getElementById(vueKey));
        
        const s = document.createElement('script');
        s.id = vueKey;
        s.type = 'text/javascript';
        s.src = scr
        document.body.appendChild(s);

        //创建定时器,加载完后回调,增加JS后,需要加载外部JS,所以有时间延迟
        var tryCount=0;//防止无限重试
        var intervalLoadJS = setInterval(() => {            
            if (window.VueExtJS == undefined) {        
                tryCount++;
                //100次 100MS =10秒 ,如果超过10秒后,调用者需要暂停流程
                if(tryCount>=99){
                    console.log("extJS is not ready;stop after  "+tryCount+"count try")
                    clearInterval(intervalLoadJS);                    
                    callBack && callBack({"errcode":1,"data":"extJS is not ready;stop after try"});
                }
            } else {
                //清除定时器
                clearInterval(intervalLoadJS);       
                //初始化外部JS
                window.VueExtJS.init && window.VueExtJS.init(vue);    
                //回调            
                callBack && callBack({"errcode":0,"data":"extJS is ready"});
            }
        }, 100);
    } else {
        callBack && callBack({"errcode":0,"data":"not contain extJS"});
    }
}
