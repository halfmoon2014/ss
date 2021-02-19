<template>
  <van-picker
    :title="title"
    show-toolbar
    :columns="options"
    value-key="mc"
    :default-index="defaultIndex"
    :loading="loading"
    @confirm="onConfirm"
    @cancel="onCancel"
    @change="onChange"
  />
</template>

<script>
import { Picker as VanPicker } from "vant";
export default {
  name: "PickerSingle",
  components: {
    VanPicker,
  },
  props: {
    mark: [String, Number], //外部调用的标识,回调是统一的,所以需要一个标识是哪个父对象调用的
    title: String, //标题
    type: String, //指定内容
    inValue: [String, Number], //初始值,有可能是数字或者是字串
    // inText: String, //初始名称,用来显示的
    inExtObj: Object, //附加属性,是一个对象,在发送请求的时候会带上这部份参数
    dataSource: Array, //数据源
  },
  data: function () {
    return {
      // titleTemp:this.title ||"",//标题
      // typeTemp:this.type || "",//取什么数据,分类用
      defaultIndex: 0, //单列选择时，默认选中项的索引
      options: [], //内容对应的值,因为显示的内容只允许是一个字符串数组,其它信息保存在这里
      // columns: [], //显示的内容 对象数组，配置每一列显示的数据
      loading: true,
    };
  },
  methods: {
    onConfirm(value, index) {
      //点击完成按钮时触发
      //  console.log(value,index)
      //this.$emit("update:model", false);//关闭组件
      // this.$emit("update:inValue", value.dm);//更新值
      // this.$emit("update:inText", value.mc);//更新显示的内容
      // console.log(this.options[index].dm,this.options[index].mc)
      if(value)
        this.$emit("goback", this.type, this.mark, value,this.options); //回调父函数
      else   this.$emit("update:model", false);
      // console.log(value,index)
    },
    onChange(picker, value, index) {
      //选项改变时触发
    },
    onCancel() {
      //点击取消按钮时触发
      this.$emit("update:model", false);
    },
  },
  created: function () {
    if (this.dataSource) {
      //存在外部指定的数据源,格式要求一定是dm,mc+其它,,, dm是主键,mc是用来显示的,
      //查找默认值 数据默认格式dm ,mc
      for (var i = 0; i < this.dataSource.length; i++) {
        // this.columns.push(this.dataSource[i].mc);
        this.options.push(this.dataSource[i]);
        if (this.dataSource[i].dm == this.inValue) {
          this.defaultIndex = i;
        }
      }
      this.loading = false;
      return false;
    }
    var params = {};
    params.action = this.type;
    // //成衣风格
    // if (this.type == "getDesignStyleList") params.action = "getDesignStyleList";
    // //调样中的颜色情况
    // if (this.type == "getMaterialApplyTypes")
    //   params.action = "getMaterialApplyTypes";

    // //调样中的颜色情况
    // if (this.type == "getMaterialApplyBm") params.action = "getMaterialApplyBm";

    // //项目经理
    // if (this.type == "getMaterialApplyXmjl")
    //   params.action = "getMaterialApplyXmjl";

    // //开发编号
    // if (this.type == "getDevNumList") params.action = "getDevNumList";

    // //调样中的采购类别
    // if (this.type == "getMaterialApplyDjlb")
    //   params.action = "getMaterialApplyDjlb";
    // //商品类别
    // if (this.type == "getClothTypes") {
    //   params.action = "getClothTypes";
    // }

    if (this.inExtObj) {
      for (var i in this.inExtObj) {
        params[i] = this.inExtObj[i];
      }
    }

    if (params.action == "") {
      console.log("type is empty");
      return false;
    }
    this.loading = true;
    this.$axios
      .get(APIUTL, {
        params: params,
      })
      .then((response) => {
        if (response.data.errcode != 0) {
          this.$message({
            showClose: true,
            message: response.data.errmsg,
            type: "error",
          });
          return;
        }
        var data = [];
        if (
          this.type == "getDesignStyleList" ||
          this.type == "getMaterialApplyTypes" ||
          this.type == "getMaterialApplyDjlb" ||
          this.type == "getMaterialApplyBm"
        )
          data = response.data.data[0];
        else if (this.type == "getDevNumList") data = response.data.data;
        else if (this.type == "getMaterialApplyXmjl") {
          //项目经理
          for (var i = 0; i < response.data.data[0].length; i++) {
            data.push({
              dm: response.data.data[0][i].ryid,
              mc: response.data.data[0][i].xm,
              fgid: response.data.data[0][i].fgid,
            });
          }
        } else if (this.type == "getClothTypes") {
          for (var i = 0; i < response.data.data.length; i++) {
            if (response.data.data[i].value == 1201) {
              //利郎运动系列 不要
            } else {
              for (var j = 0; j < response.data.data[i].children.length; j++) {
                //jb2
                var jb3 = response.data.data[i].children[j].children;

                for (var z = 0; z < jb3.length; z++) {
                  //加空格转成字符串
                  data.push({ dm: jb3[z].value, mc: jb3[z].label });
                }
              }
            }
          }
        }

        //查找默认值 数据默认格式dm ,mc
        for (var i = 0; i < data.length; i++) {
          // this.columns.push(data[i].mc);
          this.options.push(data[i]);
          if (data[i].dm == this.inValue) {
            this.defaultIndex = i;
          }
        }
       
        this.loading = false;
      })
      .catch((error) => {
        console.log(error);
        this.loading = false;
      });
  },
  watch: {
    inValue(newValue) {
      //如果父组件有直接修改值,那么默认值不会变,要手动更改
      for (var i = 0; i < this.options.length; i++) {
        if (this.options[i].dm == this.inValue) {
          this.defaultIndex = i;
        }
      }
    },
  },
};
</script>

 