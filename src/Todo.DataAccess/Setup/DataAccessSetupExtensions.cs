using Todo.DataAccess.Context;
using Todo.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.DataAccess.Setup;

/// <summary>
/// Contains extension methods for setting up the data access layer e.g. registering the serivces in the IoC.
/// </summary>
public static class DataAccessSetupExtensions
{
    /// <summary>
    /// Registers and sets up the application specific db-context.
    /// </summary>
    /// <param name="services">Service collection / the current IoC.</param>
    /// <param name="configuration">Staged application configuration.</param>
    /// <exception cref="InvalidOperationException">Exception will be thrown if no connection string could be found.</exception>
    public static void AddDataBaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Default"); // Get connection string from config for own Db

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("No valid connection string could be found in the app configuration, therefore cannot add the db context to the service collection");
        }

        _ = services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));
    }

    /// <summary>
    /// Registers the application specific unit of work implementation which hosts all repository implementations.
    /// </summary>
    /// <param name="services">Service collection / the current IoC.</param>
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        _ = services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}