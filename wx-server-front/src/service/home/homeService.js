import { post, get } from '@/scripts/request';
import { useAppStore } from "@/store";
const appStore = useAppStore();
const userInfo= await appStore.getUserInfo();
export default {
    /**
     * 首页状态数据
     * 
     * @param {*} userName 
     * @param {*} userPassword 
     */
    getWxUserInfo() {
        return get(`/admin/home/user-info/${userInfo.id}`, null, false);
    },
    /**
    * 获取登录二维码
    */
    getLoginQrCode() {
        return get(`/admin/home/login-qrcode/${userInfo.id}`,null, false);
    },
}