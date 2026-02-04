using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelMarketplace.Api.Data;

/// <summary>
/// Design-time factory for creating TravelMarketplaceDbContext during migrations.
/// </summary>
public class TravelMarketplaceDbContextFactory : IDesignTimeDbContextFactory<TravelMarketplaceDbContext>
{
    public TravelMarketplaceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TravelMarketplaceDbContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=TravelMarketplace;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

        return new TravelMarketplaceDbContext(optionsBuilder.Options);
    }
}
