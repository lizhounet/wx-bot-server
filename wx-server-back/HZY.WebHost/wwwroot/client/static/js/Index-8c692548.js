import{_ as E,r as N,a as U,o as q,R as z,c as i,j as _,d as k,f as n,w as t,l as c,F as M,C as V,n as a,t as L,q as p,e as m,k as f,z as u,A as D,g as A,h as J}from"./index-dfb70614.js";import G from"./Info-ecc334bf.js";import{s as b}from"./timedTaskService-0a917ffa.js";import P from"./JobLogger-956ef5ad.js";import"./GenerateCron-ed8b449c.js";import"./Index-852311c3.js";const w=v=>(A("data-v-839fa544"),v=v(),J(),v),R=a("\u6DFB\u52A0"),H={key:0},K=w(()=>m("span",{class:"normal bg-danger"},null,-1)),Q={key:1},W=w(()=>m("span",{class:"normal bg-cyan"},null,-1)),X=a("\u4EFB\u52A1\u89C4\u5219"),Y=a("POST"),Z=a("GET"),ee=a("DELETE"),te=a(" \u542F\u52A8 "),oe=a(" \u505C\u6B62 "),ne=a("\u4FEE\u6539"),se=a("\u5220\u9664"),ae=a("\u6267\u884C\u8BB0\u5F55"),le={name:"TimedTaskCom"},re=Object.assign(le,{setup(v){const l=N({list:[],filter:"",jobForm:{visible:!1,key:Object},timer:null}),x=U(null),s={findList(){b.findList(l.filter).then(r=>{l.list=r.data})},deleteList(r){let o=[];r?o.push(r):o=[],b.deleteList(o).then(d=>{d.code==1&&(L.message("\u5220\u9664\u6210\u529F!","\u6210\u529F"),s.findList())})},openForm(r){x.value.openForm({visible:!0,key:r})},run(r){let o=[];o.push(r),b.run(o).then(d=>{d.code==1&&(L.message("\u8FD0\u884C\u6210\u529F!","\u6210\u529F"),s.findList())})},close(r){let o=[];o.push(r),b.close(o).then(d=>{d.code==1&&(L.message("\u5173\u95ED\u6210\u529F!","\u6210\u529F"),s.findList())})},openJobLogger(r){l.jobForm.visible=!0,l.jobForm.key=r},onSearch(){s.findList()}};return q(()=>{s.findList(),l.timer=setInterval(()=>{s.findList()},60*1e3)}),z(()=>{clearInterval(l.timer)}),(r,o)=>{const d=i("a-button"),y=i("a-col"),j=i("a-input-search"),F=i("a-row"),I=i("a-divider"),g=i("a-tag"),C=i("a-menu-item"),S=i("a-menu"),$=i("a-dropdown"),T=i("a-card"),O=i("a-tooltip"),B=i("a-drawer");return _(),k("div",null,[n(F,{gutter:[15,15],class:"mb-15"},{default:t(()=>[n(y,{span:8},{default:t(()=>[n(d,{type:"primary",onClick:o[0]||(o[0]=e=>s.openForm(null))},{default:t(()=>[R]),_:1})]),_:1}),n(y,{span:8,offset:8},{default:t(()=>[n(j,{value:c(l).filter,"onUpdate:value":o[1]||(o[1]=e=>c(l).filter=e),placeholder:"\u8BF7\u8F93\u5165\u5173\u952E\u5B57\u68C0\u7D22","enter-button":"",onSearch:s.onSearch},null,8,["value","onSearch"])]),_:1})]),_:1}),n(F,{gutter:[15,15]},{default:t(()=>[(_(!0),k(M,null,V(c(l).list,e=>(_(),p(y,{xs:24,sm:12,md:8,lg:6,xl:4},{default:t(()=>[n(O,{placement:"top"},{title:t(()=>[m("span",null,f(e.apiUrl),1)]),default:t(()=>[n(T,{hoverable:""},{default:t(()=>[e.state==0?(_(),k("h3",H,[K,a(" "+f(e.name),1)])):u("",!0),e.state==1?(_(),k("h3",Q,[W,a(" "+f(e.name),1)])):u("",!0),m("h4",null,f(e.groupName),1),m("p",null,[X,n(I,{type:"vertical"}),a(f(e.cron),1)]),e.requsetMode==0?(_(),p(g,{key:2,color:"blue"},{default:t(()=>[Y]),_:1})):u("",!0),e.requsetMode==1?(_(),p(g,{key:3,color:"green"},{default:t(()=>[Z]),_:1})):u("",!0),e.requsetMode==2?(_(),p(g,{key:4,color:"orange"},{default:t(()=>[ee]),_:1})):u("",!0),a(" "+f(e.executeTime)+" ",1),n(I),e.state==0?(_(),p(d,{key:5,type:"primary",size:"small",onClick:h=>s.run(e.id)},{default:t(()=>[te]),_:2},1032,["onClick"])):u("",!0),e.state==1?(_(),p(d,{key:6,type:"primary",size:"small",danger:"",onClick:h=>s.close(e.id)},{default:t(()=>[oe]),_:2},1032,["onClick"])):u("",!0),n($,{class:"ml-15"},{overlay:t(()=>[n(S,null,{default:t(()=>[n(C,{key:"1",onClick:h=>s.openForm(e.id)},{default:t(()=>[ne]),_:2},1032,["onClick"]),n(C,{key:"2",onClick:h=>s.deleteList(e.id)},{default:t(()=>[se]),_:2},1032,["onClick"]),n(C,{key:"3",onClick:h=>s.openJobLogger(e.id)},{default:t(()=>[ae]),_:2},1032,["onClick"])]),_:2},1024)]),default:t(()=>[n(d,{size:"small"},{default:t(()=>[n(D,{name:"DashOutlined"})]),_:1})]),_:2},1024)]),_:2},1024)]),_:2},1024)]),_:2},1024))),256))]),_:1}),n(G,{ref_key:"refForm",ref:x,onOnSuccess:o[2]||(o[2]=e=>s.findList())},null,512),n(B,{title:"\u6267\u884C\u8BB0\u5F55",placement:"right",closable:!1,visible:c(l).jobForm.visible,"onUpdate:visible":o[4]||(o[4]=e=>c(l).jobForm.visible=e),width:1e3,destroyOnClose:""},{default:t(()=>[n(P,{formId:c(l).jobForm.key,"onUpdate:formId":o[3]||(o[3]=e=>c(l).jobForm.key=e)},null,8,["formId"])]),_:1},8,["visible"])])}}});var fe=E(re,[["__scopeId","data-v-839fa544"]]);export{fe as default};
