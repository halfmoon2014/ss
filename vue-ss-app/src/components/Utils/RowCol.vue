<template>
<div>
  <div v-for="(rowList, dIndex) in dataList"
      :key="dIndex"
      :name="dIndex"
      @click="rowClick(rowList)"
       class="van-doc-block__card">
    <van-row
      v-for="(rItem, rIndex) in rowList.waterFallList"
      :key="rIndex"
      :name="rIndex" 
      style="margin-top:5px;"
    >
      <van-col
        v-for="(colItem, colIndex) in rItem"
        :span="colItem.span"
        :key="colIndex"
        :name="colIndex"
      >
        {{ colItem.text }}
      </van-col>
    </van-row>
  </div>
</div>
</template>

<script>
import { Col as VanCol } from "vant";
import { Row as VanRow } from "vant";
export default {
  name: "RowCol",
  components: {
    VanRow,
    VanCol,
  },
  props: {
    
    dataList: Array,
  },
  data: function () {
    return {};
  },
  methods: {
    rowClick(item) {
      this.$emit("goback", item); //回调父函数
    },
  },
  created: function () {
    //  console.log(this.dataList);
  },
  watch: {},
};
</script>
<style scoped>
.van-doc-block__card {  
  border-top-left-radius: 4px;
  border-top-right-radius: 4px;
  border-bottom-left-radius: 4px;
  border-bottom-right-radius: 4px;
  border: 1px solid #ebeef5;
  padding: 20px;
  box-shadow: 0px 0px 10px 1px rgb(210 202 202 / 50%);
}
</style>

 