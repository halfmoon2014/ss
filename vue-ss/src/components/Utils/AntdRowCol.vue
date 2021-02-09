<template>
  <a-row>
    <a-col
      v-for="(item, index) in colList"
      :key="index"
      :name="index"
      :span="item.colSize"
    >
      {{ item.wtitle + ":" + item.testVal }}
    </a-col>
  </a-row>
</template>

<script>
export default {
  name: "AntdRowCol",
  components: {},
  props: {
    colList: Array,
  },
  data: function () {
    return {};
  },
  methods: {},
  created: function () {},
  watch: {},
};
</script>
<style scoped>
 
</style>

 