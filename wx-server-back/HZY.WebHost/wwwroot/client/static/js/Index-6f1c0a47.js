import{b as N,r as U,a as C,i as V,o as z,c as u,j as r,d as m,f as t,w as n,l,q as g,A as x,v as f,F as j,C as A,n as h,e as P,t as T,k as q}from"./index-703a0050.js";import E from"./List-909695e3.js";import K from"./Cdrawer-ec2f9ddb.js";import{I as M,s as k}from"./Info-08bd16e6.js";import"./wxContactService-3495e852.js";const W=h("\u67E5\u8BE2"),G=h("\u91CD\u7F6E"),H=h(" \u65B0\u5EFA "),J=h(" \u6279\u91CF\u5220\u9664 "),Q=h("\u91CD\u7F6E"),X=h(" \u68C0\u7D22 "),Y=["onClick"],Z=["onClick"],ee=["onClick"],te=["onClick"],ae=["onClick"],ne=P("a",{class:"text-danger"},"\u5220\u9664",-1),oe={name:"wxSayEveryDayIndex"},ue=Object.assign(oe,{setup(ie){const D=N(),a=U({search:{state:!1,vm:{name:null}},loading:!1,pageSizeOptions:[10,20,50,100,500,1e3],rows:10,page:1,total:0,columns:[],data:[]}),b=C(null),w=C(null),L=C(!1),S=C(null),d=D.getPowerByMenuId(V.currentRoute.value.meta.menuId),o={onChange(i){const{currentPage:e,pageSize:p}=i;a.page=e==0?1:e,a.rows=p,o.findList()},onResetSearch(){a.page=1;let i=a.search.vm;for(let e in i)i[e]=null;o.findList()},findList(){a.loading=!0,k.findList(a.rows,a.page,a.search.vm).then(i=>{let e=i.data;a.loading=!1,a.page=e.page,a.rows=e.size,a.total=e.total,a.columns=e.columns,a.data=e.dataSource})},deleteList(i){let e=[];i?e.push(i):e=w.value.table.getCheckboxRecords().map(p=>p.id),k.deleteList(e).then(p=>{p.code==1&&(T.message("\u5220\u9664\u6210\u529F!","\u6210\u529F"),o.findList())})},openForm(i){b.value.openForm({visible:!0,key:i})},execTimedTask(i){k.execTimedTask(i).then(e=>{e.code==1&&(T.message("\u6267\u884C\u6210\u529F!","\u6210\u529F"),o.findList())})},startTimdTask(i){k.startTimdTask(i).then(e=>{e.code==1&&(T.message("\u542F\u52A8\u6210\u529F!","\u6210\u529F"),o.findList())})},stopTimdTask(i){k.stopTimdTask(i).then(e=>{e.code==1&&(T.message("\u505C\u6B62\u6210\u529F!","\u6210\u529F"),o.findList())})},showLog(i){k.queryRunLog(i).then(e=>{console.log(e),e.code==1&&(L.value=!0,S.value.state.data=e.data)})}};return z(()=>{o.findList()}),(i,e)=>{const p=u("a-input"),O=u("a-col"),_=u("a-button"),F=u("a-row"),$=u("a-popconfirm"),I=u("a-checkbox"),R=u("a-popover"),c=u("vxe-column"),y=u("a-divider");return r(),m("div",null,[t(E,{ref_key:"refList",ref:w,tableData:l(a),onOnChange:o.onChange},{search:n(()=>[t(F,{gutter:[15,15]},{default:n(()=>[t(O,{xs:24,sm:12,md:8,lg:6,xl:4},{default:n(()=>[t(p,{value:l(a).search.vm.name,"onUpdate:value":e[0]||(e[0]=s=>l(a).search.vm.name=s),placeholder:"\u540D\u79F0"},null,8,["value"])]),_:1}),t(O,{xs:24,sm:12,md:8,lg:6,xl:4,style:{float:"right"}},{default:n(()=>[t(_,{type:"primary",class:"mr-15",onClick:o.findList},{default:n(()=>[W]),_:1},8,["onClick"]),t(_,{class:"mr-15",onClick:o.onResetSearch},{default:n(()=>[G]),_:1},8,["onClick"])]),_:1})]),_:1})]),"toolbar-left":n(()=>[l(d).insert?(r(),g(_,{key:0,type:"primary",onClick:e[1]||(e[1]=s=>o.openForm())},{icon:n(()=>[t(x,{name:"PlusOutlined"})]),default:n(()=>[H]),_:1})):f("",!0),l(d).delete?(r(),g($,{key:1,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:e[2]||(e[2]=s=>o.deleteList()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:n(()=>[t(_,{type:"danger"},{icon:n(()=>[t(x,{name:"DeleteOutlined"})]),default:n(()=>[J]),_:1})]),_:1})):f("",!0)]),"toolbar-right":n(()=>[t(p,{value:l(a).search.vm.name,"onUpdate:value":e[3]||(e[3]=s=>l(a).search.vm.name=s),placeholder:"\u540D\u79F0",onKeyup:o.findList},null,8,["value","onKeyup"]),t(_,{onClick:o.onResetSearch},{default:n(()=>[Q]),_:1},8,["onClick"]),l(d).search?(r(),g(_,{key:0,onClick:e[4]||(e[4]=s=>l(a).search.state=!l(a).search.state)},{icon:n(()=>[t(x,{name:l(a).search.state?"UpOutlined":"DownOutlined"},null,8,["name"])]),default:n(()=>[X]),_:1})):f("",!0),t(R,null,{content:n(()=>[(r(!0),m(j,null,A(l(a).columns.filter(s=>s.fieldName.substr(0,1)!="_"),(s,v)=>(r(),m("div",{key:v},[t(I,{checked:s.show,"onUpdate:checked":B=>s.show=B,onChange:e[5]||(e[5]=()=>i.nextTick(()=>w.value.table.refreshColumn()))},{default:n(()=>[h(q(s.title),1)]),_:2},1032,["checked","onUpdate:checked"])]))),128))]),default:n(()=>[t(_,null,{default:n(()=>[t(x,{name:"BarsOutlined"})]),_:1})]),_:1})]),"table-col-default":n(()=>[t(c,{field:"receivingObjectWxId",title:"\u63A5\u6536\u5BF9\u8C61wxId",width:"150"}),t(c,{field:"receivingObjectName",title:"\u63A5\u6536\u5BF9\u8C61",width:"200"}),t(c,{field:"sendTime",title:"\u53D1\u9001\u65F6\u95F4(cron\u8868\u8FBE\u5F0F)",width:"150"}),t(c,{field:"city",title:"\u6240\u5728\u57CE\u5E02",width:"150"}),t(c,{field:"closingRemarks",title:"\u7ED3\u5C3E\u5907\u6CE8",width:"150"}),t(c,{field:"anniversaryDay",title:"\u7EAA\u5FF5\u65E5",width:"150"}),t(c,{field:"birthdayDate",title:"\u4E0B\u4E00\u6B21\u751F\u65E5\u65E5\u671F",width:"150"}),t(c,{field:"taskStateText",title:"\u8FD0\u884C\u72B6\u6001",width:"100"}),t(c,{field:"creationTime",title:"\u521B\u5EFA\u65F6\u95F4",width:"150"}),l(d).update||l(d).delete?(r(),g(c,{key:0,field:"id",title:"\u64CD\u4F5C",width:"300",fixed:"right"},{default:n(({row:s})=>[l(d).update?(r(),m(j,{key:0},[s.taskState==1?(r(),m("a",{key:0,href:"javascript:void(0)",onClick:v=>o.stopTimdTask(s.id)},"\u505C\u6B62",8,Y)):(r(),m("a",{key:1,href:"javascript:void(0)",onClick:v=>o.startTimdTask(s.id)},"\u542F\u52A8",8,Z))],64)):f("",!0),t(y,{type:"vertical"}),l(d).update?(r(),m("a",{key:1,href:"javascript:void(0)",onClick:v=>o.execTimedTask(s.id)},"\u7ACB\u5373\u53D1\u9001",8,ee)):f("",!0),t(y,{type:"vertical"}),l(d).update?(r(),m("a",{key:2,href:"javascript:void(0)",onClick:v=>o.showLog(s.id)},"\u67E5\u770B\u65E5\u5FD7",8,te)):f("",!0),t(y,{type:"vertical"}),l(d).update?(r(),m("a",{key:3,href:"javascript:void(0)",onClick:v=>o.openForm(s.id)},"\u7F16\u8F91",8,ae)):f("",!0),t(y,{type:"vertical"}),l(d).delete?(r(),g($,{key:4,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:v=>o.deleteList(s.id),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:n(()=>[ne]),_:2},1032,["onConfirm"])):f("",!0)]),_:1})):f("",!0)]),_:1},8,["tableData","onOnChange"]),t(M,{ref_key:"refForm",ref:b,onOnSuccess:e[6]||(e[6]=()=>o.findList())},null,512),t(K,{visible:L.value,ref_key:"refCdrawer",ref:S},null,8,["visible"])])}}});export{ue as default};
