import axios from 'axios'

// Add a request interceptor
axios.interceptors.request.use(function (config) {
  // console.log(config)
  var token = localStorage.getItem('token')
  config.headers['Authorization'] = token

  // Do something before request is sent
  return config
}, function (error) {
  // Do something with request error
  return Promise.reject(error)
})

export const login = params => {
  return axios.get('/login/' + params.password + '/' + params.username)
}
