# ContactBook API

A RESTful Web API for managing contacts, built with Clean Architecture, CQRS, and the Mediator pattern.

## Tech Stack

- **.NET 10** / ASP.NET Core
- **Entity Framework Core** with SQL Server
- **MediatR** - Mediator pattern for CQRS
- **FluentValidation** - Request validation via MediatR pipeline
- **Scalar** - Interactive API documentation

## Architecture

The project follows **Clean Architecture** with four layers:

```
src/
├── ContactBook.API              # Controllers, middleware, Program.cs
├── ContactBook.Application      # Commands, queries, handlers, validators, DTOs
├── ContactBook.Domain           # Domain models and shared types
└── ContactBook.Infrastructure   # EF Core, DbContext, repository implementations
```

Dependencies point inward: `API → Application → Domain ← Infrastructure`

### Key Patterns

- **CQRS** - Commands (create, update, delete) and Queries (read) are separated
- **Mediator** - Controllers delegate all logic to MediatR handlers
- **Repository Pattern** - Data access abstracted behind interfaces
- **Result Pattern** - `OperationResult<T>` for consistent error handling
- **Validation Pipeline** - FluentValidation runs automatically before each handler

## API Endpoints

| Method | Endpoint                  | Description              |
|--------|---------------------------|--------------------------|
| POST   | `/api/contacts`           | Create a new contact     |
| GET    | `/api/contacts`           | Get all contacts         |
| GET    | `/api/contacts/{id}`      | Get a contact by ID      |
| PUT    | `/api/contacts/{id}`      | Update a contact         |
| DELETE | `/api/contacts/{id}`      | Delete a contact         |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or SQL Server Express)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/Burra17/ContactBook.git
   cd ContactBook
   ```

2. Update the connection string in `src/ContactBook.API/appsettings.Development.json` if needed

3. Apply database migrations
   ```bash
   cd src/ContactBook.API
   dotnet ef database update --project ../ContactBook.Infrastructure
   ```

4. Run the application
   ```bash
   dotnet run --project src/ContactBook.API
   ```

5. Open the Scalar API docs at `https://localhost:7027/scalar/v1`

## Project Structure

```
Application/
└── Contacts/
    ├── Commands/
    │   ├── CreateContact/    # Command, Handler, Validator
    │   ├── UpdateContact/    # Command, Handler, Validator
    │   └── DeleteContact/    # Command, Handler
    ├── Queries/
    │   ├── GetContacts/      # Query, Handler
    │   └── GetContactById/   # Query, Handler
    └── Dtos/
        └── ContactDto.cs
```

## Branch Strategy

- `main` - Stable, production-ready code
- `feature/*` - One branch per feature (e.g., `feature/api-endpoints`, `feature/get-contacts`)

Each feature is developed on its own branch and merged into `main` via pull request.
