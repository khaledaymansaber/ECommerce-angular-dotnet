# 🛒 Ecom-Angular-With-DotNetApi

A full-stack, enterprise-level e-commerce web application combining a responsive **Angular 17** frontend with a robust, modular **ASP.NET Core (.NET 8)** backend API. 

This project follows **Clean Architecture** principles, ensuring a strict separation of concerns among the API, core domain logic, and infrastructure layers, making it highly scalable and maintainable.

---

## ✨ Key Features

### Backend (.NET 8 API)
* **Modular Architecture:** Separated into `API`, `Core`, and `Infrastructure` layers.
* **Comprehensive REST Endpoints:** Manages Products, Categories, Baskets (Shopping Cart), Orders, Payments, Ratings, and User Accounts.
* **Security & Auth:** JWT-based authentication and authorization.
* **Data Mapping & Caching:** AutoMapper for DTO-to-Domain mapping and Memory Cache implementation.
* **Error Handling:** Centralized custom exception handling middleware.
* **API Documentation:** Interactive Swagger UI enabled in development.
* **CORS Policy:** Configured to accept requests from the Angular frontend (`http://localhost:4200`).

### Frontend (Angular 17)
* **Modern UI Framework:** Built with Angular 17 utilizing components, services, route guards, and pipes.
* **State & Data Management:** Seamless integration with backend REST APIs.
* **Testing:** Configured for unit testing with **Karma**.
* **Production Ready:** Build-ready configuration optimized for deployment.

---

## 🛠️ Tech Stack

* **Frontend:** Angular 17, TypeScript, HTML, CSS, Karma.
* **Backend:** C# 12, .NET 8, ASP.NET Core Web API.
* **Data & Infrastructure:** Entity Framework Core, SQL Server (or relational DB of choice).
* **Patterns & Libraries:** Clean Architecture, Repository Pattern, Unit of Work, AutoMapper, Swagger.

---

## 📂 Repository Structure

```text
├── Ecom-Angular/                # Angular Frontend Application
│   ├── src/                     # Angular source code (components, services)
│   ├── angular.json             # Workspace configuration
│   └── package.json             # Dependencies and scripts
│
├── Ecom.sln                     # .NET Backend Solution
├── Ecom.API/                    # ASP.NET Core Web API project (Runtime)
│   ├── Controllers/             # API endpoints
│   ├── Middleware/              # Exception handling middleware
│   └── Mapping/                 # AutoMapper profiles
│
├── Ecom.Core/                   # Domain models and abstractions
│   ├── Entities/                # Domain entities (Product, Order, AppUser, etc.)
│   ├── DTO/                     # Data Transfer Objects
│   └── Interfaces/              # Core and Repository interfaces
│
└── Ecom.Infrastructure/         # Data access and external services
    ├── Data/                    # DbContext and persistence code
    └── Repositories/            # Concrete repository implementations
