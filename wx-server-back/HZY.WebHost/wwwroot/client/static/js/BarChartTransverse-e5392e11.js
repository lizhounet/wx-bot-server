var s=Object.defineProperty;var c=Object.getOwnPropertySymbols;var d=Object.prototype.hasOwnProperty,h=Object.prototype.propertyIsEnumerable;var i=(e,t,a)=>t in e?s(e,t,{enumerable:!0,configurable:!0,writable:!0,value:a}):e[t]=a,l=(e,t)=>{for(var a in t||(t={}))d.call(t,a)&&i(e,a,t[a]);if(c)for(var a of c(t))h.call(t,a)&&i(e,a,t[a]);return e};import{C as u}from"./index-535268b1.js";import{_ as f,A as b,r as j,L as o,o as m,J as p,z as O,j as v,d as _}from"./index-5b43cdaa.js";const g=b({name:"LineChartSingle",props:{id:{type:String,default(){return new Date().getTime()+"_"+Math.floor(Math.random()*1e3)}},data:Array,height:{type:Number,default(){return 500}},valueAlias:String},setup(e){const t=j({id:o(()=>e.id),chartObject:null,data:o(()=>e.data)}),a=()=>{t.chartObject=new u({container:t.id,autoFit:!0,height:e.height}),t.chartObject.scale({value:{min:0,alias:e.valueAlias}}),t.chartObject.axis("type",{title:null,tickLine:null,line:null}),t.chartObject.axis("value",{label:null,title:{offset:30,style:{fontSize:12,fontWeight:300}}}),t.chartObject.legend(!1),t.chartObject.coordinate().transpose(),t.chartObject.interval().position("type*value").size(26).label("value",{style:{fill:"#8d8d8d"},offset:10}),t.chartObject.interaction("element-active")},n=r=>{!t.chartObject||(t.chartObject.annotation().clear(!0),t.chartObject.data(r),t.chartObject.render())};return m(()=>{a(),n(t.data)}),p(t.data,r=>{n(r)}),l({},O(t))}}),y=["id"];function C(e,t,a,n,r,x){return v(),_("div",{id:e.id},null,8,y)}var k=f(g,[["render",C]]);export{k as default};
