<template>
  <a-card
    class="home-status"
    :bordered="false"
    hoverable
    :headStyle="headStyle"
  >
    <template #title>
      微秘书状态
      <a-tag :color="state.wxUserInfo.wxId ? '#87d068' : '#6a615d'">{{
        state.wxUserInfo.wxId ? "在线" : "离线"
      }}</a-tag>
    </template>
    <div class="content">
      <vue-qr
        :logoSrc="qrCodeLoginUrl"
        text="https://blog.csdn.net/weixin_42601136"
        :size="200"
      ></vue-qr>
      <!-- <a-image
        :width="150"
        :preview="false"
        :src="state.wxUserInfo.avatarUrl"
      />
      <div class="content-info">
        <div class="name">{{ state.wxUserInfo.wxName }}</div>
        <div class="vx-code">微信Code：{{ state.wxUserInfo.wxCode }}</div>
      </div> -->
    </div>
  </a-card>
</template>
<script >
export default {
  name: "HomeStatus",
};
</script >
<script setup>
import vueQr from "vue-qr";
import { onMounted, reactive } from "vue";
import homeService from "@/service/home/homeService";
import qrCodeLoginUrl from "@/assets/logo.png";

//微信用户信息
const state = reactive({
  wxUserInfo: {
    avatarUrl: "https://i.52112.com/icon/jpg/256/20210901/130307/5566843.jpg",
    wxId: "",
    wxCode: "未登录",
    wxName: "未登录",
  },
  qrCodeLoginUrl: qrCodeLoginUrl,
  loginQrCodeInfo: {},
});

onMounted(() => {
  methods.init();
});
const methods = {
  init: async () => {
    let res = await homeService.getWxUserInfo();
    if (res && res.code == 1) {
      if (res.data) state.wxUserInfo = res.data;
    }
  },
};
</script>

<style lang="less">
.home-status {
  height: 249px;
  border-radius: 10px;

  .content {
    display: flex;
    padding: 0 20px;

    .ant-image {
      .ant-image-img {
        border-radius: 50%;
      }
    }

    .content-info {
      margin-left: 35px;
      padding: 30px 0;

      .name {
        font-size: 30px;
        font-weight: 700;
      }

      .vx-code {
        margin-top: 5px;
      }
    }
  }
}
</style>
