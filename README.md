📦 Inventory Management System yc 

ASP.NET Core Web API + ASP.NET MVC | MySQL | Admin Dashboard

A complete Inventory Management System built using ASP.NET Core, featuring:

Product & Category Management

Purchase (Stock In) Tracking

Sales (Stock Out) Tracking

Supplier & Customer Handling


Dashboard Analytics & Reports

Export Reports to PDF / Excel

Secure Login Authentication

Professional Admin UI (AdminLTE + Bootstrap)

This project is designed for college final year submission and real-world inventory operations.

🚀 Features
🔐 Authentication

Admin login system

Cookie-based authorization

Secure session handling

📦 Product Management

Add / Edit / Delete products

Category-wise organization

Automatic stock update

🗂 Categories

Create and manage product categories

Dropdown integration with products

🛒 Purchases (Stock IN)

Add purchase records

Supplier tracking

Auto increase stock quantity

💰 Sales (Stock OUT)

Record sales transactions

Customer tracking

Auto decrease stock quantity

Prevents selling when stock is insufficient

📊 Dashboard & Reports

Total products, categories, sales, purchases

Revenue, cost, and profit calculation

Table-based professional report view

Export to PDF and Excel

🏗 Technology Stack
Backend

ASP.NET Core Web API

Entity Framework Core

MySQL Database

Frontend

ASP.NET Core MVC

Bootstrap 5

AdminLTE Dashboard

jQuery

Tools

Visual Studio / VS Code

Git & GitHub

Postman / Swagger

📁 Project Structure
Inventory Management
│
├── InventoryAPI        → ASP.NET Core Web API (Backend)
├── InventoryMVC        → ASP.NET MVC Admin Panel (Frontend)
└── Database (MySQL)

⚙️ Setup Instructions
1️⃣ Clone Repository
git clone https://github.com/yourusername/inventory-management-system.git
cd inventory-management-system

2️⃣ Configure Database

Update appsettings.json in:

InventoryAPI/appsettings.json


Set your MySQL connection string.

3️⃣ Run Backend API
cd InventoryAPI
dotnet run


Swagger will open at:

http://localhost:5120/swagger

4️⃣ Run MVC Frontend

Open new terminal:

cd InventoryMVC
dotnet run


App will open at:

http://localhost:5286

🔑 Default Login
Username: admin
Password: admin123
