<template>
  <div class="fee-calculator">
    
    <label for="price">Vehicle Price:</label>
    <input type="number" id="price" v-model="vehiclePrice" min="1" placeholder="Enter price" />
    <p v-if="vehiclePriceError" class="error-message">Enter a valid price.</p>

    <label for="type">Vehicle Type:</label>
    <select id="type" v-model="vehicleType" >
      <option disabled value="" selected>Select type</option>
      <option value="Common">Common</option>
      <option value="Luxury">Luxury</option>
    </select>
    <p v-if="vehicleTypeError" class="error-message">Select a vehicle type.</p>

    <div v-if="feesReady">
        <FeeItem label="Basic Fee" :value="basicFee" />
        <FeeItem label="Special Fee" :value="specialFee" />
        <FeeItem label="Association Fee" :value="associationFee" />
        <FeeItem label="Storage Fee" :value="storageFee" />
        <p class="success-message">Fees calculated successfully!</p>

        <p class="total"><strong>Total: {{ totalCost.toFixed(2) }}</strong></p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import FeeItem from './FeeItem.vue'

const touched = ref(false)
const vehiclePrice = ref<number | null>(null)
const vehicleType = ref<'Common' | 'Luxury' | ''>('')  
const isLoading = ref(false)

const basicFee = ref(0)
const specialFee = ref(0)
const associationFee = ref(0)
const storageFee = ref(100)
const totalCost = ref(0)

const feesReady = computed(() =>
  vehiclePrice.value !== null &&
  vehiclePrice.value > 0 &&
  vehicleType.value !== ''
)

watch([vehiclePrice, vehicleType], async () => {
     touched.value = true
  if (!feesReady.value || vehiclePrice.value! < 1) {
    resetFees()
    return
  }
  isLoading.value = true
  await calculateFees()
  isLoading.value = false
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

const vehiclePriceError = computed(() =>
  touched.value && (vehiclePrice.value === null || vehiclePrice.value < 1)
)

const vehicleTypeError = computed(() =>
  touched.value && vehicleType.value === ''
)

</script>

<style scoped>
input,
select {
  width: 100%;
  padding: 0.5rem;
  margin-top: 0.25rem;
  background-color: #f9f9f9; /* fondo claro */
  color: #161616; /* texto oscuro */
  border: 1px solid #ccc;
  border-radius: 4px;
}


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
.error-message {
  background-color: #ffe6e6;
  border: 1px solid #ff3333;
  padding: 0.5rem;
  border-radius: 4px;
  color:red;
}



.total {
  background-color: #f0f8ff;
  padding: 0.5rem;
  border-radius: 4px;
  font-weight: bold;
  color: #004488;
}

.success-message {
  background-color: #e8f5e9;
  border: 1px solid #81c784;
  padding: 0.5rem;
  border-radius: 4px;
  color: #2e7d32;
  margin-top: 1rem;
}

</style>
