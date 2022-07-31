import { post, get } from '@/scripts/request';
export default {
    /**
     * 首页状态数据
     * 
     * @param {*} userName 
     * @param {*} userPassword 
     */
    getWxUserInfo() {
        return get('/admin/home/user-info', null, false);
    },
    /**
    * 获取登录二维码
    */
    getLoginQrCode() {
        return get('/admin/home/login-qrcode');
    },
}