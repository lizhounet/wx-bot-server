import{h as l}from"./config-756651c9.js";import{s as x,r as p,o as h,v as u,c as o,j as g,q as w,w as s,f as r,n as i,k as c,l as a,e as n}from"./index-b138b776.js";var v={getWxUserInfo(){return x("/admin/WxBotConfig/WxUserInfo")}};const I=i(" \u5FAE\u79D8\u4E66\u72B6\u6001 "),U={class:"content"},y={class:"content-info"},S={class:"name"},b={name:"HomeStatus"},W=Object.assign(b,{setup(N){const e=p({wxUserInfo:{avatarUrl:"https://i.52112.com/icon/jpg/256/20210901/130307/5566843.jpg",wxId:"",wxCode:"\u672A\u767B\u5F55",wxName:"\u672A\u767B\u5F55"}});h(()=>{d.init()});const d={init:async()=>{let t=await v.getWxUserInfo();t&&t.code==1&&t.data&&(e.wxUserInfo=t.data)}};return u(e.wxUserInfo),(t,j)=>{const _=o("a-tag"),m=o("a-image"),f=o("a-card");return g(),w(f,{class:"home-status",bordered:!1,hoverable:"",headStyle:a(l)},{title:s(()=>[I,r(_,{color:a(e).wxUserInfo.wxId?"#87d068":"#6a615d"},{default:s(()=>[i(c(a(e).wxUserInfo.wxId?"\u5728\u7EBF":"\u79BB\u7EBF"),1)]),_:1},8,["color"])]),default:s(()=>[n("div",U,[r(m,{width:150,preview:!1,src:a(e).wxUserInfo.avatarUrl},null,8,["src"]),n("div",y,[n("div",S,c(a(e).wxUserInfo.wxName),1)])])]),_:1},8,["headStyle"])}}});export{W as default};
