import { describe, it, expect } from 'vitest'
import { computed, ref } from 'vue'

describe('FeeCalculator logic', () => {
  it('feesReady should be true when price > 0 and type is set', () => {
    const vehiclePrice = ref(1000)
    const vehicleType = ref<'Common' | 'Luxury' | ''>('Common')

    const feesReady = computed(() =>
      vehiclePrice.value !== null &&
      vehiclePrice.value > 0 &&
      vehicleType.value !== ''
    )

    expect(feesReady.value).toBe(true)
  })

  it('feesReady should be false when price is null', () => {
    const vehiclePrice = ref(null)
    const vehicleType = ref<'Common' | 'Luxury' | ''>('Luxury')

    const feesReady = computed(() =>
      vehiclePrice.value !== null &&
      vehiclePrice.value > 0 &&
      vehicleType.value !== ''
    )

    expect(feesReady.value).toBe(false)
  })
})

describe('resetFees', () => {
  it('should reset all fee values to 0', () => {
    const basicFee = ref(25)
    const specialFee = ref(10)
    const associationFee = ref(15)
    const totalCost = ref(105)

    function resetFees() {
      basicFee.value = 0
      specialFee.value = 0
      associationFee.value = 0
      totalCost.value = 0
    }

    resetFees()

    expect(basicFee.value).toBe(0)
    expect(specialFee.value).toBe(0)
    expect(associationFee.value).toBe(0)
    expect(totalCost.value).toBe(0)
  })
})



