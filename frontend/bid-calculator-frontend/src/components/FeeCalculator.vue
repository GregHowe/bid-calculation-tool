<template>
  <div class="fee-calculator">
    <label for="price">Vehicle Price:</label>
    <input type="number" id="price" v-model="vehiclePrice" min="1" placeholder="Enter price" />
    <p v-if="vehiclePriceError" class="error-message">Enter a valid price.</p>

    <label for="type">Vehicle Type:</label>
    <select id="type" v-model="vehicleType">
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
import { useFeeCalculator } from '../composables/useFeeCalculator'

const touched = ref(false)
const vehiclePrice = ref<number | null>(null)
const vehicleType = ref<'Common' | 'Luxury' | ''>('')

const feesReady = computed(() =>
  vehiclePrice.value !== null &&
  vehiclePrice.value > 0 &&
  vehicleType.value !== ''
)

const vehiclePriceError = computed(() =>
  touched.value && (vehiclePrice.value === null || vehiclePrice.value < 1)
)

const vehicleTypeError = computed(() =>
  touched.value && vehicleType.value === ''
)

const {
  basicFee,
  specialFee,
  associationFee,
  storageFee,
  totalCost,
  calculateFees,
  resetFees
} = useFeeCalculator()

watch([vehiclePrice, vehicleType], async () => {
  touched.value = true
  if (!feesReady.value || vehiclePrice.value! < 1) {
    resetFees()
    return
  }
  await calculateFees(vehiclePrice.value!, vehicleType.value)
})
</script>
