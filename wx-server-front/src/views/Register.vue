<template>
    <div id="register">
        <div class="herder">
            <img class="logo" src="../assets/logo.png" alt="">
            <div class="titie">微信机器人管理平台</div>
        </div>
        <div class="content">
            <a-form :model="formState" :label-col="labelCol" :wrapper-col="wrapperCol">
                <a-form-item label="昵称：">
                    <a-input v-model:value="formState.name" />
                </a-form-item>
                <a-form-item label="账号：">
                    <a-input v-model:value="formState.userName" />
                </a-form-item>
                <a-form-item label="密码：">
                    <a-input v-model:value="formState.userPassword" />
                </a-form-item>
                <a-form-item :wrapper-col="{ span: 14, offset: 4 }">
                    <a-button type="primary" @click="onSubmit">注册</a-button>
                </a-form-item>
            </a-form>
        </div>
    </div>
</template>
<script setup>
import { reactive, toRaw } from 'vue';

const formState = reactive({
    name: '',
    userName: '',
    userPassword: '',
});
const onSubmit = () => {
    console.log('submit!', toRaw(formState));
};
</script>


<style lang="less" scoped>
#register {
    // background-color: #eff8fd;

    .herder {
        display: flex;
        align-items: center;
        padding: 20px 50px;
        height: 150px;
        line-height: 200px;

        .logo {
            width: 100px;
            height: 100px;
        }

        .titie {
            height: 150px;
            padding-top: -20px;
            margin-left: 10px;
            font-size: 25px;
            color: #46574d;
            font-weight: 500;
            // border-bottom: 1px solid #ccc;
        }
    }

    .content {
        height: calc(100vh - 200px);
        display: flex;
        justify-content: center;
        align-items: center;

        .ant-form {
            margin-top: 20px;
            width: 350px;
            height: 500px;

            .ant-input {
                // background-color: #eff8fd;

            }
        }
    }
}
</style>