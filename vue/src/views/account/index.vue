<template>
  <div class="account-container">
    <el-container>
      <el-header :style="headerHeight">
        <h2 class="el-icon-notebook-2">套账选择</h2>
      </el-header>
      <el-main :style="mainHeight">
        <el-table @row-click="handleRowChange" :data="tableData"  :height="tableHeight" :row-class-name="tableRowClassName">
          <el-table-column prop="gmtCreate" :formatter="dateFormatter" label="创建日期" width="140">
          </el-table-column>
          <el-table-column prop="name" label="名称" width="100">
          </el-table-column>
          <el-table-column prop="remark" label="备注">
          </el-table-column>
        </el-table>
      </el-main>
      <el-footer :style="footHeight" >
        <el-checkbox v-model="checked">初始建账</el-checkbox>
      </el-footer>
    </el-container>
  </div>
</template>

<script>
export default {
  name: 'Account',
  data () {
    return {
      tableData: [],
      mainHeight: {
        height: '360px'
      },
      tableHeight: 160,
      headerHeight: {
        height: '60px',
        marginTop: '20px',
        marginLeft: '20px',
        marginRight: '20px',
        lineHeight: '60px'
      },
      footHeight: {
        height: '60px',
        marginLeft: '20px',
        marginRight: '20px',
        marginBottom: '20px',
        lineHeight: '60px',
        textAlign: 'right'
      },
      checked: false
    }
  },
  mounted () {
  },
  watch: {
  },
  methods: {
    getHeight () {
      this.mainHeight.height = window.innerHeight - Number(this.headerHeight.height.replace('px', '')) - Number(this.footHeight.height.replace('px', '')) - Number(this.headerHeight.marginTop.replace('px', '')) - Number(this.footHeight.marginBottom.replace('px', '')) + 'px'
      this.tableHeight = Number(this.mainHeight.height.replace('px', '')) - 40
    },
    tableRowClassName ({row, rowIndex}) {
      if (rowIndex % 2 === 1) {
        return 'warning-row'
      } else if (rowIndex % 2 === 0) {
        return 'success-row'
      }
      return ''
    },
    handleRowChange (row, event, column) {
      console.log(this, row.id)
      this.$store.dispatch('Account', row.id)
      this.$router.push({ path: '/menu' })
    },
    // 日期格式化
    dateFormatter (row, column) {
      let datetime = row.gmtCreate
      if (datetime) {
        datetime = new Date(datetime)
        let y = datetime.getFullYear() + '-'
        let mon = datetime.getMonth() + 1 + '-'
        let d = datetime.getDate()
        return y + mon + d
      }
      return ''
    }
  },
  created () {
    if (this.$route.params.data) {
      this.tableData = this.$route.params.data.accountList.concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList).concat(this.$route.params.data.accountList)
    } else {
      // 如果没有选择账套，关闭界面，然后再进来，这个时候界面没有得到数据，把登陆信息删掉，重新登陆吧
      this.$store.dispatch('CleanLogin', 0)
      this.$router.push({ path: '/login' })
    }
    // 页面创建时执行一次getHeight进行赋值，顺道绑定resize事件
    window.addEventListener('resize', this.getHeight)
    this.getHeight()
  }
}
</script>

<style lang='scss' scoped>
.account-container{
  //动态计算中间区域高度，会有出现1PX的高度滚条
  overflow-y: hidden;
  .el-header {
    background-color: #fff;
    color: #333
  }
  .el-footer {
    background-color: #fff;
    color: #333;
  }
    // .el-table /deep/ .warning-row {
    //    background: oldlace;
    // }

    .el-table /deep/ .success-row {
      background: #f2f7fb;
    }
}

</style>
