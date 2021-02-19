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

let APIUTL = '../webuser/Ws.asmx'
window.APIUTL = APIUTL;
// 使用axios
Vue.prototype.$axios = http
Vue.prototype.$axiosPost = httpPost

Vue.config.productionTip = false
Vue.use(Antd);
console.log(status.userInfo)  
router.beforeEach((to,from,next)=>{
  /* 路由发生变化修改页面title */
  if (to.meta.title) {
    document.title = to.meta.title
  }
	if(to.meta.requireAuth){    
		if(JSON.stringify(status.userInfo) !="{}"){
			next()
		}else{
      // let toQuery = JSON.parse(JSON.stringify(to.query));            
      // toQuery.path=to.fullPath; 
			next({path:'/Login'})
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
