<template>
  <div style="margin: 50px" v-show="show">
    {{userid}}
    {{tzid}}
    
  </div>
</template>

<script>
import { getWsResult } from "@/assets/js/utils";
import myStore from "@/components/Utils/Store";
export default {
  name: "Menu",
  components: {},
  data() {
    return {
      mdata: {
       
      },
      show: true,
      tzid:myStore.userInfo.Tzid,
      userid:myStore.userInfo.Userid,
    };
  },
  methods: {
    init() {
     
    },

    choose(item) {      
      let param = new Object();
      param.token = myStore.userInfo.Token;
      param.updata = false;
      param.tzid = item.id;

      this.show = false;
      this.$axiosPost.post(APIUTL + "/ChooseTz/json", param).then((response) => {
        let res = getWsResult(response);
        console.log(res)
        if (res.Errcode == 0) {
          console.log(myStore.userInfo)
          myStore.userInfo.Tzid=item.tzid
          console.log(myStore.userInfo)
          this.show = true;
          //this.$router.push({ path: getUrlKey("path", window.location.href) });
        } else {
          // console.log(res);
          // this.mdata.errmsg = res.Errmsg;
          // this.loading = false;
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
</style>



