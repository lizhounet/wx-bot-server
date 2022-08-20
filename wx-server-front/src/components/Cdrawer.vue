<template>
  <a-drawer v-model:visible="visible" class="custom-class" title="日志" width="700" placement="right"
    @after-visible-change="close">
    <div v-if="state.spinning" class="example">
      <a-spin tip="Loading..." />
    </div>
    <div v-else class="content">
      <pre>
      <p v-for="(value, key) in state.data" :key="key"  v-html="value"></p>
    </pre>
    </div>
  </a-drawer>

</template>
<script>
export default { name: "Cdrawer" };
</script>
<script setup>
import { ref, reactive, defineProps, defineEmits, watch, defineExpose } from "vue";

const props = defineProps({ visible: Boolean });
const v = ref(props.visible);
const state = reactive({
  spinning: true,
  data: []
})

watch(() => props.visible, (value) => {
  if (!value) {
    state.data = [];
  }
  v.value = value;
});

watch(() => state.data, (value) => {
  state.spinning = false;
});

const emit = defineEmits(["update:visible"]);

// 关闭弹窗
const close = () => {
  console.log(v.value)
  emit("update:visible", v.value);
};

defineExpose({
  state
})
</script>
<style lang="less" scoped>
.custom-class {
  .example {
    text-align: center;
    border-radius: 4px;
    margin-bottom: 20px;
    padding: 30px 50px;
    margin: 20px 0;
  }

  .ant-drawer-body {

    // overflow: auto;
    p {
      border-bottom: 2px dashed #000000;
    }
  }
}
</style>
