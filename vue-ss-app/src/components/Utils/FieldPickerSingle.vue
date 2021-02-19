<template>
  <div>
    <van-field
      readonly
      clickable
      :label="
        inExtObj && inExtObj.labelHidden != undefined && inExtObj.labelHidden
          ? ''
          : title
      "
      :value="dm2mc"
      :placeholder="placeholder"
      @click="
        show =
          inExtObj && inExtObj.disabled != undefined ? !inExtObj.disabled : true
      "
    />
    <van-popup v-model="show" v-if="show" round position="bottom">
      <PickerSingle
        :model.sync="show"
        :title="title"
        :type="type"
        :inValue="inValue"
        :inExtObj="inExtObj"
        :dataSource="dataSource"
        @goback="pickerSingleBack"
        :mark="mark"
      />
    </van-popup>
  </div>
</template>

<script>
import { Picker as VanPicker } from "vant";
import { Field as VanField } from "vant";
import { Popup as VanPopup } from "vant";
export default {
  name: "FieldPickerSingle",
  components: {
    PickerSingle: () => import("@/components/Utils/PickerSingle"),
    VanPicker,
    VanField,
    VanPopup,
  },
  props: {
    placeholder: String,
    mark: [String, Number], //外部调用的标识,回调是统一的,所以需要一个标识是哪个父对象调用的
    title: String, //标题
    inValue: [String, Number], //初始值,有可能是数字或者是字串
    // inText: String, //初始名称,用来显示的
    type: String, //指定内容

    inExtObj: Object, //附加属性,是一个对象,在发送请求的时候会带上这部份参数,
    //disabled表示是否禁用,默认不禁,
    //labelHidden隐藏title,默认不隐藏
    dataSource: Array, //数据源
  },
  computed: {
    dm2mc() {
      if (this.dataSource) {//名称优先外部数据源查出来
        let arr = this.dataSource.filter((item) => {
          return item.dm == this.inValue;
        });

        return (arr[0] || {}).mc || "";
      }

      if (this.options) {
        let arr = this.options.filter((item) => {
          return item.dm == this.inValue;
        });

        return (arr[0] || {}).mc || "";
      }
    },
  },
  data: function () {
    return {
      show: false,
      // value: this.inValue,
      // text: "",
      options: [],
    };
  },
  methods: {
    pickerSingleBack(type, mark, obj, options) {
      this.options=options;
      //options 如果数据源不是外面给的,是里面查询出来的,返回出来,并且给当前组件查找名称
      this.show = false;
      // console.log(obj)
      this.$emit("update:inValue", obj.dm); //更新值
      // this.$emit("update:inText", obj.mc); //更新值
      // this.value=obj.dm;
      // this.text = obj.mc;
      this.$emit("goback", type, mark, obj); //回调父函数
    },
  },
  created: function () {
    //只加载一次
    // console.log(this.inValue);
  },
  watch: {
    // inValue(v1, v2) {
    //   console.log(v1, v2, this.text, this.inValue);
    // },
    // value(newValue) {
    //   this.$emit("update:inValue", newValue); //更新值
    // },
    // text(newValue) {
    //   this.$emit("update:inText", newValue); //更新值
    // },
  },
};
</script>

 