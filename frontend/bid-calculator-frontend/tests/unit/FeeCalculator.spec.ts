import { describe, it, expect } from 'vitest'
import { computed, ref } from 'vue'
import { mount } from '@vue/test-utils'
import FeeCalculator from '../../src/components/FeeCalculator.vue'
const DEFAULT_STORAGE_FEE = Number(import.meta.env.VITE_STORAGE_FEE)

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
      const wrapper = mount(FeeCalculator)
      wrapper.vm.basicFee = 10
      wrapper.vm.specialFee = 20
      wrapper.vm.associationFee = 30
      wrapper.vm.totalCost = 1000

      wrapper.vm.resetFees()

      expect(wrapper.vm.basicFee).toBe(0)
      expect(wrapper.vm.specialFee).toBe(0)
      expect(wrapper.vm.associationFee).toBe(0)
      expect(wrapper.vm.totalCost).toBe(0)

  })
})

describe('FeeCalculator.vue', () => {
    it('feesReady is false when inputs are empty', () => {
      const wrapper = mount(FeeCalculator)
      expect(wrapper.vm.feesReady).toBe(false)
    })

    it('feesReady is true when valid inputs are set', async () => {
        const wrapper = mount(FeeCalculator)
        wrapper.vm.vehiclePrice = 1000
        wrapper.vm.vehicleType = 'Common'
        await wrapper.vm.$nextTick()

        expect(wrapper.vm.feesReady).toBe(true)
    })
})

it('resetFees sets all fees to 0 except storageFee', async () => {
  const wrapper = mount(FeeCalculator)
  wrapper.vm.basicFee = 10
  wrapper.vm.specialFee = 20
  wrapper.vm.associationFee = 30
  wrapper.vm.totalCost = 1000
  wrapper.vm.storageFee = DEFAULT_STORAGE_FEE

  wrapper.vm.resetFees()

  expect(wrapper.vm.basicFee).toBe(0)
  expect(wrapper.vm.specialFee).toBe(0)
  expect(wrapper.vm.associationFee).toBe(0)
  expect(wrapper.vm.totalCost).toBe(0)
  expect(wrapper.vm.storageFee).toBe(DEFAULT_STORAGE_FEE)
})

describe('Integration', () => {
  it('calculates and renders fees after user input', async () => {
    const wrapper = mount(FeeCalculator)

    const priceInput = wrapper.find('input#price')
    const typeSelect = wrapper.find('select#type')

    await priceInput.setValue(1000)
    await typeSelect.setValue('Common')

    await new Promise(resolve => setTimeout(resolve, 500))

    expect(wrapper.text()).toContain('Basic Fee')
    expect(wrapper.text()).toContain('Total: 1180.00')
  })
})

describe('preventNegative', () => {
  it('should set vehiclePrice to 0 if input is negative', () => {
    const vehiclePrice = ref(-500)

    function preventNegative() {
      if (vehiclePrice.value < 0) {
        vehiclePrice.value = 0
      }
    }

    preventNegative()

    expect(vehiclePrice.value).toBe(0)
  })

  it('should keep vehiclePrice unchanged if input is positive', () => {
    const vehiclePrice = ref(1200)

    function preventNegative() {
      if (vehiclePrice.value < 0) {
        vehiclePrice.value = 0
      }
    }

    preventNegative()

    expect(vehiclePrice.value).toBe(1200)
  })
})






