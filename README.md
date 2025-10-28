# 🚗 Bid Calculation Tool — Full Stack App

This is a full-stack web application that allows users to calculate the total cost of a vehicle at auction. It dynamically computes all applicable fees based on the vehicle's price and type (Common or Luxury), using a backend API and a responsive frontend interface.

---

## 🧱 Architecture Overview

The project is divided into two main components:

- **Frontend**: Built with Vue 3 + TypeScript + Vite  
- **Backend**: Built with ASP.NET Core Web API

The backend handles fee calculations, while the frontend provides a user-friendly interface for input and result display.

---

## 📦 Tech Stack

| Layer     | Technologies Used                                      |
|-----------|--------------------------------------------------------|
| Frontend  | Vue 3, TypeScript, Vite, Vitest, @vue/test-utils       |
| Backend   | ASP.NET Core Web API, C# 10, xUnit, Moq, FluentAssertions |

---

## 📊 Fee Calculation Logic

| Fee Type           | Common Vehicle                     | Luxury Vehicle                     |
|--------------------|-------------------------------------|-------------------------------------|
| Basic Buyer Fee    | 10% of price (min $10, max $50)     | 10% of price (min $25, max $200)    |
| Special Seller Fee | 2% of price                         | 4% of price                         |
| Association Fee    | Based on price range:               | Same as Common                      |
|                    | $1–$500 → $5                        |                                     |
|                    | $501–$1000 → $10                    |                                     |
|                    | $1001–$3000 → $15                   |                                     |
|                    | Over $3000 → $20                    |                                     |
| Storage Fee        | Fixed $100                          | Fixed $100                          |

---

## 🚀 Running the App Locally

### 🔧 Prerequisites

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- Node.js + npm
- Visual Studio or VS Code

### ▶️ Backend

```bash
cd backend/BidCalculator.API
dotnet run
