<template>
  <a-layout class="container components-layout-demo-custom-trigger">
    <a-layout-sider v-model="collapsed" :trigger="null" collapsible>
      <div class="logo" />
      <a-menu theme="dark" mode="inline" :default-selected-keys="['1']">
        <a-menu-item key="1">
          <a-icon type="user" />
          <span>Makeup</span>
        </a-menu-item>
        <a-menu-item key="2">
          <a-icon type="video-camera" />
          <span>TypeSet</span>
        </a-menu-item>
        <a-menu-item key="3">
          <a-icon type="upload" />
          <span>Upload</span>
        </a-menu-item>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header style="background: #fff; padding: 0">
        <!-- <a-form-model
          layout="inline"
          @submit="handleSubmit"
          @submit.native.prevent
        > -->
        <a-space
          align="center"
          style="display: flex; width: 100%"
          class="headerRigh"
        >
          <!-- <a-form-model-item> -->
          <a-icon
            class="trigger"
            :type="collapsed ? 'menu-unfold' : 'menu-fold'"
            @click="() => (collapsed = !collapsed)"
          />
          <!-- </a-form-model-item> -->
          <div style="flex: 1; text-align: right">
            <!-- <a-form-model-item> -->
            <a-avatar icon="user" /><span
              style="margin-right: 50px; margin-left: 10px"
            >
              {{ cname }}</span
            >
            <!-- </a-form-model-item> -->
          </div>
        </a-space>
        <!-- </a-form-model> -->
      </a-layout-header>
      <a-layout-content
        :style="{
          margin: '24px 16px',
          background: '#fff',
          minHeight: '280px',
        }"
      >
        <a-spin :spinning="spinning">
          <a-page-header
            style="border: 1px solid rgb(235, 237, 240)"
            :title="headerTitle"
            @back="() => null"
          >
            <template slot="backIcon">
              <a-icon type="vertical-align-bottom" />
            </template>
            <template slot="extra">
              <a-form-model layout="inline">
                <a-form-model-item
                  :colon="false"
                  :label="waterFallGuid + (waterFallGuid ? '-' : '')"
                >
                  <a-input v-model="waterFallName" placeholder="name" />
                </a-form-model-item>
                <a-form-model-item>
                  <a-button @click="save" icon="save">save</a-button>
                  <a-button
                    @click="showDrawer"
                    shape="circle"
                    icon="small-dash"
                    style="margin-left: 10px"
                  />
                </a-form-model-item>
              </a-form-model>
            </template>
          </a-page-header>

          <a-drawer
            title="Search"
            placement="right"
            :closable="false"
            :visible="drawerVisible"
            :after-visible-change="afterVisibleChange"
            @close="onClose"
          >
            <a-form-model-item style="" label="Guid">
              <a-input v-model="waterFallSearchGuid"> </a-input>
            </a-form-model-item>

            <a-form-model-item style="" label="Name">
              <a-input v-model="waterFallSearchName"> </a-input>
            </a-form-model-item>

            <a-form-model-item>
              <a-button type="primary" @click="search" html-type="submit"
                >search
              </a-button>
            </a-form-model-item>
            <a-drawer
              title="choose one waterfall"
              width="320"
              :closable="false"
              :visible="childrenDrawer"
              @close="onChildrenDrawerClose"
            >
              <a-card
                style="width: 300px"
                v-for="item in contentTempList"
                :key="item.index"
                @click="chooseContent(item)"
              >
                <p>{{ item.id }}</p>
                <p>{{ item.name }}</p>
                <p>{{ item.remark }}</p>
                <p>{{ item.creator }}</p>
              </a-card>
            </a-drawer>
          </a-drawer>

          <a-tabs default-active-key="1" style="margin: 10px">
            <a-tab-pane key="1" tab="SqlText">
              <a-comment>
                <div slot="content">
                 
                    <a-textarea
                      :rows="5"
                      :value="sqlText"
                      v-model="sqlText"
                      @change="handleChange"
                    />
             
                </div>
              </a-comment>
            </a-tab-pane>
            <a-tab-pane key="2" tab="API" force-render>
              <a-row>
                <a-col :span="12">
                  <a-form-model>
                    <a-form-model-item label="URL">
                      <a-input v-model="api" />
                    </a-form-model-item>

                    <a-form-model-item label="RequestType">
                      <a-select
                        :defaultValue="RequestType"
                        v-model="RequestType"
                        placeholder="please select your RequestType"
                      >
                        <a-select-option
                          v-for="province in ['GET', 'POST']"
                          :key="province"
                        >
                          {{ province }}
                        </a-select-option>
                      </a-select>
                    </a-form-model-item>
                  </a-form-model>
                </a-col>
                <a-col :offset="2" :span="10">
                  <a-tabs default-active-key="1" style="margin: 10px">
                    <a-tab-pane key="1" tab="key:value">
                      <a-form-model>
                        <a-form-model-item
                          v-for="(item, index) in parameters"
                          :key="index"
                          :label="index == 0 ? 'Parameters' : ''"
                        >
                          <a-input v-model="item.val" />
                        </a-form-model-item>
                      </a-form-model>
                      <a-form-model>
                        <a-form-model-item>
                          <a-button @click="parameters.push({val:''})">
                            <a-icon type="plus" />
                          </a-button>
                        </a-form-model-item>
                      </a-form-model>
                    </a-tab-pane>
                    <a-tab-pane key="2" tab="json">
                      <a-comment>
                        <div slot="content">
                          
                            <a-textarea
                              :rows="5"
                              v-model="json"
                              :value="json"                                  
                            />
                          
                        </div>
                      </a-comment>
                    </a-tab-pane>
                  </a-tabs>
                </a-col>
              </a-row>
            </a-tab-pane>
          </a-tabs>

          <a-row>
            <!--使用draggable组件-->
            <a-col :span="6">
              <draggable
                v-model="example"
                :group="{ name: 'site', pull: 'clone', put: false }"
                animation="300"
                dragClass="dragClass"
                ghostClass="ghostClass"
                chosenClass="chosenClass"
                @start="onStart"
                @end="onEnd"
              >
                <transition-group>
                  <div class="item" v-for="item in example" :key="item.index">
                    <AntdRowCol :colList="item.cols" />
                  </div>
                </transition-group>
              </draggable>
            </a-col>
            <a-col :span="6">
              <draggable
                v-model="typeSetList"
                group="site"
                animation="100"
                dragClass="dragClass"
                ghostClass="ghostClass"
                chosenClass="chosenClass"
                @start="onStart"
                @end="onEnd"
                @add="addItem"
              >
                <transition-group>
                  <div
                    class="item"
                    v-for="item in typeSetList"
                    :key="item.index"
                    v-bind:class="{
                      clickClass: item.index == typeSetListIndex,
                    }"
                    @click="typeSetClick(item)"
                  >
                    <AntdRowCol :colList="item.cols" />
                  </div>
                </transition-group>
              </draggable>
            </a-col>
            <a-col :span="12">
              <div
                class="colsInfo"
                v-for="(colsInfo, index) in currentWaterFall"
                :key="index"
              >
                <a-row>
                  <a-col :span="18">
                    <a-row>
                      <a-col :span="12">
                        <a-form-model
                          :label-col="labelCol"
                          :wrapper-col="wrapperCol"
                        >
                          <a-form-model-item label="name">
                            <a-input v-model="colsInfo.wname" />
                          </a-form-model-item>
                        </a-form-model>
                      </a-col>
                      <a-col :span="12">
                        <a-form-model
                          :label-col="labelCol"
                          :wrapper-col="wrapperCol"
                        >
                          <a-form-model-item label="title">
                            <a-input v-model="colsInfo.wtitle" />
                          </a-form-model-item>
                        </a-form-model>
                      </a-col>
                    </a-row>
                    <a-row>
                      <a-col :span="12">
                        <a-form-model
                          :label-col="labelCol"
                          :wrapper-col="wrapperCol"
                        >
                          <a-form-model-item label="testVal">
                            <a-input v-model="colsInfo.testVal" />
                          </a-form-model-item>
                        </a-form-model>
                      </a-col>
                      <a-col :span="12">
                        <a-form-model
                          :label-col="labelCol"
                          :wrapper-col="wrapperCol"
                        >
                          <a-form-model-item
                            label="colSize"
                            style="text-align: left"
                          >
                            <a-input-number
                              v-model="colsInfo.colSize"
                              :min="1"
                              :max="24"
                            />
                          </a-form-model-item>
                        </a-form-model>
                      </a-col>
                    </a-row>
                  </a-col>
                  <a-col :offset="2" :span="4" style="text-align: left">
                    <a-button-group>
                      <a-button
                        v-if="currentWaterFall.length != 1"
                        @click="decline(colsInfo)"
                      >
                        <a-icon type="minus" />
                      </a-button>
                      <a-button
                        v-if="index == currentWaterFall.length - 1"
                        @click="increase(colsInfo)"
                      >
                        <a-icon type="plus" />
                      </a-button>
                    </a-button-group>
                  </a-col>
                </a-row>
              </div>
            </a-col>
          </a-row>
        </a-spin>
      </a-layout-content>
    </a-layout>
  </a-layout>
</template>

<script>
import myStore from "@/components/Utils/Store";
import AntdRowCol from "@/components/Utils/AntdRowCol";
//导入draggable组件
import draggable from "vuedraggable";
export default {
  name: "Section",
  components: { draggable, AntdRowCol },
  data: function () {
    return {
      labelCol: { span: 10 },
      wrapperCol: { span: 14 },
      collapsed: false, //layout左边伸缩
      waterFallSearchName: "", //查询条件NAME
      waterFallSearchGuid: "", //查询条件GUID
      waterFallName: "", //NAME
      waterFallGuid: "", //GUID
      sqlText: "", //SQL
      json:"",
      api: "",
      RequestType: "POST",
      parameters: [{val:''}],
      // typeSet: "", //布局
      spinning: false, //content的等待框
      drawerVisible: false, //查询抽屉
      childrenDrawer: false,
      cname: myStore.userInfo.cname,
      headerTitle: "", //
      dragTag: false,
      contentTempList: [],
      // colsInfoList: [
      //   // { wname: "col1", colSize: 8, wtitle: "标题1", testVal: "内容1" },
      //   // { wname: "col2", colSize: 8, wtitle: "标题2", testVal: "内容2" },
      //   // { wname: "col3", colSize: 8, wtitle: "标题3", testVal: "内容3" },
      // ],
      //定义要被拖拽对象的数组
      example: [
        {
          index: 10001,
          cols: [
            { wname: "col1", colSize: 24, wtitle: "标题", testVal: "内容" },
          ],
        },
        {
          index: 10002,
          cols: [
            { wname: "col1", colSize: 12, wtitle: "标题1", testVal: "内容1" },
            { wname: "col2", colSize: 12, wtitle: "标题2", testVal: "内容2" },
          ],
        },
        {
          index: 10003,
          cols: [
            { wname: "col1", colSize: 8, wtitle: "标题1", testVal: "内容1" },
            { wname: "col2", colSize: 8, wtitle: "标题2", testVal: "内容2" },
            { wname: "col3", colSize: 8, wtitle: "标题3", testVal: "内容3" },
          ],
        },
      ],
      typeSetListIndex: -1,
      typeSetList: [
        {
          index: 1,
          cols: [
            {
              wname: "name",
              wtitle: "标题",
              testVal: "名称",
              colSize: "24",
            },
          ],
        },
        // {
        //   index: 2,
        //   cols: [
        //     {
        //       name: "sphh",
        //       title: "货号",
        //       testVal: "21XTX6702Y",
        //       colSize: "12",
        //     },
        //     { name: "ddsl", title: "订单数", testVal: "500", colSize: "12" },
        //   ],
        // },
      ],
    };
  },
  methods: {
    save() {
      if (this.waterFallName.length == 0) {
        this.openNotification("Name is empty");
        return;
      }
      this.spinning = true;
      this.savePromise().then((result) => {
        if (result.errcode == 0) {
          this.openNotification("success");
          this.waterFallGuid = result.data.id;
          this.spinning = false;
        } else {
          this.openNotification(result.errmsg);
          this.spinning = false;
        }
      });
    },
    savePromise() {
      let url = APIUTL + "?serviceName=svr-waterfall&action=waterfall";
      return new Promise((resolve, reject) => {
        this.$axiosPost
          .post(url, {
            name: this.waterFallName,
            id: this.waterFallGuid,
            sqlText: this.sqlText,
            json:this.json,
            RequestType: this.RequestType,
            parameters: JSON.stringify(this.parameters),
            api: this.api,
            typeSet: JSON.stringify(this.typeSetList),
            creator: myStore.userInfo.userid,
          })
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    addItem(evt) {
      //  console.log(this.typeSetList, 'add')
      let obj = JSON.parse(JSON.stringify(this.example[evt.oldIndex]));
      let maxIndex = 0;
      for (var i = 0; i < this.typeSetList.length; i++) {
        if (this.typeSetList[i].index < 1000)
          maxIndex =
            this.typeSetList[i].index > maxIndex
              ? this.typeSetList[i].index
              : maxIndex;
      }
      obj.index = maxIndex + 1;
      this.typeSetList.splice(evt.newIndex, 1, obj);
    },

    decline(item) {
      this.currentWaterFall.splice(this.currentWaterFall.indexOf(item), 1);
    },
    increase(item) {
      let i = {};
      i.wname = "col";
      i.colSize = 8;
      i.wtitle = "标题";
      i.testVal = "内容";
      this.currentWaterFall.push(i);
    },

    typeSetClick(item) {
      //this.colsInfoList = item.cols;
      this.typeSetListIndex = item.index;
      // console.log(item);
    },
    //开始拖拽事件
    onStart() {
      this.dragTag = true;
    },
    //拖拽结束事件
    onEnd(e) {
      // console.log(e);
      this.dragTag = false;
    },
    onChildrenDrawerClose() {
      //关闭2层抽屉
      this.childrenDrawer = false;
      this.drawerVisible = false;
    },
    showDrawer() {
      this.drawerVisible = true;
    },
    onClose() {
      this.drawerVisible = false;
    },
    chooseContent(item) {
      this.sqlText = item.sqlText;
      this.json = item.json;
      this.RequestType = item.RequestType;
      this.parameters = JSON.parse(item.parameters) || [{val:''}];
      this.api = item.api;
      this.typeSetList = JSON.parse(item.typeSet);
      this.waterFallName = item.name;
      this.waterFallGuid = item.id;
      this.typeSetListIndex = -1;
      this.onChildrenDrawerClose();
    },
    afterVisibleChange(v) {
      //切换抽屉时动画结束后的回调
      // console.log(v);
    },
    search() {
      if (
        this.waterFallSearchName.length > 0 ||
        this.waterFallSearchGuid.length > 0
      ) {
        this.spinning = true;
        this.searchPromise(
          this.waterFallSearchName,
          this.waterFallSearchGuid
        ).then((result) => {
          if (result.errcode == 0) {
            if (result.data[0].numberOfElements == 1) {
              //只有一条记录
              this.sqlText = result.data[0].content[0].sqlText;
              this.json = result.data[0].content[0].json;
              this.RequestType = result.data[0].content[0].RequestType;
              this.parameters = result.data[0].content[0].parameters && JSON.parse(
                result.data[0].content[0].parameters
              ) || [{val:''}];
              this.api = result.data[0].content[0].api;
              this.typeSetList = JSON.parse(result.data[0].content[0].typeSet);
              this.waterFallName = result.data[0].content[0].name;
              this.waterFallGuid = result.data[0].content[0].id;
              this.onClose();
              // this.headerTitle =
              //   result.data[0].content[0].id +
              //   "/" +
              //   result.data[0].content[0].name;
            } else if (result.data[0].numberOfElements > 1) {
              //多条不处理
              //this.openNotification("Multiple records");
              this.contentTempList = result.data[0].content;
              this.childrenDrawer = true;
            } else {
              //没记录
              this.onClose();
              this.openNotification("No record");
            }
            this.spinning = false;
          } else {
            this.onClose();
            this.openNotification(result.errmsg);
            this.spinning = false;
          }
        });
      } else {
        this.onClose();
        this.openNotification("查询条件不能空");
      }
    },
    searchPromise(waterFallSearchName, waterFallSearchGuid) {
      return new Promise((resolve, reject) => {
        this.$axiosPost
          .post(
            APIUTL +
              "?serviceName=svr-waterfall&action=waterfalls/search/1/100",
            { id: waterFallSearchGuid, name: waterFallSearchName }
          )
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    handleChange: function () {},
    handleSubmit: function () {},
    handleClick(event) {
      // If you don't want click extra trigger collapse, you can prevent this:
      event.stopPropagation();
    },

    openNotification(msg) {
      this.$notification.open({
        message: "-",
        description: msg,
        icon: <a-icon type="smile" style="color: #108ee9" />,
        onClick: () => {
          console.log("Notification Clicked!");
        },
      });
    },
  },
  mounted() {},
  watch: {
 
  },
  computed: {
    currentWaterFall() {
      // console.log(this.typeSetListIndex)
      // console.log(this.typeSetList)
      for (var i = 0; i < this.typeSetList.length; i++) {
        if (this.typeSetList[i].index == this.typeSetListIndex)
          return this.typeSetList[i].cols;
      }
    },
     
  },
  created() {},
};
</script>

<style scoped>
.colsInfo {
  padding-top: 15px;
  margin: 0 auto;
  border: 1px solid #e4ecec;
  border-radius: 5px;
  text-align: center;
}

.headerRigh > div:nth-child(2) {
  flex: 1;
}
.container {
  height: 100%;
}
.components-layout-demo-custom-trigger .trigger {
  font-size: 18px;
  line-height: 64px;
  padding: 0 24px;
  cursor: pointer;
  transition: color 0.3s;
}

.components-layout-demo-custom-trigger .trigger:hover {
  color: #1890ff;
}

.components-layout-demo-custom-trigger .logo {
  height: 32px;
  background: rgba(255, 255, 255, 0.2);
  margin: 16px;
}
/** drag*/
/*定义要拖拽元素的样式*/
.ghostClass {
  background-color: blue !important;
}
.clickClass {
  background-color: rgb(23, 155, 41) !important;
}
.chosenClass {
  background-color: red !important;
  opacity: 1 !important;
}
.dragClass {
  background-color: blueviolet !important;
  opacity: 1 !important;
  box-shadow: none !important;
  outline: none !important;
  background-image: none !important;
}

.item {
  padding: 6px 12px;
  margin: 0px 10px 0px 10px;
  border: solid 1px #eee;
  background-color: #f1f1f1;
}
.item:hover {
  background-color: #fdfdfd;
  cursor: move;
}
.item + .item {
  border-top: none;
  margin-top: 6px;
}
/** drag end*/
</style>
