<template>
  <a-card
    title="项目说明"
    class="home-description"
    :bordered="false"
    hoverable
    :headStyle="headStyle"
  >
    <h3>源码地址：<a href="https://github.com/lizhounet/wx-intimate-pro" target="_blank">https://github.com/lizhounet/wx-intimate-pro</a></h3>
    <h3>使用教程：<a href="https://www.zhouli.info/archives/wx-intimate-pro" target="_blank">https://www.zhouli.info/archives/wx-intimate-pro</a></h3>
    <p>
      注：用户微信会在第一次启动机器人成功后进行绑定，请选择合适的微信启动机器人。<br />
      智能配置的任务需用户微信成功启动机器人过后才能获取到联系人。<br />
      机器人启动默认开启群聊回复 私聊回复默认关闭<br />
      群聊中需有人@机器人 才会进行回复<br />
      给机器人发送"更新配置"机器人会自动拉取最新配置信息<br />
      给机器人发送"更新联系人"机器人会自动上传最新联系人到后台
    </p>
  </a-card>
</template>
<script>
export default { name: "HomeDescription" };
</script>
<script setup>
import { headStyle } from "@/views/home/config";
import { ref } from "vue";
</script>

<style lang="less">
.home-description {
  margin-top: 15px;
  border-radius: 10px;
  font-size: 16px;

  .ant-card-body {
    height: 290px;
    overflow: auto;
  }
}
</style>
