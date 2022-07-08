var H=Object.defineProperty;var U=Object.getOwnPropertySymbols;var M=Object.prototype.hasOwnProperty,E=Object.prototype.propertyIsEnumerable;var I=(s,r,d)=>r in s?H(s,r,{enumerable:!0,configurable:!0,writable:!0,value:d}):s[r]=d,V=(s,r)=>{for(var d in r||(r={}))M.call(r,d)&&I(s,d,r[d]);if(U)for(var d of U(r))E.call(r,d)&&I(s,d,r[d]);return s};import{p as w,b as Q,r as W,a as C,i as X,H as Y,aS as Z,c as u,j as b,q as g,w as a,f as e,e as O,K as ee,L as te,l as n,A as k,v as ae,n as m,k as K,t as z}from"./index-2e9c34c5.js";import{C as le}from"./CodeGeneration-3de6504f.js";import oe from"./CodeLoadToProject-bffd61de.js";import"./MdEditorShowCode-c437a92b.js";const N="admin/LowCodeTableInfo";var T={findList(s,r,d={}){return w(`${N}/findList/${s}/${r}`,d,!1)},deleteList(s){return s&&s.length===0?tools.message("\u8BF7\u9009\u62E9\u8981\u5220\u9664\u7684\u6570\u636E!","\u8B66\u544A"):w(`${N}/deleteList`,s,!1)},synchronization(s){return w(`${N}/synchronization/${s}`,null,!1)},change(s){return w(`${N}/change`,s,!1)}};const ne=m("\u67E5\u8BE2"),se=m("\u91CD\u7F6E"),ie=m(" \u68C0\u7D22 "),re=m(" \u540C\u6B65\u5B57\u6BB5 "),de=m(" \u63D0\u4EA4\u66F4\u6539 "),ce=m(" \u6279\u91CF\u5220\u9664 "),ue=m("\u662F"),pe=m("\u5426"),me=m("\u662F"),fe=m("\u5426"),_e=O("a",{class:"text-danger"},"\u5220\u9664",-1),be={name:"LowCode-ColumnIndex"},we=Object.assign(be,{setup(s,{expose:r}){const d=Q(),t=W({table:{search:{state:!1,vm:{columnName:null,describe:null,low_Code_TableId:null}},loading:!1,pageSizeOptions:[10,20,50,100,500,1e3],rows:20,page:1,total:0,data:[]},row:{},title:"",activeKey:"1"}),F=C(null),$=C(null),P=C(null),D=C(null),R=d.getPowerByMenuId(X.currentRoute.value.meta.menuId),c={onChange(i){const{currentPage:l,pageSize:f}=i;t.table.page=l==0?1:l,t.table.rows=f,c.findList()},onResetSearch(){t.table.page=1;let i=t.table.search.vm;for(let l in i)l!="low_Code_TableId"&&(i[l]=null);c.findList()},findList(){t.table.loading=!0,T.findList(t.table.rows,t.table.page,t.table.search.vm).then(i=>{let l=i.data;t.table.loading=!1,t.table.page=l.page,t.table.rows=l.size,t.table.total=l.total,t.table.data=l.dataSource})},deleteList(i){let l=[];i?l.push(i):l=$.value.getCheckboxRecords().map(f=>f.id),T.deleteList(l).then(f=>{f.code==1&&(z.message("\u5220\u9664\u6210\u529F!","\u6210\u529F"),c.findList())})},openForm(i){F.value.openForm({visible:!0,key:i})},synchronization(){T.synchronization(t.row.id).then(i=>{z.message("\u540C\u6B65\u6210\u529F!","\u6210\u529F"),c.findList()})},change(){T.change(t.table.data).then(i=>{z.message("\u6570\u636E\u53D8\u66F4\u6210\u529F!","\u6210\u529F"),c.findList()})},loadData(i){t.row=i,t.table.search.vm.low_Code_TableId=t.row.id,c.findList()}};return r(V({},c)),Y(()=>t.activeKey,i=>{i=="3"&&Z(()=>{P.value.openForm({key:t.row.tableName})})}),(i,l)=>{const f=u("a-input"),v=u("a-col"),_=u("a-button"),S=u("a-row"),j=u("a-card"),h=u("a-popconfirm"),B=u("a-space"),p=u("vxe-column"),y=u("a-tag"),J=u("vxe-table"),A=u("vxe-pager"),G=u("a-spin"),L=u("a-tab-pane"),q=u("a-tabs");return b(),g(q,{activeKey:n(t).activeKey,"onUpdate:activeKey":l[10]||(l[10]=o=>n(t).activeKey=o)},{default:a(()=>[e(L,{key:"1",tab:"\u5B57\u6BB5\u8BBE\u7F6E"},{default:a(()=>[O("div",null,[ee(e(j,{class:"mb-15"},{default:a(()=>[e(S,{gutter:[15,15]},{default:a(()=>[e(v,{xs:24,sm:12,md:8,lg:6,xl:4},{default:a(()=>[e(f,{value:n(t).table.search.vm.columnName,"onUpdate:value":l[0]||(l[0]=o=>n(t).table.search.vm.columnName=o),placeholder:"\u540D\u79F0"},null,8,["value"])]),_:1}),e(v,{xs:24,sm:12,md:8,lg:6,xl:4},{default:a(()=>[e(f,{value:n(t).table.search.vm.describe,"onUpdate:value":l[1]||(l[1]=o=>n(t).table.search.vm.describe=o),placeholder:"\u663E\u793A\u540D\u79F0"},null,8,["value"])]),_:1}),e(v,{xs:24,sm:12,md:8,lg:6,xl:4,style:{float:"right"}},{default:a(()=>[e(_,{type:"primary",class:"mr-15",onClick:c.findList},{default:a(()=>[ne]),_:1},8,["onClick"]),e(_,{class:"mr-15",onClick:c.onResetSearch},{default:a(()=>[se]),_:1},8,["onClick"])]),_:1})]),_:1})]),_:1},512),[[te,n(t).table.search.state]]),e(S,{gutter:[15,15]},{default:a(()=>[e(v,{xs:24,sm:24,md:12,lg:12,xl:12},{default:a(()=>[e(B,{size:15},{default:a(()=>[n(R).search?(b(),g(_,{key:0,onClick:l[2]||(l[2]=o=>n(t).table.search.state=!n(t).table.search.state)},{icon:a(()=>[e(k,{name:n(t).table.search.state?"UpOutlined":"DownOutlined"},null,8,["name"])]),default:a(()=>[ie]),_:1})):ae("",!0),e(h,{title:"\u60A8\u786E\u5B9A\u8981\u66F4\u65B0\u8868\u5417?\u53EF\u80FD\u4F1A\u5BFC\u81F4\u6570\u636E\u4E22\u5931",onConfirm:l[3]||(l[3]=o=>c.synchronization()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:a(()=>[e(_,{type:"primary"},{icon:a(()=>[e(k,{name:"ReloadOutlined"})]),default:a(()=>[re]),_:1})]),_:1}),e(h,{title:"\u60A8\u786E\u5B9A\u8981\u63D0\u4EA4\u66F4\u6539?",onConfirm:l[4]||(l[4]=o=>c.change()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:a(()=>[e(_,{type:"primary"},{icon:a(()=>[e(k,{name:"PlusOutlined"})]),default:a(()=>[de]),_:1})]),_:1}),e(h,{title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:l[5]||(l[5]=o=>c.deleteList()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:a(()=>[e(_,{type:"danger"},{icon:a(()=>[e(k,{name:"DeleteOutlined"})]),default:a(()=>[ce]),_:1})]),_:1})]),_:1})]),_:1}),e(v,{xs:24,sm:24,md:12,lg:12,xl:12,class:"text-right"})]),_:1}),e(G,{spinning:n(t).table.loading},{default:a(()=>[e(J,{class:"mt-15",ref_key:"refTable",ref:$,size:"medium",border:"",stripe:"",data:n(t).table.data,"row-config":{isCurrent:!0,isHover:!0},"column-config":{isCurrent:!0,resizable:!0},"checkbox-config":{highlight:!0},"edit-config":{trigger:"click",mode:"cell"}},{default:a(()=>[e(p,{type:"seq",width:"50"}),e(p,{type:"checkbox",width:"50"}),e(p,{field:"columnName",title:"\u5217\u540D\u79F0"}),e(p,{field:"databaseColumnType",title:"\u6570\u636E\u5E93\u7C7B\u578B",width:"130px"}),e(p,{field:"csField",title:"C#\u5B57\u6BB5\u540D\u79F0"}),e(p,{field:"csType",title:"C#\u6570\u636E\u7C7B\u578B",width:"100px"}),e(p,{field:"isPrimary",title:"\u662F\u5426\u4E3B\u952E",width:"100px"},{default:a(({row:o})=>[o.isPrimary?(b(),g(y,{key:0,color:"success"},{default:a(()=>[ue]),_:1})):(b(),g(y,{key:1,color:"default"},{default:a(()=>[pe]),_:1}))]),_:1}),e(p,{field:"isIdentity",title:"\u662F\u5426\u81EA\u589E",width:"100px"},{default:a(({row:o})=>[o.isIdentity?(b(),g(y,{key:0,color:"success"},{default:a(()=>[me]),_:1})):(b(),g(y,{key:1,color:"default"},{default:a(()=>[fe]),_:1}))]),_:1}),e(p,{field:"displayName",title:"\u663E\u793A\u540D\u79F0","edit-render":{},width:"150px"},{default:a(({row:o})=>[m(K(o.displayName),1)]),edit:a(({row:o})=>[e(f,{value:o.displayName,"onUpdate:value":x=>o.displayName=x,placeholder:"\u663E\u793A\u540D\u79F0"},null,8,["value","onUpdate:value"])]),_:1}),e(p,{field:"describe",title:"\u5217\u63CF\u8FF0","edit-render":{},width:"150px"},{default:a(({row:o})=>[m(K(o.describe),1)]),edit:a(({row:o})=>[e(f,{value:o.describe,"onUpdate:value":x=>o.describe=x,placeholder:"\u5217\u63CF\u8FF0"},null,8,["value","onUpdate:value"])]),_:1}),e(p,{field:"lastModificationTime",title:"\u66F4\u65B0\u65F6\u95F4",width:"120px"}),e(p,{field:"creationTime",title:"\u521B\u5EFA\u65F6\u95F4",width:"120px"}),e(p,{field:"id",title:"\u64CD\u4F5C",width:"80px"},{default:a(({row:o})=>[e(h,{title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:x=>c.deleteList(o.id),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:a(()=>[_e]),_:2},1032,["onConfirm"])]),_:1})]),_:1},8,["data"]),e(A,{background:"","current-page":n(t).table.page,"onUpdate:current-page":l[6]||(l[6]=o=>n(t).table.page=o),"page-size":n(t).table.rows,"onUpdate:page-size":l[7]||(l[7]=o=>n(t).table.rows=o),total:n(t).table.total,"page-sizes":n(t).table.pageSizeOptions,layouts:["PrevJump","PrevPage","JumpNumber","NextPage","NextJump","Sizes","FullJump","Total"],onPageChange:c.onChange},null,8,["current-page","page-size","total","page-sizes","onPageChange"])]),_:1},8,["spinning"])])]),_:1}),e(L,{key:"3",tab:"\u4EE3\u7801\u9884\u89C8"},{default:a(()=>[e(le,{ref_key:"refCodeGenerationVue",ref:P,tableName:n(t).row.tableName,"onUpdate:tableName":l[8]||(l[8]=o=>n(t).row.tableName=o)},null,8,["tableName"])]),_:1}),e(L,{key:"4",tab:"\u4EE3\u7801\u8F7D\u5165\u9879\u76EE"},{default:a(()=>[e(oe,{ref_key:"refCodeLoadToProjectVue",ref:D,tableName:n(t).row.tableName,"onUpdate:tableName":l[9]||(l[9]=o=>n(t).row.tableName=o)},null,8,["tableName"])]),_:1})]),_:1},8,["activeKey"])}}});export{we as default};
