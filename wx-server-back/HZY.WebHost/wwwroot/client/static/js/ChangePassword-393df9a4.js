import{a as P,r as b,c as t,j as x,q,w as s,f as e,l as r,n as f,t as p}from"./index-dfb70614.js";import{s as y}from"./personal_center_service-02795948.js";const h=f("\u63D0\u4EA4"),k=f("\u91CD\u7F6E"),C={name:"system_change_password"},B=Object.assign(C,{setup(F){const u=P(null),a=b({vm:{oldPassword:"",newPassword:"",qrPassword:""}}),w={oldPassword:[{required:!0,message:"\u8BF7\u8F93\u5165\u65E7\u5BC6\u7801",trigger:"blur"},{min:3,max:20,message:"\u957F\u5EA6\u5728 3 \u81F3 20 \u4E4B\u95F4",trigger:"blur"}],newPassword:[{required:!0,message:"\u8BF7\u8F93\u5165\u65B0\u5BC6\u7801",trigger:"blur"},{min:3,max:20,message:"\u957F\u5EA6\u5728 3 \u81F3 20 \u4E4B\u95F4",trigger:"blur"}],qrPassword:[{required:!0,message:"\u8BF7\u8F93\u5165\u786E\u8BA4\u65B0\u5BC6\u7801",trigger:"blur"},{min:3,max:20,message:"\u957F\u5EA6\u5728 3 \u81F3 20 \u4E4B\u95F4",trigger:"blur"}]},c={saveForm(){u.value.validate().then(()=>{if(a.vm.newPassword!=a.vm.qrPassword)return p.message("\u4E24\u6B21\u5BC6\u7801\u4E0D\u4E00\u81F4!","\u8B66\u544A");y.changePassword(a.vm).then(n=>{n.code==1&&p.message("\u64CD\u4F5C\u6210\u529F!","\u6210\u529F")})}).catch(n=>{console.log("error",n)})},resetForm(){u.value.resetFields()}};return(n,o)=>{const _=t("a-input"),d=t("a-form-item"),m=t("a-col"),i=t("a-button"),v=t("a-row"),g=t("a-form");return x(),q(g,{layout:"vertical",ref_key:"formRef",ref:u,model:r(a).vm,rules:w},{default:s(()=>[e(v,{gutter:[15,15]},{default:s(()=>[e(m,{xs:24},{default:s(()=>[e(d,{label:"\u65E7\u5BC6\u7801",name:"oldPassword"},{default:s(()=>[e(_,{value:r(a).vm.oldPassword,"onUpdate:value":o[0]||(o[0]=l=>r(a).vm.oldPassword=l),type:"password",placeholder:"\u65E7\u5BC6\u7801"},null,8,["value"])]),_:1})]),_:1}),e(m,{xs:24},{default:s(()=>[e(d,{label:"\u65B0\u5BC6\u7801",name:"newPassword"},{default:s(()=>[e(_,{value:r(a).vm.newPassword,"onUpdate:value":o[1]||(o[1]=l=>r(a).vm.newPassword=l),type:"password",placeholder:"\u65B0\u5BC6\u7801"},null,8,["value"])]),_:1})]),_:1}),e(m,{xs:24},{default:s(()=>[e(d,{label:"\u786E\u8BA4\u65B0\u5BC6\u7801",name:"qrPassword"},{default:s(()=>[e(_,{value:r(a).vm.qrPassword,"onUpdate:value":o[2]||(o[2]=l=>r(a).vm.qrPassword=l),type:"password",placeholder:"\u786E\u8BA4\u65B0\u5BC6\u7801"},null,8,["value"])]),_:1})]),_:1}),e(m,{xs:24},{default:s(()=>[e(d,{"wrapper-col":{span:14}},{default:s(()=>[e(i,{type:"primary",onClick:c.saveForm},{default:s(()=>[h]),_:1},8,["onClick"]),e(i,{style:{"margin-left":"10px"},onClick:c.resetForm},{default:s(()=>[k]),_:1},8,["onClick"])]),_:1})]),_:1})]),_:1})]),_:1},8,["model"])}}});export{B as default};
