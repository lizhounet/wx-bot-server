import{s as m}from"./monitor_ef_core_service-a013c08d.js";import{a as u,R as f,c as n,j as v,d as w,f as t,w as a,e as o,k as s,n as h}from"./index-2e9c34c5.js";const x={class:"text-danger"},b={setup(k){const d=u([]),i={getNewest(){m.getNewest().then(r=>{d.value=r.data})}};i.getNewest();let c=u(null);return c.value=setInterval(()=>{i.getNewest()},10*1e3),f(()=>{clearInterval(c.value)}),(r,N)=>{const l=n("a-table-column"),_=n("a-popover"),p=n("a-table");return v(),w("div",null,[t(p,{"data-source":d.value},{default:a(()=>[t(l,{key:"index",title:"\u5E8F\u53F7",width:80},{default:a(({index:e})=>[o("span",null,s(e+1),1)]),_:1}),t(l,{key:"sql",title:"Sql \u811A\u672C","data-index":"sql",ellipsis:!0},{default:a(({text:e})=>[t(_,{title:"Sql \u811A\u672C"},{content:a(()=>[h(s(e),1)]),default:a(()=>[o("div",null,s(e),1)]),_:2},1024)]),_:1}),t(l,{key:"elapsedMilliseconds",title:"\u8017\u65F6","data-index":"elapsedMilliseconds",width:100},{default:a(({text:e})=>[o("span",x,s(e)+" \u6BEB\u79D2",1)]),_:1}),t(l,{key:"time",title:"\u8BB0\u5F55\u65F6\u95F4","data-index":"time",width:200})]),_:1},8,["data-source"])])}}};export{b as default};
