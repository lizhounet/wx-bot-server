import{p as t,t as n,y as a,aU as s}from"./index-5b43cdaa.js";const r="admin/SysOrganization";var l={findList(e={}){return t(`${r}/findList/`,e,!1)},deleteList(e){return console.log(e),e&&e.length===0?n.message("\u8BF7\u9009\u62E9\u8981\u5220\u9664\u7684\u6570\u636E!","\u8B66\u544A"):t(`${r}/deleteList`,e,!1)},findForm(e,o){return a(`${r}/findForm/${e||n.guidEmpty}/${o||""}`)},saveForm(e){return t(`${r}/saveForm`,e)},exportExcel(e){return s(`${r}/exportExcel`,e)},sysOrganizationTree(){return t(`${r}/sysOrganizationTree`)}};export{l as o};
