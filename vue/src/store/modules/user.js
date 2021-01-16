import { login } from '@/api'
const user = {
  state: {
    token: '',
    accountID: 0,
    roles: null,
    isMasterAccount: true
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = 'Bearer ' + token
    },
    SET_ACCOUNTID: (state, accountID) => {
      state.accountID = accountID
    }
  },
  actions: {
    // 登录
    Login ({
      commit
    }, userInfo) {
      return new Promise((resolve, reject) => {
        login(userInfo).then(x => {
          if (x.status === 200) {
            if (x.data.errcode === 0) {
              // console.log(x.data.errcode)
              const tokenV = x.data.data.token
              commit('SET_TOKEN', tokenV)
              localStorage.setItem('token', tokenV)
              // document.cookie = `AuthInfo=Bearer ${tokenV};path:/`
            }
            resolve(x)
          }
        }).catch(error => {
          // console.log('登录失败')
          reject(error)
        })
      })
    },
    Account ({commit}, data) {
      commit('SET_ACCOUNTID', data)
      localStorage.setItem('accountID', data)
    },
    CleanLogin ({commit}, tag) {
      // console.log(2)
      commit('SET_TOKEN', '')
      commit('SET_ACCOUNTID', 0)
      localStorage.removeItem('token')
      localStorage.removeItem('accountID')
    }
  }
}
export default user
