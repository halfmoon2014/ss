import Vue from 'vue'
import Router from 'vue-router'

// 这里用到了webpack2的import()它会返回一个promise  
let Login = () => import('@/views/Login')
let Empty = () => import('@/views/Empty')
let Account = () => import('@/views/Account')
let Menu = () => import('@/views/Menu')
let Guadrk = () => import('@/views/Guadrk')

Vue.use(Router)

export default new Router({
  routes: [       
    {
      path: '/Login',
      default: 'Login',
      component: Login,
    } ,   
    {
      path: '/Account',
      default: 'Account',
      component: Account,
      meta:{         
        requireAuth:true ,
        title:"Account"
      }
    } ,    
    {
      path: '/Menu',
      default: 'Menu',
      component: Menu,
      meta:{         
        requireAuth:true ,
        title:"Menu"
      }
    } ,        
         
    {
      path: '/Guadrk',
      default: 'Guadrk',
      component: Guadrk,
      meta:{         
        requireAuth:true ,        
      }
    },   
    {
      path: '/',
      default: 'Empty',
      component: Empty,
    },      
       
  ]
})
