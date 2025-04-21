# 🛒 DeveloperStore - Sales System (Technical Assessment)

This project is part of a **technical assessment** for the **SR .NET Developer** position, with the objective of implementing a new feature in an existing solution with an architecture based on **DDD (Domain-Driven Design)**.

A RESTful API was developed responsible for the **complete CRUD of Sales and Sales Items**, respecting the business rules provided and using good practices such as **separation by layers**, **dependency injection**, **denormalization with external identities** and **domain events**.

---

## 🧠 Use Case Domain

The sales API handles the following data:

- Sale number
- Sale date
- Customer
- Total sale value
- Branch where the sale was made
- Products
- Quantities
- Unit prices
- Discounts applied
- Total value of each item
- Cancellation status (cancelled or not)

---

## ✅ Business Rules

1. Purchases with **4+ identical items** have a **10% discount**
2. Purchases between **10 and 20 identical items** have a **20% discount**
3. It is not possible to sell **more than 20 identical items**
4. Purchases with **less than 4 items** have no discount

---

## 🧱 Project Structure

### 📦 Ambev.DeveloperEvaluation.Domain

Responsible for **entities, events and repository interfaces**:

- `Entities/Sale.cs`: main sales entity
- `Entities/SaleItem.cs`: sales item entity
- `Events/SaleEvent.cs`: abstract base class for events (SaleCreated, SaleCanceled, etc.)
- `Repositories/ISaleRepository.cs`: interface for sales repository
- `Repositories/ISaleItemRepository.cs`: interface for item repository

---

### 💡 Ambev.DeveloperEvaluation.Application

Contains the **DTOs, Services, Mappings and Interfaces**:

- `DTOs/`
- `SaleDto.cs`
- `SaleItemDto.cs`
- `Interfaces/`
- `ISaleService.cs`
- `ISaleItemService.cs`
- `IEventPublisher.cs`
- `Services/`
- `SaleService.cs`
- `SaleItemService.cs`
- `LoggingEventPublisher.cs`
- `Mappings/DomainToDTOMappingProfile.cs`: Mapping via AutoMapper

---

### 🗃 Ambev.DeveloperEvaluation.ORM

Responsible for **database access** with Entity Framework Core:

- `Repositories/`
- `SaleRepository.cs`
- `SaleItemRepository.cs`
- `DefaultContext.cs`: DbSets and general configuration
- `YourDbContextFactory.cs`: factory class for use with migrations

> **Included DbSets**:
> - `Sales`
> - `SaleItems`

---

### ⚙️ Ambev.DeveloperEvaluation.IoC

Contains the **dependency injection** configuration:

- `InfrastructureModuleInitializer.cs`:
- Repositories
- Services
- Events
- AutoMapper

---

### 🌐 Ambev.DeveloperEvaluation.WebApi

Exposing the **RESTful API** with endpoints organized by controllers:

- `Controllers/SalesController.cs`: Sales CRUD
- `Controllers/SalesItemController.cs`: Item CRUD
- `appsettings.json`: Connection string configuration

#### 🧪 Available endpoints:

##### 🔹 Sales (`/api/Sales`)

- `GET /api/Sales`
- `POST /api/Sales`
- `GET /api/Sales/{id}`
- `PUT /api/Sales/{id}`
- `DELETE /api/Sales/{id}`
- `PUT /api/Sales/{id}/cancel/status`

##### 🔸 Sale Items (`/api/SalesItem`)

- `GET /api/SalesItem`
- `POST /api/SalesItem`
- `GET /api/SalesItem/{id}`
- `PUT /api/SalesItem/{id}`
- `DELETE /api/SalesItem/{id}`
- `PUT /api/SalesItem/{id}/cancelItem/status`

---

### 🧪 Ambev.DeveloperEvaluation.Unit

Unit tests with **XUnit**:

- `Domain/Entities/SaleTest.cs`
- `Domain/Entities/SaleItemTest.cs`

---

## 📤 Event Publishing

Event system in the **Domain Events** standard, publishing logs via `LoggingEventPublisher.cs`.

Supported events:

- `SaleCreated`
- `SaleModified`
- `SaleCanceled`
- `ItemCanceled`

---

## 🚀 How to use

1. Clone the repository:

- `git clone https://github.com/Paulo110998/DeveloperStore.git`
- `cd DeveloperStore`

2. Create the PostgreSQL database with the name "DeveloperEvaluation".

3. Update the database with the migration:

- `dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi`

4. Run the application:

- `cd src/Ambev.DeveloperEvaluation.WebApi`

- `dotnet run`

5. Access Swagger:

- `Available at: https://localhost:7181/swagger/index.html`
