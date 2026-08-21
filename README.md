
# 🏢 Property Management API

A production-oriented **Property Management REST API** built with **ASP.NET Core 8**, following **Clean Architecture** principles and modern .NET development practices.

The project focuses on building a maintainable, scalable, and testable backend while applying patterns commonly used in professional .NET applications.

---

## 🚀 Tech Stack

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12-239120?style=flat&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-7-DC382D?style=flat&logo=redis&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?style=flat&logo=docker&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-blue?style=flat)
![Mapster](https://img.shields.io/badge/Mapster-Mapping-orange?style=flat)

---

# 📌 Project Overview

The Property Management API provides a backend foundation for managing:

- 👤 Property Agents
- 🏠 Properties
- 🔗 Agent–Property relationships
- 💾 Persistent data using SQL Server
- ⚡ Distributed caching using Redis
- ✅ Request validation
- 🔄 Standardized API responses
- 📦 CQRS-based application architecture

The project is designed with **separation of concerns** in mind so that business logic, infrastructure, and API concerns remain independent.

---

# 🏗️ Architecture

The application follows **Clean Architecture** principles.

```text
                    ┌──────────────────────┐
                    │      WebAPI          │
                    │                      │
                    │ Controllers /        │
                    │ Endpoints             │
                    └──────────┬───────────┘
                               │
                               ▼
                    ┌──────────────────────┐
                    │    Application       │
                    │                      │
                    │ CQRS                 │
                    │ MediatR              │
                    │ Validators           │
                    │ DTOs                 │
                    │ Interfaces           │
                    └──────────┬───────────┘
                               │
                               ▼
                    ┌──────────────────────┐
                    │       Domain         │
                    │                      │
                    │ Entities             │
                    │ Business Rules       │
                    │ Domain Models        │
                    └──────────────────────┘
                               ▲
                               │
                    ┌──────────┴───────────┐
                    │    Infrastructure    │
                    │                      │
                    │ EF Core              │
                    │ SQL Server           │
                    │ Redis                │
                    │ Repositories         │
                    │ External Services    │
                    └──────────────────────┘
````

### Project Structure

```text
PropertyManagement/
│
├── WebAPI/
│   ├── Controllers/
│   ├── Program.cs
│   └── WebAPI.csproj
│
├── Application/
│   ├── Features/
│   │   ├── Agents/
│   │   │   ├── Commands/
│   │   │   └── Queries/
│   │   │
│   │   └── Properties/
│   │       ├── Commands/
│   │       └── Queries/
│   │
│   ├── Models/
│   ├── Validators/
│   └── Application.csproj
│
├── Domain/
│   ├── Entities/
│   ├── Interfaces/
│   └── Domain.csproj
│
├── Infrastructure/
│   ├── Contexts/
│   ├── Repositories/
│   ├── Services/
│   ├── Migrations/
│   └── Infrastructure.csproj
│
├── docker-compose.yml
└── README.md
```

---

# 🧱 Architecture Principles

## Clean Architecture

The application separates responsibilities into independent layers:

### Domain

Contains the core business entities and rules.

```text
Domain
 └── Entities
      ├── Agent
      └── Property
```

The Domain layer does not depend on Infrastructure or WebAPI.

---

### Application

Contains application-specific business logic.

Responsibilities include:

* Commands
* Queries
* MediatR handlers
* DTOs
* Validators
* Interfaces
* Response models

Example:

```text
Application
 └── Features
      └── Agents
           ├── Commands
           │    ├── Create
           │    ├── Update
           │    └── Delete
           │
           └── Queries
                ├── GetAll
                └── GetById
```

---

### Infrastructure

Responsible for communicating with external systems.

Includes:

* Entity Framework Core
* SQL Server
* Redis
* Repository implementations
* Database migrations
* External services

---

### WebAPI

The entry point of the application.

Responsible for:

* HTTP endpoints
* Controllers
* Minimal API endpoints
* Dependency Injection configuration
* Middleware
* API configuration

---

# 🔄 CQRS & MediatR

The application uses **CQRS (Command Query Responsibility Segregation)** to separate operations that modify data from operations that retrieve data.

### Commands

Commands modify application state.

```text
CreateAgentCommand
UpdateAgentCommand
DeleteAgentCommand
```

### Queries

Queries retrieve information without modifying state.

```text
GetAllAgentsQuery
GetAgentByIdQuery
GetAllPropertiesQuery
GetPropertyByIdQuery
```

MediatR acts as the mediator between the API and application handlers.

```text
Controller
    │
    ▼
MediatR
    │
    ▼
Command / Query Handler
    │
    ▼
Application Service / Repository
    │
    ▼
Database
```

---

# 📦 Response Wrapper Pattern

The API uses a standardized response wrapper to provide a consistent response structure.

Example:

```json
{
  "success": true,
  "message": "Agent retrieved successfully",
  "data": {
    "id": 1,
    "firstName": "John",
    "lastName": "Smith",
    "email": "john@example.com"
  }
}
```

This provides a consistent contract between the API and its consumers.

---

# 🗄️ Entity Framework Core

Entity Framework Core is used as the ORM for SQL Server.

The project demonstrates:

* Code-first development
* Database migrations
* Entity relationships
* Eager loading
* Lazy loading concepts
* LINQ queries
* Async database operations
* DbContext configuration

### Agent → Property Relationship

An Agent can manage multiple properties.

```text
Agent
  │
  ├── Property
  ├── Property
  └── Property
```

This is implemented using a one-to-many relationship.

```csharp
public class Agent
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public List<Property> PropertyListings { get; set; }
}
```

```csharp
public class Property
{
    public int Id { get; set; }

    public int AgentId { get; set; }

    public Agent Agent { get; set; }
}
```

---

# 🗃️ Repository Pattern

The Repository Pattern provides an abstraction between application logic and data access.

Example:

```text
Application
     │
     ▼
 IRepository
     │
     ▼
Repository
     │
     ▼
Entity Framework Core
     │
     ▼
SQL Server
```

This helps reduce coupling between the application and the persistence layer.

---

# ⚡ Redis Caching

Redis is integrated as a distributed caching layer.

Caching can reduce unnecessary database queries and improve API response times.

```text
Client
  │
  ▼
API
  │
  ├── Redis Cache ──► Cached Data
  │
  └── SQL Server ──► Persistent Data
```

The application can check Redis before querying SQL Server.

---

# ✅ FluentValidation

FluentValidation is used to validate incoming requests before they reach the business logic.

Example:

```csharp
public class CreateAgentValidator
    : AbstractValidator<CreateAgentRequest>
{
    public CreateAgentValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
```

This keeps validation logic separate from controllers and domain entities.

---

# 🔄 Mapster

Mapster is used for converting between entities and DTOs.

Instead of manually mapping:

```csharp
new AgentResponse(
    agent.Id,
    agent.FirstName,
    agent.LastName,
    agent.PhoneNumber,
    agent.Email
);
```

the application can use:

```csharp
var response = agent.Adapt<AgentResponse>();
```

This keeps handlers and services cleaner.

---

# 🌐 Hybrid API Endpoints

The project explores both:

### Controllers

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    // ...
}
```

### Minimal APIs

```csharp
app.MapGet("/api/agents/{id}", async (int id) =>
{
    // ...
});
```

This provides experience with both approaches and demonstrates when different endpoint styles may be appropriate.

---

# ⚙️ Dependency Injection

The application uses the built-in ASP.NET Core Dependency Injection container.

Services are registered according to their required lifetime:

```csharp
services.AddScoped<IAgentService, AgentService>();
```

This keeps dependencies loosely coupled and makes components easier to test and maintain.

---

# 🗄️ SQL Server

Microsoft SQL Server is used as the primary relational database.

Database operations are handled through:

* Entity Framework Core
* LINQ
* EF Core migrations
* Relationships
* Transactions
* Async operations

---

# 🐳 Docker

The application can be containerized using Docker.

The development environment contains:

```text
┌──────────────────────────────┐
│ Docker Compose               │
│                              │
│ ┌────────────┐               │
│ │ WebAPI     │               │
│ │ .NET 8     │               │
│ └─────┬──────┘               │
│       │                      │
│ ┌─────▼──────┐  ┌─────────┐ │
│ │ SQL Server │  │  Redis  │ │
│ └────────────┘  └─────────┘ │
│                              │
└──────────────────────────────┘
```

Example:

```bash
docker compose up --build
```

API:

```text
http://localhost:5000
```

Swagger:

```text
http://localhost:5000/swagger
```

---

# 🔌 API Endpoints

## Agents

| Method | Endpoint          | Description     |
| ------ | ----------------- | --------------- |
| GET    | `/api/Agent`      | Get all agents  |
| GET    | `/api/Agent/{id}` | Get agent by ID |
| POST   | `/api/Agent`      | Create an agent |
| PUT    | `/api/Agent/{id}` | Update an agent |
| DELETE | `/api/Agent/{id}` | Delete an agent |

## Properties

| Method | Endpoint             | Description        |
| ------ | -------------------- | ------------------ |
| GET    | `/api/Property`      | Get all properties |
| GET    | `/api/Property/{id}` | Get property by ID |
| POST   | `/api/Property`      | Create a property  |
| PUT    | `/api/Property/{id}` | Update a property  |
| DELETE | `/api/Property/{id}` | Delete a property  |

---

# 📋 Example Request

### Create Agent

```http
POST /api/Agent
Content-Type: application/json
```

```json
{
  "firstName": "John",
  "lastName": "Smith",
  "phoneNumber": "0821234567",
  "email": "john@example.com"
}
```

### Create Property

```http
POST /api/Property
Content-Type: application/json
```

```json
{
  "agentId": 1,
  "shortDescription": "Modern 3 Bedroom House",
  "longDescription": "A spacious modern property located in Johannesburg.",
  "price": 1850000,
  "listingDate": "2026-08-21T00:00:00"
}
```

---

# 🛠️ Getting Started

## Prerequisites

Make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/)
* [Docker](https://www.docker.com/)
* SQL Server
* Git

---

## Clone the Repository

```bash
git clone https://github.com/yourusername/property-management-api.git

cd property-management-api
```

---

## Run with Docker

```bash
docker compose up --build
```

The API will be available at:

```text
http://localhost:5000
```

Swagger:

```text
http://localhost:5000/swagger
```

---

## Run Without Docker

Restore dependencies:

```bash
dotnet restore
```

Apply migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run --project WebAPI
```

---

# 🧪 Testing

The project is designed to support automated testing across different layers.

Planned test coverage includes:

* Unit tests
* Application layer tests
* Handler tests
* Repository tests
* Integration tests
* API endpoint tests

---

# 📚 Concepts Demonstrated

This project demonstrates practical experience with:

* ✅ Clean Architecture
* ✅ SOLID principles
* ✅ ASP.NET Core 8
* ✅ REST API development
* ✅ Entity Framework Core
* ✅ SQL Server
* ✅ Database migrations
* ✅ Entity relationships
* ✅ Repository Pattern
* ✅ CQRS
* ✅ MediatR
* ✅ Pipeline Behaviors
* ✅ Redis
* ✅ Distributed caching
* ✅ FluentValidation
* ✅ Mapster
* ✅ Dependency Injection
* ✅ DTOs
* ✅ Response Wrapper Pattern
* ✅ Controllers
* ✅ Minimal APIs
* ✅ Docker
* ✅ Docker Compose
* ✅ Async programming
* ✅ API documentation with Swagger/OpenAPI

---

# 🎯 Project Goals

The main goal of this project is to demonstrate how a modern .NET backend can be structured for:

* Maintainability
* Scalability
* Testability
* Separation of concerns
* Clean dependency management
* Performance
* Consistent API design

Rather than simply building CRUD endpoints, the project focuses on understanding **why architectural patterns exist and where they should be applied**.

---

# 🚧 Roadmap

Future improvements may include:

* [ ] Authentication & Authorization
* [ ] JWT authentication
* [ ] Role-based authorization
* [ ] Global exception handling middleware
* [ ] Structured logging
* [ ] Serilog
* [ ] Unit tests
* [ ] Integration tests
* [ ] Health checks
* [ ] API versioning
* [ ] Pagination
* [ ] Filtering & sorting
* [ ] Rate limiting
* [ ] CI/CD pipeline
* [ ] Azure deployment
* [ ] Kubernetes deployment

---

# 👨‍💻 Author

**Your Name**

Software Developer | .NET | C# | ASP.NET Core | SQL | Docker

This project was built as part of my journey toward becoming a professional backend/.NET developer.

---

# ⭐ Why This Project?

This project is continuously evolving as I learn and apply modern .NET architecture and engineering practices.

The objective is not only to make the API work, but to understand how to build software that can be **maintained, tested, extended, and deployed in a real-world environment**.

If you find the project useful, consider giving it a ⭐.

```

### One change I'd recommend for your GitHub profile

Don't call it simply **"Property Management API"** if this is going to be one of your main portfolio projects. A stronger repository name would be:

**`PropertyManagementAPI`**

and your GitHub description could be:

> **Production-oriented ASP.NET Core 8 Property Management API demonstrating Clean Architecture, CQRS/MediatR, EF Core, SQL Server, Redis, FluentValidation, Mapster and Docker.**

That immediately tells a recruiter what you've actually worked with without making the README look like a list of buzzwords.
```
