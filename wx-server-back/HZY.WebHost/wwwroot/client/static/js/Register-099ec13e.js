import{p as w,_ as D,r as h,a as z,u as U,b as W,o as L,c as d,d as N,e as u,f as t,w as a,g as V,h as $,j as R,k as C,l as o,n as m,t as n,i as b}from"./index-dfb70614.js";var y={register(i){return w("account/register",i)},getverifyCode(i){return w("account/register-verifyCode",i,!1)}};const T=i=>(V("data-v-01421e4e"),i=i(),$(),i),j={id:"register"},A=T(()=>u("div",{class:"register-modal"},null,-1)),E={class:"register-container"},M={class:"register-title"},q={class:"p-24"},F=m("\u6CE8\u518C"),G={class:"login"},H=m("\u53BB\u767B\u5F55"),J={setup(i){const e=h({name:"",password:"",verifyCode:"",email:""}),r=h({codeCountdown:60,codeBtnWord:"\u83B7\u53D6\u9A8C\u8BC1\u7801",codeBtnDisabled:!1}),g=z(!1),x=U();W();const k=x.state.title,_={register(){if(!e.name)return n.message("\u6635\u79F0\u4E0D\u80FD\u4E3A\u7A7A!","\u8B66\u544A");if(!e.password)return n.message("\u5BC6\u7801\u4E0D\u80FD\u4E3A\u7A7A!","\u8B66\u544A");if(!e.verifyCode)return n.message("\u9A8C\u8BC1\u7801\u4E0D\u80FD\u4E3A\u7A7A!","\u8B66\u544A");if(!e.email)return n.message("\u90AE\u7BB1\u4E0D\u80FD\u4E3A\u7A7A!","\u8B66\u544A");if(!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(e.email))return n.message("\u90AE\u7BB1\u683C\u5F0F\u4E0D\u6B63\u786E!","\u8B66\u544A");g.value=!0,y.register(e).then(p=>{if(p.code!==1)return g.value=!1;n.alert("\u6CE8\u518C\u6210\u529F,\u524D\u5F80\u767B\u5F55\uFF01","\u6210\u529F",()=>{b.push({path:"/login"})})}).catch(()=>{g.value=!1})},getCode(){if(!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(e.email))return n.message("\u90AE\u7BB1\u683C\u5F0F\u4E0D\u6B63\u786E!","\u8B66\u544A");r.codeBtnDisabled=!0,y.getverifyCode({email:e.email,ts:n.getTimeStamp()}).then(p=>{if(p.code!==1)return;n.message("\u9A8C\u8BC1\u7801\u53D1\u9001\u6210\u529F,\u8BF7\u6CE8\u610F\u67E5\u6536!","\u6210\u529F");let s=setInterval(()=>{r.codeCountdown>1?(r.codeCountdown--,r.codeBtnWord=`${r.codeCountdown}s \u540E\u83B7\u53D6`):(clearInterval(s),r.codeBtnDisabled=!1,r.codeBtnWord="\u83B7\u53D6\u9A8C\u8BC1\u7801",r.codeCountdown=60)},1e3)})},goLogin(){b.push({path:"/login"})}};return L(()=>{}),(p,s)=>{const f=d("a-input"),c=d("a-form-item"),v=d("a-button"),B=d("a-input-group"),S=d("a-form"),I=d("a-card");return R(),N("div",j,[A,u("div",E,[t(I,null,{default:a(()=>[u("div",M,C(o(k))+"\u6CE8\u518C",1),u("div",q,[t(S,{layout:"vertical"},{default:a(()=>[t(c,{label:"\u6635\u79F0"},{default:a(()=>[t(f,{value:o(e).name,"onUpdate:value":s[0]||(s[0]=l=>o(e).name=l),placeholder:"\u8BF7\u8F93\u5165\u6635\u79F0"},null,8,["value"])]),_:1}),t(c,{label:"\u90AE\u7BB1"},{default:a(()=>[t(f,{value:o(e).email,"onUpdate:value":s[1]||(s[1]=l=>o(e).email=l),placeholder:"\u8BF7\u8F93\u5165\u90AE\u7BB1"},null,8,["value"])]),_:1}),t(c,{label:"\u9A8C\u8BC1\u7801"},{default:a(()=>[t(B,null,{default:a(()=>[t(f,{value:o(e).verifyCode,"onUpdate:value":s[2]||(s[2]=l=>o(e).verifyCode=l),placeholder:"\u53D1\u9001\u9A8C\u8BC1\u7801",style:{width:"calc(100% - 103px)","text-align":"left"}},null,8,["value"]),t(v,{onClick:_.getCode,type:"link",disabled:o(r).codeBtnDisabled},{default:a(()=>[m(C(o(r).codeBtnWord),1)]),_:1},8,["onClick","disabled"])]),_:1})]),_:1}),t(c,{label:"\u5BC6\u7801"},{default:a(()=>[t(f,{value:o(e).password,"onUpdate:value":s[3]||(s[3]=l=>o(e).password=l),placeholder:"\u8BF7\u8F93\u5165\u5BC6\u7801"},null,8,["value"])]),_:1}),t(c,null,{default:a(()=>[t(v,{type:"primary",onClick:_.register,loading:g.value,size:"large",block:""},{default:a(()=>[F]),_:1},8,["onClick","loading"])]),_:1})]),_:1}),u("div",G,[t(v,{type:"link",onClick:_.goLogin,size:"small"},{default:a(()=>[H]),_:1},8,["onClick"])])])]),_:1})])])}}};var O=D(J,[["__scopeId","data-v-01421e4e"]]);export{O as default};
