import Vue from 'vue'
import Router from 'vue-router'
 
import Login from '@/components/Utils/Login'
import Account from '@/components/Utils/Account'
import Menu from '@/components/Utils/Menu'
import Empty from '@/components/Utils/Empty'

// 这里用到了webpack2的import()它会返回一个promise
let SectionMain = () => import('@/components/Section/Main')

Vue.use(Router)

export default new Router({
  routes: [
     
    
       
    {
      path: '/Section',
      default: 'SectionMain',
      component: SectionMain,
      meta:{         
        requireAuth:true ,
        title:"S"
      }
    } ,    
   
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
      path: '/',
      default: 'Empty',
      component: Empty,
    },      
          
  ]
})
