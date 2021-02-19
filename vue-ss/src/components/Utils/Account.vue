<template>
  <div style="margin: 50px" v-show="show">
    <a-list item-layout="horizontal" :data-source="mdata.accountList">
      <a-list-item slot="renderItem" slot-scope="item" @click="choose(item)" >
        <a-list-item-meta :description="item.tzmc"> 
          <a slot="title" href="#">{{ item.sm }}</a>
        </a-list-item-meta>
      </a-list-item>
      
    </a-list>
  </div>
</template>

<script>
import { getWsResult } from "@/assets/js/utils";
import myStore from "@/components/Utils/Store";
export default {
  name: "Account",
  components: {},
  data() {
    return {
      mdata: {
        accountList: [],
      },
      show: true,
    };
  },
  methods: {
    init() {
      let param = new Object();
      param.token = myStore.userInfo.Token;
      this.show = false;
      this.$axiosPost.post(APIUTL + "/TzList/json", param).then((response) => {
        let res = getWsResult(response);
        // console.log(res);
        if (res.Errcode == 0) {
          let disposeData=[];
          for(let i=0;i<res.Data.length;i++){
            let isExt=false;
            for(let j=0;j<disposeData.length;j++){
              if(disposeData[j].tzid==res.Data[i].tzid){
                isExt=true;
                break;
              }
            }
            if(!isExt){
              disposeData.push({tzid:res.Data[i].tzid,sm:res.Data[i].sm})
            }
          }
          this.mdata.accountList = disposeData;
          this.show = true;     
        } else {
          console.log(res);          
          this.loading = false;
          this.show = true;
        }
      });
    },

    choose(item) {      
      let param = new Object();
      param.token = myStore.userInfo.Token;
      param.updata = false;
      param.tzid = item.tzid;

      this.show = false;
      this.$axiosPost.post(APIUTL + "/ChooseTz/json", param).then((response) => {
        let res = getWsResult(response);
        // console.log(res)
        if (res.Errcode == 0) {
          // console.log(myStore.userInfo)          
          let obj=myStore.userInfo;
          obj.Tzid=item.tzid;
          Object.assign(myStore.userInfo, obj)       

          // console.log(myStore.userInfo)
          this.show = true;
          this.$router.push({ path: "/Menu" });
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



