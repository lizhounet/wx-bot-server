import { get, post, download } from '@/scripts/request';
import tools from "@/scripts/tools";

const controllerName = "admin/WxTimedTask";

export default {
    /**
     * 查询列表
     * 
     * @param {一页显示多少行} rows 
     * @param {当前页码} page 
     */
    findList(rows, page, search = {}) {
        return post(`${controllerName}/findList/${rows}/${page}`, search, false);
    },
    /**
     * 删除数据
     * 
     * @param {要删除的id 数组} ids 
     */
    deleteList(ids) {
        console.log(ids);
        if (ids && ids.length === 0) {
            return tools.message("请选择要删除的数据!", "警告");
        }
        return post(`${controllerName}/deleteList`, ids, false);
    },
    /**
     * 获取表单数据
     * 
     * @param {*} id 
     */
    findForm(id) {
        return get(`${controllerName}/findForm${(id ? '/' + id : '')}`);
    },
    /**
     * 保存表单
     * 
     * @param {表单数据} form 
     */
    saveForm(form) {
        return post(`${controllerName}/saveForm`, form);
    },
    /**
     * 导出 excel
     * @param {*} search 
     */
    exportExcel(search) {
        return download(`${controllerName}/exportExcel`, search);
    },
    /**
    * 执行定时任务
    * @param {定时任务id} id 
    */
    execTimedTask(id) {
        return post(`${controllerName}/exec/${id}`);
    },
    /**
    * 启动定时任务
    * @param {定时任务id} id 
    */
    startTimdTask(id) {
        return post(`${controllerName}/start/${id}`);
    },
    /**
    * 停止定时任务
    * @param {定时任务id} id 
    */
    stopTimdTask(id) {
        return post(`${controllerName}/stop/${id}`);
    },
    /**
    * 查询定时任务运行日志
    * @param {定时任务id} id 
    */
    queryRunLog(id) {
        return post(`${controllerName}/queryRunLog/${id}`);
    },

};