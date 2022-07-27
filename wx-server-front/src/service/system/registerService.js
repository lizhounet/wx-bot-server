import { post } from '@/scripts/request';

export default {
    /**
     * 注册账号
     * 
     */
     register(registerData) {
        return post('account/register',registerData);
    }
}