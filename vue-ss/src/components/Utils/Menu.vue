<template>
  <div style="margin: 50px" v-show="show">
       <a-list item-layout="horizontal" :data-source="mdata.menuList">
      <a-list-item slot="renderItem" slot-scope="item" @click="menu(item)" >
        <a-list-item-meta :description="item.mc">          
        </a-list-item-meta>
      </a-list-item>
      
    </a-list>
    
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
        menuList: [],
      },
      show: true,
      tzid:myStore.userInfo.Tzid,
      userid:myStore.userInfo.Userid,
    };
  },
  methods: {
    init() {
     this.mdata.menuList.push({mc:'挂镀入库模块',path:'/guadrk'})
     this.mdata.menuList.push({mc:'滚镀入库模块',path:'/gundrk'})
    },

    menu(item) {      
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



