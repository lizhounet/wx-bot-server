import{r as k,L as x,o as M,c as n,j as t,d as i,f as l,w as s,F as u,E as C,h as r,x as c,C as L,m as f,t as S}from"./index-7474c45c.js";import{s as v}from"./timedTaskService-b697fb42.js";const y=f(" \u9996\u9875 "),w={key:0,style:{textAlign:"center",marginTop:"12px",height:"32px",lineHeight:"32px"}},B=f(" \u66F4\u591A... "),E={props:{formId:String},setup(_){const m=_,e=k({vm:x(()=>m.formId),loading:!0,loadingMore:!1,showLoadingMore:!0,table:{page:1,size:20},dataSource:[],input:"1",timer:void 0}),o={getloginfo(){e.loading=!0,v.getJobLoggers(e.vm,e.table.page,e.table.size).then(a=>{a.data.length>0?e.table.page==1?e.dataSource=a.data.concat(e.dataSource):e.dataSource=e.dataSource.concat(a.data):(S.message("\u672A\u67E5\u8BE2\u5230\u6570\u636E"),e.showLoadingMore=!1)}).finally(()=>{e.loading=!1,e.loadingMore=!1})},onLoadMore(){e.loadingMore=!0,e.table.page+=1,o.getloginfo(e.table.page)},firstPage(){e.table.page=1,e.dataSource=[],o.getloginfo()}};return M(()=>{o.getloginfo()}),(a,N)=>{const d=n("a-button"),h=n("a-step"),g=n("a-spin"),b=n("a-steps");return t(),i(u,null,[l(d,{class:"mb-15",onClick:o.firstPage},{default:s(()=>[y]),_:1},8,["onClick"]),l(g,{spinning:r(e).loading},{default:s(()=>[l(b,{"progress-dot":"",direction:"vertical"},{default:s(()=>[(t(!0),i(u,null,C(r(e).dataSource,p=>(t(),c(h,{key:p.id,status:"process",title:p.text},null,8,["title"]))),128)),r(e).showLoadingMore?(t(),i("div",w,[r(e).loadingMore?(t(),c(g,{key:0})):(t(),c(d,{key:1,onClick:o.onLoadMore},{default:s(()=>[B]),_:1},8,["onClick"]))])):L("",!0)]),_:1})]),_:1},8,["spinning"])],64)}}};export{E as default};
