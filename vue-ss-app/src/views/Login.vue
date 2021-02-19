<template>
  <div class="wrap">
    <div style="margin: 50px" v-show="show">
      <h2 style="text-align: center">Login In</h2>
      <van-cell-group>
        <van-field
          v-model="mdata.username"
          label="用户名"
          placeholder="用户名"
          :rules="[{ required: true, message: '请填写用户名' }]"
        />
        <van-field
          type="password"
          v-model="mdata.userpass"
          label="密码"
          placeholder="密码"
          :rules="[{ required: true, message: '请填写密码' }]"
        />
      </van-cell-group>
      <van-notice-bar :text="this.mdata.errmsg" />
      <div style="margin: 16px">
        <van-button size="small" @click="login" type="primary"
          >Login</van-button
        >
      </div>
    </div>
  </div>
</template>

<script>
import "vant/lib/index.css";
import { getWsResult } from "@/assets/js/utils";
import myStore from "@/components/Utils/Store";
import { Cell as VanCell } from "vant";
import { CellGroup as VanCellGroup } from "vant";
import { Button as VanButton } from "vant";
import { Field as VanField } from "vant";
import { Popup as VanPopup } from "vant";
import { Icon as VanIcon } from "vant";
import { NoticeBar as VanNoticeBar } from "vant";
export default {
  name: "Login",
  components: {
    VanButton,

    VanField,
    VanPopup,

    VanIcon,

    VanNoticeBar,
    VanCellGroup,
    VanCell,
  },
  data() {
    return {
      mdata: {
        username: "",
        userpass: "",
        errmsg: "",
      },
      show: true,
    };
  },
  methods: {
    init() {},

    login() {
      if (this.mdata.username.length == 0 || this.mdata.userpass == 0) return;
      let param = new Object();
      param.ur = this.mdata.username;
      param.ps = this.mdata.userpass;
      this.show = false;
      this.$axiosPost.post(APIUTL + "/Login/json", param).then((response) => {
        let res = getWsResult(response);
        if (res.Errcode == 0) {
          Object.assign(myStore.userInfo, res.Data);
          // this.show = true;
          // console.log(myStore.userInfo);
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


<style scoped>
.wrap {
  height: calc(100%);
  overflow-y: auto;
}
</style>