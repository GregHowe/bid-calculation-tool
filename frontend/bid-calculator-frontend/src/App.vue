<template>
  <div>
    <label for="price">Vehicle Price:</label>
    <input type="number" id="price" v-model="price" />

    <label for="type">Vehicle Type:</label>
    <select id="type" v-model="type">
      <option value="Common">Common</option>
      <option value="Luxury">Luxury</option>
    </select>

    <div>
      <p>Basic Fee: {{ basicFee.toFixed(2) }}</p>
      <p>Special Fee: {{ specialFee.toFixed(2) }}</p>
      <p>Association Fee: {{ associationFee.toFixed(2) }}</p>
      <p>Storage Fee: {{ storageFee.toFixed(2) }}</p>
      <p><strong>Total: {{ total.toFixed(2) }}</strong></p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'

const price = ref<number | null>(null)
const type = ref<'Common' | 'Luxury' | null>(null)

const basicFee = ref(0)
const specialFee = ref(0)
const associationFee = ref(0)
const storageFee = ref(100)
const total = ref(0)

watch([price, type], async () => {
  if (price.value === null || type.value === null) {
    basicFee.value = 0
    specialFee.value = 0
    associationFee.value = 0
    total.value = 0
    return
  }

    const response = await fetch('http://localhost:5120/api/FeeCalculator', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      price: price.value,
      type: type.value
    })
  })

  const data = await response.json()

  basicFee.value = data.basicFee
  specialFee.value = data.specialFee
  associationFee.value = data.associationFee
  storageFee.value = data.storageFee
  total.value = data.total

})
</script>
