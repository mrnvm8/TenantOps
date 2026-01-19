# TenantOps

TenantOps is a multi-tenant, domain-driven ASP.NET Core system designed to demonstrate Clean Architecture, DDD principles, and enterprise-grade backend practices.

The project focuses on correct boundaries, explicit domain modeling, and testable, scalable architecture, rather than shortcuts or framework-heavy abstractions.

# Key Features

1. Multi-tenancy with tenant isolation
2. Clean Architecture (Domain, Application, Infrastructure, API)
3. Domain-Driven Design (DDD)
4. JWT Authentication & Role-Based Authorization
5. Explicit Repositories (No Generics)
6. Unit of Work for transaction boundaries
7. Global Exception Handling
8. EF Core with Migrations
9. Production-ready API structure

# Architecture Overview
# The solution strictly follows dependency inversion:

API
 ↓
Application
 ↓
Domain
 ↑
Infrastructure

# Layer	Responsibility

Domain	Business rules, entities, value objects
Application	Use cases, orchestration, workflows
Infrastructure	EF Core, repositories, security, JWT
API	Controllers, HTTP, middleware

# Solution Folder Structure
# Domain

Domain
├── Common
│   ├── Entity.cs
│   ├── AggregateRoot.cs
│   ├── ValueObject.cs
│   └── DomainException.cs
│
├── Authorization
│   └── Roles.cs
│
├── Contracts
│   └── Repositories
│       ├── ITenantRepository.cs
│       ├── IUserRepository.cs
│       ├── IEmployeeRepository.cs
│       ├── IAttendanceRecordRepository.cs
│       ├── IAssetRepository.cs
│       └── IAssetAssignmentRepository.cs
│
├── Tenants
│   ├── Tenant.cs
│   ├── Department.cs
│   └── TenantStatus.cs
│
├── Users
│   ├── User.cs
│   └── Email.cs
│
├── Employees
│   ├── Employee.cs
│   └── EmployeeStatus.cs
│
├── Attendance
│   ├── AttendanceRecord.cs
│   └── AttendanceStatus.cs
│
└── Assets
    ├── Asset.cs
    ├── AssetAssignment.cs
    └── AssetStatus.cs

# Application

Application
├── Common
│   └── Interfaces
│       ├── ITenantProvider.cs
│       ├── IUnitOfWork.cs
│       └── IJwtTokenGenerator.cs
│
├── Auth
│   └── Commands
│       └── Login
│
├── Users
│   └── Commands
│       └── CreateUser
│
├── Employees
│   └── Commands
│       └── CreateEmployee
│
├── Departments
│   └── Commands
│       └── CreateDepartment
│
├── Attendance
│   └── Commands
│       └── ClockIn
│
└── Assets
    └── Commands
        ├── CreateAsset
        ├── AssignAsset
        └── ReturnAsset

# Infrastructure

Infrastructure
├── Persistence
│   ├── TenantOpsDbContext.cs
│   ├── Configurations
│   └── Migrations
│
├── Repositories
│   ├── TenantRepository.cs
│   ├── UserRepository.cs
│   ├── EmployeeRepository.cs
│   ├── AttendanceRecordRepository.cs
│   ├── AssetRepository.cs
│   └── AssetAssignmentRepository.cs
│
├── UnitOfWork
│   └── UnitOfWork.cs
│
├── Security
│   ├── TenantProvider.cs
│   ├── Authorization
│   │   ├── Policies.cs
│   │   └── RoleConstants.cs
│   └── Jwt
│       ├── JwtOptions.cs
│       └── JwtTokenService.cs
│
└── DependencyInjection.cs

# API

Api
├── Controllers
│   ├── AuthController.cs
│   ├── UsersController.cs
│   ├── EmployeesController.cs
│   ├── DepartmentsController.cs
│   ├── AttendanceController.cs
│   └── AssetsController.cs
│
├── Middleware
│   └── ExceptionHandlingMiddleware.cs
│
├── Models
│   └── ErrorResponse.cs
│
├── Program.cs
└── appsettings.json

# Authentication & Authorization

JWT-based authentication
Role-based authorization
Roles deare fined centrally in the Domain.Authorization.Roles
Authorization policies enforced at the controller level


# Database & Migrations

EF Core
SQL Server

Migrations stored in Infrastructure
** Connection String
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TenantOpsDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

# Run Migrations
dotnet ef migrations add InitialCreate \
  --project Infrastructure \
  --startup-project Api \
  --context TenantOpsDbContext

dotnet ef database update \
  --project Infrastructure \
  --startup-project Api

# Running the Application
dotnet run --project Api

The API will be available at:
https://localhost:7150


# Design Principles Followed

No generic repositories
One repository per Aggregate Root
No EF Core in Domain or Application
No business logic in controllers
Explicit transaction boundaries
Infrastructure depends inward only

# Why This Project Exists

This project is intentionally designed to:
Demonstrate real-world backend architecture
Avoid common Clean Architecture anti-patterns
Serve as a reference implementation


# Future Improvements

Password hashing & verification
User–Role persistence
Refresh tokens
Read models/queries
Swagger / OpenAPI
Auditing & logging
Tenant-level query filters

    # Testing (Planned / Partial)
Domain unit tests (pure business rules)
Application tests (use cases with mocks)
Integration tests (API + EF Core)

# Author

Lindokuhle Magagula
Focused on Clean Architecture, DDD, and scalable backend systems
