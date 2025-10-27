import { ref } from 'vue'

const API_URL = import.meta.env.VITE_API_URL
const DEFAULT_STORAGE_FEE = Number(import.meta.env.VITE_STORAGE_FEE)

export function useFeeCalculator() {
  const basicFee = ref(0)
  const specialFee = ref(0)
  const associationFee = ref(0)
  const storageFee = ref(DEFAULT_STORAGE_FEE)
  const totalCost = ref(0)

  async function calculateFees(vehiclePrice: number, vehicleType: string) {
    try {
      const response = await fetch(`${API_URL}/FeeCalculator`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ price: vehiclePrice, type: vehicleType })
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

  function resetFees() {
    basicFee.value = 0
    specialFee.value = 0
    associationFee.value = 0
    totalCost.value = 0
  }

  return {
    basicFee,
    specialFee,
    associationFee,
    storageFee,
    totalCost,
    calculateFees,
    resetFees
  }
}
