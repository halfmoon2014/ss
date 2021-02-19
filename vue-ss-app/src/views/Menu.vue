<template>
 <div class="wrap">
    <div style="margin: 50px" v-show="show">
      <van-list>
        <van-cell
          v-for="(item, index) in mdata.menuList"
          :key="index"
          :title="item.mc"
          @click="menu(item)"
        />
      </van-list>
    </div>
 </div>
</template>

<script>

import myStore from "@/components/Utils/Store";
import { Cell as VanCell } from "vant";
import { List as VanList } from "vant";
export default {
  name: "Menu",
  components: {VanCell,
  VanList,},
  data() {
    return {
      mdata: {
        menuList: [],
      },
      show: true,
      tzid: myStore.userInfo.Tzid,
      userid: myStore.userInfo.Userid,
    };
  },
  methods: {
    init() {
      this.mdata.menuList.push({ mc: "挂镀入库模块", path: "/Guadrk" });
      this.mdata.menuList.push({ mc: "滚镀入库模块", path: "/Gundrk" });
    },

    menu(item) {
      this.$router.push({ path: item.path });
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
  height: calc(100% );
  overflow-y: auto;
}
</style>

