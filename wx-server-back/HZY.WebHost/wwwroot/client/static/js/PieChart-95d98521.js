var d=Object.defineProperty;var c=Object.getOwnPropertySymbols;var h=Object.prototype.hasOwnProperty,u=Object.prototype.propertyIsEnumerable;var o=(e,t,a)=>t in e?d(e,t,{enumerable:!0,configurable:!0,writable:!0,value:a}):e[t]=a,i=(e,t)=>{for(var a in t||(t={}))h.call(t,a)&&o(e,a,t[a]);if(c)for(var a of c(t))u.call(t,a)&&o(e,a,t[a]);return e};import{C as l}from"./index-535268b1.js";import{_ as p,x as f,r as m,J as s,o as b,H as j,v as O,j as _,d as g}from"./index-6b23fe46.js";const $=f({name:"LineChartSingle",props:{id:{type:String,default(){return new Date().getTime()+"_"+Math.floor(Math.random()*1e3)}},data:Array,height:{type:Number,default(){return 500}},valueAlias:String},setup(e){const t=m({id:s(()=>e.id),chartObject:null,data:s(()=>e.data)}),a=()=>{t.chartObject=new l({container:t.id,autoFit:!0,height:e.height}),t.chartObject.coordinate("theta",{radius:.75}),t.chartObject.scale("percent",{formatter:r=>(r=(r*100).toFixed(2)+"%",r)}),t.chartObject.tooltip({showTitle:!1,showMarkers:!1}),t.chartObject.interval().position("percent").color("type").label("percent",{content:r=>`${r.type}: ${(r.percent*100).toFixed(2)}%`}).adjust("stack"),t.chartObject.interaction("element-active")},n=r=>{!t.chartObject||(t.chartObject.annotation().clear(!0),t.chartObject.data(r),t.chartObject.render())};return b(()=>{a(),n(t.data)}),j(t.data,r=>{n(r)}),i({},O(t))}}),v=["id"];function w(e,t,a,n,r,x){return _(),g("div",{id:e.id},null,8,v)}var M=p($,[["render",w]]);export{M as default};
