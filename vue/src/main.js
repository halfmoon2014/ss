// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import store from './store'
import router from './router'
import Axios from 'axios'
// 导入ElementUI
import ElementUI from 'element-ui'
// 动态换肤
import 'element-ui/lib/theme-chalk/index.css'
// 使用scss

// 使用ElementUI
Vue.use(ElementUI)

Vue.config.productionTip = false

// Axios.defaults.baseURL = process.env.API_ROOT

Vue.prototype.$axios = Axios
Axios.defaults.baseURL = '/api'
Axios.defaults.headers.post['Content-Type'] = 'application/json'
router.beforeEach((to, from, next) => {
  var token = localStorage.getItem('token')
  var account = localStorage.getItem('accountID')
  // console.log(331)
  if (to.path === '/login') {
    if (token) {
      next({
        path: '/account'
      })
    } else {
      next()
    }
  } else if (to.path === '/account') {
    if (!token) {
      next({
        path: '/login'
      })
    } else if (account) {
      next({
        path: '/menu'
      })
    } else {
      next()
    }
  } else if (to.meta.requireAuth) {
    // console.log(token)
    // console.log(account)
    if (!token) {
      next({
        path: '/login'
      })
    } else if (!account) {
      next({
        path: '/account'
      })
    } else {
      // console.log(33)
      next()
    }
  } else {
    next()
  }
})
/* eslint-disable no-new */
new Vue({
  el: '#app',
  store,
  router,
  components: { App },
  template: '<App/>'
})
