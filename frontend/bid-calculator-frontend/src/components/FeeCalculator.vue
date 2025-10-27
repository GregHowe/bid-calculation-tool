<template>
  <div class="fee-calculator">
    <label for="price">Vehicle Price:</label>
    <input
    type="number"
    id="price"
    v-model="vehiclePrice"
    min="1"
    placeholder="Enter price"
    />

    <label for="type">Vehicle Type:</label>
    <select id="type" v-model="vehicleType">
      <option disabled value="" selected>Select type</option>
      <option value="Common">Common</option>
      <option value="Luxury">Luxury</option>
    </select>

    <div v-if="feesReady">
      <FeeItem label="Basic Fee" :value="basicFee" />
      <FeeItem label="Special Fee" :value="specialFee" />
      <FeeItem label="Association Fee" :value="associationFee" />
      <FeeItem label="Storage Fee" :value="storageFee" />
      <p><strong>Total: {{ totalCost.toFixed(2) }}</strong></p>
    </div>

    <div v-else>
      <p>Please enter a valid price and select a vehicle type.</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import FeeItem from './FeeItem.vue'

const vehiclePrice = ref<number | null>(null)
const vehicleType = ref<'Common' | 'Luxury' | ''>('')  

const basicFee = ref(0)
const specialFee = ref(0)
const associationFee = ref(0)
const storageFee = ref(100)
const totalCost = ref(0)

const feesReady = computed(() =>
  vehiclePrice.value !== null &&
  vehiclePrice.value > 0 &&
  vehicleType.value !== null
)

watch([vehiclePrice, vehicleType], async () => {
  if (!feesReady.value || vehiclePrice.value! < 1) {
    resetFees()
    return
  }

  await calculateFees()
})

function resetFees() {
  basicFee.value = 0
  specialFee.value = 0
  associationFee.value = 0
  totalCost.value = 0
}

async function calculateFees() {
  try {
    const response = await fetch('http://localhost:5120/api/FeeCalculator', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        price: vehiclePrice.value,
        type: vehicleType.value
      })
    })

    const data = await response.json()

    basicFee.value = data.basicFee
    specialFee.value = data.specialFee
    associationFee.value = data.associationFee
    storageFee.value = data.storageFee
    totalCost.value = data.total
  } catch (error) {
    console.error('Error fetching fees:', error)
    resetFees()
  }
}

function preventNegative(event: Event) {
  const input = event.target as HTMLInputElement
  if (Number(input.value) < 1) {
    input.value = ''
    vehiclePrice.value = null
  }
}

</script>

<style scoped>
.fee-calculator {
  max-width: 400px;
  margin: auto;
  font-family: Arial, sans-serif;
}
label {
  display: block;
  margin-top: 1rem;
}
input,
select {
  width: 100%;
  padding: 0.5rem;
  margin-top: 0.25rem;
}
p {
  margin: 0.5rem 0;
}
</style>
