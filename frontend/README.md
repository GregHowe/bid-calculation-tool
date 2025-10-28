# 🚗 Bid Calculation Tool — Frontend

This is the **frontend** of the Bid Calculation Tool, a web application that allows buyers to calculate the total cost of a vehicle at auction, including dynamic fees based on price and vehicle type.

Built with **Vue 3 + TypeScript + Vite**, this UI communicates with a backend API to compute fees and display results in real time.

---

## 📦 Tech Stack

- Vue 3 with Composition API
- TypeScript
- Vite
- Vitest + @vue/test-utils
- Modular architecture with composables and environment config

---

## 🎯 Features

- Input fields for vehicle price and type (Common or Luxury)
- Dynamic fee calculation via backend API
- Real-time display of:
  - Basic buyer fee
  - Seller's special fee
  - Association fee
  - Fixed storage fee
  - Total cost
- Input validation with error messages
- Success message with icon and fade-in animation
- Responsive layout and centralized styling
- Environment variables for configuration

---

## 📁 Project Structure

```plaintext
src/
├── components/
│   ├── FeeCalculator.vue
│   └── FeeItem.vue
├── composables/
│   └── useFeeCalculator.ts
├── style.css
├── App.vue
└── main.ts
tests/
├── unit/
│   └── FeeCalculator.spec.ts
.env
```

---

## ⚙️ Environment Setup

Create a `.env` file in the root of the project:

```env
VITE_API_URL=http://localhost:5120/api
VITE_STORAGE_FEE=100
```
These variables configure the backend endpoint and default storage fee.

## 🚀 Getting Started
bash
npm install
npm run dev

Then open https://localhost:5173/  in your browser.

## 🧪 Testing
Unit tests are written using Vitest and cover:

    Fee calculation logic

    Input validation

    Reset behavior

    Component rendering and integration

To run tests:
bash
npx vitest run

Or watch mode:
bash
npx vitest --watch

