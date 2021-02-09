<template>
  <div v-show="show">
    <a-row type="flex" justify="center">
      <a-col  style="width:500px;" :span="24">
        <h2 style="text-align: center">Login In</h2>
        <a-form-model
          :model="mdata"
          :label-col="labelCol"
          :wrapper-col="wrapperCol"
        >
          <a-form-model-item label="用户名">
            <a-input v-model="mdata.username" />
          </a-form-model-item>

          <a-form-model-item label="密 码">
            <a-input v-model="mdata.userpass" />
          </a-form-model-item>
        </a-form-model>
        <div class="errtips">{{ this.mdata.errmsg }}</div>
        <div style="text-align: right">
          <!-- <el-button type="primary" @click="doLogin" :loading="loading">{{this.mdata.loading ? '登录中..':'登 录'}}</el-button> -->
        </div>
      </a-col>
    </a-row>
  </div>
</template>

<script>
import { getUrlKey } from "@/assets/js/utils";
import myStore from "@/components/Utils/Store";
export default {
  name: "Login",
  components: {},
  data() {
    return {
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      mdata: {
        username: "",
        userpass: "",
        errmsg: "",
      },
      show: false, //这个用来自动登陆的,所以界面不用展示
    };
  },
  methods: {
    init() {
      this.imLogin();
    },
    doLogin() {},
    imLogin() {
      //取IM中身份
      let param = new Object();
      param.action = "getUserinfo";
      param.token = getUrlKey("apptoken", window.location.href);
      param.dept = "";
      param.Parameter = [1];
      param.orgid = 0;
      this.$axiosPost.post(APIUTLOuth, param).then((response) => {
        if (response.data.code == 200) {
          myStore.userInfo = response.data.data;
          // console.log(myStore.userInfo);
          this.show = false;
          this.$router.push({ path: getUrlKey("path", window.location.href) });
        } else {
          console.log(response.data.message);
          this.mdata.errmsg = response.data.message;
          this.loading = false;
          this.show = true;
        }
      });
      //取IM中身份 end
    },
  },
  mounted() {},
  watch: {},
  computed: {},
  created() {
    this.init();
  },
};
</script>

<style>
.login_dialog {
  width: 100%;
  height: 100%;
  margin-top: 0px !important;
}
</style>
