<template>
  <div>
    <List ref="refList" :tableData="state" @onChange="methods.onChange">
      <!-- 检索插槽 -->
      <template #search>
        <a-row :gutter="[15, 15]">
          <a-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4">
            <a-input v-model:value="state.search.vm.name" placeholder="名称" />
          </a-col>
          <!--button-->
          <a-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" style="float: right">
            <a-button type="primary" class="mr-15" @click="methods.findList">查询</a-button>
            <a-button class="mr-15" @click="methods.onResetSearch">重置</a-button>
          </a-col>
        </a-row>
      </template>
      <!-- 工具栏左侧插槽 -->
      <template #toolbar-left>
        <!-- 新建 -->
        <template v-if="power.insert">
          <a-button type="primary" @click="methods.openForm()">
            <template #icon>
              <AppIcon name="PlusOutlined" />
            </template>
            新建
          </a-button>
        </template>
        <!-- 批量删除 -->
        <template v-if="power.delete">
          <a-popconfirm title="您确定要删除吗?" @confirm="methods.deleteList()" okText="确定" cancelText="取消">
            <a-button type="danger">
              <template #icon>
                <AppIcon name="DeleteOutlined" />
              </template>
              批量删除
            </a-button>
          </a-popconfirm>
        </template>
      </template>
      <!-- 工具栏右侧插槽 -->
      <template #toolbar-right>
        <a-input v-model:value="state.search.vm.name" placeholder="名称" @keyup="methods.findList" />
        <a-button @click="methods.onResetSearch">重置</a-button>
        <!-- 检索 -->
        <template v-if="power.search">
          <a-button @click="state.search.state = !state.search.state">
            <template #icon>
              <AppIcon :name="state.search.state ? 'UpOutlined' : 'DownOutlined'" />
            </template>
            检索
          </a-button>
        </template>
        <!-- 列的隐藏显示 -->
        <a-popover>
          <template #content>
            <div v-for="(item,index) in state.columns.filter((w) => w.fieldName.substr(0, 1) != '_')" :key="index" >
              <a-checkbox v-model:checked="item.show" @change="() => nextTick(() => refList.table.refreshColumn())">{{ item.title }}</a-checkbox>
            </div>
          </template>
          <a-button><AppIcon name="BarsOutlined" /></a-button>
        </a-popover>
        <!--  -->
      </template>

      <!-- 表格 -->
      <template #table-col-default>
        <!-- 动态列 -->
        <vxe-column field="keyWord" title="关键词" show-overflow></vxe-column>
        <vxe-column field="matchTypeText" title="匹配类型(模糊匹配,精确匹配)" show-overflow></vxe-column>
        <vxe-column field="messageTypeText" title="消息类型" show-overflow></vxe-column>
        <vxe-column field="sendTypeText" title="发送类型" show-overflow></vxe-column>
        <vxe-column field="sendContent" title="发送内容" show-overflow></vxe-column>

          <!--  v-if="power.update || power.delete" 预防操作列还存在 -->
        <vxe-column field="id" title="操作" v-if="(power.update || power.delete)">
          <template #default="{ row }">
            <template v-if="power.update">
              <a href="javascript:void(0)" @click="methods.openForm(row.id)">编辑</a>
            </template>
            <a-divider type="vertical" />
            <template v-if="power.delete">
              <a-popconfirm title="您确定要删除吗?" @confirm="methods.deleteList(row.id)" okText="确定" cancelText="取消">
                <a class="text-danger">删除</a>
              </a-popconfirm>
            </template>
          </template>
        </vxe-column>
      </template>
    </List>

    <!--表单弹层-->
    <Info ref="refForm" @onSuccess="() => methods.findList()" />
  </div>
</template>

<script>
export default { name: "wxKeyordReplyIndex" };
</script>

<script setup>
import { defineComponent, onMounted, reactive, toRefs, ref } from "vue";
import { useAppStore } from "@/store";
import List from "@/components/curd/List.vue";
import AppIcon from "@/components/AppIcon.vue";
import Info from "./Info.vue";
import tools from "@/scripts/tools";
import service from "@/service/wxbot/wxKeywordReplyService";
import router from "@/router";

const appStore = useAppStore();

const state = reactive({
  //检索
  search: {
    state: false,
    vm: {
      name: null,
    },
  },
  loading: false,
  pageSizeOptions: [10, 20, 50, 100, 500, 1000],
  rows: 10,
  page: 1,
  total: 0,
  columns: [], //表列头
  data: [],
});

//表单 ref 对象
const refForm = ref(null);
const refList = ref(null);

//权限
const power = appStore.getPowerByMenuId(router.currentRoute.value.meta.menuId);

const methods = {
  onChange(info) {
    const { currentPage, pageSize } = info;
    state.page = currentPage == 0 ? 1 : currentPage;
    state.rows = pageSize;
    methods.findList();
  },
  //重置检索条件
  onResetSearch() {
    state.page = 1;
    let searchVm = state.search.vm;
    for (let key in searchVm) {
      searchVm[key] = null;
    }
    methods.findList();
  },
  //获取列表数据
  findList() {
    state.loading = true;
    service.findList(state.rows, state.page, state.search.vm).then((res) => {
      let data = res.data;
      state.loading = false;
      state.page = data.page;
      state.rows = data.size;
      state.total = data.total;
      state.columns = data.columns;
      state.data = data.dataSource;
    });
  },
  //删除数据
  deleteList(id) {
    let ids = [];
    if (id) {
      ids.push(id);
    } else {
      ids = refList.value.table.getCheckboxRecords().map((w) => w.id);
    }
    service.deleteList(ids).then((res) => {
      if (res.code != 1) return;
      tools.message("删除成功!", "成功");
      methods.findList();
    });
  },
  //打开表单页面
  openForm(id) {
    refForm.value.openForm({ visible: true, key: id });
  },
  // exportExcel() {
  //   service.exportExcel(state.search.vm);
  // },
};

onMounted(() => {
  methods.findList();
});
</script>