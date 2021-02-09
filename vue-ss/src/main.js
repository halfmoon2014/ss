// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import status from '@/components/Utils/Store'; // 引用模
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import VConsole from 'vconsole/dist/vconsole.min.js' //import vconsole
// 导入axios
import axios from "axios"
// 导入babel-polyfill兼容IE
import 'babel-polyfill'

// let vConsole = new VConsole() // 初始化
 
Vue.prototype.$status = status; // 挂载到 Vue 实例上
const http = axios.create({
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded',
  },
})

const httpPost = axios.create({
  headers: {
    'Content-Type': 'application/json',
  },
})

http.interceptors.request.use(config => {
  if (config.method === 'get') {
    //  给data赋值以绕过if判断
    config.data = true 
  }

  config.headers['Content-Type'] = 'application/x-www-form-urlencoded'
  return config
})

httpPost.interceptors.request.use(config => {
  config.headers['Content-Type'] = 'application/json'
  return config
})

//需求点
//1 [开发环境]分测试和正式,  目地是可以在开发环境连接正式环境
//process.env.type=231 就是测试,
//2.编译分测试和正式,因为连接不一样,具体上传文件TM下是不行,

// 设置基础URL地址 项目放在tm域名下，访问后台的时候用这个路径
//本机测试的时候会跳转到http://192.168.35.231/QYWX/project/ErpScan
let APIUTL = '../ErpScan/HttpRequestSkill.ashx'
if (process.env.type == '10') {
  APIUTL= '../ErpScan10/HttpRequestSkill.ashx'
}
window.APIUTL = APIUTL;

//上传图片路径，主要处理数据类型=multipart/form-data,HttpRequestSkill是10和tm下是一致的， 上传图只有tm下用
//本机测试的时候会跳转http://192.168.35.231/QYWX/project/ErpScan
const APIUTLFile = '../ErpScan/HttpRequestSkillFile.ashx'
window.APIUTLFile = APIUTLFile;

const APIUTLOuth = '../MobileScan/checkScan.ashx'
window.APIUTLOuth = APIUTLOuth;

//从哪里获取数据标签信息，正式的时候从tm下面，测试的时候从231
let NetUrl = 'http://tm.lilanz.com/'
console.log(process.env.type)
if (process.env.type == '231') {
  NetUrl='http://192.168.35.231/'
}
window.NetUrl = NetUrl;

//上传路径，tm下面不能访问myupload的只能跳到9001
let NetUrlUpload = 'http://webt.lilang.com:9001/'
if (process.env.type == '231') {
  NetUrlUpload='http://192.168.35.231/'
}
window.NetUrlUpload = NetUrlUpload;

// 使用axios
Vue.prototype.$axios = http
Vue.prototype.$axiosPost = httpPost

Vue.config.productionTip = false
Vue.use(Antd);
// console.log(window.location.href)
router.beforeEach((to,from,next)=>{
  /* 路由发生变化修改页面title */
  if (to.meta.title) {
    document.title = to.meta.title
  }
	if(to.meta.requireAuth){    
		if(JSON.stringify(status.userInfo) !="{}"){
			next()
		}else{
      let toQuery = JSON.parse(JSON.stringify(to.query));            
      toQuery.path=to.fullPath;            
			next({path:'/Login',query: toQuery})
		}
	}else{
		next()
	}
})
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
