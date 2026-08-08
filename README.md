# 🛍️ Boutique — ASP.NET Core E-Commerce Platform

[![.NET](https://img.shields.io/badge/ASP.NET%20Core-Multi--App-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-68217A?style=for-the-badge&logo=nuget)](https://docs.microsoft.com/en-us/ef/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-UI-7952B3?style=for-the-badge&logo=bootstrap)](https://getbootstrap.com/)
[![Architecture](https://img.shields.io/badge/Architecture-3--Layer-blue?style=for-the-badge)](#-architecture)

**Boutique** is a modern, modular e-commerce platform engineered with ASP.NET Core. Designed with a strict separation of concerns, the project is divided into two distinct applications: a customer-facing **Web Store** built with Razor Pages, and a secure **Admin Panel** driven by ASP.NET Core MVC.

The system features dynamic inventory tracking, asynchronous pagination, and a highly decoupled backend architecture.

---

## 📸 Screenshots
<img width="1100" height="820" alt="Screenshot 2026-08-08 050650" src="https://github.com/user-attachments/assets/b5b6290b-6a9b-4ea9-a5a8-20aef898ea42" />
<img width="1718" height="859" alt="Screenshot 2026-08-08 050722" src="https://github.com/user-attachments/assets/42f4471d-90ef-4fde-a1e8-cdbf31cb9bf1" />
<img width="1896" height="912" alt="Screenshot 2026-08-08 050621" src="https://github.com/user-attachments/assets/f2529552-2677-4a9a-9741-334618d4a9af" />
<img width="1699" height="735" alt="Screenshot 2026-08-08 050813" src="https://github.com/user-attachments/assets/563a0257-e24a-4599-949c-cfee918826b2" />




---

## ✨ Features

* **Dual-Application Architecture:** Separate presentation layers for customers (Razor Pages) and administrators (MVC).
* **Dynamic Inventory Calculation:** Product stock is calculated in real-time, factoring in items currently held in active user carts.
* **Fluid Pagination:** Catalog browsing utilizes JavaScript and asynchronous data fetching to paginate results without requiring full-page reloads.
* **Responsive UI:** Built with Bootstrap to ensure a seamless shopping experience across mobile, tablet, and desktop devices.
* **Granular Product Filtering:** Deeply nested category and subcategory filtering combined with robust search capabilities.

---

## 🏗 Architecture

This solution adheres to a **Three-Layer Architecture**, enforcing a strict separation of concerns between data persistence, business logic, and presentation. 

A core principle of this project is that the presentation layers (**Web Store** and **Admin Panel**) **never** communicate directly with Entity Framework Core or the database. All data access and business rules are abstracted behind interfaces provided by the Core layer.

### Layer Responsibilities

1. **Web / Presentation Layer:** Consumes Core layer interfaces. Handles HTTP requests, UI rendering, and user input validation.
2. **Core Layer:** The business engine. Contains DTOs, domain abstractions, and Service implementations (e.g., `ItemService`, `CategoryService`, `FileService`).
3. **Data Layer:** Manages persistence. Houses the domain entities, EF Core `DbContext`, database configurations, and migrations.

### Architecture Diagram

```text
       [ Boutique.Web (Razor Pages) ]     [ Boutique.Admin (MVC) ]
                    │                                │
                    └───────────────┬────────────────┘
                                    │ (Depends on Interfaces)
                                    ▼
       [ Boutique.Core (Services, DTOs, Interfaces) ]
                                    │
                                    │ (Depends on Entities/EF)
                                    ▼
       [ Boutique.Data (EF Core, DbContext, Migrations) ]
                                    │
                                    ▼
                             [ Database ]
📂 Project StructurePlaintextBoutique-Solution/
├── src/
│   ├── Boutique.Data/                  # Data access & persistence
│   │   ├── Entities/                   # Database entity classes
│   │   ├── Context/                    # EF Core DbContext
│   │   └── Migrations/                 # Entity Framework migrations
│   │
│   ├── Boutique.Core/                  # Business logic & abstractions
│   │   ├── DTOs/                       # Data Transfer Objects
│   │   ├── Interfaces/                 # Service contracts (IItemService, etc.)
│   │   └── Services/                   # Business logic implementations
│   │
│   ├── Boutique.Admin/                 # Admin Panel (Presentation)
│   │   ├── Controllers/                # MVC Controllers
│   │   ├── Views/                      # MVC Views
│   │   └── appsettings.json
│   │
│   └── Boutique.Web/                   # Web Store (Presentation)
│       ├── Pages/                      # Razor Pages
│       ├── wwwroot/                    # Static assets (JS, CSS, Bootstrap)
│       └── appsettings.json
│
├── .gitignore
├── Boutique.sln                        # Solution file
└── README.md
🛠 TechnologiesDomainTechnologies UsedFrameworksASP.NET Core, ASP.NET Core MVC, Razor PagesLanguageC#Data AccessEntity Framework CoreDatabaseRelational SQL Database (Configured via EF Core)FrontendBootstrap, HTML5, CSS3, JavaScript (AJAX/Fetch)Architecture3-Layer Architecture, Dependency Injection, DTO Pattern🔐 Authentication & User AccountsSecurity and identity management are powered by native ASP.NET Core Authentication.Account Management: Users can securely register and log in to the platform.Session State: Authenticated users are assigned personalized, persistent shopping carts tied directly to their accounts.👕 Product & Category SystemThe application features a robust catalog management system designed for clothing and retail:Hierarchical Organization: Products are mapped to a dynamic structure of Categories and Subcategories.Variant Support: Products support variant sizing, allowing customers to select specific sizes before adding to their cart.Search & Discovery: Integrated search functionality combined with category filtering makes finding specific items frictionless.🛒 Shopping CartThe shopping cart acts as a dynamic state engine for authenticated users:Cart Operations: Users can add multiple items, specify sizes, adjust quantities, or remove products entirely.Real-time Totals: The cart dynamically calculates financial totals based on current quantities.Smart Stock Tracking: The system prevents overselling by dynamically reflecting available stock based on what is actively sitting in users' carts system-wide.🚀 Installation / SetupPrerequisites.NET SDK (Version compatible with the project, e.g., .NET 8.0)SQL Database Server (e.g., SQL Server, LocalDB)Git1. Clone the RepositoryBashgit clone <repository-url>
cd <project-directory>
2. Configure Database ConnectionsNavigate to the presentation layer projects and update the connection strings to point to your local database instance.Update Boutique.Web/appsettings.json:JSON"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BoutiqueDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
(Repeat for Boutique.Admin/appsettings.json if it uses a separate configuration).🗄️ Database SetupThe project uses Entity Framework Core Code-First migrations. To generate the database schema, run the EF Core CLI tools.Bash# Navigate to the Data project or run from solution root specifying the project
dotnet ef database update --project src/Boutique.Data --startup-project src/Boutique.Web
⚙️ Running the ProjectBecause the solution contains two separate web applications, you can run them simultaneously or individually depending on your needs.To run the Web Storefront:Bashcd src/Boutique.Web
dotnet run
Navigate to https://localhost:<port> to view the store.To run the Admin Panel:Bashcd src/Boutique.Admin
dotnet run
Navigate to the assigned local port to access the management dashboard.🔮 Future Improvements[ ] Integrate a third-party payment gateway (e.g., Stripe or PayPal).[ ] Implement an automated email notification system for order confirmations.[ ] Add caching (e.g., Redis or In-Memory) to optimize category and product catalog load times.[ ] Introduce an order history and tracking dashboard for users.🤝 ContributingContributions are welcome! If you would like to improve the project:Fork the RepositoryCreate your Feature Branch (git checkout -b feature/AmazingFeature)Commit your Changes (git commit -m 'Add some AmazingFeature')Push to the Branch# 🛍️ Boutique
