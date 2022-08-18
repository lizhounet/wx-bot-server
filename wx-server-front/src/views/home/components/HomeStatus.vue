<template>
  <a-spin tip="正在获取登录二维码" :spinning="loading">
    <a-card class="home-status" :bordered="false" hoverable :headStyle="headStyle">
      <template #title>
        小助手状态
        <a-tag :color="state.online ? '#87d068' : '#6a615d'">{{ state.online ? '在线' : '离线' }}</a-tag>
      </template>
      <div class="content">
        <a-image :width="150" class="avatar" :preview="false" v-if="state.online" :src="state.wxUserInfo.avatarUrl" />
        <a-image :width="150" :preview="false" v-if="!state.online" :src="state.loginQrCode" />
        <template v-if="state.online">
          <div class="content-info">
            <div class="name">微信id: {{ state.wxUserInfo.wxId }}</div>
            <div class="name">微信code: {{ state.wxUserInfo.wxCode }}</div>
            <div class="name">昵称: {{ state.wxUserInfo.wxName }}</div>
          </div>
        </template>
        <template v-if="!state.online">
          <div class="content-info">
            <div class="name">请扫码登录</div>
            <a @click="methods.getLoginQrCode" >点击获取登录二维码</a>
          </div>
        </template>
      </div>
    </a-card>
  </a-spin>
</template>
<script>
export default { name: "HomeStatus" };
</script>
<script setup>
import { onMounted, ref, reactive } from "vue";

import homeService from "@/service/home/homeService";
import tools from "@/scripts/tools";


//微信用户信息
const state = reactive({
  wxUserInfo: {
    avatarUrl: "https://i.52112.com/icon/jpg/256/20210901/130307/5566843.jpg",
    wxId: "",
    wxCode: "未登录",
    wxName: "未登录",
  },
  loginQrCode: "https://i.52112.com/icon/jpg/256/20210901/130307/5566843.jpg",
  online: false,//是否在线
  remindLoginOk: false,//是否提醒成功登录
});
const loading = ref(false);


onMounted(() => {
  methods.startUpdateUserStatus();
});
const methods = {
  getLoginQrCode: async () => {
    loading.value = true;
    let res = await homeService.getLoginQrCode();
    if (res && res.code == 1) {
      if (res.data) {
        tools.message("登录二维码获取成功,请扫码登录!", "成功");
        state.loginQrCode = res.data;
        state.remindLoginOk = false;
      }
    }
    loading.value = false;
  },
  /**
   * 启动更新用户状态
   */
  startUpdateUserStatus: () => {
    methods.getWxUserInfo(true);
    window.updateUserStatusTimed = setInterval(()=>{methods.getWxUserInfo(true)}, 3000);
  },
  getWxUserInfo(bRefresh) {
    homeService.getWxUserInfo(bRefresh).then(res => {
      console.log(res);
      if (res && res.code == 1) {
        if (res.data) {
          state.wxUserInfo = res.data;
          state.online = state.wxUserInfo.wxId != null && state.wxUserInfo.wxId != ''
          if (state.online && !state.remindLoginOk) {
            tools.message("登陆成功!", "成功");
            state.remindLoginOk = true;
            clearInterval(window.updateUserStatusTimed)
          }
        }
      }
    }).catch(e => {
      console.log(e)
      clearInterval(window.updateUserStatusTimed)
    });
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

    .avatar {
      border-radius: 50%;
    }

    .content-info {
      margin-left: 35px;
      padding: 30px 0;

      .name {
        font-size: 15px;
        font-weight: 700;
      }

      .vx-code {
        margin-top: 5px;
      }
    }
  }
}
</style>
