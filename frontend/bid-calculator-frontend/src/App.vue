<template>
  <main>
    <h1>Bid Calculation Tool</h1>
    <FeeCalculator />
  </main>
</template>

<script setup lang="ts">
import FeeCalculator from './components/FeeCalculator.vue'
</script>

<style scoped>
main {
  max-width: 600px;
  margin: 2rem auto;
  padding: 1rem;
  font-family: Arial, sans-serif;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  background-color: #f7f57e; /* fondo claro */
  color: #333; /* texto oscuro */
}
h1 {
  text-align: center;
  margin-bottom: 2rem;
}
</style>
