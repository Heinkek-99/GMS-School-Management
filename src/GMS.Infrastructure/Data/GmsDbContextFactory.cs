using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GMS.Infrastructure.Data;

/// <summary>
/// Factory pour créer le DbContext au moment des migrations
/// </summary>
public class GmsDbContextFactory : IDesignTimeDbContextFactory<GmsDbContext>
{
    public GmsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GmsDbContext>();
        
        // Connection string pour les migrations
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=GMSDb;User Id=sa;Password=Azerty12;TrustServerCertificate=True;MultipleActiveResultSets=true;Encrypt=False",
            b => b.MigrationsAssembly("GMS.Infrastructure")
        );

        return new GmsDbContext(optionsBuilder.Options);
    }
}