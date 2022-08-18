import { post, get } from '@/scripts/request';
import { useAppStore } from "@/store";
const appStore = useAppStore();
export default {
    /**
     * 首页状态数据
     * 
     * @param {*} bRefresh 
     */
    async getWxUserInfo(bRefresh) {
        const userInfo = await appStore.getUserInfo();
        return get(`/admin/home/user-info/${userInfo.id}`, { bRefresh }, false);
    },
    /**
    * 获取登录二维码
    */
     async getLoginQrCode() {
        const userInfo = await appStore.getUserInfo();
        return get(`/admin/home/login-qrcode/${userInfo.id}`, null, false);
    },
}