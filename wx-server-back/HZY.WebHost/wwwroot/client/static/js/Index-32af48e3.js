import{b as E,r as I,a as w,i as R,o as V,c as r,j as u,d as b,f as e,w as t,h as l,x as f,B as h,C as v,m as c,e as N,t as j}from"./index-5b43cdaa.js";import z from"./List-fa9c0eac.js";import{I as M,s as g}from"./Info-2f4a1685.js";const P=c("\u67E5\u8BE2"),U=c("\u91CD\u7F6E"),A=c(" \u65B0\u5EFA "),K=c(" \u6279\u91CF\u5220\u9664 "),q=c("\u5BFC\u51FA Excel"),G=c(" \u66F4\u591A\u64CD\u4F5C "),H=c("\u91CD\u7F6E"),J=c(" \u68C0\u7D22 "),Q=c("\u6B63\u5E38"),W=c("\u505C\u7528"),X=["onClick"],Y=N("a",{class:"text-danger"},"\u5220\u9664",-1),Z={name:"system_post"},ae=Object.assign(Z,{setup(ee){const O=E(),n=I({search:{state:!1,fieldCount:2,vm:{name:null}},loading:!1,pageSizeOptions:[10,20,50,100,500,1e3],rows:10,page:1,total:0,data:[]}),k=w(null),C=w(null),p=O.getPowerByMenuId(R.currentRoute.value.meta.menuId),a={onChange(s){const{currentPage:o,pageSize:d}=s;n.page=o==0?1:o,n.rows=d,a.findList()},onResetSearch(){n.page=1;let s=n.search.vm;for(let o in s)s[o]=null;a.findList()},findList(){n.loading=!0,g.findList(n.rows,n.page,n.search.vm).then(s=>{let o=s.data;n.loading=!1,n.page=o.page,n.rows=o.size,n.total=o.total,n.data=o.dataSource})},deleteList(s){let o=[];s?o.push(s):o=C.value.table.getCheckboxRecords().map(d=>d.id),g.deleteList(o).then(d=>{d.code==1&&(j.message("\u5220\u9664\u6210\u529F!","\u6210\u529F"),a.findList())})},openForm(s){k.value.openForm({visible:!0,key:s})},exportExcel(){g.exportExcel(n.search.vm)}};return V(()=>{a.findList()}),(s,o)=>{const d=r("a-input"),y=r("a-col"),m=r("a-button"),S=r("a-row"),x=r("a-popconfirm"),T=r("a-menu-item"),B=r("a-menu"),F=r("a-dropdown"),_=r("vxe-column"),L=r("a-tag"),$=r("a-divider");return u(),b("div",null,[e(z,{ref_key:"refList",ref:C,tableData:l(n),onOnChange:a.onChange},{search:t(()=>[e(S,{gutter:[15,15]},{default:t(()=>[e(y,{xs:24,sm:12,md:8,lg:6,xl:4},{default:t(()=>[e(d,{value:l(n).search.vm.name,"onUpdate:value":o[0]||(o[0]=i=>l(n).search.vm.name=i),placeholder:"\u540D\u79F0"},null,8,["value"])]),_:1}),e(y,{xs:24,sm:12,md:8,lg:6,xl:4,style:{float:"right"}},{default:t(()=>[e(m,{type:"primary",class:"mr-15",onClick:a.findList},{default:t(()=>[P]),_:1},8,["onClick"]),e(m,{class:"mr-15",onClick:a.onResetSearch},{default:t(()=>[U]),_:1},8,["onClick"])]),_:1})]),_:1})]),"toolbar-left":t(()=>[l(p).insert?(u(),f(m,{key:0,type:"primary",onClick:o[1]||(o[1]=i=>a.openForm())},{icon:t(()=>[e(h,{name:"PlusOutlined"})]),default:t(()=>[A]),_:1})):v("",!0),l(p).delete?(u(),f(x,{key:1,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:o[2]||(o[2]=i=>a.deleteList()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:t(()=>[e(m,{type:"danger"},{icon:t(()=>[e(h,{name:"DeleteOutlined"})]),default:t(()=>[K]),_:1})]),_:1})):v("",!0),e(F,null,{overlay:t(()=>[e(B,null,{default:t(()=>[e(T,{key:"1",onClick:s.exportExcel},{default:t(()=>[q]),_:1},8,["onClick"])]),_:1})]),default:t(()=>[e(m,null,{default:t(()=>[G,e(h,{name:"DownOutlined"})]),_:1})]),_:1})]),"toolbar-right":t(()=>[e(d,{value:l(n).search.vm.name,"onUpdate:value":o[3]||(o[3]=i=>l(n).search.vm.name=i),placeholder:"\u540D\u79F0",onKeyup:a.findList},null,8,["value","onKeyup"]),e(m,{onClick:a.onResetSearch},{default:t(()=>[H]),_:1},8,["onClick"]),l(p).search?(u(),f(m,{key:0,onClick:o[4]||(o[4]=i=>l(n).search.state=!l(n).search.state)},{icon:t(()=>[e(h,{name:l(n).search.state?"UpOutlined":"DownOutlined"},null,8,["name"])]),default:t(()=>[J]),_:1})):v("",!0)]),"table-col-default":t(()=>[e(_,{field:"number",title:"\u5C97\u4F4D\u7F16\u53F7"}),e(_,{field:"code",title:"\u5C97\u4F4D\u7F16\u7801"}),e(_,{field:"name",title:"\u89D2\u8272\u540D\u79F0"}),e(_,{field:"state",title:"\u72B6\u6001"},{default:t(({row:i})=>[i.state==1?(u(),f(L,{key:0,color:"success"},{default:t(()=>[Q]),_:1})):(u(),f(L,{key:1,color:"warning"},{default:t(()=>[W]),_:1}))]),_:1}),e(_,{field:"lastModificationTime",title:"\u66F4\u65B0\u65F6\u95F4"}),e(_,{field:"creationTime",title:"\u521B\u5EFA\u65F6\u95F4"}),e(_,{field:"id",title:"\u64CD\u4F5C"},{default:t(({row:i})=>[l(p).update?(u(),b("a",{key:0,href:"javascript:void(0)",onClick:D=>a.openForm(i.id)},"\u7F16\u8F91",8,X)):v("",!0),e($,{type:"vertical"}),l(p).delete?(u(),f(x,{key:1,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:D=>a.deleteList(i.id),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:t(()=>[Y]),_:2},1032,["onConfirm"])):v("",!0)]),_:1})]),_:1},8,["tableData","onOnChange"]),e(M,{ref_key:"refForm",ref:k,onOnSuccess:o[5]||(o[5]=()=>a.findList())},null,512)])}}});export{ae as default};
