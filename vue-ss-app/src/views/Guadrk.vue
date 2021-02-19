<template>
  <div class="wrap">
    <div
      style="margin-left: 10px; margin-right: 10px; margin-top: 10px"      
      v-show="scanVisible"
    >
      <van-divider
        :style="{ color: '#1989fa', borderColor: '#1989fa', padding: '0 16px' }"
        content-position="left"
        >订单信息</van-divider
      >
      <PopupRowCol
        type="fwsphh"
        :inExtObj="{ sphh: mdata.sphh, userid: userid }"
        v-if="mdata.sphhModel"
        @goback="popupRowColBack"
      />
      <van-cell-group>
        <van-field v-model="mdata.sphh" label="货号" placeholder="请输入">
          <template #button>
            <van-button
              size="small"
              @click="mdata.sphhModel = true"
              type="primary"
              >查询</van-button
            >
          </template>
        </van-field>
        <van-field v-model="mdata.khmc" readonly label="工厂" />
        <van-field
          v-model="mdata.ddsl"
          type="number"
          readonly
          label="订单数量"
        />

        <FieldPickerSingle
          title="样品类别"
          :placeholder="mdata.yplbDisabled ? '请输入货号' : '请选择'"
          :inValue.sync="mdata.yplb"
          :dataSource="mdata.yplbDS"
          :type="mdata.yplbGuid"
          :inExtObj="{ disabled: mdata.yplbDisabled }"
          @goback="pickerSingleBack"
        />

        <FieldPickerSingle
          title="洗涤程序"
          placeholder="请选择"
          :inValue.sync="mdata.xdcx"
          :dataSource="mdata.xdcxDS"
          @goback="pickerSingleBack"
        />

        <FieldPickerSingle
          title="洗涤次数"
          placeholder="请选择"
          :inValue.sync="mdata.xdcs"
          :dataSource="mdata.xdcsDS"
        />

        <FieldPickerSingle
          title="选择尺码"
          placeholder="请选择"
          :inValue.sync="mdata.xzcm"
          :dataSource="mdata.xzcmDS"
          :type="mdata.xzcmGuid"
          @goback="pickerSingleBack"
        />

        <van-divider
          :style="{
            color: '#1989fa',
            borderColor: '#1989fa',
            padding: '0 16px',
          }"
          content-position="left"
          >尺寸</van-divider
        >

        <van-row style="font-weight: 800">
          <van-col span="4">部位</van-col>
          <van-col span="5">标准尺寸</van-col>
          <van-col span="5">洗前尺寸</van-col>
          <van-col span="5">洗后尺寸</van-col>
          <van-col span="5">百分比</van-col>
        </van-row>

        <van-row
          type="flex"
          v-for="(item, index) in mdata.bwDetail"
          :key="index"
          :name="index"
          align="center"
        >
          <van-col span="4">{{ item.mc }}</van-col>
          <van-col span="5">{{ item.bzcc }}</van-col>
          <van-col span="5"
            ><van-field v-model="item.xqcc" type="number" placeholder="请输入"
          /></van-col>
          <van-col span="5"
            ><van-field v-model="item.xhcc" type="number" placeholder="请输入"
          /></van-col>
          <van-col span="5"
            >{{
              Math.round(((item.xhcc - item.xqcc) * 100) / item.xqcc, 2)
            }}%</van-col
          >
        </van-row>
        <van-divider
          :style="{
            color: '#1989fa',
            borderColor: '#1989fa',
            padding: '0 16px',
          }"
          content-position="left"
          >测试结果</van-divider
        >
        <div
          v-for="(item, index) in mdata.sysDetail"
          :key="item.mc"
          :name="index"
        >
          <div class="van-doc-block__card">
            <van-row>
              <van-col span="14">{{ item.mc }}</van-col>
              <van-col span="10">{{ item.bsbzz }}</van-col>
            </van-row>
            <van-row>
              <van-col span="18">
                <FieldPickerSingle
                  v-if="item.kxz.length > 0"
                  placeholder="请选择"
                  :inValue.sync="item.jcjg"
                  :dataSource="item.kxzList"
                  :inExtObj="{ labelHidden: true }"
                />
                <van-field
                  v-else
                  v-model="item.jcjg"
                  rows="1"
                  autosize
                  type="textarea"
                  placeholder="测试结果"
                />
              </van-col>
              <van-col span="6">
                <FieldPickerSingle
                  v-if="mdata.flowcs.length > 0"
                  title="判定结果"
                  placeholder="请选择"
                  :inValue.sync="item.pdjg"
                  :dataSource="[
                    { dm: '合格', mc: '合格' },
                    { dm: '不合格', mc: '不合格' },
                  ]"
                  :inExtObj="{ labelHidden: true }"
                />
                <van-field
                  v-else
                  v-model="item.pdjg"
                  rows="1"
                  readonly
                  autosize
                  type="textarea"
                  placeholder="判定结果"
                />
              </van-col>
            </van-row>
          </div>
        </div>
        <van-divider
          :style="{
            color: '#1989fa',
            borderColor: '#1989fa',
            padding: '0 16px',
          }"
          content-position="left"
          v-show="mdata.flowcs.length > 0"
          >审批意见</van-divider
        >
        <div class="van-doc-block__card" v-show="mdata.flowcs.length > 0">
          <van-row>
            <van-col span="14">QA工程师审核意见</van-col>
            <van-col span="10">
              <FieldPickerSingle
                title="意见"
                placeholder="请选择"
                :inValue.sync="mdata.xzyj"
                :dataSource="[
                  { dm: '合格', mc: '合格' },
                  { dm: '不合格', mc: '不合格' },
                  { dm: '让步接收', mc: '让步接收' },
                  { dm: '评审接受', mc: '评审接受' },
                ]"
                :inExtObj="{ labelHidden: true }"
              />
            </van-col>
          </van-row>
          <van-row>
            <van-col span="24">
              <van-field
                v-model="mdata.shyj"
                rows="1"
                autosize
                type="textarea"
                placeholder="请输入"
              />
            </van-col>
          </van-row>
        </div>
        <div class="van-doc-block__card" v-show="mdata.flowcs.length > 0">
          <van-row>
            <van-col span="14">商品调控中心分管品质副总监意见</van-col>
            <van-col span="10">
              <FieldPickerSingle
                title="意见"
                placeholder="请选择"
                :inValue.sync="mdata.ldxzyj"
                :dataSource="[
                  { dm: '合格', mc: '合格' },
                  { dm: '不合格', mc: '不合格' },
                  { dm: '让步接收', mc: '让步接收' },
                  { dm: '评审接受', mc: '评审接受' },
                ]"
                :inExtObj="{ labelHidden: true }"
              />
            </van-col>
          </van-row>
          <van-row>
            <van-col span="24">
              <van-field
                v-model="mdata.ldshyj"
                rows="1"
                autosize
                type="textarea"
                placeholder="请输入"
              />
            </van-col>
          </van-row>
        </div>
      </van-cell-group>
    </div>

    <van-tabbar v-model="active" @change="handleSelect">
      <van-tabbar-item name="save" icon="certificate">保存</van-tabbar-item>

      <van-tabbar-item name="send" icon="sign">办理</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
import myStore from "@/components/Utils/Store";
import { Button as VanButton } from "vant";
import { Tabbar as VanTabbar } from "vant";
import { TabbarItem as VanTabbarItem } from "vant";
import { Collapse as VanCollapse } from "vant";
import { CollapseItem as VanCollapseItem } from "vant";
import { Field as VanField } from "vant";
import { Popup as VanPopup } from "vant";
import { Col as VanCol } from "vant";
import { Row as VanRow } from "vant";
import { Icon as VanIcon } from "vant";
import { Divider as VanDivider } from "vant";
import { Cell as VanCell } from "vant";
import { CellGroup as VanCellGroup } from "vant";
import { guid } from "@/assets/js/utils";
import { getUrlKey } from "@/assets/js/utils";
import { Toast as VanToast } from "vant";
export default {
  name: "Guadrk",
  components: {
    FieldPickerSingle: () => import("@/components/Utils/FieldPickerSingle"),
    PickerSingle: () => import("@/components/Utils/PickerSingle"),
    PopupRowCol: () => import("@/components/Utils/PopupRowCol"),
    VanButton,
    VanTabbar,
    VanTabbarItem,
    VanCollapse,
    VanCollapseItem,
    VanField,
    VanPopup,
    VanCol,
    VanRow,
    VanIcon,
    VanDivider,
    VanCellGroup,
    VanCell,
    VanToast,
  },
  data: function () {
    return {
      active: "",
      activeNames: [0],

      mdata: {
        cmzbid: 0,
        gzh: "",
        lymxid: 0,
        htdjlx: 0,
        djid: 0,
        syid: "", //?
        wtsid: "", //?
        flowcs: "",
        yplbGuid: guid(),
        xzcmGuid: guid(),
        yplbDisabled: true,
        sphhModel: false, //是否显示货号列表
        yplb: 0, //样品类别
        yplbDS: [
          {
            dm: 1,
            mc: "产前封样",
          },
          {
            dm: 2,
            mc: "预投封样",
          },
          {
            dm: 3,
            mc: "测试封样",
          },
          {
            dm: 4,
            mc: "大货",
          },
        ],
        xdcx: 0, //洗涤程序
        xdcxDS: [
          {
            dm: 0,
            mc: "标准洗",
          },
          {
            dm: 1,
            mc: "快洗",
          },
          {
            dm: 2,
            mc: "干洗",
          },
        ],
        xdcs: 0, //洗涤次数
        xdcsDS: [
          {
            dm: 1,
            mc: "机洗1遍",
          },
          {
            dm: 3,
            mc: "机洗3遍",
          },
          {
            dm: 5,
            mc: "机洗5遍",
          },
        ],
        xzcm: "",
        xzcmDS: [],
        sphh: "",
        sphhHidden: "",
        khmc: "",
        khid: 0,
        ddsl: "",
        xzyj: "", //QA工程师审核意见 判定
        shyj: "", //QA工程师审核意见 意见
        ldxzyj: "", //商品调控中心分管品质副总监意见 判定
        ldshyj: "", //商品调控中心分管品质副总监意见 意见
        bwDetail: [
          // {
          //   xqcc: 413,
          //   mc: "领长",
          //   bzcc: 41,
          //   xhcc: 407,
          //   dm: "cm21",
          //   id: 473049,
          //   ypkh: "Q213ZC1022",
          // },
          // {
          //   xqcc: 733,
          //   mc: "衣长",
          //   bzcc: 73,
          //   xhcc: 725,
          //   dm: "cm21",
          //   id: 473049,
          //   ypkh: "Q213ZC1022",
          // },
        ],
        sysDetail: [
          // {
          //   pdjg: "",
          //   mc: "拼接互染*沾色，级",
          //   jcjg: "未考核",
          //   bjdmxid: 49693223,
          //   bsbzz: "≥4",
          //   kxz: "合格/不合格/未考核",
          // },
          // {
          //   pdjg: "",
          //   mc: "尺寸变化率*水洗直向，%",
          //   jcjg: "-1.0",
          //   bjdmxid: 49693224,
          //   bsbzz: "包含于(-2.5,+1.0)",
          //   kxz: "",
          // },
          // {
          //   pdjg: "",
          //   mc: "尺寸变化率*水洗横向，%",
          //   jcjg: "-1.0",
          //   bjdmxid: 49693225,
          //   bsbzz: "包含于(-2.5,+1.0)",
          //   kxz: "",
          // },
          // {
          //   pdjg: "",
          //   mc: "水洗扭曲率，%",
          //   jcjg: "0.0",
          //   bjdmxid: 49693226,
          //   bsbzz: "≤2.5",
          //   kxz: "",
          // },
          // {
          //   pdjg: "",
          //   mc: "水洗外观*变色，级",
          //   jcjg: " 合格",
          //   bjdmxid: 49693227,
          //   bsbzz: "≥3.5",
          //   kxz: " 合格/不合格",
          // },
          // {
          //   pdjg: "",
          //   mc: "洗后外观评价",
          //   jcjg: "洗后门襟起皱3级 后省道起皱3级 挂烫门襟起皱3级",
          //   bjdmxid: 49693228,
          //   bsbzz: "",
          //   kxz: "",
          // },
          // {
          //   pdjg: "",
          //   mc: "水洗外观*变色，级（加机洗两遍）",
          //   jcjg: "未考核",
          //   bjdmxid: 49693229,
          //   bsbzz: "≥3.5",
          //   kxz: "合格/不合格/未考核",
          // },
          // {
          //   pdjg: "",
          //   mc: "洗后外观评价（加机洗两遍）",
          //   jcjg: "未考核",
          //   bjdmxid: 49693230,
          //   bsbzz: "",
          //   kxz: "",
          // },
        ],
      },
      loading: false,
      scanVisible: true, //是否隐藏
      userid: myStore.userInfo.userid,
    };
  },
  methods: {
    init() {
         VanToast.loading({
        duration: 0,
        message: "加载中...",
        forbidClick: true,
      });
    },
    pickerSingleBack(type, mark, obj) {
      this.loading = true;
      if (type == this.mdata.yplbGuid) {
        this.yplbPromise();
      }
      if (type == this.mdata.xzcmGuid) {
        this.xzcmPromise();
      }
    },
    yplbPromise() {
      return new Promise((resolve, reject) => {
        this.$axios
          .get(APIUTL, {
            params: {
              serviceName: "svr-external",
              action: "factoryWash/cmList",
              sphh: this.mdata.sphh,
              yplb: this.mdata.yplb,
            },
          })
          .then((response) => {
            let result = response.data;
            if (result.errcode != 0) {
              this.errMsg(result.errmsg);
              this.loading = false;
              return;
            }
            this.loading = false;
            this.mdata.xzcmDS.splice(0, this.mdata.xzcmDS.length);
            for (var i = 0; i <= result.data.length; i++) {
              result.data[i] &&
                this.mdata.xzcmDS.push({
                  dm: result.data[i].dm,
                  mc: result.data[i].cm,
                });
            }

            this.mdata.xzcm = "";
            this.mdata.bwDetail.splice(0, this.mdata.bwDetail.length);
            resolve(response.data);
          })
          .catch((error) => {
            this.errMsg(error);
            reject(error);
          });
      });
    },
    sysDetailPromise(splbid) {
      return new Promise((resolve, reject) => {
        this.$axios
          .get(APIUTL, {
            params: {
              serviceName: "svr-external",
              action: "factoryWash/jcxm",
              splbid: splbid,
            },
          })
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    xzcmPromise() {
      return new Promise((resolve, reject) => {
        this.$axios
          .get(APIUTL, {
            params: {
              serviceName: "svr-external",
              action: "factoryWash/bzcm",
              sphh: this.mdata.sphh,
              yplb: this.mdata.yplb,
              xzcm: this.mdata.xzcm,
            },
          })
          .then((response) => {
            let result = response.data;
            if (result.errcode != 0) {
              this.errMsg(result.errmsg);
              this.loading = false;
              return;
            }
            this.mdata.bwDetail = result.data;
            this.loading = false;
            resolve(response.data);
          })
          .catch((error) => {
            this.errMsg(error);
            reject(error);
          });
      });
    },

    initPromise(MyDJid) {
      return new Promise((resolve, reject) => {
        this.$axios
          .get(APIUTL, {
            params: {
              serviceName: "svr-external",
              action: "factoryWash/" + MyDJid,
            },
          })
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    handleSelect(index) {
      if (index == "save") {
        this.save();
      }
    },

    save() {
      if (this.mdata.sphh != this.mdata.sphhHidden) {
        this.errMsg("请查询货号信息");
        this.active = ""; //不让下面的菜单选中,默认会选中再点的时候不会触发事件
        return;
      }
      if (this.mdata.xzcm.length == 0) {
        this.errMsg("请选择尺码");
        this.active = ""; //不让下面的菜单选中,默认会选中再点的时候不会触发事件
        return;
      }
      for (var i = 0; i < this.mdata.bwDetail.length; i++) {
        if (this.mdata.bwDetail[i].xqcc.length == 0) {
          this.errMsg("请填写洗前尺寸");
          this.active = "";
          return;
        }
        if (this.mdata.bwDetail[i].xhcc.length == 0) {
          this.errMsg("请填写洗后尺寸");
          this.active = "";
          return;
        }
      }

      for (var i = 0; i < this.mdata.sysDetail.length; i++) {
        if (this.mdata.sysDetail[i].jcjg.length == 0) {
          this.errMsg("请填写测试结果");
          this.active = "";
          return;
        }
      }
      let param = this.mdata;
      param.userssid = 1;
      param.username = myStore.userInfo.cname;
      param.userid = this.userid;
      let jcjgList = [];
      param.jcjg = jcjgList;
      for (var i = 0; i < this.mdata.sysDetail.length; i++) {
        jcjgList.push({
          id: this.mdata.sysDetail[i].testid,
          jcjg: this.mdata.sysDetail[i].jcjg,
          pdjg: this.mdata.sysDetail[i].pdjg,
          bjdmxid: this.mdata.sysDetail[i].bjdmxid || 0,
        });
      }
      let cmjgList = [];
      param.cmjg = cmjgList;
      for (var i = 0; i < this.mdata.bwDetail.length; i++) {
        cmjgList.push({
          cmdm: this.mdata.bwDetail[i].dm,
          cmmc: this.mdata.bwDetail[i].mc,
          ypkh: this.mdata.bwDetail[i].ypkh,
          xqcc: this.mdata.bwDetail[i].xqcc,
          xhcc: this.mdata.bwDetail[i].xhcc,
          bzcc: this.mdata.bwDetail[i].bzcc,
        });
      }
      // console.log(param);
      this.savePromise(param)
        .then((r) => {
          if (r.errcode == 0) {
            this.$message("保存成功");
            this.mdata.djid = r.data[0].djid;

            this.goflowPromise(this.mdata.djid)
              .then((r) => {
                if (r.errcode == 0) {
                } else {
                  this.errMsg(r.errmsg);
                }
              })
              .catch((e) => {});
          } else {
            this.errMsg(r.errmsg);
          }
          this.active = "";
        })
        .catch((e) => {
          this.active = "";
        });
    },

    savePromise(param) {
      return new Promise((resolve, reject) => {
        this.$axiosPost
          .post(APIUTL + "?serviceName=svr-external&action=factoryWash", param)
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    errMsg(msg) {
      this.$message({
        showClose: true,
        message: msg,
        type: "error",
      });
    },
    popupRowColBack(type, mark, result) {
      if (type == "fwsphh") {
        this.mdata.sphhModel = false;
        if (result.errcode > 0) {
          this.$message(result.errmsg);
          this.mdata.yplbDisabled = true;
          return false;
        }        
        if (!result.data) {//直接点取消回来的
          this.mdata.yplbDisabled = true;
          return false;
        }
        let item=result.data;
        this.mdata.sphh = item.sphh;
        this.mdata.sphhHidden = item.sphh; //有可能输入了货号但没点查询,这个用来判断保存的
        this.mdata.khmc = item.khmc;
        this.mdata.gzh = item.gzh;
        this.mdata.khid = item.khid;
        this.mdata.ddsl = item.ddsl;
        this.mdata.lymxid = item.mxid;
        this.mdata.htdjlx = item.htdjlx;
        this.mdata.yplbDisabled = false;
        //清除有货号相关的选择
        this.mdata.yplb = 0;
        this.mdata.xzcmDS.splice(0, this.mdata.xzcmDS.length);
        this.mdata.xzcm = "";
        this.mdata.bwDetail.splice(0, this.mdata.bwDetail.length);

        this.loading = true;
        this.sysDetailPromise(item.splbid)
          .then((result) => {
            if (result.errcode != 0) {
              this.errMsg(result.errmsg);
              this.loading = false;
              return;
            }
            this.loading = false;
            this.mdata.sysDetail.splice(0, this.mdata.sysDetail.length);
            for (var i = 0; i < result.data.length; i++) {
              let kxz = result.data[i].kxz;
              if (kxz) {
                let kxzList = [];
                for (var j = 0; j < kxz.split("/").length; j++) {
                  kxzList.push({
                    dm: kxz.split("/")[j],
                    mc: kxz.split("/")[j],
                  });
                }
                result.data[i].kxzList = kxzList;
              }
            }
            this.mdata.sysDetail = result.data;
          })
          .catch((error) => {
            this.errMsg(error);
            this.loading = false;
          });
      }
      // console.log(type, mark, item);
    },

    goflowPromise(id) {
      return new Promise((resolve, reject) => {
        this.$axios
          .get(APIUTL, {
            params: {
              serviceName: "svr-external",
              action: "getFlowConfig",
              id: id,
              tableName: "Yf_T_bjdlb-factoryWash",
            },
          })
          .then((response) => {
            let result = response.data;
            if (result.errcode != 0) {
              this.errMsg(result.errmsg);
              this.loading = false;
              this.init();
              return;
            }
            this.loading = false;
            if (result.data.isneed == 1) {
              let baseUrl =
                "http://sj.lilang.com:186/llsj/docDetailDataFF.aspx?";
              for (let key in result.data) {
                if (
                  key == "tzid" ||
                  key == "docid" ||
                  key == "dxid" ||
                  key == "flowid" ||
                  key == "dbname" ||
                  key == "cname" ||
                  key == "userid"
                ) {
                  if (baseUrl.indexOf("?") === -1) {
                    baseUrl = baseUrl + "?" + key + "=" + result.data[key] + "";
                  } else {
                    baseUrl = baseUrl + "&" + key + "=" + result.data[key] + "";
                  }
                }
              }
              baseUrl +=
                "&gourl=" +
                encodeURIComponent(
                  "http://tm.lilanz.com/oa/project/LabDev/index.html#/FActoryWash?apptoken=" +
                    myStore.userInfo.apptoken
                );
              console.log(window.location.href);
              window.location.href = baseUrl;
            } else {
              //不需要办理，流程已经办理过了，
              this.init();
            }
            resolve(response.data);
          })
          .catch((error) => {
            this.errMsg(error);
            this.init();
            reject(error);
          });
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
.wrap {
  height: calc(100% - 50px);
  overflow-y: auto;
}
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
