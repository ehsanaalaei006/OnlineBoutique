
**Boutique** is a modern, modular e-commerce platform built with ASP.NET Core.

To ensure maintainability and separation of concerns, the platform is divided into two distinct applications operating on a shared core logic layer:

1. **Web Store:** A customer-facing shopping interface built with **ASP.NET Core Razor Pages**.
2. **Admin Panel:** A dedicated management dashboard built with **ASP.NET Core MVC** for store and inventory administration.

---

## ✨ Features

* **Dual-Application Architecture:** Separate routing, views, and controllers for administrators and customers.
* **Separation of Concerns:** Strict adherence to a 3-layer architecture, isolating database operations from the presentation layer.
* **Dynamic Stock Management:** Real-time calculation of available product quantities based on items currently held in active user carts.
* **Asynchronous Pagination:** Seamless UI updates for product grids using JavaScript and AJAX, eliminating full-page reloads.
* **Responsive UI:** Built with Bootstrap for a fluid mobile and desktop experience.

---

## 🏗️ Architecture

The solution implements a strict **Three-Layer Architecture**. The Web and Admin applications are entirely decoupled from the database layer, communicating solely through interfaces exposed by the Core layer.

### 1. Data Layer

Responsible for all data access and persistence mechanics. It contains the Entity Framework Core `DbContext`, domain entity classes, database configuration logic, and EF Core migrations.

### 2. Core Layer

Acts as the central business logic hub. It contains Data Transfer Objects (DTOs), service implementations, and abstract interfaces (`IItemService`, `IFileService`, `ICategoryService`).

### 3. Web & Admin Layers (Presentation)

Handles HTTP requests, routing, and UI rendering via Razor Pages and MVC. These layers inject Core interfaces via Dependency Injection (DI) and never query Entity Framework directly.

### Architecture Diagram

```text
       +-------------------------+      +-------------------------+
       |       Web Store         |      |       Admin Panel       |
       |     (Razor Pages)       |      |     (ASP.NET MVC)       |
       +-------------------------+      +-------------------------+
                    |                                |
                    +---------------+----------------+
                                    |
                                    v
       +----------------------------------------------------------+
       |                        Core Layer                        |
       |  (DTOs, ItemService, CategoryService, FileService, etc.) |
       +----------------------------------------------------------+
                                    |
                                    v
       +----------------------------------------------------------+
       |                        Data Layer                        |
       |   (Entities, EF Core DbContext, Migrations, Repositories)|
       +----------------------------------------------------------+
                                    |
                                    v
                          +-------------------+
                          | Relational Engine |
                          |   (SQL Database)  |
                          +-------------------+

```

---

## 📂 Project Structure

```text
Boutique/
├── src/
│   ├── Boutique.Core/                 # Business logic, DTOs, and Interfaces
│   │   ├── DTOs/
│   │   ├── Interfaces/                # IItemService, ICategoryService, etc.
│   │   └── Services/                  # Concrete service implementations
│   │
│   ├── Boutique.Data/                 # Persistence and Entity Framework
│   │   ├── Context/                   # ApplicationDbContext
│   │   ├── Entities/                  # Database models
│   │   └── Migrations/                # EF Core migrations
│   │
│   ├── Boutique.Web/                  # Customer-facing Razor Pages app
│   │   ├── Pages/                     # Razor pages (Index, Product, Cart)
│   │   └── wwwroot/                   # JS, CSS, and Bootstrap assets
│   │
│   └── Boutique.Admin/                # Management MVC app
│       ├── Controllers/               # MVC Controllers
│       └── Views/                     # MVC Views
│
├── .gitignore
├── Boutique.sln
└── README.md

```

---

## 🛠️ Technologies

| Component | Technology |
| --- | --- |
| **Framework** | .NET 8, C# |
| **Customer Web App** | ASP.NET Core Razor Pages |
| **Admin Web App** | ASP.NET Core MVC |
| **ORM / Data Access** | Entity Framework Core (Code-First) |
| **Database** | SQL Server / Relational Database |
| **Frontend / UI** | Bootstrap, HTML5, CSS3 |
| **Client-Side Logic** | JavaScript, AJAX |

---

## 🔐 Authentication & User Accounts

The platform leverages native ASP.NET Core Authentication to manage identity and access control:

* **Registration & Login:** Secure user authentication flows.
* **Session Management:** Authenticated users are assigned personalized, persistent shopping carts tied to their account identity.
* **Authorization:** Administrative routes are securely isolated from standard user accounts.

---

## 📦 Product & Category System

The catalog is designed for deep hierarchical organization and robust searchability:

* **Nested Categories:** Products are organized into primary categories and deeper subcategories for refined browsing.
* **Search & Filtering:** Users can filter the catalog through the category tree or utilize text-based search.
* **Variant Support:** Products support variant sizing, allowing customers to select specific sizes prior to purchase.

---

## 🛒 Shopping Cart & Dynamic Inventory

A highly interactive shopping cart system ensures accurate inventory representation:

* **Per-User Carts:** Authenticated users manage their own dedicated cart state.
* **Cart Operations:** Users can add items, specify sizes, adjust quantities, remove items, and view real-time price calculations.
* **Dynamic Stock Calculation:** To prevent overselling, the application dynamically calculates available product stock by deducting quantities currently held in active users' shopping carts across the platform.

---

## ⚙️ Installation / Setup

### Prerequisites

* [.NET 8.0 SDK](https://www.google.com/search?q=https://dotnet.microsoft.com/download)
* A local SQL Database engine (e.g., SQL Server LocalDB)
* Visual Studio, VS Code, or Rider

### Clone the Repository

```bash
git clone [https://github.com/ehsanaalaei006/boutique-ecommerce-aspnet.git](https://github.com/ehsanaalaei006/boutique-ecommerce-aspnet.git)
cd boutique-ecommerce-aspnet

```

---

## 🗄️ Database Setup

1. Open the `appsettings.json` file in both `Boutique.Web` and `Boutique.Admin` and ensure the connection string points to your local database instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BoutiqueDb;Trusted_Connection=True;"
}

```


2. Apply the Entity Framework Core migrations to construct the database schema. Open your terminal in the solution root and run:
```bash
dotnet ef database update --project src/Boutique.Data --startup-project src/Boutique.Web

```



---

## 🚀 Running the Project

Because the project consists of two separate web applications, you can run them concurrently or individually.

**To run the Web Store (Razor Pages):**

```bash
dotnet run --project src/Boutique.Web

```

**To run the Admin Panel (MVC):**

```bash
dotnet run --project src/Boutique.Admin

```

Navigate to `https://localhost:<port>` in your browser to view the application.

---

## 📸 Screenshots

<img width="1100" height="820" alt="Screenshot 2026-08-08 050650" src="https://github.com/user-attachments/assets/b5b6290b-6a9b-4ea9-a5a8-20aef898ea42" />
<img width="1718" height="859" alt="Screenshot 2026-08-08 050722" src="https://github.com/user-attachments/assets/42f4471d-90ef-4fde-a1e8-cdbf31cb9bf1" />
<img width="1896" height="912" alt="Screenshot 2026-08-08 050621" src="https://github.com/user-attachments/assets/f2529552-2677-4a9a-9741-334618d4a9af" />
<img width="1699" height="735" alt="Screenshot 2026-08-08 050813" src="https://github.com/user-attachments/assets/563a0257-e24a-4599-949c-cfee918826b2" />

---

## 🔮 Future Improvements

* Integrate a third-party payment gateway (e.g., Stripe) for checkout processing.
* Implement a caching layer (MemoryCache/Redis) for category trees and heavily accessed product catalogs.
* Add unit and integration tests for Core services and Data repositories.

---

## 🤝 Contributing

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/NewFeature`)
3. Commit your Changes (`git commit -m 'feat: Add NewFeature'`)
4. Push to the Branch (`git push origin feature/NewFeature`)
5. Open a Pull Request

---

## 📜 License

Distributed under the MIT License. See `LICENSE` for more information.
