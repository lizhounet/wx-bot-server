<template>
  <div id="register">
    <div class="register-modal"></div>
    <div class="register-container">
      <a-card>
        <div class="register-title">{{ title }}注册</div>
        <div class="p-24">
          <a-form layout="vertical">
            <a-form-item label="昵称">
              <a-input v-model:value="state.name" placeholder="请输入昵称" />
            </a-form-item>
            <a-form-item label="邮箱">
              <a-input v-model:value="state.email" placeholder="请输入邮箱" />
            </a-form-item>
            <a-form-item label="验证码">
              <a-input-group>
                <a-input
                  v-model:value="state.verifyCode"
                  placeholder="发送验证码"
                  style="width: calc(100% - 103px); text-align: left"
                />
                <a-button
                  @click="methods.getCode"
                  type="link"
                  :disabled="verifyCodeInfo.codeBtnDisabled"
                  >{{ verifyCodeInfo.codeBtnWord }}</a-button
                >
              </a-input-group>
              <!-- <a-input v-model:value="state.email" placeholder="请输入邮箱" /> -->
            </a-form-item>
            <a-form-item label="密码">
              <a-input
                v-model:value="state.password"
                placeholder="请输入密码"
              />
            </a-form-item>
            <a-form-item>
              <a-button
                type="primary"
                @click.enter="methods.register"
                :loading="loading"
                size="large"
                block
                >注册</a-button
              >
            </a-form-item>
          </a-form>
          <div class="login">
            <a-button type="link" @click="methods.goLogin" size="small"
              >去登录</a-button
            >
          </div>
        </div>
      </a-card>
    </div>
  </div>
</template>

<script setup>
import { reactive, ref, onMounted } from "vue";
import { useLayoutStore, useAppStore } from "@/store";
import router from "@/router/index";
import tools from "@/scripts/tools";
import registerService from "@/service/system/registerService";

const state = reactive({
  name: "",
  password: "",
  verifyCode: "",
  email: "",
});
const verifyCodeInfo = reactive({
  codeCountdown: 60,
  codeBtnWord: "获取验证码",
  codeBtnDisabled: false,
});

const loading = ref(false);

const layoutStore = useLayoutStore();
const appStore = useAppStore();
const title = layoutStore.state.title;

const methods = {
  register() {
    if (!state.name) return tools.message("昵称不能为空!", "警告");
    if (!state.password) return tools.message("密码不能为空!", "警告");
    if (!state.verifyCode) return tools.message("验证码不能为空!", "警告");
    if (!state.email) return tools.message("邮箱不能为空!", "警告");
    if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(state.email))
      return tools.message("邮箱格式不正确!", "警告");
    loading.value = true;
    registerService
      .register(state)
      .then((res) => {
        if (res.code !== 1) return (loading.value = false);
        tools.alert("注册成功,前往登录！", "成功", () => {
          router.push({ path: "/login" });
        });
      })
      .catch(() => {
        loading.value = false;
      });
  },
  getCode() {
    if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(state.email)) {
      return tools.message("邮箱格式不正确!", "警告");
    }
    verifyCodeInfo.codeBtnDisabled = true;
    //获取验证码
    registerService
      .getverifyCode({
        email: state.email,
        ts: tools.getTimeStamp(),
      })
      .then((res) => {
        if (res.code !== 1) return;
        tools.message("验证码发送成功,请注意查收!", "成功");
        //倒计时验证码
        let timer = setInterval(() => {
          if (verifyCodeInfo.codeCountdown > 1) {
            verifyCodeInfo.codeCountdown--;
            verifyCodeInfo.codeBtnWord = `${verifyCodeInfo.codeCountdown}s 后获取`;
          } else {
            clearInterval(timer);
            verifyCodeInfo.codeBtnDisabled = false;
            verifyCodeInfo.codeBtnWord = "获取验证码";
            verifyCodeInfo.codeCountdown = 60;
          }
        }, 1000);
      });
  },
  goLogin() {
    router.push({ path: "/login" });
  },
};
onMounted(() => {});
</script>
<style lang="less" scoped>
#register {
  text-align: center;
  position: fixed;
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  //可以解开一下注解 放置一个背景图片
  // background: url("../assets/images/register3.jpg") no-repeat;
  // background: url("../assets/undraw_Tree_swing_re_pqee.png") no-repeat;
  background-size: cover;
  background: #f0f2f5 url("../assets/background.svg") no-repeat 50%;

  .register-modal {
    position: absolute;
    width: 100%;
    height: 100%;
    // background-color: #000000;
    // background: #f0f2f5;
    margin: 0 auto;
    // opacity: 0.3;
  }

  .register-container {
    position: absolute;
    width: 26rem;
    top: 50%;
    left: 50%;
    -webkit-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);

    .register-title {
      padding: 20px;
      font-size: 25px;
      font-weight: 500;
      padding-top: 24px;
      padding-bottom: 24px;
    }

    .form-title {
      text-align: left;
    }

    .ant-card {
      border: 0;
    }

    .login {
      text-align: right;
    }
  }

  @media (max-width: 720px) {
    .register-container {
      width: 100%;

      .ant-card {
        padding: 0;
      }
    }
  }
}
</style>
