<template>
  <a-modal v-model:visible="state.visible" :title="state.vm.id ? '编辑' : '新建'" centered @ok="state.visible = false"
    :width="1000">
    <template #footer>
      <a-button type="primary" @click="methods.saveForm()" :loading="state.saveLoading">提交</a-button>
      <a-button type="danger" ghost @click="state.visible = false" class="ml-15">关闭</a-button>
    </template>
    <a-spin :spinning="state.saveLoading">
      <a-form layout="vertical" :model="state.vm.form">
        <a-row :gutter="[15, 15]">
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <a-form-item label="关键词(多个用逗号,隔开)">
              <a-input v-model:value="state.vm.form.keyWord" placeholder="请输入 关键词" />
            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <a-form-item label="匹配类型(模糊匹配,精确匹配)">
              <a-select placeholder="请选择 匹配类型" v-model:value="state.vm.form.matchType" style="width: 200px">
                <a-select-option :value="1">模糊匹配</a-select-option>
                <a-select-option :value="2">精确匹配</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <a-form-item label="发送类型">
              <a-select placeholder="请选择 发送类型" v-model:value="state.vm.form.sendType" style="width: 200px">
                <a-select-option :value="1">文本内容</a-select-option>
                <a-select-option :value="2">新闻咨询</a-select-option>
                <a-select-option :value="3">故事大全</a-select-option>
                <a-select-option :value="4">土味情话</a-select-option>
                <a-select-option :value="5">笑话大全</a-select-option>
                <a-select-option :value="6">HTTP请求</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <a-form-item label="消息类型">
              <a-radio-group v-model:value="state.vm.form.messageType" name="messageType">
                <a-radio :value="1">文本</a-radio>
                <a-radio :value="2">图片</a-radio>
              </a-radio-group>
            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" v-show="state.vm.form.sendType == 1">
            <a-form-item label="发送内容(发送类型为文本时生效)">
              <a-textarea v-model:value="state.vm.form.sendContent" placeholder="请输入 发送内容(发送类型为文本时生效)" :rows="8" />
            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" v-show="state.vm.form.sendType == 6">
            <a-form-item label="http请求url(只支持GET请求)" name="httpSendUrl">
              <pre>
说明:
  会以url参数自动传递接受到的消息内容,参数名为:content
  举例:配置请求路径为 https://www.baidu.com
  则接口端收到的地址是 https://www.baidu.com?content=发送的消息内容
</pre>
              <a-input v-model:value="state.vm.form.httpSendUrl" placeholder="请输入 http请求url" />

            </a-form-item>
          </a-col>
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" v-show="state.vm.form.sendType == 6">
            <a-form-item label="取值表达式脚本" name="analyzeExpression">
              <pre>
说明:
 使用art-template引擎渲染解析;语法和js大同小异;语法参考:https://aui.github.io/art-template/zh-cn/docs/syntax.html
 在脚本中使用变量 R 就是http请求返回的json对象
示例:
 取对象属性： {'name':'张三'} 可以使用如下表达式取name：R.name
 取对象数组：{'data':[{'name':'测试'}]} 可以使用如下表达式取name：R.data[0].name
 循环生成模板： {'data':[{'name':'测试1'},{'name':'测试2'}]} 
<p></p>
</pre>
              <a-textarea v-model:value="state.vm.form.analyzeExpression" placeholder="请输入 取值表达式脚本(不填则发送http请求原始响应内容)"
                :rows="8" />

            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script setup>
import { reactive } from "vue";
import tools from "@/scripts/tools";
import service from "@/service/wxbot/wxKeywordReplyService";
import { useAppStore } from "@/store";

//定义组件事件
const emits = defineEmits(["onSuccess"]);
const appStore = useAppStore();
const state = reactive({
  vm: {
    id: "",
    form: {},
  },
  visible: false,
  saveLoading: false,
});

const methods = {
  findForm() {
    state.saveLoading = true;
    service.findForm(state.vm.id).then((res) => {
      state.saveLoading = false;
      if (res.code != 1) return;
      state.vm = res.data;
    });
  },
  saveForm() {
    state.saveLoading = true;
    if (!state.vm.form.keyWord) return tools.message("关键词必填!", "警告");
    if (!state.vm.form.messageType) return tools.message("消息类型必填!", "警告");
    state.vm.form.applicationToken = appStore.getApplicationToken();
    service.saveForm(state.vm.form).then((res) => {
      state.saveLoading = false;
      if (res.code != 1) return;
      tools.message("操作成功!", "成功");
      emits("onSuccess", res.data);
      state.visible = false;
    });
  },
  //打开表单初始化
  openForm({ visible, key }) {
    state.visible = visible;
    if (visible) {
      state.vm.id = key;
      methods.findForm();
    }
  },
};
// 暴露函数或者属性到外部
defineExpose({ ...methods });
</script>
<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 0;
}
</style>