import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import Login from '@/views/login/Index'
import Account from '@/views/account/Index'
import Menu from '@/views/menu/Index'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/account',
      name: 'Account',
      component: Account,
      meta: {
        // 在需要登录的路由的meta中添加响应的权限标识
        requireAuth: true
      }
    },
    {
      path: '/menu',
      name: 'Menu',
      component: Menu,
      meta: {
        // 在需要登录的路由的meta中添加响应的权限标识
        requireAuth: true
      }
    }
  ]
})
