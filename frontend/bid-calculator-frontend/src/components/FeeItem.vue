<template>
  <p>{{ label }}: {{ value.toFixed(2) }}</p>
</template>

<script setup lang="ts">
defineProps<{
  label: string
  value: number
}>()
</script>
