<template>
  <div style="margin:50px;" v-show="show">
        <div style="margin: 16px">
      <van-button size="small" @click="login" type="primary">查询</van-button>
    </div>
    <a-row type="flex"  justify="center">
      <a-col  :span="24">
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
            <a-input type="password" v-model="mdata.userpass" />
          </a-form-model-item>
        </a-form-model>
        <div class="errtips">{{ this.mdata.errmsg }}</div>
        <div style="text-align: right">
          <a-button @click="login" icon="login">Login</a-button>
        </div>
      </a-col>
    </a-row>
  </div>
</template>

<script>
import { getWsResult } from "@/assets/js/utils";
import myStore from "@/components/Utils/Store";
import { Button as VanButton } from "vant";
export default {
  name: "Login",
  components: {
    VanButton,
  },
  data() {
    return {
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      mdata: {
        username: "",
        userpass: "",
        errmsg: "",
      },
      show: true, 
    };
  },
  methods: {
    init() {
      
    },
   
    login() {
      if(this.mdata.username.length ==0 || this.mdata.userpass==0 ) return;
      let param = new Object();      
      param.ur = this.mdata.username;
      param.ps = this.mdata.userpass;   
      this.show = false;   
      this.$axiosPost.post(APIUTL+"/Login/json", param).then((response) => {        
        let res=getWsResult(response);
        if (res.Errcode == 0) {
          
          Object.assign(myStore.userInfo, res.Data)       
          this.show = true;
          console.log(myStore.userInfo)
          this.$router.push({ path: "/Account" });
        } else {
          console.log(res);
          this.mdata.errmsg = res.Errmsg;          
          this.show = true;
        }
      });      
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


