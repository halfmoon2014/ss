import Vue from 'vue'
import Router from 'vue-router'
 
import Login from '@/components/Utils/Login'

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
      path: '/',
      default: 'Empty',
      component: Empty,
    },      
          
  ]
})
