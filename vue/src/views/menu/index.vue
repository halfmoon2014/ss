<template>
<div class="menu-container">
  <el-container>
    <el-header :style="headerHeight">
      <MenuItem/>
    </el-header>
    <el-container>
        <el-main :style="mainHeight">Main</el-main>
        <el-footer :style="footHeight">FOOT
        </el-footer>
    </el-container>
  </el-container>
</div>
</template>

<script>
import MenuItem from '@/components/menu/MenuItem.vue'
export default {
  name: 'Menu',
  components: {
    MenuItem
  },
  data () {
    return {
      mainHeight: {
        height: ''
      },
      headerHeight: {
        height: '60px'
      },
      footHeight: {
        height: '60px'
      }
    }
  },
  mounted () {
  },
  watch: {
  },
  methods: {
    getHeight () {
      this.mainHeight.height = window.innerHeight - Number(this.headerHeight.height.replace('px', '')) - Number(this.footHeight.height.replace('px', '')) + 'px'
    }
  },
  created () {
    // 页面创建时执行一次getHeight进行赋值，顺道绑定resize事件
    window.addEventListener('resize', this.getHeight)
    this.getHeight()
  }
}
</script>

<style lang='scss' scope>
.menu-container{
  .el-header, .el-footer {
    background-color:#fff;
    color: #333;
    text-align: center;
    line-height: 60px;
  }
  .el-main {
    background-color: #fff;
    color: #333;
    text-align: center;
    line-height: 160px;
  }
  body > .el-container {
    margin-bottom: 40px;
  }
  .el-container:nth-child(5) .el-aside,
  .el-container:nth-child(6) .el-aside {
    line-height: 260px;
  }
  .el-container:nth-child(7) .el-aside {
    line-height: 320px;
  }
}
</style>
