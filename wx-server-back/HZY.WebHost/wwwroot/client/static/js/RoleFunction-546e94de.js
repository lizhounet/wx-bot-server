var S=Object.defineProperty;var b=Object.getOwnPropertySymbols;var U=Object.prototype.hasOwnProperty,L=Object.prototype.propertyIsEnumerable;var x=(t,a,e)=>a in t?S(t,a,{enumerable:!0,configurable:!0,writable:!0,value:e}):t[a]=e,C=(t,a)=>{for(var e in a||(a={}))U.call(a,e)&&x(t,e,a[e]);if(b)for(var e of b(a))L.call(a,e)&&x(t,e,a[e]);return t};import{p as I,y as V,r as z,a as j,c as r,j as g,x as v,w as d,f as m,h as p,e as B,k as y,C as M,d as R,E as D,F as H,m as F,aS as O,t as T}from"./index-7474c45c.js";const k="admin/SysRoleMenuFunction";var A={findList(t,a,e={}){return I(`${k}/findList/${t}/${a}`,e,!1)},saveForm(t){return I(`${k}/saveForm`,t)},getRoleMenuFunctionByRoleId(t){return V(`${k}/getRoleMenuFunctionByRoleId/${t}`)}};const W=B("span",{class:"mr-15"},"\u89D2\u8272\u529F\u80FD\u8BBE\u7F6E",-1),Y=F("\u4FDD\u5B58/\u63D0\u4EA4"),q={class:"mr-15"},G=F(" \u5168\u9009 "),P={setup(t,{expose:a}){const e=z({roleId:"",data:[],loading:!1,visible:!1}),_=j(null),h={getRoleMenuFunctionTree(){e.loading=!0,A.getRoleMenuFunctionByRoleId(e.roleId).then(o=>{e.loading=!1;let n=o.data;e.data=n,O(()=>{for(var c=0;c<e.data.length;c++){var i=e.data[c];if(i.menuFunctions.length>0){var u=i.levelCode.substr(0,i.levelCode.lastIndexOf("."));_.value.setTreeExpand(e.data.filter(f=>f.levelCode==u),!0)}}})})},save(){for(var o=e.data.filter(u=>u.menuFunctions.length>0),n=[],c=0;c<o.length;c++){var i=o[c];n.push({roleId:e.roleId,menuId:i.id,menuFunctionIdList:i.checkedMenuFunctionIds})}A.saveForm(n).then(u=>{if(u.code!=1)return T.message("\u4FDD\u5B58\u5931\u8D25!","\u9519\u8BEF");h.getRoleMenuFunctionTree(),T.message("\u4FDD\u5B58\u6210\u529F!","\u6210\u529F")})},onChangeCheckbox({values:o,row:n}){n.indeterminate=!!o.length&&o.length<n.menuFunctions.length,n.checkAll=o.length===n.menuFunctions.length},openWindow({visible:o,key:n}){e.visible=o,e.roleId=n,h.getRoleMenuFunctionTree()},setAllTreeExpand(){_.value.setAllTreeExpand(!0)},onCheckAllChange(o,n){o.target.checked?(n.checkedMenuFunctionIds=n.menuFunctions.map(c=>c.id),n.indeterminate=n.checkedMenuFunctionIds.length>0&&n.checkedMenuFunctionIds.length<n.menuFunctions.length,n.checkAll=n.checkedMenuFunctionIds.length===n.menuFunctions.length):(n.checkedMenuFunctionIds=[],n.indeterminate=!1)}};return a(C({},h)),(o,n)=>{const c=r("a-button"),i=r("a-checkbox"),u=r("vxe-column"),f=r("a-checkbox-group"),$=r("vxe-table"),E=r("a-spin"),N=r("a-modal");return g(),v(N,{visible:p(e).visible,"onUpdate:visible":n[0]||(n[0]=l=>p(e).visible=l),centered:"",width:"1200px",bodyStyle:{overflowY:"auto",height:"calc(100vh - 100px)"},footer:null},{title:d(()=>[W,m(c,{type:"primary",onClick:h.save},{default:d(()=>[Y]),_:1},8,["onClick"])]),default:d(()=>[m(E,{spinning:p(e).loading},{default:d(()=>[m($,{ref_key:"refTable",ref:_,resizable:"",data:p(e).data,"row-config":{isHover:!0},"tree-config":{transform:!0,rowField:"id",parentField:"parentId"},size:"small"},{default:d(()=>[m(u,{field:"name",title:"\u83DC\u5355\u540D\u79F0","tree-node":"",width:"250"},{default:d(({row:l})=>[B("span",q,y(l.name),1),l.menuFunctions.length>0?(g(),v(i,{key:0,checked:l.checkAll,"onUpdate:checked":s=>l.checkAll=s,indeterminate:l.indeterminate,onChange:s=>h.onCheckAllChange(s,l)},{default:d(()=>[G]),_:2},1032,["checked","onUpdate:checked","indeterminate","onChange"])):M("",!0)]),_:1}),m(u,{field:"id",title:"\u6743\u9650"},{default:d(({row:l})=>[l.menuFunctions.length>0?(g(),v(f,{key:0,style:{display:"block"},value:l.checkedMenuFunctionIds,"onUpdate:value":s=>l.checkedMenuFunctionIds=s,onChange:s=>h.onChangeCheckbox({values:s,row:l})},{default:d(()=>[(g(!0),R(H,null,D(l.menuFunctions,s=>(g(),R("span",{key:s.id,class:"mr-15"},[m(i,{value:s.id},{default:d(()=>[F(y(s.label),1)]),_:2},1032,["value"])]))),128))]),_:2},1032,["value","onUpdate:value","onChange"])):M("",!0)]),_:1})]),_:1},8,["data"])]),_:1},8,["spinning"])]),_:1},8,["visible","bodyStyle"])}}};export{P as default};
