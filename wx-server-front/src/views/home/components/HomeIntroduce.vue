<template>
  <a-card
    title="平台介绍"
    class="home-status"
    :bordered="false"
    hoverable
    :headStyle="headStyle"
  >
    <p>
      实现联系人管理、头像处理、智能机器人对话、好友互动、定时任务，情侣每日说等功能的配置
    </p>
    <p>让你的个人微信号，可以像公众号一样自动收发消息</p>
  </a-card>
</template>
<script>
export default { name: "HomeStatus" };
</script>
<script setup>
import { headStyle } from "@/views/home/config";
import { ref } from "vue";
</script>

<style lang="less">
.home-status {
  height: 249px;
  border-radius: 10px;
}
</style>
