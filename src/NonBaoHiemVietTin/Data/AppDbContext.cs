using Microsoft.EntityFrameworkCore;

namespace NonBaoHiemVietTin.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // The legacy application uses an EDMX database-first model.
    // Entities and DbSet properties will be added here module-by-module
    // while preserving the existing SQL Server schema.
}
