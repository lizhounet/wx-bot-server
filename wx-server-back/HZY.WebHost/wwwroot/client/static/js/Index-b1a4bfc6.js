import{r as U,a as O,o as P,c as d,j as _,d as p,K as D,L as z,l as a,f as e,w as l,A as f,e as C,k as F,z as N,n as r,t as w}from"./index-6b23fe46.js";import T from"./Info-57340c8f.js";const V=r("\u67E5\u8BE2"),j=r("\u91CD\u7F6E"),B={key:0},A=r("\xA0\xA0\u6536\u8D77"),K={key:1},M=r("\xA0\xA0\u5C55\u5F00"),R=r(" \u65B0\u5EFA "),H=r(" \u6279\u91CF\u5220\u9664 "),q=r("\u5BFC\u51FA Excel"),G=r("\u4E0B\u8F7D \u5BFC\u5165\u6A21\u677F"),J=r("\u5BFC\u5165 Excel"),Q=r(" \u66F4\u591A\u64CD\u4F5C "),W={key:0},X=["onClick"],Y={class:"text-danger"},Z={name:"ListIndexCom"},ae=Object.assign(Z,{setup(ee){const S=[{title:"\u5E8F\u53F7",dataIndex:"key",width:80,fixed:"left"},{title:"\u59D3\u540D",dataIndex:"name",ellipsis:!0,width:130},{title:"\u5E74\u9F84",dataIndex:"age",ellipsis:!0,width:180},{title:"\u5730\u5740",dataIndex:"address",ellipsis:!0,width:180},{title:"\u52171",dataIndex:"column1",ellipsis:!0,width:180},{title:"\u52172",dataIndex:"column2",ellipsis:!0,width:180},{title:"\u52173",dataIndex:"column3",ellipsis:!0,width:180},{title:"\u52174",dataIndex:"column4",ellipsis:!0,width:180},{title:"\u64CD\u4F5C",dataIndex:"id",width:120,fixed:"right"}],o=U({fromSearch:{state:!1,fieldCount:7,vm:{value:""}},table:{loading:!1,columns:[],data:[]}}),x=O(null),u={findList(){o.table.loading=!0,o.table.columns=S,setTimeout(()=>{const s=[];for(let t=0;t<100;t++)s.push({key:t+1,name:`Hzy ${t+1}`,age:18+t,address:`addr. ${t+1}`,column1:`London Park no. ${t}`,column2:`London Park no. ${t}`,column3:`London Park no. ${t}`,column4:`London Park no. ${t}`,id:t});o.table.data=s,o.table.loading=!1},1.5*1e3)},exportExcel(){w.notice("\u5BFC\u51FAExcel\u6210\u529F!","\u6210\u529F","\u63D0\u9192")},confirm(){w.message("\u5220\u9664\u6210\u529F!","\u6210\u529F")},openForm(s){x.value.openForm({visible:!0,key:s})}},y={onChange:(s,t)=>{console.log(`selectedRowKeys: ${s}`,"selectedRows: ",t)},getCheckboxProps:s=>({name:s.name})};return P(()=>{u.findList()}),(s,t)=>{const m=d("a-input"),i=d("a-col"),c=d("a-button"),h=d("a-row"),k=d("a-card"),b=d("a-popconfirm"),v=d("a-menu-item"),I=d("a-menu"),$=d("a-dropdown"),L=d("a-divider"),E=d("a-table");return _(),p("div",null,[D(e(k,{class:"mb-15"},{default:l(()=>[e(h,{gutter:[15,15]},{default:l(()=>[e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[0]||(t[0]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u7528\u6237\u540D"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[1]||(t[1]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u5E74\u9F84"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[2]||(t[2]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u5730\u5740"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[3]||(t[3]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u7528\u6237\u540D"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[4]||(t[4]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u5730\u5740"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[5]||(t[5]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u5730\u57401"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4},{default:l(()=>[e(m,{value:a(o).fromSearch.vm.value,"onUpdate:value":t[6]||(t[6]=n=>a(o).fromSearch.vm.value=n),placeholder:"\u5730\u57402"},null,8,["value"])]),_:1}),e(i,{xs:24,sm:12,md:8,lg:6,xl:4,style:{float:"right"}},{default:l(()=>[e(c,{type:"primary",class:"mr-15",onClick:t[7]||(t[7]=n=>u.findList())},{default:l(()=>[V]),_:1}),e(c,{class:"mr-15",onClick:t[8]||(t[8]=n=>u.findList())},{default:l(()=>[j]),_:1})]),_:1})]),_:1})]),_:1},512),[[z,a(o).fromSearch.state]]),e(k,null,{default:l(()=>[e(h,{gutter:[15,15]},{default:l(()=>[e(i,{xs:24,sm:24,md:12,lg:12,xl:12},{default:l(()=>[e(c,{class:"mr-15",onClick:t[9]||(t[9]=n=>a(o).fromSearch.state=!a(o).fromSearch.state)},{default:l(()=>[a(o).fromSearch.state?(_(),p("div",B,[e(f,{name:"UpOutlined"}),A])):(_(),p("div",K,[e(f,{name:"DownOutlined"}),M]))]),_:1}),e(c,{type:"primary",class:"mr-15",onClick:t[10]||(t[10]=n=>u.openForm())},{icon:l(()=>[e(f,{name:"PlusOutlined"})]),default:l(()=>[R]),_:1}),e(b,{title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664?",onConfirm:s.confirm,okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:l(()=>[e(c,{type:"primary",danger:"",class:"mr-15"},{icon:l(()=>[e(f,{name:"DeleteOutlined"})]),default:l(()=>[H]),_:1})]),_:1},8,["onConfirm"])]),_:1}),e(i,{xs:24,sm:24,md:12,lg:12,xl:12,class:"text-right"},{default:l(()=>[e($,null,{overlay:l(()=>[e(I,{onClick:s.handleMenuClick},{default:l(()=>[e(v,{key:"1",onClick:s.exportExcel},{default:l(()=>[q]),_:1},8,["onClick"]),e(v,{key:"2",onClick:s.exportExcel},{default:l(()=>[G]),_:1},8,["onClick"]),e(v,{key:"3",onClick:s.exportExcel},{default:l(()=>[J]),_:1},8,["onClick"])]),_:1},8,["onClick"])]),default:l(()=>[e(c,null,{default:l(()=>[Q,e(f,{name:"DownOutlined"})]),_:1})]),_:1})]),_:1})]),_:1}),e(E,{size:"middle",class:"mt-15",columns:a(o).table.columns,"data-source":a(o).table.data,pagination:{pageSize:10},loading:a(o).table.loading,scroll:{x:"calc(100wh - 300px)"},"row-selection":y},{bodyCell:l(({column:n,text:g})=>[n.dataIndex==="id"?(_(),p("span",W,[C("a",{href:"javascript:;",onClick:te=>u.openForm(g)},"\u4FEE\u6539",8,X),e(L,{type:"vertical"}),C("a",Y,"\u5220\u9664"+F(g),1)])):N("",!0)]),_:1},8,["columns","data-source","loading","scroll"])]),_:1}),e(T,{ref_key:"refInfo",ref:x,onOnSuccess:t[11]||(t[11]=()=>u.findList())},null,512)])}}});export{ae as default};
