import { post, get } from '@/scripts/request';
export default {
    /**
     * 首页状态数据
     * 
     * @param {*} userName 
     * @param {*} userPassword 
     */
    getWxUserInfo() {
        return get('/admin/WxBotConfig/WxUserInfo');
    },
    /**
  * 首页状态数据
  * 
  * @param {string} salt 获取登录二维码时的返回值
  */
    checkLogin(salt) {
        return get(`/api/feixunbot/check-login/${salt}`);
    },
    /**
    * 获取登录二维码
    */
    getLoginQrCode() {
        return get('/api/feixunbot/login-qrcode');
    },
}