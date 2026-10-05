# .NET 10 migration

This directory is the new ASP.NET Core application replacing the legacy ASP.NET MVC 5 / .NET Framework 4.5.2 application.

## Configuration

Do not commit credentials. Configure SQL Server with:

```
ConnectionStrings__DefaultConnection=Server=...;Database=nonbaohiemviettin;User Id=...;Password=...;TrustServerCertificate=True
```

## Migration approach

1. Keep the existing SQL Server schema.
2. Replace EDMX/EF6 with EF Core database-first mappings.
3. Port public controllers and Razor views while preserving existing Vietnamese routes.
4. Port authentication/session behavior to ASP.NET Core.
5. Port the admin area and Web API.
6. Remove the legacy project only after feature parity is verified.
