<template>
  <van-popup
    v-model="model"
    v-loading="loading"
    closeable
    @close="closeEvn"
    position="bottom"
    style="max-height: 300px"
  >
    <RowCol style="margin:5px;" :dataList="options" @goback="popupBack"></RowCol>
  </van-popup>
</template>

<script>
import { Popup as VanPopup } from "vant";

export default {
  name: "PopupRowCol",
  components: {
    RowCol: () => import("@/components/Utils/RowCol"),
    VanPopup,
  },
  props: {
    dataSource: Array, //数据源
    mark: [String, Number], //外部调用的标识,回调是统一的,所以需要一个标识是哪个父对象调用的
    type: String, //指定内容
    inExtObj: Object, //附加属性,是一个对象,在发送请求的时候会带上这部份参数
  },
  data: function () {
    return {
      options: [], //内容对应的值,因为显示的内容只允许是一个字符串数组,其它信息保存在这里
      // rowList:[],//显示的内容
      loading: true,
      model: true,
    };
  },
  methods: {
    closeEvn() {
      let result = {};
      result.errcode = 0;
      result.data = null;
      this.close(result);
    },

    close(item) {
      this.model = false;
      this.$emit("goback", this.type, this.mark, item); //回调父函数
    },

    popupBack(item) {
      let result = {};
      result.errcode = 0;
      result.data = item.data;
      
      this.close(result);
    },
    search() {
      this.searchPromise(this.waterFallSearchName).then((result) => {
        console.log(result);
        return result;
      });
      return false;
    },

    searchPromise(waterFallSearchName) {
      return new Promise((resolve, reject) => {
        this.$axiosPost
          .post(
            APIUTL +
              "?serviceName=svr-waterfall&action=waterfalls/search/1/100",
            { name: waterFallSearchName }
          )
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
  },
  created: function () {
    if (this.dataSource) {
      this.options = this.dataSource;
      return false;
    }
    var params = {};
    var url = APIUTL;
    if (this.type == "fwsphh") {
      params.action = "factoryWash/sphh";
      params.sphh = this.inExtObj.sphh;
      if (!this.inExtObj.sphh) {
        let result = {};
        result.errcode = 100;
        result.errmsg = "请输入货号";
        this.close(result);
        return;
      }

      params.userid = this.inExtObj.userid;
      params.tzid = 1;
      url = APIUTL + "?serviceName=svr-external";
    }
    this.$axios
      .get(url, {
        params: params,
      })
      .then((response) => {
        if (response.data.errcode != 0) {
          let result = {};
          result.errcode = 100;
          result.errmsg = response.data.errmsg;
          this.close(result);
          return;
        }
        let dataList=[];
        if (this.type == "fwsphh") dataList = response.data.data;

        this.searchPromise(this.type).then((result) => {
          if (result.errcode == 0) {
            if (result.data[0].numberOfElements == 1) {
              //只有一条记录
              let typeSetList = JSON.parse(result.data[0].content[0].typeSet);

              for (var i = 0; i < dataList.length; i++) {
                let o = {};
                o.waterFallList = [];
                o.data = dataList[i];
                for (var j = 0; j < typeSetList.length; j++) {
                  let rowList = [];
                  for (var k = 0; k < typeSetList[j].cols.length; k++) {
                    rowList.push({
                      span: typeSetList[j].cols[k].colSize,
                      text: dataList[i][typeSetList[j].cols[k].wname],
                    });
                    // console.log(rowList)
                  }
                  o.waterFallList.push(rowList);
                }
                this.options.push(o);
              }
              this.loading = false;
              // console.log(this.options);
            } else if (result.data[0].numberOfElements > 1) {
              //多条不处理
              let result = {};
              result.errcode = 100;
              result.errmsg = "查询多条";
              this.close(result);
            } else {
              //没记录
              let result = {};
              result.errcode = 100;
              result.errmsg = "查询没记录";
              this.close(result);
            }
          } else {
            let result = {};
            result.errcode = 100;
            result.errmsg = "查询失败";
            this.close(result);
          }
        });
      })
      .catch((error) => {
        console.log(error);
      });
  },
  watch: {},
};
</script>

 