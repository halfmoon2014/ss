<template>
  <div class="login-container">
    <el-form class="login-form" autoComplete="on" :model="loginForm" :rules="loginRules" ref="loginForm" label-position="left">
      <h2>Please sign in</h2>
      <span class="el-icon-user"></span>
      <el-form-item  prop="username">
        <el-input name="username" type="text" v-model="loginForm.username" autoComplete="on" placeholder="用户名" />
      </el-form-item>
      <span class="el-icon-key"></span>
      <el-form-item prop="password">
        <el-input  name="password" :type="pwdType" @keyup.enter.native="handleLogin" v-model="loginForm.password" autoComplete="on"
          placeholder="密码"></el-input>
      </el-form-item>
      <div class="tips">
        <el-checkbox v-model="loginForm.checked">记住我的账号</el-checkbox>
        <el-alert
          v-if="errorShow"
          :title="errorTitle"
          ref="error"
          type="error">
        </el-alert>
      </div>
      <el-form-item>
        <el-button  class="el-icon-switch-button" type="primary" size="medium" style="width:100%;" :loading="loading" @click.native.prevent="handleLogin">
          登 陆
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { isvalidUsername } from '@/utils/validate'
export default {
  name: 'Login',
  data () {
    const validateUsername = (rule, value, callback) => {
      if (!isvalidUsername(value)) {
        callback(new Error('请输入正确的用户名'))
      } else {
        callback()
      }
    }
    const validatePass = (rule, value, callback) => {
      if (value.length < 5) {
        //  callback(new Error('密码不能小于5位'))
        callback()
      } else {
        callback()
      }
    }
    return {
      loginForm: {
        username: '',
        password: '',
        checked: false
      },
      loginRules: {
        username: [{ required: true, trigger: 'blur', validator: validateUsername }],
        password: [{ required: true, trigger: 'blur', validator: validatePass }]
      },
      loading: false,
      pwdType: 'password',
      errorShow: false,
      errorTitle: ''
    }
  },
  methods: {
    handleLogin () {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          // this.$router.push({ path: '/account' })
          this.loading = true
          // console.log(this.$store)
          this.$store.dispatch('Login', this.loginForm).then((x) => {
            console.log(this, x.data)
            this.loading = false
            if (x.data.errcode === 0) {
              this.$router.push({
                name: 'Account',
                params: {
                  data: x.data.data
                }
              })
            } else {
              this.errorTip(true, x.data.errmsg)
            }
          }).catch(() => {
            this.loading = false
            this.errorTip(true, '登陆失败')
          })
        } else {
          // console.log('error submit!!')
          return false
        }
      })
    },
    errorTip (show, title) {
      this.errorShow = show
      this.errorTitle = title
    }

  }
}
</script>

<style lang='scss' scoped>
.login-container {
  padding-top: 40px;
  max-width: 330px;
  margin: 0 auto;
  .tips {
    padding-bottom: 10px;
  }
}
</style>
