using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    private readonly string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STARSHIP", EnvironmentVariableTarget.Machine)!;
    public MyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
        return new MyContext(optionsBuilder.Options);
    }
}