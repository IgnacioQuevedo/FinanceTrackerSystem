using Microsoft.EntityFrameworkCore;

namespace DataManagers;

public interface IAppContextFactory
{
    SqlContext CreateDbContext();
}

public class InMemoryAppContextFactory : IAppContextFactory
{
    public SqlContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
        optionsBuilder.UseInMemoryDatabase("TestDB");

        return new SqlContext(optionsBuilder.Options);
    }
}