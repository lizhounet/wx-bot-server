import{r as f,a as w,S as N,c as d,T as v,j as t,d as c,K as B,q as C,w as l,n as m,k as s,l as a,e as i,f as D,z as L,B as S}from"./index-7722fba7.js";const P=m("\u6253\u5370"),V={id:"print"},I={key:0},J={name:"BaseListCom"},R=Object.assign(J,{setup(j){const o=f({columns:[{title:"Name",dataIndex:"name"},{title:"Age",dataIndex:"age"},{title:"Address",dataIndex:"address"}],data:[{key:"1",name:"John Brown",age:32,address:"New York No. 1 Lake Park"},{key:"2",name:"Jim Green",age:42,address:"London No. 1 Lake Park"},{key:"3",name:"Joe Black",age:32,address:"Sidney No. 1 Lake Park"},{key:"4",name:"Disabled User",age:99,address:"Sidney No. 1 Lake Park"}]}),_={onChange:(e,r)=>{console.log(`selectedRowKeys: ${e}`,"selectedRows: ",r)},getCheckboxProps:e=>({disabled:e.name==="Disabled User",name:e.name})},n=w(null),{x:k,y:u,style:p}=N(n,{initialValue:{x:500,y:200}});return(e,r)=>{const y=d("a-button"),g=d("a-table"),x=v("print");return t(),c("div",null,[B((t(),C(y,{type:"primary",class:"mb-15"},{default:l(()=>[P]),_:1})),[[x,"#print"]]),m(" x:"+s(a(k))+"y:"+s(a(u))+" ",1),i("div",V,[D(g,{"row-selection":_,columns:a(o).columns,"data-source":a(o).data},{bodyCell:l(({column:b,text:h})=>[b.dataIndex==="name"?(t(),c("a",I,s(h),1)):L("",!0)]),_:1},8,["columns","data-source"])]),i("div",{ref_key:"el",ref:n,style:S([a(p),{position:"fixed","background-color":"red",width:"100px",height:"100px"}])},"\u5FEB\u62D6\u52A8\u6211...",4)])}}});export{R as default};