# Clean Architecture for ASP.NET Core with Entity Framework Core

## Presentation Layer
- **Controllers**: Handle HTTP requests and direct them to the appropriate services.
- **Views**: Render the UI using HTML, CSS, and JavaScript.
- **APIs**: Expose endpoints for communication with client applications.
- **Input Validation**: Ensure data received from the client is valid before processing.

---

## Business Logic Layer
- **Services**: Implement business rules and coordinate application workflows.
- **Domain Models**: Represent core entities and their properties.
- **Validations**: Enforce business constraints and ensure integrity.
- **Application-level Orchestration**: Manage the interactions between different components.

---

## Data Access Layer
- **Repositories**: Encapsulate database queries and ensure abstraction.
- **Entity Framework Core (EF Core)**: Manage database operations with an ORM.
- **Unit of Work**: Handle transactions to maintain consistency.
- **Mappings**: Define relationships between entities and database tables.
- **Query Optimizations**: Enhance the efficiency of data retrieval.

---

## Infrastructure Layer
- **Logging**: Record and monitor application events.
- **Caching**: Improve performance by temporarily storing frequently accessed data.
- **Authentication**: Manage user identity and secure access.
- **Authorization**: Define and enforce user roles and permissions.
- **Email Service**: Handle notifications and email delivery.
- **External API Integrations**: Communicate with external services and APIs.

---

## Database Layer
- **SQL Server**: Relational database for persistent storage.
- **Tables**: Organize data into structured formats.
- **Indexes**: Optimize query performance and speed up searches.
- **Migrations**: Handle schema changes and versioning.
- **Relationships**: Maintain data integrity using keys and constraints.

# Class Dependencies in Clean Architecture

This document outlines the dependencies among different classes in the clean architecture setup. Each layer should only depend on the layer directly beneath it, following the Dependency Inversion Principle.

---

## **Presentation Layer**
- **Controllers**
  - **Depends On**: Services (from Business Logic Layer)
  - Example: `MovieController.cs` depends on `MovieService.cs`
- **ViewModels / DTOs**
  - **Depends On**: Domain Models (from Business Logic Layer)
  - Example: `MovieViewModel.cs` depends on `Movie.cs`

---

## **Business Logic Layer**
- **Services**
  - **Depends On**: Repositories (from Data Access Layer)
  - Example: `MovieService.cs` depends on `IMovieRepository.cs`
- **Domain Models**
  - **No Direct Dependency**: Standalone representations of entities
  - Example: `Movie.cs` has no dependencies

---

## **Data Access Layer**
- **Repositories**
  - **Depends On**: Entity Framework Core and Domain Models
  - Example: `MovieRepository.cs` depends on `Movie.cs` and `DbContext`
- **DbContext**
  - **Depends On**: Entity Configurations (Mappings) and Database
  - Example: `AppDbContext.cs` uses `MovieConfiguration.cs`
- **Entity Configurations (Mappings)**
  - **Depends On**: Domain Models
  - Example: `MovieConfiguration.cs` depends on `Movie.cs`

---

## **Infrastructure Layer**
- **Logging Service**
  - **No Direct Dependency**: Implements logging logic for all layers
  - Example: `FileLogger.cs`
- **Caching Service**
  - **Depends On**: External caching mechanisms (e.g., Redis)
  - Example: `RedisCacheService.cs`
- **Email Service**
  - **Depends On**: External SMTP libraries
  - Example: `SmtpEmailService.cs`

---

## **Database Layer**
- **SQL Server**
  - **Depends On**: DbContext (from Data Access Layer)
  - Example: SQL tables are generated via `DbContext` migrations

---

## **Dependency Flow**
```
Presentation Layer
  |-- Controllers --> Business Logic Layer (Services)
  |-- ViewModels --> Business Logic Layer (Domain Models)

Business Logic Layer
  |-- Services --> Data Access Layer (Repositories)
  |-- Domain Models --> No dependencies

Data Access Layer
  |-- Repositories --> Entity Framework Core + Domain Models
  |-- DbContext --> Entity Configurations + Database
  |-- Entity Configurations --> Domain Models

Infrastructure Layer
  |-- Services (e.g., Logging, Caching, Email) --> External Libraries/APIs

Database Layer
  |-- SQL Server --> Data Access Layer (DbContext)
```
