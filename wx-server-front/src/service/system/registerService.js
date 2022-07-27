import { post } from '@/scripts/request';

export default {
    /**
     * 注册账号
     * 
     */
    register(registerData) {
        return post('account/register', registerData);
    },
    /**
     * 获取验证码
     * @param {*} data 请求参数
     * @returns 
     */
    getverifyCode(data) {
        return post('account/register-verifyCode', data, false);
    }
}