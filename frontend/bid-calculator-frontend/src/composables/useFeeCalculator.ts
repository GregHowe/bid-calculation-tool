// src/composables/useFeeCalculator.ts
import { ref, computed, watch } from 'vue'

export function useFeeCalculator() {
  const vehiclePrice = ref<number | null>(null)
  const vehicleType = ref<'Common' | 'Luxury' | null>(null)

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
    if (!feesReady.value) {
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

  return {
    vehiclePrice,
    vehicleType,
    basicFee,
    specialFee,
    associationFee,
    storageFee,
    totalCost,
    feesReady
  }
}
