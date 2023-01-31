import{b as T,r as N,a as b,i as R,o as $,c as i,j as d,d as h,f as t,w as n,h as a,x as v,B as k,C as _,F as j,E as U,m as p,e as V,t as z,k as E}from"./index-5b43cdaa.js";import P from"./List-fa9c0eac.js";import{I as A,s as L}from"./Info-1a1d3649.js";import"./wxContactService-a8fcb9f9.js";const K=p("\u67E5\u8BE2"),M=p("\u91CD\u7F6E"),W=p(" \u65B0\u5EFA "),q=p(" \u6279\u91CF\u5220\u9664 "),G=p("\u91CD\u7F6E"),H=p(" \u68C0\u7D22 "),J=["onClick"],Q=V("a",{class:"text-danger"},"\u5220\u9664",-1),X={name:"wxSayEveryDayIndex"},ne=Object.assign(X,{setup(Y){const O=T(),o=N({search:{state:!1,vm:{name:null}},loading:!1,pageSizeOptions:[10,20,50,100,500,1e3],rows:10,page:1,total:0,columns:[],data:[]}),w=b(null),g=b(null),f=O.getPowerByMenuId(R.currentRoute.value.meta.menuId),s={onChange(r){const{currentPage:e,pageSize:c}=r;o.page=e==0?1:e,o.rows=c,s.findList()},onResetSearch(){o.page=1;let r=o.search.vm;for(let e in r)r[e]=null;s.findList()},findList(){o.loading=!0,L.findList(o.rows,o.page,o.search.vm).then(r=>{let e=r.data;o.loading=!1,o.page=e.page,o.rows=e.size,o.total=e.total,o.columns=e.columns,o.data=e.dataSource})},deleteList(r){let e=[];r?e.push(r):e=g.value.table.getCheckboxRecords().map(c=>c.id),L.deleteList(e).then(c=>{c.code==1&&(z.message("\u5220\u9664\u6210\u529F!","\u6210\u529F"),s.findList())})},openForm(r){w.value.openForm({visible:!0,key:r})}};return $(()=>{s.findList()}),(r,e)=>{const c=i("a-input"),C=i("a-col"),m=i("a-button"),S=i("a-row"),x=i("a-popconfirm"),D=i("a-checkbox"),F=i("a-popover"),u=i("vxe-column"),I=i("a-divider");return d(),h("div",null,[t(P,{ref_key:"refList",ref:g,tableData:a(o),onOnChange:s.onChange},{search:n(()=>[t(S,{gutter:[15,15]},{default:n(()=>[t(C,{xs:24,sm:12,md:8,lg:6,xl:4},{default:n(()=>[t(c,{value:a(o).search.vm.name,"onUpdate:value":e[0]||(e[0]=l=>a(o).search.vm.name=l),placeholder:"\u540D\u79F0"},null,8,["value"])]),_:1}),t(C,{xs:24,sm:12,md:8,lg:6,xl:4,style:{float:"right"}},{default:n(()=>[t(m,{type:"primary",class:"mr-15",onClick:s.findList},{default:n(()=>[K]),_:1},8,["onClick"]),t(m,{class:"mr-15",onClick:s.onResetSearch},{default:n(()=>[M]),_:1},8,["onClick"])]),_:1})]),_:1})]),"toolbar-left":n(()=>[a(f).insert?(d(),v(m,{key:0,type:"primary",onClick:e[1]||(e[1]=l=>s.openForm())},{icon:n(()=>[t(k,{name:"PlusOutlined"})]),default:n(()=>[W]),_:1})):_("",!0),a(f).delete?(d(),v(x,{key:1,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:e[2]||(e[2]=l=>s.deleteList()),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:n(()=>[t(m,{type:"danger"},{icon:n(()=>[t(k,{name:"DeleteOutlined"})]),default:n(()=>[q]),_:1})]),_:1})):_("",!0)]),"toolbar-right":n(()=>[t(c,{value:a(o).search.vm.name,"onUpdate:value":e[3]||(e[3]=l=>a(o).search.vm.name=l),placeholder:"\u540D\u79F0",onKeyup:s.findList},null,8,["value","onKeyup"]),t(m,{onClick:s.onResetSearch},{default:n(()=>[G]),_:1},8,["onClick"]),a(f).search?(d(),v(m,{key:0,onClick:e[4]||(e[4]=l=>a(o).search.state=!a(o).search.state)},{icon:n(()=>[t(k,{name:a(o).search.state?"UpOutlined":"DownOutlined"},null,8,["name"])]),default:n(()=>[H]),_:1})):_("",!0),t(F,null,{content:n(()=>[(d(!0),h(j,null,U(a(o).columns.filter(l=>l.fieldName.substr(0,1)!="_"),(l,y)=>(d(),h("div",{key:y},[t(D,{checked:l.show,"onUpdate:checked":B=>l.show=B,onChange:e[5]||(e[5]=()=>r.nextTick(()=>g.value.table.refreshColumn()))},{default:n(()=>[p(E(l.title),1)]),_:2},1032,["checked","onUpdate:checked"])]))),128))]),default:n(()=>[t(m,null,{default:n(()=>[t(k,{name:"BarsOutlined"})]),_:1})]),_:1})]),"table-col-default":n(()=>[t(u,{field:"receivingObjectWxId",title:"\u63A5\u6536\u5BF9\u8C61wxId","show-overflow":""}),t(u,{field:"receivingObjectName",title:"\u63A5\u6536\u5BF9\u8C61","show-overflow":""}),t(u,{field:"sendTime",title:"\u53D1\u9001\u65F6\u95F4(cron\u8868\u8FBE\u5F0F)","show-overflow":""}),t(u,{field:"city",title:"\u6240\u5728\u57CE\u5E02","show-overflow":""}),t(u,{field:"closingRemarks",title:"\u7ED3\u5C3E\u5907\u6CE8","show-overflow":""}),t(u,{field:"anniversaryDay",title:"\u7EAA\u5FF5\u65E5","show-overflow":""}),t(u,{field:"birthdayDate",title:"\u4E0B\u4E00\u6B21\u751F\u65E5\u65E5\u671F","show-overflow":""}),a(f).update||a(f).delete?(d(),v(u,{key:0,field:"id",title:"\u64CD\u4F5C"},{default:n(({row:l})=>[a(f).update?(d(),h("a",{key:0,href:"javascript:void(0)",onClick:y=>s.openForm(l.id)},"\u7F16\u8F91",8,J)):_("",!0),t(I,{type:"vertical"}),a(f).delete?(d(),v(x,{key:1,title:"\u60A8\u786E\u5B9A\u8981\u5220\u9664\u5417?",onConfirm:y=>s.deleteList(l.id),okText:"\u786E\u5B9A",cancelText:"\u53D6\u6D88"},{default:n(()=>[Q]),_:2},1032,["onConfirm"])):_("",!0)]),_:1})):_("",!0)]),_:1},8,["tableData","onOnChange"]),t(A,{ref_key:"refForm",ref:w,onOnSuccess:e[6]||(e[6]=()=>s.findList())},null,512)])}}});export{ne as default};
