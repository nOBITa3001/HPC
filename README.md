# HPC

## Technologies

- [ASP.NET Core 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
- [Angular 12](https://angular.io/)
- [ngx-admin](https://github.com/akveo/ngx-admin)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
- `TODO:` ~~[Docker](https://www.docker.com/)~~

## Getting Started

### Command

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/) or [nvm](https://github.com/nvm-sh/nvm) / [nvm-windows](https://github.com/coreybutler/nvm-windows)
3. Navigate to `src/WebUI/ClientApp` and run `npm install`
4. Navigate to `src/WebUI/ClientApp` and run `npm start` to launch the front end (Angular)
5. Navigate to `src/WebUI` and run `dotnet run` to launch the back end (ASP.NET Core Web API)
6. Go to https://localhost:5001/
7. Demo user: `admin@hpc.io` / `P@ssw0rd`

### Visual Studio

`TODO:`

### Testing

Go to `root` folder: `~\workspace\HPC\`

```
$ dotnet test HPC.sln
```

### Docker Configuration

`TODO:`

## Database Configuration

The template is configured to use an in-memory database by default for development purpose. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

## Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

- `--project src/Infrastructure` (optional if in this folder)
- `--startup-project src/WebUI`
- `--output-dir Persistence/Migrations`

For example, to add a new migration from the `src` folder: `~\workspace\HPC\src\`

**_Add a new migration_**

```
$ dotnet ef migrations add AddShipEntity -p src\Infrastructure -s src\WebUI -o Persistence\Migrations
```

**_Update database_**

```
$ dotnet ef database update -p src\Infrastructure -s src\WebUI
$ dotnet ef database update InitialCreateWithIdentity -p src\Infrastructure -s src\WebUI
```

**_Remove the latest migration_**

```
$  dotnet ef migrations remove -p src\Infrastructure -s src\WebUI
```

## Angular CLI

If you want to add a new angular component, server, or model, follow below commands

**_Add a new component and child components_**

```
$ ng g c ships -s --skip-tests --module app
$ ng g c ships/show-ship -s --skip-tests --module app
$ ng g c ships/add-edit-ship -s --skip-tests --module app
```

**_Add a new service_**

```
$ ng g s shared/ship --skip-tests
```

**_Add a new model_**

```
$ ng g class shared/ship --type=model --skip-tests
```

## Troubleshooting

- warning: `The EF Core tools version '3.x.x' is older than that of the runtime '5.0.8'. Update the tools for the latest features and bug fixes.`

  ```
  $ dotnet tool update --global dotnet-ef
  ```

- Unable to resolve service for type `'Microsoft.EntityFrameworkCore.Migrations.IMigrator'`. This is often because no database provider has been configured for this DbContext. A provider can be configured by overriding the 'DbContext.OnConfiguring' method or by using 'AddDbContext' on the application service provider. If 'AddDbContext' is used, then also ensure that your DbContext type accepts a DbContextOptions<TContext> object in its constructor and passes it to the base constructor for DbContext.

  ```
  $ use Database instead of InMemory by configuring '"UseInMemoryDatabase": false' in appsetting.json
  ```

- Get 500 internal server error after restart application and you are using `UseInMemoryDatabase`, try to clear browser's cookie. This may relate to logged in User Id from seeded data has gone

## Overview

### Domain

This will contain all entities, events, and logic specific to the domain layer.

### Application

The biggest layer containing all application logic that is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application needs to access a notification service, a new interface would be added to the application and implementation would be created within Infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as database, file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer. Moreover, this layer will publish all registered events on each request.

### WebUI

This layer is a single-page application based on Angular 12 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only _Startup.cs_ should reference Infrastructure.
